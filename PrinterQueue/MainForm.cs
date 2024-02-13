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


			LoadDriversInforTask();
		}

		public void ReadPortName(string enteredPortName)
		{
			_dataModel.PortName = enteredPortName.Trim();

			if (_generatePort.PrinterPortExists(enteredPortName))
			{
				string randomAlphabet = Guid.NewGuid().ToString("N").Substring(0, 1);

				string modifiedPortName = enteredPortName + "_" + randomAlphabet;

				if (_generatePort.PrinterPortExists(enteredPortName))
				{
					bool portAdded = _generatePort.AddPrinterPort(modifiedPortName, _dataModel.PortAddress, 9100, 1, "public");
				}
			}
			else if (!_generatePort.PrinterPortExists(enteredPortName))
			{
				bool portAdded = _generatePort.AddPrinterPort(enteredPortName, _dataModel.PortAddress, 9100, 1, "public");
			}
		}


		private void RaedPrinterName(string printerQueueName) // consider printer as a Printer Queue.
		{
			_dataModel.PrinterName = printerQueueName.Trim();

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

		private void LoadDriversInforTask()
		{
			Task.Run(() => DriversInfor());
		}

		private void DriversInfor()
		{
			List<DataModel> driverNames = _printerDrivers.DriversInfor();

			if (comboDrivers.InvokeRequired)
			{
				comboDrivers.Invoke(new Action(() =>
				{
					comboDrivers.Items.Clear(); // Clear existing items if needed

					foreach (DataModel printerInfo in driverNames)
					{
						comboDrivers.Items.Add(printerInfo.DriverName);
					}

					if (comboDrivers.Items.Count > 0)
					{
						comboDrivers.SelectedIndex = 0;
					}
				}));
			}
			else
			{
				comboDrivers.Items.Clear(); // Clear existing items if needed

				foreach (DataModel printerInfo in driverNames)
				{
					comboDrivers.Items.Add(printerInfo.DriverName);
				}

				if (comboDrivers.Items.Count > 0)
				{
					comboDrivers.SelectedIndex = 0;
				}
			}
		}

		private void InstallPrinter_Click(object sender, EventArgs e)
		{

			_dataModel.PortAddress = txtPortAddress.Text.Trim(); // Do not move from here. First need to read portAddress then Read PortName.

			string userInputPortName = txtPortName.Text;
			ReadPortName(userInputPortName);                     // Do not move from here. First need to read portAddress then Read PortName.

			string userInputPrinterQueue = txtPrinterQueue.Text;
			RaedPrinterName(userInputPrinterQueue);

			_dataModel.Comment = txtComment.Text;

			_dataModel.Location = txtLocation.Text;

			//string userInputGroup = txtGroups.Text;
			//if (!string.IsNullOrEmpty(userInputGroup))
			//{
			//	bool conditionMet = true;

			//	if (conditionMet)
			//	{
			//		GroupPermission();
			//	}
			//}

			string selectedDriverInfo = comboDrivers.SelectedItem?.ToString();
			if (selectedDriverInfo != null)
			{
				string driverName = selectedDriverInfo.Substring(selectedDriverInfo.IndexOf(":") + 1);
				_dataModel.DriverName = driverName;

				if (_installMgt.PrinterExists(_dataModel.PrinterName))
				{
					MessageBox.Show($"Printer '{_dataModel.PrinterName}' is already installed.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
				{
					_installMgt.ExecutePowerShellScript($"Add-Printer -Name '{_dataModel.PrinterName}' -DriverName '{_dataModel.DriverName}' -PortName '{_dataModel.PortName}' -Comment '{_dataModel.Comment}' -Location '{_dataModel.Location}'");
				}
			}

		}
	}
}
