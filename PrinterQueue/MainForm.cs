using System.Management;
using System.Net;

namespace PrinterQueue
{
	public partial class MainForm : Form
	{
		private PrinterInfo _printerInfo;
		private PrinterDrivers _printerDrivers;

		public MainForm()
		{
			_printerInfo = new PrinterInfo();
			_printerDrivers = new PrinterDrivers();

			InitializeComponent();

			InitializeGUI();
		}

		private void InitializeGUI()
		{
			this.Text += " Printer Queue Installer ";

			PrinterServerFQDN();

			DriversInfor();
		}

		private void PrinterServerFQDN()
		{
			try
			{
				string serverFQDN = Dns.GetHostEntry(Dns.GetHostName()).HostName;
				textPrinterServer.Text = serverFQDN;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error getting FQDN: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void DriversInfor()
		{
			try
			{
				List<string> driverNames = _printerDrivers.DriversInfor();

				foreach (string driverName in driverNames)
				{
					string printerInfo = $"Driver: {driverName}";
					comboDrivers.Items.Add(printerInfo);
				}
			}
			catch (ApplicationException ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void InstallPrinter_Click(object sender, EventArgs e)
		{
			try
			{
				_printerInfo.PortName = textPortname.Text;
				_printerInfo.PortAddressIP = textPortAddress.Text;
				_printerInfo.PrinterName = textPrintername.Text;
				_printerInfo.Comment = textComment.Text;
				_printerInfo.Location = textLocation.Text;
				_printerInfo.Groups = textGroups.Text;

				string? selectedDriverInfo = comboDrivers.SelectedItem?.ToString();
				if (selectedDriverInfo != null)
				{
					string driverName = selectedDriverInfo.Substring(selectedDriverInfo.IndexOf(":") + 2);
					_printerInfo.DriverName = driverName;
				}
				else
				{
					MessageBox.Show("Please select a driver.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}

				//Installation on going

				MessageBox.Show("Printer installed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (ApplicationException ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
