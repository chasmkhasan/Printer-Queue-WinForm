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
		public async Task<bool> AddPrinterPortAsync(string portName, string printerHostAddress, int portNumber, int snmp, string snmpCommunity)
		{
			return await Task.Run(() =>
			{
				using (PowerShell PowerShellInstance = PowerShell.Create())
				{
					string script = $"Add-PrinterPort -Name '{portName}' -PrinterHostAddress '{printerHostAddress}' -PortNumber {portNumber} -SNMP {snmp} -SNMPCommunity '{snmpCommunity}'";

					PowerShellInstance.AddScript(script);

					//Collection<PSObject> PSOutput = await Task.Run(() => PowerShellInstance.Invoke());
					Collection<PSObject> PSOutput = PowerShellInstance.Invoke();

					if (PSOutput.Count == 0)
					{
						return true;
					}
					else
					{
						return false;
					}
				}
			});
		}

		public async Task<bool> PrinterPortExistsAsync(string portName)
		{
			return await Task.Run(() => 
			{
				using (PowerShell PowerShellInstance = PowerShell.Create())
				{
					string script = $"Get-CimInstance -Query \"SELECT * FROM Win32_TCPIPPrinterPort WHERE Name = '{portName}'\"";

					PowerShellInstance.AddScript(script);

					//Collection<PSObject> PSOutput = await Task.Run(() => PowerShellInstance.Invoke());
					Collection<PSObject> PSOutput = PowerShellInstance.Invoke();

					bool portExists = PSOutput.Count > 0;

					return portExists;
				}
			});
		}
	}
}
