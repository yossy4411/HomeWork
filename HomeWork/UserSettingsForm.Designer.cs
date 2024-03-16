namespace HomeWork
{
    partial class UserSettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            openFileDialog1 = new OpenFileDialog();
            groupBox2 = new GroupBox();
            changeName = new Button();
            label7 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            lastnamekana = new Label();
            firstnamekana = new Label();
            firstname = new Label();
            lastname = new Label();
            label6 = new Label();
            学校 = new Label();
            changeSchool = new Button();
            label5 = new Label();
            groupBox1 = new GroupBox();
            changePassword = new Button();
            label3 = new Label();
            label4 = new Label();
            label2 = new Label();
            changeUsername = new Button();
            label1 = new Label();
            changePictureButton = new Button();
            userPicture = new PictureBox();
            groupBox2.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)userPicture).BeginInit();
            SuspendLayout();
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            openFileDialog1.Filter = "画像ファイル|*.jpg;*.jpeg;*.png;*.gif|すべてのファイル|*.*";
            openFileDialog1.Title = "画像を選択してください";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(changeName);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(tableLayoutPanel1);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(学校);
            groupBox2.Controls.Add(changeSchool);
            groupBox2.Controls.Add(label5);
            groupBox2.Location = new Point(283, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(332, 177);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            groupBox2.Text = "ユーザー情報";
            // 
            // changeName
            // 
            changeName.Location = new Point(6, 77);
            changeName.Name = "changeName";
            changeName.Size = new Size(139, 23);
            changeName.TabIndex = 14;
            changeName.Text = "名前を変更";
            changeName.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            label7.Location = new Point(54, 130);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(272, 15);
            label7.TabIndex = 13;
            label7.Text = "１年１組１番";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(lastnamekana, 1, 0);
            tableLayoutPanel1.Controls.Add(firstnamekana, 0, 0);
            tableLayoutPanel1.Controls.Add(firstname, 0, 1);
            tableLayoutPanel1.Controls.Add(lastname, 1, 1);
            tableLayoutPanel1.Location = new Point(6, 21);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(320, 50);
            tableLayoutPanel1.TabIndex = 5;
            // 
            // lastnamekana
            // 
            lastnamekana.Location = new Point(163, 0);
            lastnamekana.Name = "lastnamekana";
            lastnamekana.Size = new Size(154, 23);
            lastnamekana.TabIndex = 1;
            lastnamekana.Text = "たろう";
            lastnamekana.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // firstnamekana
            // 
            firstnamekana.Location = new Point(3, 0);
            firstnamekana.Name = "firstnamekana";
            firstnamekana.Size = new Size(154, 23);
            firstnamekana.TabIndex = 0;
            firstnamekana.Text = "しゅくだい";
            firstnamekana.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // firstname
            // 
            firstname.Location = new Point(3, 23);
            firstname.Name = "firstname";
            firstname.Size = new Size(154, 56);
            firstname.TabIndex = 3;
            firstname.Text = "宿題";
            firstname.TextAlign = ContentAlignment.TopCenter;
            // 
            // lastname
            // 
            lastname.Location = new Point(163, 23);
            lastname.Name = "lastname";
            lastname.Size = new Size(154, 56);
            lastname.TabIndex = 2;
            lastname.Text = "太郎";
            lastname.TextAlign = ContentAlignment.TopCenter;
            // 
            // label6
            // 
            label6.Location = new Point(6, 130);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(42, 15);
            label6.TabIndex = 12;
            label6.Text = "学年";
            // 
            // 学校
            // 
            学校.Location = new Point(6, 106);
            学校.Margin = new Padding(4, 0, 4, 0);
            学校.Name = "学校";
            学校.Size = new Size(42, 15);
            学校.TabIndex = 9;
            学校.Text = "学校";
            // 
            // changeSchool
            // 
            changeSchool.Location = new Point(6, 147);
            changeSchool.Margin = new Padding(4, 2, 4, 2);
            changeSchool.Name = "changeSchool";
            changeSchool.Size = new Size(201, 23);
            changeSchool.TabIndex = 10;
            changeSchool.Text = "学校または学年を変更";
            changeSchool.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.Location = new Point(56, 106);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(270, 15);
            label5.TabIndex = 11;
            label5.Text = "ここ市立どこか高校";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(changePassword);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(changeUsername);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(283, 193);
            groupBox1.Margin = new Padding(4, 2, 4, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 2, 4, 2);
            groupBox1.Size = new Size(332, 94);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "認証情報";
            // 
            // changePassword
            // 
            changePassword.Location = new Point(168, 60);
            changePassword.Margin = new Padding(4, 2, 4, 2);
            changePassword.Name = "changePassword";
            changePassword.Size = new Size(152, 23);
            changePassword.TabIndex = 8;
            changePassword.Text = "パスワードを変更";
            changePassword.UseVisualStyleBackColor = true;
            changePassword.Click += ChangePassword_Click;
            // 
            // label3
            // 
            label3.Location = new Point(6, 18);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(68, 15);
            label3.TabIndex = 7;
            label3.Text = "ユーザー名";
            // 
            // label4
            // 
            label4.Location = new Point(80, 18);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(80, 15);
            label4.TabIndex = 6;
            label4.Text = "AAAAAAAAAA";
            // 
            // label2
            // 
            label2.Location = new Point(6, 43);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(68, 15);
            label2.TabIndex = 4;
            label2.Text = "パスワード";
            // 
            // changeUsername
            // 
            changeUsername.Location = new Point(6, 60);
            changeUsername.Margin = new Padding(4, 2, 4, 2);
            changeUsername.Name = "changeUsername";
            changeUsername.Size = new Size(154, 23);
            changeUsername.TabIndex = 2;
            changeUsername.Text = "ユーザー名を変更";
            changeUsername.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.Location = new Point(80, 43);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(65, 20);
            label1.TabIndex = 3;
            label1.Text = "******";
            // 
            // changePictureButton
            // 
            changePictureButton.Location = new Point(12, 193);
            changePictureButton.Margin = new Padding(4, 2, 4, 2);
            changePictureButton.Name = "changePictureButton";
            changePictureButton.Size = new Size(187, 25);
            changePictureButton.TabIndex = 1;
            changePictureButton.Text = "画像を変更";
            changePictureButton.UseVisualStyleBackColor = true;
            changePictureButton.Click += ChangePictureButton_Click;
            // 
            // userPicture
            // 
            userPicture.Image = Properties.Resources.User;
            userPicture.Location = new Point(12, 13);
            userPicture.Margin = new Padding(4, 2, 4, 2);
            userPicture.Name = "userPicture";
            userPicture.Size = new Size(187, 176);
            userPicture.SizeMode = PictureBoxSizeMode.Zoom;
            userPicture.TabIndex = 0;
            userPicture.TabStop = false;
            // 
            // UserSettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(626, 294);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(changePictureButton);
            Controls.Add(userPicture);
            Margin = new Padding(4, 2, 4, 2);
            Name = "UserSettingsForm";
            Text = "UserForm";
            groupBox2.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)userPicture).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private OpenFileDialog openFileDialog1;
        private GroupBox groupBox2;
        private Button changeName;
        private Label label7;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lastnamekana;
        private Label firstnamekana;
        private Label firstname;
        private Label lastname;
        private Label label6;
        private Label 学校;
        private Button changeSchool;
        private Label label5;
        private GroupBox groupBox1;
        private Button changePassword;
        private Label label3;
        private Label label4;
        private Label label2;
        private Button changeUsername;
        private Label label1;
        private Button changePictureButton;
        private PictureBox userPicture;
    }
}