using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace HomeWork.Views
{
    public partial class SchedulesListView : UserControl
    {
        public SchedulesListView()
        {
            InitializeComponent();
            Loaded += SchedulesListView_Loaded;
        }

        private void SchedulesListView_Loaded(object? sender, EventArgs e)
        {
            for (int j = 0; j <= 48; j++)
            {
                int i = j;
                var line = new Line()
                {
                    StartPoint = new(0, i % 2 == 0 ? 0 : 2.5),
                    EndPoint = new(0, 10),
                    Stroke = new SolidColorBrush(Colors.Red),
                    StrokeThickness = 1,
                };
                Canvas.SetLeft(line, i / 48.0 * time.Bounds.Width);
                time.Children.Add(line);
                if (i % 4 == 0)
                {
                    var text = new TextBlock()
                    {
                        Text = $"{i / 2}h",
                        FontSize = 7
                    };

                    Canvas.SetLeft(text, i / 48.0 * time.Bounds.Width);
                    time.Children.Add(text);
                    time.SizeChanged += (s, e) =>
                    {
                        Canvas.SetLeft(text, i / 48.0 * time.Bounds.Width);
                        Canvas.SetLeft(line, i / 48.0 * time.Bounds.Width);
                    };
                }
                else
                {
                    time.SizeChanged += (s, e) => Canvas.SetLeft(line, i / 48.0 * time.Bounds.Width);
                }
            }
        }
    }
}
