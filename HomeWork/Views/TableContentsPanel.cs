using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HomeWork.Views
{
    internal class TableContentsPanel : TableLayoutPanel
    {

        public int ContWidth
        {
            get { return (int)ColumnStyles[1].Width; }

        }

        private void AddRow(int row, string fieldname, Control control)
        {
            Controls.Add(new Label() { Text = fieldname, AutoSize = true, MaximumSize = new((int)ColumnStyles[0].Width, 0), TextAlign = ContentAlignment.TopLeft }, 0, row);
            //control.MaximumSize = new(ContWidth, 0);
            Controls.Add(control, 1, row);
            RowCount++;
        }
        public void AddTextRow(string fieldname, string text)
        {
            AddRow(RowCount, fieldname, new Label() { Text = text, AutoSize = true });
        }
        public void AddCustomRow(string fieldname, Control control)
        {
            AddRow(RowCount, fieldname, control);

        }
    }
}
