using Database_Models.Interfaces;
using Notenerfassungstool.Factorys;
using RezepteSammelung.UIHelpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using UtitlityFunctions.Atributte;
using UtitlityFunctions.ClassExtention;
using UtitlityFunctions.InterfaceExtention;

namespace RezepteSammelung.Windows;

/// <summary>
/// Interaktionslogik für NewItem.xaml
/// </summary>
public partial class NewItem : Window
{
    private static IDataBaseModel? _toReturn;
    private readonly Type _currentType;
    private NewItem(Type type, object dataContext, object? existingObject = null)
    {
        _currentType = type;
        InitializeComponent();
        DataContext = dataContext;
        CreateView(type, existingObject);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dataContext">List will be Binded by the name of the Peroperty plus an 's'</param>
    /// <returns></returns>
    internal static T? HandelNewItem<T>(object dataContext)
        where T : IDataBaseModel
    {
        _toReturn = null;
        var newItem = new NewItem(typeof(T), dataContext)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen
        };

        if ((bool)newItem.ShowDialog()!)
        {
            return default;
        }

        return (T?)_toReturn;
    }

    internal static T HandelNewItem<T>(T item, object dataContext)
        where T : IDataBaseModel
    {
        _toReturn = null;
        var newItem = new NewItem(typeof(T), dataContext)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen
        };

        if ((bool)newItem.ShowDialog()!)
        {
            return (T)_toReturn!;
        }

