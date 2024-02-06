using System.IO;
using System.IO.Ports;
using System.Management;
using System.Net;

namespace PrinterQueue
{
	public partial class MainForm : Form
	{
		private PrinterInfo _printerInfo;
		private PrinterDrivers _printerDrivers;
		private GeneratePortName _generatePortName;

		public MainForm()
		{
			_printerInfo = new PrinterInfo();
			_printerDrivers = new PrinterDrivers();
			_generatePortName = new GeneratePortName();

			InitializeComponent();

			InitializeGUI();
		}

		private void InitializeGUI()
		{
			this.Text += " Printer Queue Installer ";

			GetPortName();
			DriversInfor();
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

		private void GetPortName()
		{
			string randomPortName = _generatePortName.GenerateRandomPortName();

			if (_generatePortName.IsPrinterPortAvailable(randomPortName))
			{
				lblPortName.Text = $"Generated Port {randomPortName} is available";
			}
			else
			{
				lblPortName.Text = $"Generated Port {randomPortName} is in use or not accessible";
			}
		}

		private void InstallPrinter_Click(object sender, EventArgs e)
		{
			try
			{
				_printerInfo.PortName = lblPortName.Text;
				_printerInfo.PrinterQueue = lblPrinterQueue.Text;
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
