using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PrinterQueue
{
	internal class PrinterDrivers
	{
		public List<DataModel> ReadDriversFromSystem()
		{
			List<DataModel> driverNames = new List<DataModel>();

			try
			{
				using (PowerShell PowerShellInstance = PowerShell.Create())
				{
					PowerShellInstance.AddScript("Get-PrinterDriver | Select-Object -ExpandProperty Name");

					Collection<PSObject> PSOutput = PowerShellInstance.Invoke();

					if (PowerShellInstance.HadErrors)
					{
						foreach (ErrorRecord errorRecord in PowerShellInstance.Streams.Error)
						{
							throw new ApplicationException($"PowerShell error: {errorRecord.Exception.Message}", errorRecord.Exception);
						}
					}

					foreach (PSObject outputItem in PSOutput)
					{
						string printerName = outputItem.BaseObject.ToString();

						if (printerName != null)
						{
							DataModel printerInfo = new DataModel();
							printerInfo.PrinterName = printerName;
							driverNames.Add(printerInfo);
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw new ApplicationException($"An unexpected error occurred: {ex.Message}", ex);
			}

			return driverNames;
		}
	}
}
