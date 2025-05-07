using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ImportData.Helpers;
public static class ControlProperties
{
    // Register the attached property "BindableSelectedItems"
    public static readonly DependencyProperty BindableSelectedItemsProperty =
        DependencyProperty.RegisterAttached(
            "BindableSelectedItems",        // Name of the attached property
            typeof(IList),                  // Type of the property (IList for collection)
            typeof(ControlProperties),      // Owner type (attached property owner)
            new PropertyMetadata(null, OnBindableSelectedItemsChanged));

    // Get method for the attached property
    public static IList GetBindableSelectedItems(DependencyObject element)
    {
        return (IList)element.GetValue(BindableSelectedItemsProperty);
    }

    // Set method for the attached property
    public static void SetBindableSelectedItems(DependencyObject element, IList value)
    {
        element.SetValue(BindableSelectedItemsProperty, value);
    }

    // Callback for when the BindableSelectedItems property changes
    private static void OnBindableSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if(d is Selector selector)
        {
            selector.SelectionChanged -= Object_SelectionChanged;
            selector.SelectionChanged += Object_SelectionChanged;
            OnSelectedItemsChanged(selector, e);
        }
    }

    static void OnSelectedItemsChanged(dynamic obj, DependencyPropertyChangedEventArgs e)
    {
        // Initialize selected item in ComboBox from bound value
        if (e.NewValue != null)
        {
            var selectedItems = e.NewValue as IList;
            if (selectedItems != null && obj.SelectedItem == null && selectedItems.Count > 0)
            {
                obj.SelectedItem = selectedItems[0];
            }
        }
    }
    static void Object_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        dynamic obj = sender;
        var selectedItems = GetBindableSelectedItems(obj);
        if (selectedItems != null)
        {
            selectedItems.Clear();
            foreach (var selectedItem in obj.SelectedItems)
            {
                selectedItems.Add(selectedItem);
            }
        }
    }
}