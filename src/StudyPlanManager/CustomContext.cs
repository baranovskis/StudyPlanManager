using Microsoft.Owin.Hosting;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace StudyPlanManager
{
    public class CustomContext : ApplicationContext
    {
        public const string BaseAddress = "http://localhost:9000/";

        private NotifyIcon _trayIcon;
        private readonly IDisposable _webApp;

        public CustomContext()
        {
            Application.ApplicationExit += new EventHandler(OnApplicationExit);

            InitializeComponent();

            _webApp = WebApp.Start<Startup>(url: BaseAddress);
            Process.Start(BaseAddress);

            _trayIcon.ShowBalloonTip(3000);
        }

        private void InitializeComponent()
        {
            _trayIcon = new NotifyIcon
            {
                BalloonTipIcon = ToolTipIcon.Info,
                BalloonTipText = "Web server started! Double click on notification icon to open application.",
                BalloonTipTitle = "Study Plan Manager",
                Text = "Study Plan Manager (Web Server)",
                Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath)
            };

            //Optional - handle doubleclicks on the icon:
            _trayIcon.DoubleClick += TrayIcon_DoubleClick;

            //Optional - Add a context menu to the TrayIcon:
            var contextMenu = new ContextMenuStrip();
   
            var closeMenuItem = new ToolStripMenuItem
            {
                Name = "CloseMenuItem",
                Size = new Size(152, 22),
                Text = "Close the tray icon program"
            };

            closeMenuItem.Click += new EventHandler(CloseMenuItem_Click);

            contextMenu.Items.AddRange(new ToolStripItem[] { closeMenuItem });
            contextMenu.Name = "TrayIconContextMenu";
            contextMenu.Size = new Size(153, 70);
  
            _trayIcon.ContextMenuStrip = contextMenu;
            _trayIcon.Visible = true;
        }
        private void OnApplicationExit(object sender, EventArgs e)
        {
            _webApp.Dispose();
            _trayIcon.Visible = false;
        }

        private void TrayIcon_DoubleClick(object sender, EventArgs e)
        {
            Process.Start(BaseAddress);
        }

        private void CloseMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to close me?",
                    "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
