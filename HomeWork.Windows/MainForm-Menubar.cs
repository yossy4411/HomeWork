using ScheduleLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeWork
{
    public partial class MainForm
    {
        public void UserProfileMenu_Click(object? sender, EventArgs e)
        {
            ShowProfile(userData.Properties.User);
        }
        private void UserSettingsMenu_Click(object sender, EventArgs e)
        {
            var form = new UserSettingsForm(userData.Properties.User);
            form.Show();
        }
        private void ShowProfile(User user)
        {
            using var columnfont = new Font(fontFamily, 12, FontStyle.Bold | FontStyle.Underline);
            TabPage tab = new() { Text = $"{user.Name}さんのプロフィール", AutoScroll=true };
            tabs.TabPages.Add(tab);
            PictureBox pictureBox = new()
            {
                Image = Properties.Resources.User,
                Size = new(150, 150),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.AliceBlue
            };
            FlowLayoutPanel controls = new()
            {
                AutoSize = true,
                WrapContents = false,
                FlowDirection = FlowDirection.TopDown,
            };
            controls.Size = controls.MaximumSize = new(tab.Size.Width - 25, 0);

            FlowLayoutPanel header = new() { Location = new(160, 0), AutoSize = true,  FlowDirection = FlowDirection.TopDown };
            tab.Controls.Add(controls);
            pictureBox.Paint += (sender, e) =>
            {
                using GraphicsPath path = new();
                path.AddEllipse(0, 0, pictureBox.Width, pictureBox.Height);
                pictureBox.Region = new Region(path); //丸くする
            };
            TableLayoutPanel tableLayoutPanel = new() { Location = new(160, 0), AutoSize = true };
            using var namefont = new Font(fontFamily, 17, FontStyle.Bold);
            tableLayoutPanel.Controls.Add(new Label() { Text = user.NameKana?.Split(' ')[0], AutoSize = true }, 0, 0);
            tableLayoutPanel.Controls.Add(new Label() { Text = user.NameKana?.Split(' ')[1], AutoSize = true }, 1, 0);
            tableLayoutPanel.Controls.Add(new Label() { Text = user.Name?.Split(' ')[0], AutoSize = true, Font = namefont }, 0, 1);
            tableLayoutPanel.Controls.Add(new Label() { Text = user.Name?.Split(' ')[1], AutoSize = true, Font = namefont }, 1, 1);
            header.Controls.Add(tableLayoutPanel);
            if (user.Creator) 
                header.Controls.Add(new Label() { Text = "おかゆグループ", AutoSize = true, ForeColor = Color.SkyBlue });
            if (userData.Friends.Contains(user.Id))
                header.Controls.Add(new Label() { Text = "フレンド", AutoSize = true, ForeColor = Color.LightCoral });
            Panel panel = new() { AutoSize = true};
            panel.Controls.Add(pictureBox);
            panel.Controls.Add(header);
            controls.Controls.Add(panel);
            {
                FlowLayoutPanel flowLayoutPanel = new()
                {
                    FlowDirection = FlowDirection.TopDown,
                    Padding = new(0, 0, 0, 10),
                    Location = new(0, 160),
                    AutoSize = true,
                    WrapContents = true,
                };
            
                foreach (var item in user.Proficiencies)
                {
                    flowLayoutPanel.Controls.Add(new Label() { Text = $"{item.Name}　{item.Text}", AutoSize = true });
                }
                controls.Controls.Add(new Label() { Text = "スキル", AutoSize = true, Font = columnfont } );
                controls.Controls.Add(flowLayoutPanel);
            }
            controls.Controls.Add(new Label() { Text = "説明", AutoSize = true, Font = columnfont, Margin = new(0, 20, 0, 0), });
            controls.Controls.Add(new Label() { Text = user.Memo, AutoSize = true, MaximumSize = controls.MaximumSize });
            {
                FlowLayoutPanel flowLayoutPanel = new()
                {
                    FlowDirection = FlowDirection.TopDown,
                    Padding = new(0, 0, 0, 10),
                    Location = new(0, 160),
                    AutoSize = true,
                    WrapContents = true,
                };
                
                for (int i = 0; i < user.Links.Count; i++)
                {
                    if (i % 2 == 0)
                        flowLayoutPanel.Controls.Add(new Label() { Text = user.Links[i], AutoSize = true });
                    else
                    {
                        LinkLabel link = new() { Text = user.Links[i], AutoSize = true };
                        link.Click += (sender, e) =>
                        {
                            Process.Start(new ProcessStartInfo()
                            {
                                FileName = link.Text,
                                UseShellExecute = true,
                            });
                            link.LinkVisited = true;
                        };
                        flowLayoutPanel.Controls.Add(link);
                    }
                    
                }
                controls.Controls.Add(new Label() { Text = "リンク", AutoSize = true, Font = columnfont, Margin = new(0, 20, 0, 0), });
                controls.Controls.Add(flowLayoutPanel);
            }
        }

    }
}
