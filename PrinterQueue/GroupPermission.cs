using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation.Runspaces;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PrinterQueue
{
	internal class GroupPermission
	{
		public void RemoveEveryonePermission(string printerName)
		{
			string removeEveryoneCommand = $"Get-Printer -Name {printerName} | Remove-PrintACL -Account 'Everyone'";

			ExecutePowerShellCommand(removeEveryoneCommand);
		}

		public void AddGroupsToPrinter(string printerName, string groups)
		{
			string addGroupsCommand = $"Set-Printer -Name {printerName} -PermissionSDDL {groups}";

			ExecutePowerShellCommand(addGroupsCommand);
		}

		private void ExecutePowerShellCommand(string command)
		{
			using (PowerShell PowerShellInstance = PowerShell.Create())
			{
				PowerShellInstance.AddScript(command);

				Collection<PSObject> PSOutput = PowerShellInstance.Invoke();

				
				if (PowerShellInstance.Streams.Error.Count > 0)
				{
					foreach (ErrorRecord errorRecord in PowerShellInstance.Streams.Error)
					{
						//Debug.WriteLine($"PowerShell Error: {errorRecord.Exception.Message}");
						MessageBox.Show($"PowerShell Error: {errorRecord.Exception.Message}");
					}
				}
			}
		}
	}
}
