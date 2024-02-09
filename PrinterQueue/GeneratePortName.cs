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
		public bool AddPrinterPort(string portName, string printerHostAddress, int portNumber, int snmp, string snmpCommunity)
		{
			using (PowerShell PowerShellInstance = PowerShell.Create())
			{
				string script = $"Add-PrinterPort -Name '{portName}' -PrinterHostAddress '{printerHostAddress}' -PortNumber {portNumber} -SNMP {snmp} -SNMPCommunity '{snmpCommunity}'";

				PowerShellInstance.AddScript(script);

				Collection<PSObject> PSOutput = PowerShellInstance.Invoke();

				if (PSOutput.Count == 0)
				{
					MessageBox.Show($"New PortName is {portName}");
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		public bool PrinterPortExists(string portName)
		{
			using (PowerShell PowerShellInstance = PowerShell.Create())
			{
				string script = $"Get-CimInstance -Query \"SELECT * FROM Win32_TCPIPPrinterPort WHERE Name = '{portName}'\"";

				PowerShellInstance.AddScript(script);

				Collection<PSObject> PSOutput = PowerShellInstance.Invoke();

				bool portExists = PSOutput.Count > 0;

				return portExists;
			}
		}
	}
}
