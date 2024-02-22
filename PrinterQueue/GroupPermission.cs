using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation.Runspaces;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Security.Principal;

namespace PrinterQueue
{
	internal class GroupPermission
	{
		public void RemoveEveryonePermission(string printerName, SecurityIdentifier newUserOrGroupSID)
		{
			using (PowerShell PowerShellInstance = PowerShell.Create())
			{
				string script = @"
								$printerName = $args[0]
								$computerName = $env:COMPUTERNAME
								$newUserOrGroupSID = $args[1]  # Passed as an argument
								$everyoneSID = 'S-1-1-0'  # SID for Everyone

								# Check if the printer exists
								$printer = Get-Printer -Name $printerName -ComputerName $computerName -Full

								if ($printer -eq $null) {
									Write-Output 'Printer not found'
								} else {
									# Remove the Everyone permission
									$newPermissionSDDL = $printer.SecurityDescriptorSDDL -replace ';${everyoneSID}', ''

									# Add the new user or group permission
									$newPermissionSDDL += '';(A;;0x3e3f;;;${newUserOrGroupSID})''

									# Update the printer permissions
									Set-Printer -Name $printerName -ComputerName $computerName -PermissionSDDL $newPermissionSDDL
									Write-Output 'Permissions updated successfully.'
								}
							";

				PowerShellInstance.AddScript(script).AddArgument(printerName).AddArgument(newUserOrGroupSID.ToString());

				try
				{
					Collection<PSObject> PSOutput = PowerShellInstance.Invoke();

					if (PowerShellInstance.HadErrors)
					{
						foreach (ErrorRecord error in PowerShellInstance.Streams.Error)
						{
							MessageBox.Show($"Error: {error.ToString()}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
					else
					{
						if (PSOutput.Count > 0)
						{
							MessageBox.Show(PSOutput[0].ToString(), "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
						}
						else
						{
							MessageBox.Show("No output from the PowerShell script.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
}
