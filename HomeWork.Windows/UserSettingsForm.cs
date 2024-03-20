using ScheduleLib;
using HomeWork.SettingForms;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Diagnostics;
namespace HomeWork
{
    public partial class UserSettingsForm : Form
    {
        public UserSettingsForm(User me)
        {
            InitializeComponent();
            Load += UserSettingsForm_Load;
            var name = me.Name?.Split(' ');
            var namekana = me.NameKana?.Split(' ');
            firstname.Text = name?[0];
            lastname.Text = name?[1];
            firstnamekana.Text = namekana?[0];
            lastnamekana.Text = namekana?[1];
            using var font = new Font(Font.FontFamily, 15, FontStyle.Bold);
            firstname.Font = lastname.Font = font;
        }

        private void UserSettingsForm_Load(object? sender, EventArgs e)
        {
            userPicture.Paint += UserPicture_Paint;
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        private void UserPicture_Paint(object? sender, PaintEventArgs e)
        {
            PictureBox pictureBox = userPicture;
            pictureBox.Height = pictureBox.Width;
            pictureBox.BackColor = Color.AliceBlue;
            // Bitmapの描画オブジェクトを取得します
            Graphics g = e.Graphics;
            // 円形のパスを作成します
            using GraphicsPath path = new();
            path.AddEllipse(0, 0, pictureBox.Width, pictureBox.Height);

            // パスを反転させます
            Region region = new(new Rectangle(0, 0, pictureBox.Width, pictureBox.Height));
            region.Exclude(path);

            // マスクを適用します
            g.FillRegion(new SolidBrush(Color.FromArgb(120,255,255,255)), region);
        }

        private void ChangePassword_Click(object sender, EventArgs e)
        {
            using var changePasswordForm = new NewPassword();
            switch (changePasswordForm.ShowDialog())
            {
                case DialogResult.OK:
                    break;
                case DialogResult.Cancel:
                    break;
            }

        }

        private void ChangePictureButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // ユーザーが選択したファイルのパスを取得します
                string selectedFilePath = openFileDialog1.FileName;

                // 選択されたファイルを処理します
                Debug.WriteLine("選択されたファイルのパス: " + selectedFilePath);
            }

        }
    }
}
