using System.Collections.ObjectModel;
using System.IO;
using System.IO.Ports;
using System.Management;
using System.Management.Automation.Runspaces;
using System.Management.Automation;
using System.Net;

namespace PrinterQueue
{
	public partial class MainForm : Form
	{
		private DataModel _dataModel;
		private PrinterDrivers _printerDrivers;
		private GeneratePortName _generatePort;
		private QueueMgt _queueMgt;
		private GroupPermission _groupPermission;
		private InstallMgt _installMgt;
		

		public MainForm()
		{
			_dataModel = new DataModel();
			_printerDrivers = new PrinterDrivers();
			_generatePort = new GeneratePortName();
			_queueMgt = new QueueMgt();
			_groupPermission = new GroupPermission();
			_installMgt = new InstallMgt();

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

			if (_generatePort.IsPortNameAvailable(enteredPortName))
			{
				txtPortName.BackColor = Color.Green;
			}
			else
			{
				txtPortName.BackColor = Color.Red;
				string newPortName = _generatePort.GenerateNewPortName();

				if (_generatePort.IsPortNameAvailableNew(newPortName))
				{
					txtPortName.Text = newPortName;
				}
			}
		}

		private void PrinterQueue()
		{
			string printerQueueName = txtPrinterQueue.Text;

			if (_queueMgt.PrinterQueueExists(printerQueueName))
			{
				_queueMgt.DeletePrinterQueueViaPowerShell(printerQueueName);
			}

			_queueMgt.CreatePrinterQueueViaPowerShell(printerQueueName);
		}

		public void GroupPermission()
		{
			if (_dataModel != null)
			{
				string printerName = _dataModel.PrinterName;
				string printServerName = _dataModel.SystemName;
				string userNameOrGroup = txtGroups.Text;

				_groupPermission.SetPrinterPermissions(printerName, printServerName, userNameOrGroup);
			}
		}

		private void DriversInfor()
		{
			try
			{
				List<DataModel> driverNames = _printerDrivers.DriversInfor();

				foreach (DataModel printerInfo in driverNames)
				{
					comboDrivers.Items.Add(printerInfo.DriverName);
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
				string userInputPortName = txtPortName.Text;
				if (!_generatePort.IsPortNameAvailable(userInputPortName))
				{
					ReadPortName();
				}
				else
				{
					_dataModel.PortName = userInputPortName;
				}

				_dataModel.PortAddress = txtPortAddress.Text;

				string userInputPrinterQueue = txtPrinterQueue.Text;
				if (!_queueMgt.PrinterQueueExists(userInputPrinterQueue))
				{
					PrinterQueue();
				}
				else
				{
					_dataModel.PrinterQueue = userInputPrinterQueue;
				}

				_dataModel.Comment = txtComment.Text;
				_dataModel.Location = txtLocation.Text;

				string selectedDriverInfo = comboDrivers.SelectedItem?.ToString();
				if (selectedDriverInfo != null)
				{
					string driverName = selectedDriverInfo.Substring(selectedDriverInfo.IndexOf(":") + 2);
					_dataModel.DriverName = driverName;

					if (_installMgt.PrinterExists(_dataModel.PrinterName))
					{
						MessageBox.Show($"Printer '{_dataModel.PrinterName}' is already installed.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					else
					{
						_installMgt.ExecutePowerShellScript($"Add-Printer -Name '{_dataModel.PrinterName}' -DriverName '{_dataModel.DriverName}' -PortName '{_dataModel.PortName}' -Comment '{_dataModel.Comment}' -Location '{_dataModel.Location}'");

						MessageBox.Show($"Printername: {_dataModel.PrinterName} has been installed. PortAddress: {_dataModel.PortAddress}, PortName: {_dataModel.PortName}, Location: {_dataModel.Location}");
					}
				}
				else
				{
					MessageBox.Show("Please select a driver.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (ApplicationException ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
