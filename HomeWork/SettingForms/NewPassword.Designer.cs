namespace HomeWork.SettingForms
{
    partial class NewPassword
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
            cancel = new Button();
            confirm = new Button();
            textBox1 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            label3 = new Label();
            label4 = new Label();
            SuspendLayout();
            // 
            // cancel
            // 
            cancel.Location = new Point(12, 122);
            cancel.Name = "cancel";
            cancel.Size = new Size(162, 24);
            cancel.TabIndex = 0;
            cancel.Text = "キャンセル";
            cancel.UseVisualStyleBackColor = true;
            // 
            // confirm
            // 
            confirm.Location = new Point(180, 122);
            confirm.Name = "confirm";
            confirm.Size = new Size(162, 24);
            confirm.TabIndex = 1;
            confirm.Text = "確認";
            confirm.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(159, 12);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(183, 23);
            textBox1.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(85, 15);
            label1.TabIndex = 3;
            label1.Text = "現在のパスワード";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 44);
            label2.Name = "label2";
            label2.Size = new Size(81, 15);
            label2.TabIndex = 4;
            label2.Text = "新しいパスワード";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(159, 41);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(183, 23);
            textBox2.TabIndex = 5;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(159, 70);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(183, 23);
            textBox3.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 73);
            label3.Name = "label3";
            label3.Size = new Size(129, 15);
            label3.TabIndex = 7;
            label3.Text = "新しいパスワード（確認）";
            // 
            // label4
            // 
            label4.ForeColor = Color.Red;
            label4.Location = new Point(12, 96);
            label4.Name = "label4";
            label4.Size = new Size(186, 23);
            label4.TabIndex = 8;
            label4.Text = "現在のパスワードが一致しません";
            // 
            // NewPassword
            // 
            AcceptButton = confirm;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            CancelButton = cancel;
            ClientSize = new Size(353, 152);
            ControlBox = false;
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Controls.Add(confirm);
            Controls.Add(cancel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "NewPassword";
            StartPosition = FormStartPosition.CenterParent;
            Text = "パスワードの変更";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button cancel;
        private Button confirm;
        private TextBox textBox1;
        private Label label1;
        private Label label2;
        private TextBox textBox2;
        private TextBox textBox3;
        private Label label3;
        private Label label4;
    }
}