using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
    private readonly Dictionary<TreeNode, PropertyChangedEventHandler> _nodeHandlers = new();

    public ObservableCollection<TreeNode> RootNodes { get; } = new();

    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;
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
            BuildTreeFromSnapshot(_snapshot);
            ProjectNameText.Text = Path.GetFileName(_snapshot.RootPath);
            ProjectPathText.Text = _snapshot.RootPath;
            FileCountText.Text = _snapshot.Files.Count.ToString("N0");
            TotalSizeText.Text = _snapshot.Files.Sum(file => file.SizeBytes).ToString("N0");
            SelectionSummaryText.Text = "Selected: 0 items";
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

    private void OnSelectAllClicked(object sender, RoutedEventArgs e)
    {
        Log("Select All action pending implementation.");
    }

    private void OnClearSelectionClicked(object sender, RoutedEventArgs e)
    {
        Log("Clear selection action pending implementation.");
    }

    private void OnRescanClicked(object sender, RoutedEventArgs e)
    {
        Log("Rescan action pending implementation.");
    }

    private void OnClearFilterClicked(object sender, RoutedEventArgs e)
    {
        FilterTextBox.Text = string.Empty;
        Log("Filter cleared (filtering pending implementation).");
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

    private void BuildTreeFromSnapshot(ProjectSnapshot snapshot)
    {
        RootNodes.Clear();
        DetachNodeHandlers();

        var rootNode = new TreeNode(Path.GetFileName(snapshot.RootPath), string.Empty, true, null);
        var folders = new Dictionary<string, TreeNode>(StringComparer.OrdinalIgnoreCase)
        {
            [string.Empty] = rootNode
        };

        foreach (var file in snapshot.Files)
        {
            var parts = file.RelativePath.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            var currentPath = string.Empty;
            var currentNode = rootNode;

            for (var index = 0; index < parts.Length; index++)
            {
                var part = parts[index];
                var isLast = index == parts.Length - 1;

                if (!isLast)
                {
                    currentPath = Path.Combine(currentPath, part);
                    if (!folders.TryGetValue(currentPath, out var folderNode))
                    {
                        folderNode = new TreeNode(part, currentPath, true, currentNode);
                        folders[currentPath] = folderNode;
                        currentNode.Children.Add(folderNode);
                    }

                    currentNode = folderNode;
                }
                else
                {
                    var fileNode = new TreeNode(part, file.RelativePath, false, currentNode);
                    currentNode.Children.Add(fileNode);
                }
            }
        }

        SortTree(rootNode);
        AttachNodeHandlers(rootNode);
        RootNodes.Add(rootNode);
    }

    private void SortTree(TreeNode node)
    {
        var sorted = node.Children
            .OrderBy(child => child.IsFolder ? 0 : 1)
            .ThenBy(child => child.Name, StringComparer.OrdinalIgnoreCase)
            .ToList();
        node.Children = new ObservableCollection<TreeNode>(sorted);

        foreach (var child in node.Children)
        {
            SortTree(child);
        }
    }

    private void AttachNodeHandlers(TreeNode node)
    {
        PropertyChangedEventHandler handler = (_, args) =>
        {
            if (args.PropertyName == nameof(TreeNode.IsChecked))
            {
                Log($"Selection changed: {node.RelativePath} => {node.IsChecked}");
            }
        };

        node.PropertyChanged += handler;
        _nodeHandlers[node] = handler;

        foreach (var child in node.Children)
        {
            AttachNodeHandlers(child);
        }
    }

    private void DetachNodeHandlers()
    {
        foreach (var entry in _nodeHandlers)
        {
            entry.Key.PropertyChanged -= entry.Value;
        }

        _nodeHandlers.Clear();
    }
}
