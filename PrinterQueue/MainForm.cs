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

			ShowingDriverListTillUI();
		}

		#region Port Name Area
		public async Task ReadPortNameAsync(string enteredPortName)
		{
			if (_generatePort.PrinterPortExists(enteredPortName))
			{
				string randomAlphabet = Guid.NewGuid().ToString("N").Substring(0, 1);

				string modifiedPortName = enteredPortName + "_" + randomAlphabet;

				if (await _generatePort.PrinterPortExistsAsync(enteredPortName))
				{
					bool portAdded = await _generatePort.AddPrinterPortAsync(modifiedPortName, _dataModel.PortAddress, 9100, 1, "public");
				}

				return;
			}
			else if (!await _generatePort.PrinterPortExistsAsync(enteredPortName))
			{
				bool portAdded = await _generatePort.AddPrinterPortAsync(enteredPortName, _dataModel.PortAddress, 9100, 1, "public");
			}
		}
		private void CheckPortNameTextValidation()
		{
			string userInputPortName = txtPortName.Text;
			if (!string.IsNullOrEmpty(userInputPortName))
			{

				ReadPortName(userInputPortName);

				//lblMessageBox.Text = $"PortName successfully taken.";
				//lblMessageBox.ForeColor = System.Drawing.Color.Green;

			}
			else
			{
				//lblMessageBox.Text = $"Condition not met. Please Check PortName!";
				//lblMessageBox.ForeColor = System.Drawing.Color.Red;

				return;
			}
		}

		#endregion Port Name Area End

		#region Port Address IP/DNS

		private void CheckPortAddressTextValidation()
		{
			string userInputPortAddress = txtPortAddress.Text.Trim();
			if (!string.IsNullOrEmpty(userInputPortAddress))
			{
				_dataModel.PortAddress = txtPortAddress.Text.Trim();
			}
			else
			{
				lblMessageBox.Text = $"Condition not met. Please Check PortAddress!";
				lblMessageBox.ForeColor = System.Drawing.Color.Red;

				return;
			}
		}

		#endregion Port Address IP/DNS

		#region Printer Name Area

		private async Task ReadPrinterNameAsync(string printerQueueName) // consider printer as a Printer Queue.
		{

			_dataModel.PrinterName = printerQueueName.Trim();

			if (await _queueMgt.PrinterQueueExistsAsync(printerQueueName))
			{
				await _queueMgt.DeletePrinterQueueAsync(printerQueueName);
			}

			string selectedDriverName = comboDrivers.SelectedItem.ToString();

			await _queueMgt.CreatePrinterQueueAsync(printerQueueName, selectedDriverName);

			txtPrinterQueue.ForeColor = System.Drawing.Color.Green;
		}

		private void CheckPrinterNameTextValidation()
		{
			string userInputPrinterQueue = txtPrinterQueue.Text;
			if (!string.IsNullOrEmpty(userInputPrinterQueue))
			{
				await ReadPrinterNameAsync(userInputPrinterQueue);
			}
			else
			{
				lblMessageBox.Text = $"Condition not met. Please Check Printer Name!";
				lblMessageBox.ForeColor = System.Drawing.Color.Red;

				return;
			}
		}

		#endregion Printer Name Area

		#region Group Permission
		public async Task CheckGroupTextValidation()
		{
			string userInputgroup = txtGroups.Text;
			if (!string.IsNullOrEmpty(userInputgroup))
			{
				string printerName = comboDrivers.SelectedItem?.ToString();

				string newSddlString = await _groupPermission.RemoveGroupPermissionAsync(printerName, userInputgroup);

				await _groupPermission.SetPrinterPermissionsAsync(printerName, newSddlString);
			}
			else
			{
				lblMessageBox.Text = $"Condition not met. Please Check Groups!";
				lblMessageBox.ForeColor = System.Drawing.Color.Red;

				return;
			}
		}

		#endregion Group Permission

		#region Driver Name Area

		private void ShowingDriverListTillUI()
		{
			List<DataModel> driverNames = _printerDrivers.ReadDriversFromSystem();

			if (comboDrivers.InvokeRequired)
			{
				comboDrivers.Invoke(new Action(() =>
				{
					comboDrivers.Items.Clear(); // Clear existing items if needed

			return null; // Return null if there's an error or no match
		}
		#endregion
		
		

		public void ShowingDriverListTillUI()
		{
			List<DataModel> driverList = _printerDrivers.ReadDriversFromSystem();

			comboDrivers.Items.Clear();

			comboDrivers.Items.Add("Select a Printer...");

				foreach (DataModel printerInfo in driverNames)
				{
					comboDrivers.Items.Add(printerInfo.PrinterName);
				}
			}
			comboDrivers.SelectedIndex = 0;
		}

		#endregion Driver Name area

		private void PrinterQueueQueryWithTask()
		{
			_installMgt.ExecutePowerShellScript($"Add-Printer " +
														  $"-Name '{_dataModel.PrinterName}' " +
														  $"-DriverName '{_dataModel.DriverName}' " +
														  $"-PortName '{_dataModel.PortName}' " +
														  $"-Comment '{_dataModel.Comment}' " +
														  $"-Location '{_dataModel.Location}'");
		}

		private void InstallPrinter_Click(object sender, EventArgs e)
		{
			Task.Run(async () => { InstallPrinterAsync(); });
		}

		private void InstallPrinterAsync()
		{
			{
				CheckPortAddressTextValidation();  // Do not move from here. First need to read portAddress then Read PortName.

				CheckPortNameTextValidation();      // Do not move from here. First need to read portAddress then Read PortName.

				CheckPrinterNameTextValidation();

				_dataModel.Comment = txtComment.Text;

				_dataModel.Location = txtLocation.Text;

				CheckGroupTextValidation();

				string selectedDriverInfo = comboDrivers.SelectedItem?.ToString();
				if (selectedDriverInfo != null)
				{
					_dataModel.DriverName = selectedDriverInfo;

					if (_installMgt.PrinterExists(_dataModel.PrinterName))
					{
						Invoke((MethodInvoker)delegate
						{
							lblMessageBox.Text = $"Printer '{_dataModel.DriverName}' is already installed.";
							lblMessageBox.ForeColor = System.Drawing.Color.Red;
						});
					}
					else
					{
						PrinterQueueQueryWithTask();

						Invoke((MethodInvoker)delegate
						{
							lblMessageBox.Text = $"'{_dataModel.DriverName}' has installed successfully!";
							lblMessageBox.ForeColor = System.Drawing.Color.Green;
						});
					}
				}
			};
		}
	}
}
