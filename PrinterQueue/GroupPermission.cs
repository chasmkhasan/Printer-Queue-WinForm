using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation.Runspaces;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PrinterQueue
{
	internal class GroupPermission
	{
		public void SetPrinterPermissions(string printerName, string printServerName, string userNameOrGroup)
		{
			try
			{
				using (var runspace = RunspaceFactory.CreateRunspace())
				{
					runspace.Open();

					using (var pipeline = runspace.CreatePipeline())
					{
						string command = $"set-printer {printerName} -computer {printServerName} -PermissionSDDL {userNameOrGroup}";

						pipeline.Commands.AddScript(command);

						Collection<PSObject> results = pipeline.Invoke();

						foreach (var result in results)
						{
							// Process the result as needed
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
