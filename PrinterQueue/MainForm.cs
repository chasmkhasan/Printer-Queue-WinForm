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

			if (_generatePort.PrinterPortExists(enteredPortName))
			{
				string randomAlphabet = Guid.NewGuid().ToString("N").Substring(0, 1);

				string modifiedPortName = enteredPortName + "_" + randomAlphabet;

				if (_generatePort.PrinterPortExists(enteredPortName))
				{
					bool portAdded = _generatePort.AddPrinterPort(modifiedPortName, "printerHostAddress", 9100, 1, "public");
				}
			}
			else if (!_generatePort.PrinterPortExists(enteredPortName))
			{
				bool portAdded = _generatePort.AddPrinterPort(enteredPortName, "printerHostAddress", 9100, 1, "public");
			}
		}

		private void PrinterName() // consider printer as a Printer Queue.
		{
			string printerQueueName = txtPrinterQueue.Text;

			if (_queueMgt.PrinterQueueExists(printerQueueName))
			{
				_queueMgt.DeletePrinterQueue(printerQueueName);
			}

			string selectedDriverName = comboDrivers.SelectedItem.ToString();

			_queueMgt.CreatePrinterQueue(printerQueueName, selectedDriverName);
		}

		public void GroupPermission()
		{
			try
			{
				string printerName = "Kyocera Mita KM-1530 KX"; // Replace with your printer name
				//string printerName = _dataModel.PrinterName;
				string groups = txtGroups.Text;

				_groupPermission.RemoveEveryonePermission(printerName);

				_groupPermission.AddGroupsToPrinter(printerName, groups);

				//MessageBox.Show("Printer permissions updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
				if (!_generatePort.PrinterPortExists(userInputPortName))
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
					PrinterName();
				}
				else
				{
					_dataModel.PrinterQueue = userInputPrinterQueue;
				}

				_dataModel.Comment = txtComment.Text;
				_dataModel.Location = txtLocation.Text;

				string userInputGroup = txtGroups.Text;
				if (!string.IsNullOrEmpty(userInputGroup))
				{
					bool conditionMet = true;

					if (conditionMet)
					{
						GroupPermission();
					}
				}

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
