using System.Windows.Forms;
using HomeWork.Views;
namespace HomeWork
{
    partial class MainForm
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
            userSettingsMenu = new ToolStripMenuItem();
            FriendsMenu = new ToolStripMenuItem();
            addFriendMenu = new ToolStripMenuItem();
            removeFriendMenu = new ToolStripMenuItem();
            listFriendsMenu = new ToolStripMenuItem();
            schedulesMenu = new ToolStripMenuItem();
            addScheduleMenu = new ToolStripMenuItem();
            editScheduleMenu = new ToolStripMenuItem();
            deleteScheduleMenu = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            SearchScheduleMenu = new ToolStripMenuItem();
            feedbackMenu = new ToolStripMenuItem();
            sendFeedbackMenu = new ToolStripMenuItem();
            SupportMenu = new ToolStripMenuItem();
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
            tabs.Location = new Point(562, 23);
            tabs.Margin = new Padding(3, 2, 3, 2);
            tabs.Name = "tabs";
            tabs.SelectedIndex = 0;
            tabs.Size = new Size(332, 398);
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
            calendar.Location = new Point(5, 39);
            calendar.Margin = new Padding(3, 0, 3, 2);
            calendar.Name = "calendar";
            calendar.RowCount = 5;
            calendar.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            calendar.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            calendar.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            calendar.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            calendar.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            calendar.Size = new Size(522, 214);
            calendar.TabIndex = 3;
            // 
            // calendarHeader
            // 
            calendarHeader.BackColor = SystemColors.ControlDark;
            calendarHeader.Controls.Add(nextMonth);
            calendarHeader.Controls.Add(previousMonth);
            calendarHeader.Controls.Add(year);
            calendarHeader.Location = new Point(5, 20);
            calendarHeader.Margin = new Padding(3, 2, 3, 0);
            calendarHeader.Name = "calendarHeader";
            calendarHeader.Size = new Size(522, 20);
            calendarHeader.TabIndex = 4;
            // 
            // nextMonth
            // 
            nextMonth.Image = Properties.Resources.arrow_right;
            nextMonth.Location = new Point(118, 0);
            nextMonth.Margin = new Padding(3, 2, 3, 2);
            nextMonth.Name = "nextMonth";
            nextMonth.Size = new Size(19, 20);
            nextMonth.SizeMode = PictureBoxSizeMode.Zoom;
            nextMonth.TabIndex = 7;
            nextMonth.TabStop = false;
            nextMonth.Click += NextMonth_Click;
            // 
            // previousMonth
            // 
            previousMonth.Image = Properties.Resources.arrow_left;
            previousMonth.Location = new Point(12, 0);
            previousMonth.Margin = new Padding(3, 2, 3, 2);
            previousMonth.Name = "previousMonth";
            previousMonth.Size = new Size(19, 20);
            previousMonth.SizeMode = PictureBoxSizeMode.Zoom;
            previousMonth.TabIndex = 6;
            previousMonth.TabStop = false;
            previousMonth.Click += PreviousMonth_Click;
            // 
            // year
            // 
            year.AutoSize = true;
            year.Location = new Point(37, 2);
            year.Name = "year";
            year.Size = new Size(67, 15);
            year.TabIndex = 5;
            year.Text = "0000年00月";
            // 
            // detailTime
            // 
            detailTime.Location = new Point(5, 20);
            detailTime.Margin = new Padding(3, 2, 3, 0);
            detailTime.Name = "detailTime";
            detailTime.Size = new Size(522, 16);
            detailTime.TabIndex = 5;
            detailTime.Paint += DetailTime_Paint;
            // 
            // detailed
            // 
            detailed.AutoScroll = true;
            detailed.Location = new Point(5, 36);
            detailed.Margin = new Padding(3, 0, 3, 2);
            detailed.Name = "detailed";
            detailed.Size = new Size(522, 140);
            detailed.TabIndex = 6;
            // 
            // calendarGroup
            // 
            calendarGroup.Controls.Add(calendarHeader);
            calendarGroup.Controls.Add(calendar);
            calendarGroup.Location = new Point(3, 2);
            calendarGroup.Margin = new Padding(3, 2, 3, 2);
            calendarGroup.Name = "calendarGroup";
            calendarGroup.Padding = new Padding(3, 2, 3, 2);
            calendarGroup.Size = new Size(533, 257);
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
            detailGroup.Location = new Point(3, 263);
            detailGroup.Margin = new Padding(3, 2, 3, 2);
            detailGroup.Name = "detailGroup";
            detailGroup.Padding = new Padding(3, 2, 3, 2);
            detailGroup.Size = new Size(533, 206);
            detailGroup.TabIndex = 9;
            detailGroup.TabStop = false;
            detailGroup.Text = "カレンダーをクリックして予定を表示できます";
            // 
            // addScheduleButton
            // 
            addScheduleButton.Location = new Point(420, 179);
            addScheduleButton.Margin = new Padding(3, 2, 3, 2);
            addScheduleButton.Name = "addScheduleButton";
            addScheduleButton.Size = new Size(108, 22);
            addScheduleButton.TabIndex = 7;
            addScheduleButton.Text = "追加する";
            addScheduleButton.UseVisualStyleBackColor = true;
            addScheduleButton.Click += AddSchedule;
            // 
            // addSchedule
            // 
            addSchedule.FormattingEnabled = true;
            addSchedule.Items.AddRange(new object[] { "宿題 を追加する", "イベント（期間） を追加する", "イベント（１日間）を追加する" });
            addSchedule.Location = new Point(5, 180);
            addSchedule.Margin = new Padding(3, 2, 3, 2);
            addSchedule.Name = "addSchedule";
            addSchedule.Size = new Size(410, 23);
            addSchedule.TabIndex = 0;
            // 
            // LeftPanel
            // 
            LeftPanel.AutoScroll = true;
            LeftPanel.Controls.Add(calendarGroup);
            LeftPanel.Controls.Add(detailGroup);
            LeftPanel.Location = new Point(0, 23);
            LeftPanel.Margin = new Padding(3, 2, 3, 2);
            LeftPanel.Name = "LeftPanel";
            LeftPanel.Size = new Size(556, 398);
            LeftPanel.TabIndex = 2;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.Window;
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { userMenu, schedulesMenu, feedbackMenu });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(5, 2, 0, 2);
            menuStrip1.Size = new Size(894, 24);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // userMenu
            // 
            userMenu.DropDownItems.AddRange(new ToolStripItem[] { userProfileMenu, userSettingsMenu, FriendsMenu });
            userMenu.Name = "userMenu";
            userMenu.Size = new Size(55, 20);
            userMenu.Text = "ユーザー";
            // 
            // userProfileMenu
            // 
            userProfileMenu.Name = "userProfileMenu";
            userProfileMenu.Size = new Size(180, 22);
            userProfileMenu.Text = "ユーザープロフィール";
            userProfileMenu.Click += UserProfileMenu_Click;
            // 
            // userSettingsMenu
            // 
            userSettingsMenu.Name = "userSettingsMenu";
            userSettingsMenu.Size = new Size(180, 22);
            userSettingsMenu.Text = "ユーザー設定...";
            userSettingsMenu.Click += UserSettingsMenu_Click;
            // 
            // FriendsMenu
            // 
            FriendsMenu.DropDownItems.AddRange(new ToolStripItem[] { addFriendMenu, removeFriendMenu, listFriendsMenu });
            FriendsMenu.Name = "FriendsMenu";
            FriendsMenu.Size = new Size(180, 22);
            FriendsMenu.Text = "フレンド";
            // 
            // addFriendMenu
            // 
            addFriendMenu.Name = "addFriendMenu";
            addFriendMenu.Size = new Size(142, 22);
            addFriendMenu.Text = "フレンドの追加";
            // 
            // removeFriendMenu
            // 
            removeFriendMenu.Name = "removeFriendMenu";
            removeFriendMenu.Size = new Size(142, 22);
            removeFriendMenu.Text = "フレンドの解除";
            // 
            // listFriendsMenu
            // 
            listFriendsMenu.Name = "listFriendsMenu";
            listFriendsMenu.Size = new Size(142, 22);
            listFriendsMenu.Text = "フレンド一覧";
            // 
            // schedulesMenu
            // 
            schedulesMenu.DropDownItems.AddRange(new ToolStripItem[] { addScheduleMenu, editScheduleMenu, deleteScheduleMenu, toolStripSeparator1, SearchScheduleMenu });
            schedulesMenu.Name = "schedulesMenu";
            schedulesMenu.Size = new Size(43, 20);
            schedulesMenu.Text = "予定";
            // 
            // addScheduleMenu
            // 
            addScheduleMenu.Name = "addScheduleMenu";
            addScheduleMenu.Size = new Size(107, 22);
            addScheduleMenu.Text = "追加";
            // 
            // editScheduleMenu
            // 
            editScheduleMenu.Name = "editScheduleMenu";
            editScheduleMenu.Size = new Size(107, 22);
            editScheduleMenu.Text = "編集...";
            // 
            // deleteScheduleMenu
            // 
            deleteScheduleMenu.Name = "deleteScheduleMenu";
            deleteScheduleMenu.Size = new Size(107, 22);
            deleteScheduleMenu.Text = "削除";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(104, 6);
            // 
            // SearchScheduleMenu
            // 
            SearchScheduleMenu.Name = "SearchScheduleMenu";
            SearchScheduleMenu.Size = new Size(107, 22);
            SearchScheduleMenu.Text = "検索...";
            // 
            // feedbackMenu
            // 
            feedbackMenu.DropDownItems.AddRange(new ToolStripItem[] { sendFeedbackMenu, SupportMenu });
            feedbackMenu.Name = "feedbackMenu";
            feedbackMenu.Size = new Size(77, 20);
            feedbackMenu.Text = "フィードバック";
            // 
            // sendFeedbackMenu
            // 
            sendFeedbackMenu.Name = "sendFeedbackMenu";
            sendFeedbackMenu.Size = new Size(193, 22);
            sendFeedbackMenu.Text = "フィードバックを送信する...";
            // 
            // SupportMenu
            // 
            SupportMenu.Name = "SupportMenu";
            SupportMenu.Size = new Size(193, 22);
            SupportMenu.Text = "サポート...";
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageSize = new Size(16, 16);
            imageList1.TransparentColor = Color.Transparent;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(894, 430);
            Controls.Add(LeftPanel);
            Controls.Add(tabs);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 2, 3, 2);
            Name = "MainForm";
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
        private ToolStripMenuItem userSettingsMenu;
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
        private ToolStripMenuItem schedulesMenu;
        private ToolStripMenuItem addScheduleMenu;
        private ToolStripMenuItem deleteScheduleMenu;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem SearchScheduleMenu;
        private ToolStripMenuItem SupportMenu;
        private ToolStripMenuItem editScheduleMenu;
    }
}
