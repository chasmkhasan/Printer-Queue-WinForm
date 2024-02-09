using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterQueue
{
	internal class DataModel
	{
		public string _portName;
		public string _portAddress;
		public string _printerQueue;
		public string _comment;
		public string _location;
		public string _groups;
		public string _driverName;
		public string _printerName;
		public string _systemName;

		public string PortName
		{
			get { return _portName; }
			set
			{
				_portName = value;
			}
		}

		public string PortAddress
		{
			get { return _portAddress; }
			set
			{
				_portAddress = value;
			}
		}
		public string PrinterQueue
		{
			get { return _printerQueue; }
			set
			{
				_printerQueue = value;
			}
		}

		public string Comment
		{
			get { return _comment; }
			set
			{
				_comment = value;
			}
		}

		public string Location
		{
			get { return _location; }
			set
			{
				_location = value;
			}
		}

		public string Groups
		{
			get { return _groups; }
			set
			{
				_groups = value;
			}
		}

		public string DriverName
		{
			get { return _driverName; }
			set
			{
				_driverName = value;
			}
		}

		public string PrinterName
		{
			get { return _printerName; }
			set
			{
				_printerName = value;
			}
		}

		public string SystemName
		{
			get { return _systemName; }
			set
			{
				_systemName = value;
			}
		}
	}
}
