using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterQueue
{
	internal class GeneratePortName
	{
		public string GenerateRandomPortName()
		{
			string[] possiblePortNames = { "LPT1", "LPT2", "LPT3" };

			Random random = new Random();
			int index = random.Next(possiblePortNames.Length);

			return possiblePortNames[index];
		}

		public bool IsPrinterPortAvailable(string portName)
		{
			string[] portNames = System.IO.Ports.SerialPort.GetPortNames();

			return portNames.Contains(portName);
		}
	}
}
