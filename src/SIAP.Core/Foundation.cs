using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace SIAP.Core;

public sealed record ProjectFile(string RelativePath, string FullPath, long SizeBytes, string Extension);

public sealed record ProjectSnapshot(string RootPath, IReadOnlyList<ProjectFile> Files, DateTime CreatedAtUtc);

public sealed class ProjectScanner
{
    public static readonly string[] DefaultExcludedDirectories =
    [
        ".git",
        "bin",
        "obj",
        "node_modules",
        "dist"
    ];

    public ProjectSnapshot Scan(string rootPath)
    {
        if (string.IsNullOrWhiteSpace(rootPath))
        {
            throw new ArgumentException("Root path is required.", nameof(rootPath));
        }

        var rootFullPath = Path.GetFullPath(rootPath);
        var files = Directory.EnumerateFiles(rootFullPath, "*", SearchOption.AllDirectories)
            .Where(path => !IsInExcludedDirectory(path, rootFullPath))
            .Select(path => CreateProjectFile(rootFullPath, path))
            .OrderBy(file => file.RelativePath, StringComparer.OrdinalIgnoreCase)
            .ToList();

        return new ProjectSnapshot(rootFullPath, files, DateTime.UtcNow);
    }

    private static bool IsInExcludedDirectory(string fullPath, string rootFullPath)
    {
        var relativePath = Path.GetRelativePath(rootFullPath, fullPath);
        var segments = relativePath.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        return segments.Any(segment =>
            DefaultExcludedDirectories.Contains(segment, StringComparer.OrdinalIgnoreCase));
    }

    private static ProjectFile CreateProjectFile(string rootFullPath, string fullPath)
    {
        var relativePath = Path.GetRelativePath(rootFullPath, fullPath);
        var fileInfo = new FileInfo(fullPath);
        var extension = Path.GetExtension(fullPath);
        return new ProjectFile(relativePath, fullPath, fileInfo.Length, extension);
    }
}

public sealed record ValidationResult(bool IsValid, IReadOnlyList<string> Errors);

public sealed class StructuralValidator
{
    public ValidationResult Validate(ProjectSnapshot snapshot, string exportDirectory)
    {
        var errors = new List<string>();

        if (snapshot is null)
        {
            errors.Add("Snapshot is required.");
            return new ValidationResult(false, errors);
        }

        if (!Directory.Exists(snapshot.RootPath))
        {
            errors.Add("Root path does not exist or is not a directory.");
        }

        if (snapshot.Files.Count == 0)
        {
            errors.Add("No files were discovered in the project.");
        }

        if (!EnsureExportDirectory(exportDirectory, errors))
        {
            return new ValidationResult(false, errors);
        }

        return new ValidationResult(errors.Count == 0, errors);
    }

    private static bool EnsureExportDirectory(string exportDirectory, List<string> errors)
    {
        if (string.IsNullOrWhiteSpace(exportDirectory))
        {
            errors.Add("Export directory is required.");
            return false;
        }

        try
        {
            Directory.CreateDirectory(exportDirectory);
        }
        catch (Exception ex)
        {
            errors.Add($"Export directory could not be created: {ex.Message}");
            return false;
        }

        try
        {
            var testFile = Path.Combine(exportDirectory, $".write-test-{Guid.NewGuid():N}.tmp");
            File.WriteAllText(testFile, "test");
            File.Delete(testFile);
        }
        catch (Exception ex)
        {
            errors.Add($"Export directory is not writable: {ex.Message}");
            return false;
        }

        return true;
    }
}

public sealed record ExportOptions(bool IncludeJsonReport = true);

public sealed record ExportResult(string ExportTextPath, string? ReportJsonPath);

public sealed class ExportService
{
    public ExportResult Export(ProjectSnapshot snapshot, string exportDirectory, ExportOptions? options = null)
    {
        if (snapshot is null)
        {
            throw new ArgumentNullException(nameof(snapshot));
        }

        var resolvedOptions = options ?? new ExportOptions();
        Directory.CreateDirectory(exportDirectory);

        var exportPath = Path.Combine(exportDirectory, "export.txt");
        var reportPath = Path.Combine(exportDirectory, "report.json");

        WriteExportText(snapshot, exportPath);

        if (resolvedOptions.IncludeJsonReport)
        {
            WriteJsonReport(snapshot, reportPath);
            return new ExportResult(exportPath, reportPath);
        }

        return new ExportResult(exportPath, null);
    }

    private static void WriteExportText(ProjectSnapshot snapshot, string exportPath)
    {
        var totalSize = snapshot.Files.Sum(file => file.SizeBytes);
        var builder = new StringBuilder();
        builder.AppendLine("SIAP Export");
        builder.AppendLine($"Root: {snapshot.RootPath}");
        builder.AppendLine($"Generated (UTC): {snapshot.CreatedAtUtc:O}");
        builder.AppendLine($"File Count: {snapshot.Files.Count}");
        builder.AppendLine($"Total Size (bytes): {totalSize}");
        builder.AppendLine("Files:");

        foreach (var file in snapshot.Files.OrderBy(file => file.RelativePath, StringComparer.OrdinalIgnoreCase))
        {
            builder.AppendLine(file.RelativePath);
        }

        File.WriteAllText(exportPath, builder.ToString());
    }

    private static void WriteJsonReport(ProjectSnapshot snapshot, string reportPath)
    {
        var totalsByExtension = snapshot.Files
            .GroupBy(file => file.Extension ?? string.Empty, StringComparer.OrdinalIgnoreCase)
            .OrderBy(group => group.Key, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(group => group.Key, group => group.Count(), StringComparer.OrdinalIgnoreCase);

        var report = new
        {
            root = snapshot.RootPath,
            generatedUtc = snapshot.CreatedAtUtc,
            fileCount = snapshot.Files.Count,
            totalSizeBytes = snapshot.Files.Sum(file => file.SizeBytes),
            countsByExtension = totalsByExtension
        };

        var json = JsonSerializer.Serialize(report, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(reportPath, json);
    }
}

public enum TaskState
{
    Created,
    Queued,
    Running,
    Validating,
    Completed,
    Failed,
    RolledBack
}

public sealed record TaskInfo(Guid Id, string Name, TaskState State, DateTime CreatedAtUtc);

public sealed record TaskResult(bool Success, string Message);
