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

        private void AddRow(int row, string fieldname, Control control) => AddRow(row, fieldname, control, false);
        private void AddRow(int row, string fieldname, Control control, bool fitting)
        {
            Label field = new() { Text = fieldname, AutoSize = true, MaximumSize = new((int)ColumnStyles[0].Width, 0), TextAlign = ContentAlignment.MiddleLeft };
            Controls.Add(field, 0, row);
            Controls.Add(control, 1, row);
            if (fitting)
            {
                control.Width = ContWidth;
            }
            
            RowCount++;
        }
        public void AddTextRow(string fieldname, string text)
        {
            AddRow(RowCount, fieldname, new Label() { Text = text, AutoSize = true });
        }
        public void AddTextInput(string fieldname, string text = "", bool field = false, EventHandler? eventHandler = null)
        {
            TextBox box = new() { Text = text, Multiline = field, ScrollBars = ScrollBars.Vertical,WordWrap = true };
            if(eventHandler != null) { box.TextChanged += eventHandler; }
            if (field) box.Height = 80;
            AddRow(RowCount, fieldname, box, true);
        }

        public void AddCustomRow(string fieldname, Control control, bool fit = false) => AddRow(RowCount, fieldname, control, fit);

        public void AddLinkRow(string fieldname, string linkText, EventHandler clicked)
        {
            LinkLabel link = new() { Text = linkText, AutoSize = true };
            link.Click += clicked;
            AddRow(RowCount, fieldname, link);
        }
    }
}
