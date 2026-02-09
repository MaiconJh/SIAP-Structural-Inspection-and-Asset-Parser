using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SIAP.App;

public sealed class TreeNode : INotifyPropertyChanged
{
    private bool? _isChecked;

    public TreeNode(string name, string relativePath, bool isFolder, TreeNode? parent)
    {
        Name = name;
        RelativePath = relativePath;
        IsFolder = isFolder;
        Parent = parent;
        Children = new ObservableCollection<TreeNode>();
    }

    public string Name { get; }

    public string RelativePath { get; }

    public bool IsFolder { get; }

    public TreeNode? Parent { get; }

    public ObservableCollection<TreeNode> Children { get; set; }

    public bool? IsChecked
    {
        get => _isChecked;
        set => SetIsChecked(value, updateChildren: true, updateParent: true);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public void SetIsChecked(bool? value, bool updateChildren, bool updateParent)
    {
        if (_isChecked == value)
        {
            return;
        }

        _isChecked = value;
        OnPropertyChanged();

        if (updateChildren && value.HasValue)
        {
            foreach (var child in Children)
            {
                child.SetIsChecked(value, updateChildren: true, updateParent: false);
            }
        }

        if (updateParent)
        {
            Parent?.UpdateParentStateFromChildren();
        }
    }

    public void UpdateParentStateFromChildren()
    {
        if (Children.Count == 0)
        {
            return;
        }

        var allChecked = true;
        var allUnchecked = true;

        foreach (var child in Children)
        {
            switch (child.IsChecked)
            {
                case true:
                    allUnchecked = false;
                    break;
                case false:
                    allChecked = false;
                    break;
                default:
                    allChecked = false;
                    allUnchecked = false;
                    break;
            }
        }

        bool? newValue = allChecked ? true : allUnchecked ? false : null;
        SetIsChecked(newValue, updateChildren: false, updateParent: true);
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
