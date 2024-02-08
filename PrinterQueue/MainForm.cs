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
		private PrinterInfo _printerInfo;
		private PrinterDrivers _printerDrivers;
		private GeneratePortName _generatePort;
		private QueueMgt _queueMgt;
		private GroupPermission _groupPermission;
		

		public MainForm()
		{
			_printerInfo = new PrinterInfo();
			_printerDrivers = new PrinterDrivers();
			_generatePort = new GeneratePortName();
			_queueMgt = new QueueMgt();
			_groupPermission = new GroupPermission();

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

				if (_generatePort.IsPortNameAvailableViaPowerShell(newPortName))
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
			if (_printerInfo != null)
			{
				string printerName = _printerInfo.PrinterName;
				string printServerName = _printerInfo.SystemName;
				string userNameOrGroup = txtGroups.Text;

				_groupPermission.SetPrinterPermissions(printerName, printServerName, userNameOrGroup);
			}
		}

		private void DriversInfor()
		{
			try
			{
				List<PrinterInfo> driverNames = _printerDrivers.DriversInfor();

				foreach (PrinterInfo printerInfo in driverNames)
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
				ReadPortName();
				//_printerInfo.PortName = textPortName.Text;
				_printerInfo.PortAddress = txtPortAddress.Text;
				//_printerInfo.PrinterQueue = txtPrinterQueue.Text;
				PrinterQueue();
				_printerInfo.Comment = txtComment.Text;
				_printerInfo.Location = txtLocation.Text;
				//_printerInfo.Groups = txtGroups.Text;
				GroupPermission();

				string selectedDriverInfo = comboDrivers.SelectedItem?.ToString();
				if (selectedDriverInfo != null)
				{
					string driverName = selectedDriverInfo.Substring(selectedDriverInfo.IndexOf(":") + 2);
					_printerInfo.DriverName = driverName;
				}
				//Installation on going

			}
			catch (ApplicationException ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
