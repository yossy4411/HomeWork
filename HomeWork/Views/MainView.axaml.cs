using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Newtonsoft.Json;
using ScheduleLib;
using System;
using System.IO;
using System.Diagnostics;
using Avalonia.Data;
using ScheduleLib.Schedule;
using HomeWork.Converter;
using Avalonia.Controls.Primitives;
using ReactiveUI;

namespace HomeWork.Views;

public partial class MainView : UserControl
{
    private readonly UserData uData;
    private DateOnly dispMonth = new(2023, 12, 1);
    private static readonly TimeOnly time = new();
    public MainView()
    {
        InitializeComponent();
        Loaded += MainView_Loaded;
        uData = JsonConvert.DeserializeObject<UserData>(File.ReadAllText(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "HWCalendar", "schedules.json"))) ?? new();
    }
    private void MainView_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var dateOnly = dispMonth.AddDays(-(int)dispMonth.DayOfWeek);
        var schedules = uData.GetSchedules(dateOnly.ToDateTime(time), dateOnly.AddDays(43).ToDateTime(time));
        var brush = new SolidColorBrush(Colors.Black);
        for (int i = 0; i < 42; i++)
        {
            Canvas panel = new()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
            };
            Rectangle rect = new() { Stroke = brush, StrokeThickness = .2, Fill = Background };
            
            rect.PointerPressed += (sender, e) => Debug.WriteLine("huh");
            Grid.SetRow(panel, i / 7);
            Grid.SetColumn(panel, i % 7);
            TextBlock textBlock = new() { Text = dateOnly.AddDays(i).Day.ToString() };
            panel.Children.Add(textBlock);
            Canvas.SetLeft(textBlock, 5);
            calendar.Children.Add(panel);
            int i2 = i;
            
            panel.Children.Add(rect);
            rect.Bind(WidthProperty, new Binding("Bounds.Width") { Source = panel });
            rect.Bind(HeightProperty, new Binding("Bounds.Height") { Source = panel });
            panel.Loaded += (sender, e) =>
            {

                for (int i3 = 0; i3 < schedules[i2]?.Count; i3++)
                {
                    int j = i3;
                    int index = schedules[i2][j];
                    var schedule = uData.Schedules[index];
                    var next = schedules[i2 + 1] == null ? 0 : schedules[i2 + 1].Contains(index) ? schedules[i2 + 1].IndexOf(index) : schedules[i2 + 1].Count - 1;

                    Polygon shape = new() { Fill = new SolidColorBrush(schedule.SystemColor.ToAvaloniaColor()) };
                    if (j == schedules[i2]?.Count - 1 && (schedules[i2 + 1] == null || !schedules[i2 + 1].Contains(index)))
                    {
                        if (!schedules[i2 + 1]?.Contains(index)??false) next++;
                        shape.Points = [new(0, j * 9), new(0, (j + 1) * 9), new(panel.Bounds.Width * 0.9, (next + 1) * 9), new(panel.Bounds.Width, next * 9 + 4.5), new(panel.Bounds.Width * 0.9, next * 9) ];
                        panel.SizeChanged += (sender, e) =>
                        {
                            shape.Points = [new(0, j * 9), new(0, (j + 1) * 9), new(panel.Bounds.Width * 0.9, (next + 1) * 9), new(panel.Bounds.Width, next * 9 + 4.5), new(panel.Bounds.Width * 0.9, next * 9)];
                        };
                    }
                    else
                    {
                        shape.Points = [new(0, j * 9), new(0, (j + 1) * 9), new(panel.Bounds.Width, (next + 1) * 9), new(panel.Bounds.Width, next * 9)];
                        panel.SizeChanged += (sender, e) =>
                        {
                            shape.Points = [new(0, j * 9), new(0, (j + 1) * 9), new(panel.Bounds.Width, (next + 1) * 9), new(panel.Bounds.Width, next * 9)];
                        };
                    }
                    
                    
                    Canvas.SetTop(shape, 20);
                    
                    panel.Children.Add(shape);
                    if (!schedules[Math.Max(0, i2 - 1)]?.Contains(index) ?? true)
                    {
                        var text = new TextBlock()
                        {
                            Foreground = new SolidColorBrush(schedule.GetTextColor().ToAvaloniaColor()),
                            Text = schedule.Title,
                            FontSize = 6,
                            FontWeight = FontWeight.Bold
                        };

                        Canvas.SetTop(text, 20 + j * 9);
                        panel.Children.Add(text);
                    }
                    
                }
            };
            
        }
    }
}
