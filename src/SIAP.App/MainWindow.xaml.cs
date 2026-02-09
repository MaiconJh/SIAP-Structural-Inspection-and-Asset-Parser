using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using SIAP.Core;

namespace SIAP.App;

public partial class MainWindow : Window
{
    private ProjectSnapshot? _snapshot;
    private readonly ProjectScanner _scanner = new();
    private readonly StructuralValidator _validator = new();
    private readonly ExportService _exporter = new();

    public MainWindow()
    {
        InitializeComponent();
    }

    private void OnOpenProjectClicked(object sender, RoutedEventArgs e)
    {
        using var dialog = new FolderBrowserDialog
        {
            Description = "Select a project folder to scan"
        };

        if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
        {
            return;
        }

        try
        {
            Log($"Scanning: {dialog.SelectedPath}");
            _snapshot = _scanner.Scan(dialog.SelectedPath);
            FilesListBox.ItemsSource = _snapshot.Files.Select(file => file.RelativePath).ToList();
            FileCountText.Text = _snapshot.Files.Count.ToString("N0");
            TotalSizeText.Text = _snapshot.Files.Sum(file => file.SizeBytes).ToString("N0");
            GenerateExportButton.IsEnabled = true;
            Log($"Scan complete. {_snapshot.Files.Count:N0} files found.");
        }
        catch (Exception ex)
        {
            Log($"Scan failed: {ex.Message}");
            GenerateExportButton.IsEnabled = false;
        }
    }

    private void OnGenerateExportClicked(object sender, RoutedEventArgs e)
    {
        if (_snapshot is null)
        {
            Log("Scan a project before generating an export.");
            return;
        }

        using var dialog = new FolderBrowserDialog
        {
            Description = "Select an export output folder"
        };

        if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
        {
            return;
        }

        var validation = _validator.Validate(_snapshot, dialog.SelectedPath);
        if (!validation.IsValid)
        {
            Log("Validation failed:");
            foreach (var error in validation.Errors)
            {
                Log($"- {error}");
            }
            return;
        }

        try
        {
            var result = _exporter.Export(_snapshot, dialog.SelectedPath, new ExportOptions());
            Log($"Export complete: {result.ExportTextPath}");
            if (result.ReportJsonPath is not null)
            {
                Log($"Report: {result.ReportJsonPath}");
            }
        }
        catch (Exception ex)
        {
            Log($"Export failed: {ex.Message}");
        }
    }

    private void Log(string message)
    {
        var builder = new StringBuilder(LogTextBox.Text);
        if (builder.Length > 0)
        {
            builder.AppendLine();
        }

        builder.Append($"[{DateTime.Now:HH:mm:ss}] {message}");
        LogTextBox.Text = builder.ToString();
        LogTextBox.ScrollToEnd();
    }
}