        return item;
    }

    private void Save(object sender, RoutedEventArgs e)
    {
        var listOfParameters = new List<object>();

        foreach (var property in GetAllPropertyInfosForField(_currentType))
        {
            if (IsFieldFormListInDataContext(property) && property.PropertyType.IsAssignableTo(typeof(IName)))
            {
                var comboBox = UIHelper.FindChild<ComboBox>(this, property.Name);
                if (comboBox == null)
                {
                    throw new NullReferenceException($"Could not find comboBox with name: '{property.Name}'.");
                }

                listOfParameters.Add(comboBox.SelectionBoxItem);
            }
            else if (property.PropertyType == typeof(string) || property.PropertyType.IsNumber())
            {
                var textBox = UIHelper.FindChild<TextBox>(this, property.Name);
                if (textBox == null)
                {
                    throw new NullReferenceException($"Could not find textbox with name: '{property.Name}'.");
                }

                if (property.PropertyType.IsNumber())
                {
                    if (double.TryParse(textBox.Text.Trim(), out var result))
                    {
                        listOfParameters.Add(result);
                        continue;
                    }

                    MessageBox.Show($"Das Feld: '{property.Name}' sollte nur eine Zahl beinhalten.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                listOfParameters.Add(textBox.Text.Trim());
            }
        }

        var constructors = _currentType.GetConstructor(listOfParameters.Select(x => x.GetType()).ToArray());

        if (constructors == null)
        {
            throw new NullReferenceException(
                $"could not find a suitable constructor for type: '{_currentType.Name}' with parameters: {string.Join(",\n", listOfParameters)}");
        }

        var newItem = constructors.Invoke(listOfParameters.ToArray());

        if (newItem == null)
        {
            throw new NullReferenceException($"Could not create Type: '{_currentType.Name}' with params to ctor {string.Join(',', listOfParameters)}.");
        }

        var newDatabaseModel = (IDataBaseModel)newItem;

        var errorList = newDatabaseModel.Validate();
        if (!errorList.IsEmpty())
        {
            MessageBox.Show(string.Join('\n', errorList), "Input Errors", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        _toReturn = newDatabaseModel;

        foreach (var property in GetAllPropertyInfosForField(_currentType))
        {
            if (property.PropertyType != typeof(string) && !property.PropertyType.IsNumber())
            {
                continue;
            }

            var textBox = UIHelper.FindChild<TextBox>(this, property.Name);
            if (textBox == null)
            {
                throw new NullReferenceException($"Could not find textbox with name: '{property.Name}'.");
            }

            textBox.Text = string.Empty;
        }

        DialogResult = false;
    }


    private void Cancel(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
    }

    private void RemovePlaceholder_GotFocus(object sender, RoutedEventArgs e)
    {
        TextBox target = (TextBox)sender;
        target.Text = "";
        target.Foreground = new SolidColorBrush(Colors.Black);
    }

    private void AddPlaceholder_LostFocus(object sender, RoutedEventArgs e)
    {
        TextBox target = (TextBox)sender;
        if (string.IsNullOrWhiteSpace(target.Text))
        {
            target.Text = target.Tag.ToString();
            target.Foreground = new SolidColorBrush(Colors.Gray);
        }
    }

    private void CreateView(Type type, object? existingObject)
    {
        var allProperties = GetAllPropertyInfosForField(type).ToList();
        AddRowLines(allProperties.Count + 1);
        Height = 60 * (allProperties.Count + 1);
        Title = type.Name;
        for (int i = 0; i < allProperties.Count; i++)
        {
            if (IsFieldFormListInDataContext(allProperties[i]) && allProperties[i].PropertyType.IsAssignableTo(typeof(IName)))
            {
                CreateComboBox(allProperties[i], i);
            }
            else if (allProperties[i].PropertyType == typeof(string) || allProperties[i].PropertyType.IsNumber())
            {
                CreateField(allProperties[i], i, existingObject);
            }
        }
        AddButtons(allProperties.Count + 1);
    }

    private void CreateField(PropertyInfo property, int rowLine, object? existingObject)
    {
        Label label = CreateLabel(property.Name);

        TextBox textBox = new TextBox
        {
            Name = property.Name,
            FontSize = 15,
            Margin = new Thickness(5, 0, 0, 0),
            Width = double.NaN,
            Height = double.NaN
        };
        if (existingObject != null)
        {
            textBox.Tag = property.GetValue(existingObject);
            textBox.Text = property.GetValue(existingObject) as string;
            textBox.GotFocus += RemovePlaceholder_GotFocus;
            textBox.LostFocus += AddPlaceholder_LostFocus;
        }

        DockPanel dockPanel = CreateDockPanel();
        dockPanel.Children.Add(label);
        dockPanel.Children.Add(textBox);
        Grid.SetRow(dockPanel, rowLine);

        MainGrid.Children.Add(dockPanel);
    }

    private void CreateComboBox(PropertyInfo property, int rowLine)
    {
        Label label = CreateLabel(property.Name);

        ComboBox comboBox = new ComboBox
        {
            Name = property.Name,
            FontSize = 15,
            Margin = new Thickness(5, 0, 0, 0),
            Width = double.NaN,
            Height = double.NaN
        };
        comboBox.SetBinding(ItemsControl.ItemsSourceProperty, $"{property.Name}s");
        comboBox.ItemTemplate = TemplateFactory.CreateDataTemplate(() => { return CreateDataTemplate(); });
        label.Target = comboBox;

        DockPanel dockPanel = CreateDockPanel();
        dockPanel.Children.Add(label);
        dockPanel.Children.Add(comboBox);
        Grid.SetRow(dockPanel, rowLine);

        MainGrid.Children.Add(dockPanel);
    }

    private Label CreateLabel(string propertyName)
    {
        Label label = new Label
        {
            FontSize = 15,
            Content = $"{propertyName}: "
        };
        return label;
    }

    private DockPanel CreateDockPanel()
    {
        DockPanel dockPanel = new DockPanel
        {
            Margin = new Thickness(10, 10, 10, 5),
            Width = double.NaN,
            Height = double.NaN,
            LastChildFill = true,
            VerticalAlignment = VerticalAlignment.Center
        };
        return dockPanel;
    }
    private object CreateDataTemplate()
    {
        Label label = new();
        label.SetBinding(Label.ContentProperty, "Name");

        return label;
    }

    private IEnumerable<PropertyInfo> GetAllPropertyInfosForField(Type type)
    {
        var listOfPropertys = new List<PropertyInfo>(type.GetProperties());
        var skips = listOfPropertys
            .Where(propertyInfo => typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType).Not() &&
                                   propertyInfo.PropertyType != typeof(string) &&
                                   propertyInfo.PropertyType.IsNumber().Not() &&
                                   propertyInfo.PropertyType.IsAssignableTo(typeof(IDataBaseModel)).Not());
        var skipFromAttributte = listOfPropertys.Where(propertyInfo => propertyInfo.GetCustomAttribute<IgnoreForCreationOfObjectAttribute>() != null &&
                                   propertyInfo.GetCustomAttribute<IgnoreForCreationOfObjectAttribute>()!.Skip);

        return listOfPropertys.Except(skips).Except(skipFromAttributte);

    }

    private void AddRowLines(int lines)
    {
        for (int i = 0; i < lines; i++)
        {
            MainGrid.RowDefinitions.Add(new RowDefinition());
        }
    }

    private void AddButtons(int indexOfLastRow)
    {
        Button cancelButton = new Button
        {
            Content = "Cancel",
            VerticalAlignment = VerticalAlignment.Center,
            FontSize = 15,
            Margin = new Thickness(10, 0, 5, 0)
        };
        cancelButton.Click += Cancel;
        Grid.SetColumn(cancelButton, 0);

        Button saveButton = new Button
        {
            Content = "Save",
            VerticalAlignment = VerticalAlignment.Center,
            FontSize = 15,
            Margin = new Thickness(5, 0, 10, 0)
        };
        saveButton.Click += Save;
        Grid.SetColumn(saveButton, 1);

        Grid buttonGrid = new Grid();
        buttonGrid.ColumnDefinitions.Add(new ColumnDefinition());
        buttonGrid.ColumnDefinitions.Add(new ColumnDefinition());
        buttonGrid.Children.Add(cancelButton);
        buttonGrid.Children.Add(saveButton);

        Grid.SetRow(buttonGrid, indexOfLastRow);
        MainGrid.Children.Add(buttonGrid);
    }

    private bool IsFieldFormListInDataContext(PropertyInfo property)
    {
        var type = DataContext.GetType();
        foreach (var propertyInfo in type.GetProperties())
        {
            if (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition().IsAssignableTo(typeof(IEnumerable)))
            {
                return propertyInfo.PropertyType.GetGenericArguments()[0].Name == property.PropertyType.Name;
            }
        }

        return false;
    }
}