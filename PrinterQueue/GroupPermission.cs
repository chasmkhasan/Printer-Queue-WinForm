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
		public string RemoveGroupPermission(string printerName, string groupName)
		{
			using (PowerShell PowerShellInstance = PowerShell.Create())
			{
				PowerShellInstance.AddScript($"Get-Printer -Name {printerName} | Select-Object -ExpandProperty SecurityDescriptorSDDL");
				Collection<PSObject> PSOutput = PowerShellInstance.Invoke();

				if (PowerShellInstance.HadErrors)
				{
					foreach (ErrorRecord error in PowerShellInstance.Streams.Error)
					{
						Debug.WriteLine($"PowerShell Error: {error.Exception.Message}");
					}
					return null;
				}

				string currentSddlString = PSOutput.Count > 0 ? PSOutput[0].ToString() : null;

				string newSddlString = currentSddlString?.Replace($"D:(A;;GA;;;{groupName})", "");

				return newSddlString;
			}
		}

		public void SetPrinterPermissions(string printerName, string newSddlString)
		{
			using (PowerShell PowerShellInstance = PowerShell.Create())
			{
				PowerShellInstance.AddScript($"Set-Printer -Name {printerName} -PermissionSDDL '{newSddlString}'");
				PowerShellInstance.Invoke();

				if (PowerShellInstance.HadErrors)
				{
					foreach (ErrorRecord error in PowerShellInstance.Streams.Error)
					{
						Debug.WriteLine($"PowerShell Error: {error.Exception.Message}");
					}
				}
			}
		}
	}
}
