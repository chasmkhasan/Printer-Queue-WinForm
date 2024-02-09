using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Printing;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PrinterQueue
{
	internal class GeneratePortName
	{
		public bool IsPortNameAvailable(string portName)
		{
			using (PowerShell PowerShellInstance = PowerShell.Create())
			{
				string script = $"Get-PrinterPort -Name '{portName}'";

				PowerShellInstance.AddScript(script);

				Collection<PSObject> PSOutput = PowerShellInstance.Invoke();

				return PSOutput.Count == 0;
			}
		}

		public string GenerateNewPortName()
		{
			using (PowerShell PowerShellInstance = PowerShell.Create())
			{
				string script = $"[System.Guid]::NewGuid().ToString()";

				PowerShellInstance.AddScript(script);

				Collection<PSObject> PSOutput = PowerShellInstance.Invoke();

				if (PSOutput.Count > 0)
				{
					return PSOutput[0].ToString();
				}
				else
				{
					throw new InvalidOperationException("Failed to generate a new port name.");
				}
			}
		}

		public bool IsPortNameAvailableNew(string portName)
		{
			using (PowerShell PowerShellInstance = PowerShell.Create())
			{
				string script = $"Get-WmiObject Win32_TCPIPPrinterPort | Where-Object {{ $_.Name -eq '{portName}' }}";

				PowerShellInstance.AddScript(script);

				Collection<PSObject> PSOutput = PowerShellInstance.Invoke();

				return PSOutput.Count == 0;
			}
		}
	}
}
