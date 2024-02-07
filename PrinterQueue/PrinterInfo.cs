using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterQueue
{
	internal class PrinterInfo
	{
		public string? PortName { get; set; }
		public string? PortAddress { get; set; }
		public string? PrinterQueue { get; set; } 
		public string? Comment { get; set; }
		public string? Location { get; set; }
		public string? Groups { get; set; }
		public string? DriverName { get; set; }
	}
}
