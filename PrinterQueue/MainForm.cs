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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Cryptography;


namespace PrinterQueue
{
    public partial class MainForm : Form
    {
        private DataModel _dataModel;
        private PrinterDrivers _printerDrivers;
        private GeneratePortName _generatePort;
        private QueueMgt _queueMgt;

        public MainForm()
        {
            _dataModel = new DataModel();
            _printerDrivers = new PrinterDrivers();
            _generatePort = new GeneratePortName();
            _queueMgt = new QueueMgt();

            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitializeGUI();

            // TODO : TEST
            //txtPortName.Text = "MyPort";
            //txtPortAddress.Text = "10.0.1.215";
            //txtPrinterQueue.Text = "MyPrinter";
            //txtGroups.Text = "Users";
            //comboDrivers.SelectedIndex = 7;
            //txtComment.Text = "My comment";
            //txtLocation.Text = "My location";
            //
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
        private void CheckPortnameTextValidation()
        {
            string userInputPortName = _dataModel.PortName.Trim();
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
        }
        #endregion Port Name Area End

        #region Port Address Area
        private void CheckPortAddressnameTextValidation()
        {
            string userInputPortAddress = _dataModel.PortAddress;
            if (!string.IsNullOrEmpty(userInputPortAddress))
            {
                _dataModel.PortAddress = _dataModel.PortAddress.Trim();
            }
            else
            {
                SetMessageBoxText("Condition not met. Please Check PortAddress!", System.Drawing.Color.Red);

                return;
            }
        }
        #endregion Port Address Area

        #region Printer Name Area

        private void CheckPrinterNameTextValidation()
        {
            string userInputPrinterQueue = _dataModel.PrinterQueue.Trim();
            if (!string.IsNullOrEmpty(userInputPrinterQueue))
            {
                SetMessageBoxText("PrinterName Processing...", System.Drawing.Color.Black);

                ReadPrinterName(userInputPrinterQueue);

				SetMessageBoxText($"'{_dataModel.PrinterQueue}' has installed successfully!", System.Drawing.Color.Green);
			}
            else
            {
                SetMessageBoxText("Condition not met. Please Check Printer Name!", System.Drawing.Color.Red);
                return;
            }
        }

        private void ReadPrinterName(string printerQueueName) // consider printer as a Printer Queue.
        {
            if (_queueMgt.PrinterQueueExists(printerQueueName))
            {
                _queueMgt.DeletePrinterQueue(printerQueueName);

                SetMessageBoxText($"{_dataModel.PrinterQueue} was Exits. Now Deleting ....", System.Drawing.Color.Red);
				SetMessageBoxText($"New Printer Queue Processing........", System.Drawing.Color.Green);

			}
            _queueMgt.CreatePrinterQueue(printerQueueName, _dataModel.DriverName, _dataModel.PortName, _dataModel.Comment, _dataModel.Location);
		}

        #endregion Printer Name Area

        #region Group Permission
        public void CheckGroupTextValidation()
        {
            string printerName = _dataModel.PrinterQueue;

            string group = txtGroups.Text;

            bool groupExists = GroupPermission.CheckIfLocalGroupExists(group);

            if (groupExists)
            {
                SecurityIdentifier sid = ConvertToSID(group);

                if (sid != null)
                {
                    GroupPermission.RemoveEveryonePermission(printerName);
					GroupPermission.AddNewGroupPermission(printerName, sid);
				}
            }
        }

        private SecurityIdentifier ConvertToSID(string groupName)
        {
            PrincipalContext context = new PrincipalContext(ContextType.Machine);
            GroupPrincipal groupPrincipal = GroupPrincipal.FindByIdentity(context, IdentityType.Name, groupName);

            if (groupPrincipal != null)
            {
                NTAccount ntAccount = new NTAccount(null, groupName);
                SecurityIdentifier sid = (SecurityIdentifier)ntAccount.Translate(typeof(SecurityIdentifier));
                return sid;
            }
            else
            {
                SetMessageBoxText("The local group does not exist.", System.Drawing.Color.Red);
            }

            return null;
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

        private void BtnInstall_Click(object sender, EventArgs e)
        {
            // TODO : Read input from UI
            _dataModel.PortName = txtPortName.Text;
            _dataModel.PortAddress = txtPortAddress.Text;
            _dataModel.PrinterQueue = txtPrinterQueue.Text;
            _dataModel.Comment = txtComment.Text;
            _dataModel.Location = txtLocation.Text;
            _dataModel.Groups = txtGroups.Text;
            _dataModel.DriverName = comboDrivers.Items[comboDrivers.SelectedIndex].ToString();

            Task.Run(() => { InstallPrinter(); });
        }

        private void InstallPrinter()
        {
            CheckPortAddressnameTextValidation();
            CheckPortnameTextValidation();
            CheckPrinterNameTextValidation();
            // CheckGroupTextValidation();
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
