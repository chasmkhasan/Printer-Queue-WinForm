using System.Collections.ObjectModel;
using System.IO;
using System.IO.Ports;
using System.Management;
using System.Management.Automation.Runspaces;
using System.Management.Automation;
using System.Net;
using System.DirectoryServices.AccountManagement;
using System.Security.Principal;

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
		public void ReadPortName(string enteredPortName)
		{
			if (_generatePort.PrinterPortExists(enteredPortName))
			{
				string randomAlphabet = Guid.NewGuid().ToString("N").Substring(0, 1);

				string modifiedPortName = enteredPortName + "_" + randomAlphabet;

				if (_generatePort.PrinterPortExists(enteredPortName))
				{
					bool portAdded = _generatePort.AddPrinterPort(modifiedPortName, _dataModel.PortAddress, 9100, 1, "public");
				}

				return;
			}
			else if (!_generatePort.PrinterPortExists(enteredPortName))
			{
				bool portAdded = _generatePort.AddPrinterPort(enteredPortName, _dataModel.PortAddress, 9100, 1, "public");
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
				//lblMessageBox.Refresh();
			}
			else
			{
				//lblMessageBox.Text = $"Condition not met. Please Check PortName!";
				//lblMessageBox.ForeColor = System.Drawing.Color.Red;
				//lblMessageBox.Refresh();

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

		private void ReadPrinterName(string printerQueueName) // consider printer as a Printer Queue.
		{

			_dataModel.PrinterName = printerQueueName.Trim();

			if (_queueMgt.PrinterQueueExists(printerQueueName))
			{
				_queueMgt.DeletePrinterQueue(printerQueueName);
			}

			string selectedDriverName = comboDrivers.SelectedItem.ToString();

			_queueMgt.CreatePrinterQueue(printerQueueName, selectedDriverName);

			txtPrinterQueue.ForeColor = System.Drawing.Color.Green;
		}

		private void CheckPrinterNameTextValidation()
		{
			string userInputPrinterQueue = txtPrinterQueue.Text;
			if (!string.IsNullOrEmpty(userInputPrinterQueue))
			{
				ReadPrinterName(userInputPrinterQueue);
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
		public void CheckGroupTextValidation()
		{
			//string printerName = "Kyocera KM-C4035E KX";
			string driverName = comboDrivers.SelectedItem?.ToString();
			string group = txtGroups.Text;

			SecurityIdentifier sid = ConvertToSID(group);

			if (sid != null)
			{
				_groupPermission.RemoveEveryonePermission(driverName, sid);
			}
		}

		private SecurityIdentifier ConvertToSID(string groupName)
		{
			string targetGroupName = "GET-LocalGroup";

			PrincipalContext context = new PrincipalContext(ContextType.Machine);
			GroupPrincipal groupPrincipal = GroupPrincipal.FindByIdentity(context, IdentityType.Name, targetGroupName);

			if (groupPrincipal != null)
			{
				if (groupName.Equals(targetGroupName, StringComparison.OrdinalIgnoreCase))
				{
					NTAccount ntAccount = new NTAccount(null, targetGroupName);
					SecurityIdentifier sid = (SecurityIdentifier)ntAccount.Translate(typeof(SecurityIdentifier));
					return sid;
				}
				else
				{
					lblMessageBox.Text = "User input does not match the local group name.";
				}
			}
			else
			{
				lblMessageBox.Text = "The local group does not exist.";
			}

			return null; // Return null if there's an error or no match
		}

		#endregion Group Permission

		#region Driver Name Area

		public void ShowingDriverListTillUI()
		{
			List<DataModel> driverList = _printerDrivers.ReadDriversFromSystem();

			comboDrivers.Items.Clear();

			foreach (DataModel printerInfo in driverList)
			{
				if (printerInfo.PrinterName != null)
				{
					comboDrivers.Items.Add(printerInfo.PrinterName);
				}
			}
			comboDrivers.SelectedIndex = 0;
		}

		#endregion Driver Name Area

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
