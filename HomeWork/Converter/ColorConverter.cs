using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork.Converter
{
    internal static class ColorConverter
    {
        public static Avalonia.Media.Color ToAvaloniaColor(this System.Drawing.Color color) => new(color.A, color.R, color.G, color.B);
    }
}
