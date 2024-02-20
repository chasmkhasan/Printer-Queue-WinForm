using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PrinterQueue
{
	internal class QueueMgt
	{
		public async Task<bool> PrinterQueueExistsAsync(string queueName)
		{
			return await Task.Run(() => 
			{
				using (PowerShell PowerShellInstance = PowerShell.Create())
				{
					string script = $"Get-Printer -Name '{queueName}'";

					PowerShellInstance.AddScript(script);
					Collection<PSObject> PSOutput = PowerShellInstance.Invoke();

					return PSOutput.Count > 0;
				}
			});
		}

		public async Task DeletePrinterQueueAsync(string queueName)
		{
			await Task.Run(() =>
			{
				using (PowerShell PowerShellInstance = PowerShell.Create())
				{
					string script = $"Remove-Printer -Name '{queueName}'";

					PowerShellInstance.AddScript(script);
					PowerShellInstance.Invoke();
				}
			});
		}

		public async Task CreatePrinterQueueAsync(string queueName, string driverName)
		{
			await Task.Run(() =>
			{
				using (PowerShell PowerShellInstance = PowerShell.Create())
				{
					string script = $"Add-Printer -Name '{queueName}' -DriverName '{driverName}'";

					PowerShellInstance.AddScript(script);
					PowerShellInstance.Invoke();
				}
			});
		}
	}
}
