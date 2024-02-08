using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork.Views
{
    public class TitledPanel : Panel
    {
        public Panel Content;
        public Color HeaderColor = Color.White;
        public bool Opened = false;
        private string Title = string.Empty;
        private readonly Label TitleLabel;
        
        public new string Text
        {
            get { return (Opened ? "▼ " : "▶ ") + Title; }
            set
            {
                Title = value;
                if (TitleLabel != null) TitleLabel.Text = Text;
            }

        }


        public TitledPanel() : this(new() { Location = new(0, 20) }) { }
        public TitledPanel(Panel content)
        {
            Content = content;
            Text = string.Empty;
            Content.Width = Width;
            Content.Height = Height;
            TitleLabel = new Label { Text = Text, BackColor = HeaderColor, Dock = DockStyle.Top };
            TitleLabel.Click += TitleLabel_Click;
            Controls.Add(TitleLabel);
            Controls.Add(Content);
            Content.Visible = Opened;
        }
        
        private void TitleLabel_Click(object? sender, EventArgs e)
        {
            Opened = !Opened;
            Content.Visible = Opened;
            Text = Title;
        }
    }
    
}
