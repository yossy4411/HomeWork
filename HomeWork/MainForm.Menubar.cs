using ScheduleLib;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeWork
{
    public partial class MainForm
    {
        public void UserProfileMenu_Click(object? sender, EventArgs e)
        {
            ShowProfile(schedules.Properties.User);
        }
        private void ShowProfile(User user)
        {
            TabPage tab = new() { Text = $"{user.Name}さんのプロフィール" };
            tabs.TabPages.Add(tab);
            PictureBox pictureBox = new()
            {
                Image = Properties.Resources.User,
                Size = new(150, 150),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Aquamarine
            };
            tab.Controls.Add(pictureBox);
            pictureBox.Paint += (sender, e) =>
            {
                using GraphicsPath path = new();
                path.AddEllipse(0, 0, pictureBox.Width, pictureBox.Height);
                pictureBox.Region = new Region(path); //丸くする
            };
            TableLayoutPanel tableLayoutPanel = new() { Location = new(160, 0) };
            tableLayoutPanel.Controls.Add(new Label() { Text = user.NameKana?.Split(' ')[0], AutoSize = true }, 0, 0);
            tableLayoutPanel.Controls.Add(new Label() { Text = user.NameKana?.Split(' ')[1], AutoSize = true }, 1, 0);
            tableLayoutPanel.Controls.Add(new Label() { Text = user.Name?.Split(' ')[0], AutoSize = true, Font = new(fontFamily, 20, FontStyle.Bold) }, 0, 1);
            tableLayoutPanel.Controls.Add(new Label() { Text = user.Name?.Split(' ')[1], AutoSize = true, Font = new(fontFamily, 20, FontStyle.Bold) }, 1, 1);
            tab.Controls.Add(tableLayoutPanel);
        }
    }
}
