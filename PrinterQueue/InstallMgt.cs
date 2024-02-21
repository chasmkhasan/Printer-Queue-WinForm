using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PrinterQueue
{
	internal class InstallMgt
	{
		
		public async Task ExecutePowerShellScriptAsync(string fullScript)
		{
			await Task.Run(() =>
			{
				using (PowerShell PowerShellInstance = PowerShell.Create())
				{
					PowerShellInstance.AddScript(fullScript);

					Collection<PSObject> PSOutput = PowerShellInstance.Invoke();
				}
			});
		}

		public bool PrinterExists(string printerName)
		{
			string script = $"Get-Printer -Name '{printerName}'";

			try
			{
				using (PowerShell PowerShellInstance = PowerShell.Create())
				{
					PowerShellInstance.AddScript(script);

					Collection<PSObject> PSOutput = PowerShellInstance.Invoke();

					return PSOutput.Count > 0; 
				}
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
