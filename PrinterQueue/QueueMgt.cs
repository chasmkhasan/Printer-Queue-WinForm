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
		public bool PrinterQueueExists(string queueName)
		{
			using (PowerShell PowerShellInstance = PowerShell.Create())
			{
				string script = $"Get-Printer -Name '{queueName}'";

				PowerShellInstance.AddScript(script);
				//Collection<PSObject> PSOutput = await Task.Run(() => PowerShellInstance.Invoke());
				Collection<PSObject> PSOutput = PowerShellInstance.Invoke();

				return PSOutput.Count > 0;
			}
		}

		public void DeletePrinterQueue(string queueName)
		{
			using (PowerShell PowerShellInstance = PowerShell.Create())
			{
				string script = $"Remove-Printer -Name '{queueName}'";

				PowerShellInstance.AddScript(script);
				//await Task.Run(() => PowerShellInstance.Invoke());
				PowerShellInstance.Invoke();
			}
		}

		public void CreatePrinterQueue(string queueName, string driverName, string portName)
		{
			using (PowerShell PowerShellInstance = PowerShell.Create())
			{
				string script = $"Add-Printer -Name '{queueName}' -DriverName '{driverName}' -PortName '{portName}'";

				PowerShellInstance.AddScript(script);
				//await Task.Run(() => PowerShellInstance.Invoke());
				PowerShellInstance.Invoke();
			}
		}
	}
}
