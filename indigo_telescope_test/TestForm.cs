using System;
using System.Timers;
using System.Windows.Forms;

namespace ASCOM.INDIGO {
  public partial class TestForm : Form {

    private ASCOM.DriverAccess.Telescope driver;
    private System.Timers.Timer timer;

    public TestForm() {
      InitializeComponent();
      SetUIState();
      timer = new System.Timers.Timer();
      timer.Elapsed += new ElapsedEventHandler(timerEvent);
      timer.Interval = 1000;
      timer.Enabled = false;
    }

    private void timerEvent(object source, ElapsedEventArgs e) {
      BeginInvoke(new MethodInvoker(() => {
        textRA.Text = driver.RightAscension.ToString();
        textDec.Text = driver.Declination.ToString();
        try {
          textAlt.Text = driver.Altitude.ToString();
          textAz.Text = driver.Azimuth.ToString();
        } catch {
        }
      }));
    }

    private void TestForm_FormClosing(object sender, FormClosingEventArgs e) {
      if (IsConnected)
        driver.Connected = false;

      Properties.Settings.Default.Save();
    }

    private void buttonChoose_Click(object sender, EventArgs e) {
      Properties.Settings.Default.DriverId = ASCOM.DriverAccess.Telescope.Choose(Properties.Settings.Default.DriverId);
      SetUIState();
    }

    private void buttonConnect_Click(object sender, EventArgs e) {
      if (IsConnected) {
        timer.Enabled = false;
        driver.Connected = false;
        driver.Dispose();
        driver = null;
      } else {
        driver = new ASCOM.DriverAccess.Telescope(Properties.Settings.Default.DriverId);
        driver.Connected = true;
        timer.Enabled = true;
      }
      SetUIState();
    }

    private void SetUIState() {
      buttonConnect.Enabled = !string.IsNullOrEmpty(Properties.Settings.Default.DriverId);
      buttonChoose.Enabled = !IsConnected;
      buttonConnect.Text = IsConnected ? "Disconnect" : "Connect";
      if (IsConnected) {
        labelDescription.Text = driver.Description;
        textRA.Text = driver.RightAscension.ToString();
        textDec.Text = driver.Declination.ToString();
        try {
          textAlt.Text = driver.Altitude.ToString();
          textAz.Text = driver.Azimuth.ToString();
        } catch {
        }
      } else {
        labelDescription.Text = string.Empty;
      }
    }

    private bool IsConnected {
      get {
        return ((this.driver != null) && (driver.Connected == true));
      }
    }
  }
}
