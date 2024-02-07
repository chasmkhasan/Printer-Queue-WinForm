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
		private PortNames _portNames;

		public MainForm()
		{
			_printerInfo = new PrinterInfo();
			_printerDrivers = new PrinterDrivers();
			_portNames = new PortNames();

			InitializeComponent();

			InitializeGUI();
		}

		private void InitializeGUI()
		{
			this.Text += " Printer Queue Installer ";

			
			DriversInfor();
		}

		private void ReadPortName()
		{
			string enteredPortName = txtPortName.Text.Trim();

			if (_portNames.IsPortNameAvailable(enteredPortName))
			{
				txtPortName.BackColor = Color.Green;
			}
			else
			{
				txtPortName.BackColor = Color.Red;
				string newportname = _portNames.GenerateNewPortName();

				txtPortName.Text = newportname;
			}
		}

		private void DriversInfor()
		{
			try
			{
				List<string> driverNames = _printerDrivers.DriversInfor();

				foreach (string driverName in driverNames)
				{
					string printerInfo = $"{driverName}";
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
				ReadPortName();
				//_printerInfo.PortName = textPortName.Text;
				_printerInfo.PortAddress = txtPortAddress.Text;
				_printerInfo.PrinterQueue = txtPrinterQueue.Text;
				_printerInfo.Comment = txtComment.Text;
				_printerInfo.Location = txtLocation.Text;
				_printerInfo.Groups = txtGroups.Text;

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
