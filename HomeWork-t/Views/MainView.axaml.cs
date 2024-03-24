using Avalonia.Controls;
using System;
using ScheduleLib.Schedule;
using ScheduleLib;
using Newtonsoft.Json;
using System.IO;
namespace HomeWork.Views;

public partial class MainView : UserControl
{
    private SchoolObject uData;
    public MainView()
    {
        InitializeComponent();
        Loaded += MainView_Loaded;
        uData = JsonConvert.DeserializeObject<SchoolObject>(File.ReadAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "HWCalendar", "schedules.json"))) ?? new();

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
