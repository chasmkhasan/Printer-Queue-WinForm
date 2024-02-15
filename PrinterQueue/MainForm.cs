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
			txtPortName.BackColor = System.Drawing.Color.Yellow;

			_dataModel.PortName = enteredPortName.Trim();

			if (_generatePort.PrinterPortExists(enteredPortName))
			{
				string randomAlphabet = Guid.NewGuid().ToString("N").Substring(0, 1);

				string modifiedPortName = enteredPortName + "_" + randomAlphabet;

				if (_generatePort.PrinterPortExists(enteredPortName))
				{
					bool portAdded = _generatePort.AddPrinterPort(modifiedPortName, _dataModel.PortAddress, 9100, 1, "public");
				}

				txtPortName.BackColor = System.Drawing.Color.Red;

				return;
			}
			else if (!_generatePort.PrinterPortExists(enteredPortName))
			{
				bool portAdded = _generatePort.AddPrinterPort(enteredPortName, _dataModel.PortAddress, 9100, 1, "public");
			}

			txtPortName.BackColor = System.Drawing.Color.Aquamarine;
		}


		private void RaedPrinterName(string printerQueueName) // consider printer as a Printer Queue.
		{
			//txtPortName.BackColor = System.Drawing.Color.Yellow;

			_dataModel.PrinterName = printerQueueName.Trim();

			if (_queueMgt.PrinterQueueExists(printerQueueName))
			{
				_queueMgt.DeletePrinterQueue(printerQueueName);
			}

			string selectedDriverName = comboDrivers.SelectedItem.ToString();

			_queueMgt.CreatePrinterQueue(printerQueueName, selectedDriverName);

			txtPrinterQueue.BackColor = System.Drawing.Color.Aquamarine;
		}

		public void ReadGroupPermission()
		{
			string userInputgroup = txtGroups.Text;
			if (!string.IsNullOrEmpty(userInputgroup))
			{
				bool conditionMet = true;
				if (conditionMet)
				{
					string printerName = comboDrivers.SelectedItem?.ToString();

					string newSddlString = _groupPermission.RemoveGroupPermission(printerName, userInputgroup);

					_groupPermission.SetPrinterPermissions(printerName, newSddlString);
				}
			}
			else
			{
				lblMessageBox.Text = $"Condition not met. Please Check Groups!";
				lblMessageBox.ForeColor = System.Drawing.Color.Red;

				return;
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

			string userInputPortAddress = txtPortAddress.Text.Trim(); // Do not move from here. First need to read portAddress then Read PortName.
			if (!string.IsNullOrEmpty(userInputPortAddress))
			{
				bool conditionMet = true;
				if (conditionMet)
				{
					_dataModel.PortAddress = txtPortAddress.Text.Trim();
				}
			}
			else
			{
				lblMessageBox.Text = $"Condition not met. Please Check PortAddress!";
				lblMessageBox.ForeColor = System.Drawing.Color.Red;

				return;
			}

			string userInputPortName = txtPortName.Text;
			if (!string.IsNullOrEmpty(userInputPortName))
			{
				bool conditionMet = true;
				if (conditionMet)
				{
					ReadPortName(userInputPortName);             // Do not move from here. First need to read portAddress then Read PortName.
				}
			}
			else
			{
				lblMessageBox.Text = $"Condition not met. Please Check PortName!";
				lblMessageBox.ForeColor = System.Drawing.Color.Red;

				return;
			}

			string userInputPrinterQueue = txtPrinterQueue.Text;
			if (!string.IsNullOrEmpty(userInputPrinterQueue))
			{
				bool conditionMet = true;
				if (conditionMet)
				{
					RaedPrinterName(userInputPrinterQueue);
				}
			}
			else
			{
				lblMessageBox.Text = $"Condition not met. Please Check Printer Name!";
				lblMessageBox.ForeColor = System.Drawing.Color.Red;

				return;
			}

			_dataModel.Comment = txtComment.Text;

			_dataModel.Location = txtLocation.Text;

			ReadGroupPermission();

			string selectedDriverInfo = comboDrivers.SelectedItem?.ToString();
			if (selectedDriverInfo != null)
			{
				_dataModel.DriverName = selectedDriverInfo;

				if (_installMgt.PrinterExists(_dataModel.PrinterName))
				{
					lblMessageBox.Text = $"Printer '{_dataModel.DriverName}' is already installed.";
					lblMessageBox.ForeColor = System.Drawing.Color.Red;
				}
				else
				{
					_installMgt.ExecutePowerShellScript($"Add-Printer -Name '{_dataModel.PrinterName}' -DriverName '{_dataModel.DriverName}' -PortName '{_dataModel.PortName}' -Comment '{_dataModel.Comment}' -Location '{_dataModel.Location}'");

					lblMessageBox.Text = $"'{_dataModel.DriverName}' has installed successfully!";
					lblMessageBox.ForeColor = System.Drawing.Color.Green;
				}
			}
		}
	}
}
