﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterQueue
{
	internal class PrinterInfo
	{
		public string? PortName { get; set; }
		public string? PortAddressIP { get; set; }
		public string? PrinterName { get; set; }
		public string? Comment { get; set; }
		public string? Location { get; set; }
		public string? Groups { get; set; }
	}
}
