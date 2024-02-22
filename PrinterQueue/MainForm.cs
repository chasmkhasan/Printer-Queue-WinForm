using System.Collections.ObjectModel;
using System.IO;
using System.IO.Ports;
using System.Management;
using System.Management.Automation.Runspaces;
using System.Management.Automation;
using System.Net;
using System.DirectoryServices.AccountManagement;
using System.Security.Principal;
using System.Windows.Forms;
using System.Drawing;


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

		private string selectedPrinterDriver;


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
		#endregion Port Name Area End

		#region Printer Name Area

		private void ReadPrinterName(string printerQueueName) // consider printer as a Printer Queue.
		{
			if (_queueMgt.PrinterQueueExists(printerQueueName))
			{
				_queueMgt.DeletePrinterQueue(printerQueueName);
			}

			string selectedDriverName = selectedPrinterDriver;

			_queueMgt.CreatePrinterQueue(printerQueueName, selectedDriverName);
		}

		#endregion Printer Name Area

		#region Group Permission
		public void CheckGroupTextValidation()
		{
			string driverName = selectedPrinterDriver;
			string group = _dataModel.Groups;

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
					SetMessageBoxText("User input does not match the local group name.", System.Drawing.Color.Red);
				}
			}
			else
			{
				SetMessageBoxText("The local group does not exist.", System.Drawing.Color.Red);
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

		private void BtnInstall_Click(object sender, EventArgs e)
		{
			Task.Run(() => { InstallPrinter(); });
		}

		private void InstallPrinter()
		{
			//PortAddress Access								// Do not move from here. First need to read portAddress then Read PortName.
			string userInputPortAddress = txtPortAddress.Text.Trim();
			if (!string.IsNullOrEmpty(userInputPortAddress))
			{
				_dataModel.PortAddress = txtPortAddress.Text.Trim();
			}
			else
			{
				SetMessageBoxText("Condition not met. Please Check PortAddress!", System.Drawing.Color.Red);

				return;
			}

			// PortName Access									// Do not move from here. First need to read portAddress then Read PortName.
			string userInputPortName = txtPortName.Text.Trim();
			if (!string.IsNullOrEmpty(userInputPortName))
			{
				SetMessageBoxText("PortName Processing...", System.Drawing.Color.Black);

				ReadPortName(userInputPortName);

				SetMessageBoxText("PortName successfully taken.", System.Drawing.Color.Green);
			}
			else
			{
				SetMessageBoxText("Condition not met. Please Check PortName!", System.Drawing.Color.Red);

				return;
			}
			
			//PrinterName Access
			string userInputPrinterQueue = txtPrinterQueue.Text;
			if (!string.IsNullOrEmpty(userInputPrinterQueue))
			{
				SetMessageBoxText("PrinterName Processing...", System.Drawing.Color.Black);

				ReadPrinterName(userInputPrinterQueue);

				SetMessageBoxText("PrinterName successfully taken.", System.Drawing.Color.Green);
			}
			else
			{
				SetMessageBoxText("Condition not met. Please Check Printer Name!", System.Drawing.Color.Red);

				return;
			}

			string selectedPrinterDriver = string.Empty;
			if (comboDrivers.InvokeRequired)
			{
				comboDrivers.Invoke(new MethodInvoker(() =>
				{
					selectedPrinterDriver = comboDrivers.SelectedItem.ToString();
				}));
			}
			else
			{
				selectedPrinterDriver = comboDrivers.SelectedItem.ToString();
			}


			_dataModel.Comment = txtComment.Text;

			_dataModel.Location = txtLocation.Text;

			_dataModel.Groups = txtGroups.Text;
			CheckGroupTextValidation();

			string selectedDriverInfo = selectedPrinterDriver;
			if (selectedDriverInfo != null)
			{
				_dataModel.DriverName = selectedDriverInfo;

				if (_installMgt.PrinterExists(_dataModel.PrinterName))
				{
					SetMessageBoxText($"Printer '{_dataModel.DriverName}' is already installed.", System.Drawing.Color.Red);
				}
				else
				{
					PrinterQueueQueryWithTask();

					SetMessageBoxText($"'{_dataModel.DriverName}' has installed successfully!", System.Drawing.Color.Green);
				}
			}

		}

		private void SetMessageBoxText(string text, System.Drawing.Color color)
		{
			if (lblMessageBox.InvokeRequired)
			{
				lblMessageBox.Invoke((MethodInvoker)delegate
				{
					lblMessageBox.Text = text;
					lblMessageBox.ForeColor = color;
					//lblMessageBox.Refresh();
				});
			}
			else
			{
				lblMessageBox.Text = text;
				lblMessageBox.ForeColor = color;
				//lblMessageBox.Refresh();
			}
		}
	}
}
