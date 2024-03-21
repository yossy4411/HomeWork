using Avalonia.Controls;
using System;
namespace HomeWork.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        Loaded += MainView_Loaded;
    }

    private void MainView_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        DateOnly dateOnly = new(2024, 3, 1);
        int offset = (int)dateOnly.DayOfWeek;
        
        for (int i = 0; i <= dateOnly.AddMonths(1).DayNumber - dateOnly.DayNumber- 1; i++)
        {
            Panel panel = new();
            Grid.SetRow(panel, (i + offset) / 7);
            Grid.SetColumn(panel, (i + offset) % 7);
            TextBlock textBox = new() { Text = dateOnly.AddDays(i).Day.ToString() };
            panel.Children.Add(textBox);
            calendar.Children.Add(panel);
        }
    }
}
