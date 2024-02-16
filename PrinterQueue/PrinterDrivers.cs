using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
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
				using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Name, SystemName, DriverName FROM Win32_Printer"))
				{
					ManagementObjectCollection printerCollection = searcher.Get();

					foreach (ManagementObject printer in printerCollection)
					{
						string printerName = printer["Name"]?.ToString();
						string systemName = printer["SystemName"]?.ToString();
						string driverName = printer["DriverName"]?.ToString();


						if (printerName != null && systemName != null && driverName != null)
						{
							DataModel printerInfo = new DataModel();
							{
								printerInfo.PrinterName = printerName;
								printerInfo.SystemName = systemName;
								printerInfo.DriverName = driverName;
							}

							driverNames.Add(printerInfo);
						}
					}
				}
			}
			catch (ManagementException ex)
			{
				throw new ApplicationException($"WMI query error: {ex.Message}", ex);
			}
			catch (Exception ex)
			{
				throw new ApplicationException($"An unexpected error occurred: {ex.Message}", ex);
			}

			return driverNames;
		}
	}
}
