using System.Management;
using System.Net;

namespace PrinterQueue
{
	public partial class MainForm : Form
	{
		private PrinterInfo _printerInfo;

		public MainForm()
		{
			_printerInfo = new PrinterInfo();

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
				using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer"))
				{
					ManagementObjectCollection printerCollection = searcher.Get();

					foreach (ManagementObject printer in printerCollection)
					{
						string? driverName = printer["DriverName"]?.ToString();

						if (driverName != null)
						{
							comboDrivers.Items.Add(driverName);
						}
					}
				}
			}
			catch (ManagementException ex)
			{
				MessageBox.Show($"WMI query error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void InstallPrinter_Click(object sender, EventArgs e)
		{
			_printerInfo.PortName = textPortname.Text;
			_printerInfo.PortAddressIP = textPortAddress.Text;
			_printerInfo.PrinterName = textPrintername.Text;
			_printerInfo.Comment = textComment.Text;
			_printerInfo.Location = textLocation.Text;
			_printerInfo.Groups = textGroups.Text;
		}
	}
}
