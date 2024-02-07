using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterQueue
{
	internal class PortNames
	{
		public bool IsPortNameAvailable(string portName)
		{
			string[] installedPrinters = PrinterSettings.InstalledPrinters.Cast<string>().ToArray();

			return installedPrinters.Contains(portName, StringComparer.OrdinalIgnoreCase);
		}

		public string GenerateNewPortName()
		{
			return $"{DateTime.Now.Ticks}";
		}
	}
}
