namespace HomeWork.SettingForms
{
    partial class Password
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
            components = new System.ComponentModel.Container();
            cancel = new Button();
            confirm = new Button();
            label1 = new Label();
            label2 = new Label();
            textBox2 = new TextBox();
            label4 = new Label();
            nowUsername = new Label();
            toolTip1 = new ToolTip(components);
            SuspendLayout();
            // 
            // cancel
            // 
            cancel.Location = new Point(12, 93);
            cancel.Name = "cancel";
            cancel.Size = new Size(162, 29);
            cancel.TabIndex = 0;
            cancel.Text = "キャンセル";
            cancel.UseVisualStyleBackColor = true;
            // 
            // confirm
            // 
            confirm.Location = new Point(180, 93);
            confirm.Name = "confirm";
            confirm.Size = new Size(162, 29);
            confirm.TabIndex = 1;
            confirm.Text = "確認";
            confirm.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(89, 15);
            label1.TabIndex = 3;
            label1.Text = "現在のユーザー名";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 44);
            label2.Name = "label2";
            label2.Size = new Size(85, 15);
            label2.TabIndex = 4;
            label2.Text = "新しいユーザー名";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(180, 41);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(162, 23);
            textBox2.TabIndex = 5;
            textBox2.Text = "username";
            toolTip1.SetToolTip(textBox2, "5文字以上16文字以内\r\n半角英数\r\n");
            // 
            // label4
            // 
            label4.ForeColor = Color.Red;
            label4.Location = new Point(12, 67);
            label4.Name = "label4";
            label4.Size = new Size(186, 23);
            label4.TabIndex = 8;
            label4.Text = "現在のパスワードが一致しません";
            // 
            // nowUsername
            // 
            nowUsername.AutoSize = true;
            nowUsername.Location = new Point(180, 15);
            nowUsername.Name = "nowUsername";
            nowUsername.Size = new Size(58, 15);
            nowUsername.TabIndex = 9;
            nowUsername.Text = "username";
            // 
            // Password
            // 
            AcceptButton = confirm;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            CancelButton = cancel;
            ClientSize = new Size(354, 132);
            ControlBox = false;
            Controls.Add(nowUsername);
            Controls.Add(label4);
            Controls.Add(textBox2);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(confirm);
            Controls.Add(cancel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "Password";
            StartPosition = FormStartPosition.CenterParent;
            Text = "パスワードの変更";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button cancel;
        private Button confirm;
        private Label label1;
        private Label label2;
        private TextBox textBox2;
        private Label label4;
        private ToolTip toolTip1;
        private Label nowUsername;
    }
}