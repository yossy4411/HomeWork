using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork
{
    internal class TableContentsPanel : TableLayoutPanel
    {
        public void AddRow(int row, string[] control)
        {
            Controls.Add(new Label() { Text = control[0], AutoSize = true, MaximumSize = new((int)ColumnStyles[0].Width, 0),TextAlign = ContentAlignment.TopLeft }, 0, row);
            for (int i = 1; i < control.Length; i++)
            {
                Controls.Add(new Label() { Text = control[i], AutoSize = true, MaximumSize = new((int)ColumnStyles[i].Width,0) }, i, row);
            }
        }
        public void AddRow( string[] control)
        {
            AddRow(RowCount, control);
            RowCount++;
        }
    }
}
