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
		public List<string> DriversInfor()
		{
			List<string> driverNames = new List<string>();

			try
			{
				using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT DriverName FROM Win32_Printer"))
				{
					ManagementObjectCollection printerCollection = searcher.Get();

					foreach (ManagementObject printer in printerCollection)
					{
						string? driverName = printer["DriverName"]?.ToString();

						if (driverName != null)
						{
							driverNames.Add(driverName);
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
