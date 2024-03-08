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
            components = new System.ComponentModel.Container();
            tabs = new TabControl();
            calendar = new TableLayoutPanel();
            calendarHeader = new Panel();
            nextMonth = new PictureBox();
            previousMonth = new PictureBox();
            year = new Label();
            detailTime = new Panel();
            detailed = new Panel();
            calendarGroup = new GroupBox();
            detailGroup = new GroupBox();
            addScheduleButton = new Button();
            addSchedule = new ComboBox();
            LeftPanel = new FlowLayoutPanel();
            menuStrip1 = new MenuStrip();
            userMenu = new ToolStripMenuItem();
            userProfileMenu = new ToolStripMenuItem();
            userPreferencesMenu = new ToolStripMenuItem();
            FriendsMenu = new ToolStripMenuItem();
            addFriendMenu = new ToolStripMenuItem();
            removeFriendMenu = new ToolStripMenuItem();
            listFriendsMenu = new ToolStripMenuItem();
            feedbackMenu = new ToolStripMenuItem();
            sendFeedbackMenu = new ToolStripMenuItem();
            toolTip = new ToolTip(components);
            imageList1 = new ImageList(components);
            colorPicker = new ColorDialog();
            calendarHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nextMonth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)previousMonth).BeginInit();
            calendarGroup.SuspendLayout();
            detailGroup.SuspendLayout();
            LeftPanel.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // tabs
            // 
            tabs.Location = new Point(642, 31);
            tabs.Name = "tabs";
            tabs.SelectedIndex = 0;
            tabs.Size = new Size(380, 530);
            tabs.TabIndex = 1;
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
            // calendarHeader
            // 
            calendarHeader.BackColor = SystemColors.ControlDark;
            calendarHeader.Controls.Add(nextMonth);
            calendarHeader.Controls.Add(previousMonth);
            calendarHeader.Controls.Add(year);
            calendarHeader.Location = new Point(6, 26);
            calendarHeader.Margin = new Padding(3, 3, 3, 0);
            calendarHeader.Name = "calendarHeader";
            calendarHeader.Size = new Size(597, 26);
            calendarHeader.TabIndex = 4;
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
            calendarGroup.Controls.Add(calendarHeader);
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
            detailGroup.Controls.Add(addScheduleButton);
            detailGroup.Controls.Add(addSchedule);
            detailGroup.Controls.Add(detailTime);
            detailGroup.Controls.Add(detailed);
            detailGroup.Location = new Point(3, 352);
            detailGroup.Name = "detailGroup";
            detailGroup.Size = new Size(609, 274);
            detailGroup.TabIndex = 9;
            detailGroup.TabStop = false;
            detailGroup.Text = "カレンダーをクリックして予定を表示できます";
            // 
            // addScheduleButton
            // 
            addScheduleButton.Location = new Point(480, 239);
            addScheduleButton.Name = "addScheduleButton";
            addScheduleButton.Size = new Size(123, 29);
            addScheduleButton.TabIndex = 7;
            addScheduleButton.Text = "追加する";
            addScheduleButton.UseVisualStyleBackColor = true;
            addScheduleButton.Click += AddSchedule;
            // 
            // addSchedule
            // 
            addSchedule.FormattingEnabled = true;
            addSchedule.Items.AddRange(new object[] { "宿題 を追加する", "イベント（期間） を追加する", "イベント（１日間）を追加する" });
            addSchedule.Location = new Point(6, 240);
            addSchedule.Name = "addSchedule";
            addSchedule.Size = new Size(468, 28);
            addSchedule.TabIndex = 0;
            // 
            // LeftPanel
            // 
            LeftPanel.AutoScroll = true;
            LeftPanel.Controls.Add(calendarGroup);
            LeftPanel.Controls.Add(detailGroup);
            LeftPanel.Location = new Point(0, 31);
            LeftPanel.Name = "LeftPanel";
            LeftPanel.Size = new Size(636, 530);
            LeftPanel.TabIndex = 2;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.Window;
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { userMenu, feedbackMenu });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1022, 28);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // userMenu
            // 
            userMenu.DropDownItems.AddRange(new ToolStripItem[] { userProfileMenu, userPreferencesMenu, FriendsMenu });
            userMenu.Name = "userMenu";
            userMenu.Size = new Size(67, 24);
            userMenu.Text = "ユーザー";
            // 
            // userProfileMenu
            // 
            userProfileMenu.Name = "userProfileMenu";
            userProfileMenu.Size = new Size(240, 26);
            userProfileMenu.Text = "ユーザープロフィールの表示";
            // 
            // userPreferencesMenu
            // 
            userPreferencesMenu.Name = "userPreferencesMenu";
            userPreferencesMenu.Size = new Size(240, 26);
            userPreferencesMenu.Text = "ユーザー設定";
            // 
            // FriendsMenu
            // 
            FriendsMenu.DropDownItems.AddRange(new ToolStripItem[] { addFriendMenu, removeFriendMenu, listFriendsMenu });
            FriendsMenu.Name = "FriendsMenu";
            FriendsMenu.Size = new Size(240, 26);
            FriendsMenu.Text = "フレンド";
            // 
            // addFriendMenu
            // 
            addFriendMenu.Name = "addFriendMenu";
            addFriendMenu.Size = new Size(177, 26);
            addFriendMenu.Text = "フレンドの追加";
            // 
            // removeFriendMenu
            // 
            removeFriendMenu.Name = "removeFriendMenu";
            removeFriendMenu.Size = new Size(177, 26);
            removeFriendMenu.Text = "フレンドの解除";
            // 
            // listFriendsMenu
            // 
            listFriendsMenu.Name = "listFriendsMenu";
            listFriendsMenu.Size = new Size(177, 26);
            listFriendsMenu.Text = "フレンド一覧";
            // 
            // feedbackMenu
            // 
            feedbackMenu.DropDownItems.AddRange(new ToolStripItem[] { sendFeedbackMenu });
            feedbackMenu.Name = "feedbackMenu";
            feedbackMenu.Size = new Size(96, 24);
            feedbackMenu.Text = "フィードバック";
            // 
            // sendFeedbackMenu
            // 
            sendFeedbackMenu.Name = "sendFeedbackMenu";
            sendFeedbackMenu.Size = new Size(229, 26);
            sendFeedbackMenu.Text = "フィードバックを送信する";
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageSize = new Size(16, 16);
            imageList1.TransparentColor = Color.Transparent;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1022, 573);
            Controls.Add(LeftPanel);
            Controls.Add(tabs);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "HomeWork";
            Load += Form1_Load_1;
            calendarHeader.ResumeLayout(false);
            calendarHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nextMonth).EndInit();
            ((System.ComponentModel.ISupportInitialize)previousMonth).EndInit();
            calendarGroup.ResumeLayout(false);
            detailGroup.ResumeLayout(false);
            LeftPanel.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TabControl tabs;
        private TableLayoutPanel calendar;
        private Panel calendarHeader;
        private PictureBox previousMonth;
        private Label year;
        private PictureBox nextMonth;
        private Panel detailTime;
        private Panel detailed;
        private GroupBox calendarGroup;
        private GroupBox detailGroup;
        private FlowLayoutPanel LeftPanel;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem userMenu;
        private ToolStripMenuItem userProfileMenu;
        private ToolStripMenuItem userPreferencesMenu;
        private ToolStripMenuItem FriendsMenu;
        private ToolStripMenuItem addFriendMenu;
        private ToolStripMenuItem removeFriendMenu;
        private ToolStripMenuItem listFriendsMenu;
        private ToolStripMenuItem feedbackMenu;
        private ToolStripMenuItem sendFeedbackMenu;
        private ComboBox addSchedule;
        private Button addScheduleButton;
        private ToolTip toolTip;
        private ImageList imageList1;
        private ColorDialog colorPicker;
    }
}
