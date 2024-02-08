using System.Windows.Forms;
using HomeWork.Views;
namespace HomeWork
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tab = new TabControl();
            tabPage1 = new TabPage();
            titledPanel1 = new TitledPanel();
            calendar = new TableLayoutPanel();
            panel1 = new Panel();
            nextMonth = new PictureBox();
            previousMonth = new PictureBox();
            year = new Label();
            detailTime = new Panel();
            detailed = new Panel();
            calendarGroup = new GroupBox();
            detailGroup = new GroupBox();
            leftPanel = new FlowLayoutPanel();
            tab.SuspendLayout();
            tabPage1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nextMonth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)previousMonth).BeginInit();
            calendarGroup.SuspendLayout();
            detailGroup.SuspendLayout();
            leftPanel.SuspendLayout();
            SuspendLayout();
            // 
            // tab
            // 
            tab.Controls.Add(tabPage1);
            tab.Location = new Point(654, 12);
            tab.Name = "tab";
            tab.SelectedIndex = 0;
            tab.Size = new Size(357, 549);
            tab.TabIndex = 1;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(titledPanel1);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(349, 516);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // titledPanel1
            // 
            titledPanel1.AutoSize = true;
            titledPanel1.Location = new Point(157, 215);
            titledPanel1.Name = "titledPanel1";
            titledPanel1.Size = new Size(97, 23);
            titledPanel1.TabIndex = 0;
            // 
            // calendar
            // 
            calendar.ColumnCount = 7;
            calendar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2857113F));
            calendar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2857161F));
            calendar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2857161F));
            calendar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2857161F));
            calendar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2857161F));
            calendar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2857161F));
            calendar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2857161F));
            calendar.Location = new Point(6, 52);
            calendar.Margin = new Padding(3, 0, 3, 3);
            calendar.Name = "calendar";
            calendar.RowCount = 5;
            calendar.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            calendar.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            calendar.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            calendar.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            calendar.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            calendar.Size = new Size(597, 285);
            calendar.TabIndex = 3;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlDark;
            panel1.Controls.Add(nextMonth);
            panel1.Controls.Add(previousMonth);
            panel1.Controls.Add(year);
            panel1.Location = new Point(6, 26);
            panel1.Margin = new Padding(3, 3, 3, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(597, 26);
            panel1.TabIndex = 4;
            // 
            // nextMonth
            // 
            nextMonth.Image = Properties.Resources.arrow_right;
            nextMonth.Location = new Point(135, 0);
            nextMonth.Name = "nextMonth";
            nextMonth.Size = new Size(22, 26);
            nextMonth.SizeMode = PictureBoxSizeMode.Zoom;
            nextMonth.TabIndex = 7;
            nextMonth.TabStop = false;
            nextMonth.Click += NextMonth_Click;
            // 
            // previousMonth
            // 
            previousMonth.Image = Properties.Resources.arrow_left;
            previousMonth.Location = new Point(14, 0);
            previousMonth.Name = "previousMonth";
            previousMonth.Size = new Size(22, 26);
            previousMonth.SizeMode = PictureBoxSizeMode.Zoom;
            previousMonth.TabIndex = 6;
            previousMonth.TabStop = false;
            previousMonth.Click += PreviousMonth_Click;
            // 
            // year
            // 
            year.AutoSize = true;
            year.Location = new Point(42, 3);
            year.Name = "year";
            year.Size = new Size(87, 20);
            year.TabIndex = 5;
            year.Text = "0000年00月";
            // 
            // detailTime
            // 
            detailTime.Location = new Point(6, 26);
            detailTime.Margin = new Padding(3, 3, 3, 0);
            detailTime.Name = "detailTime";
            detailTime.Size = new Size(597, 22);
            detailTime.TabIndex = 5;
            detailTime.Paint += DetailTime_Paint;
            // 
            // detailed
            // 
            detailed.AutoScroll = true;
            detailed.Location = new Point(6, 48);
            detailed.Margin = new Padding(3, 0, 3, 3);
            detailed.Name = "detailed";
            detailed.Size = new Size(597, 186);
            detailed.TabIndex = 6;
            // 
            // calendarGroup
            // 
            calendarGroup.Controls.Add(panel1);
            calendarGroup.Controls.Add(calendar);
            calendarGroup.Location = new Point(3, 3);
            calendarGroup.Name = "calendarGroup";
            calendarGroup.Size = new Size(609, 343);
            calendarGroup.TabIndex = 8;
            calendarGroup.TabStop = false;
            calendarGroup.Text = "カレンダー";
            calendarGroup.UseCompatibleTextRendering = true;
            // 
            // detailGroup
            // 
            detailGroup.Controls.Add(detailTime);
            detailGroup.Controls.Add(detailed);
            detailGroup.Location = new Point(3, 352);
            detailGroup.Name = "detailGroup";
            detailGroup.Size = new Size(609, 240);
            detailGroup.TabIndex = 9;
            detailGroup.TabStop = false;
            detailGroup.Text = "カレンダーをクリックして予定を表示できます";
            // 
            // leftPanel
            // 
            leftPanel.AutoScroll = true;
            leftPanel.Controls.Add(calendarGroup);
            leftPanel.Controls.Add(detailGroup);
            leftPanel.Location = new Point(12, 12);
            leftPanel.Name = "leftPanel";
            leftPanel.Size = new Size(636, 549);
            leftPanel.TabIndex = 2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1023, 573);
            Controls.Add(leftPanel);
            Controls.Add(tab);
            Name = "Form1";
            Text = "HomeWork";
            Load += Form1_Load_1;
            tab.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nextMonth).EndInit();
            ((System.ComponentModel.ISupportInitialize)previousMonth).EndInit();
            calendarGroup.ResumeLayout(false);
            detailGroup.ResumeLayout(false);
            leftPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private TabControl tab;
        private TableLayoutPanel calendar;
        private Panel panel1;
        private PictureBox previousMonth;
        private Label year;
        private PictureBox nextMonth;
        private Panel detailTime;
        private Panel detailed;
        private GroupBox calendarGroup;
        private GroupBox detailGroup;
        private FlowLayoutPanel leftPanel;
        private TabPage tabPage1;
        private TitledPanel titledPanel1;
    }
}
