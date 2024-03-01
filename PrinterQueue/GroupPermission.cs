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
using System.DirectoryServices.AccountManagement;

namespace PrinterQueue
{
	internal class GroupPermission
	{

		public static bool CheckIfLocalGroupExists(string groupName)
		{
			using (Runspace runspace = RunspaceFactory.CreateRunspace())
			{
				runspace.Open();

				using (PowerShell PowerShellInstance = PowerShell.Create())
				{
					PowerShellInstance.Runspace = runspace;

					string script = $"Get-LocalGroup -Name '{groupName}'";

					PowerShellInstance.AddScript(script);

					try
					{
						Collection<PSObject> PSOutput = PowerShellInstance.Invoke();

						if (PowerShellInstance.HadErrors)
						{
							foreach (ErrorRecord error in PowerShellInstance.Streams.Error)
							{
								MessageBox.Show($"Error: {error.ToString()}");
							}
						}
						else
						{
							return PSOutput.Count > 0;
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show($"Error: {ex.Message}");
					}
				}

				return false; // Return false if there's an error or no match
			}
		}

		public static void RemoveEveryonePermission(string printerName)
		{
			using (PowerShell PowerShellInstance = PowerShell.Create())
			{
				string script = @"
								$printerName = $args[0]
								$computerName = $env:COMPUTERNAME
								$everyoneSID = 'S-1-1-0'  # SID for Everyone
								
								#Getting Printer Data
								$printer = Get-Printer -Name $printerName -ComputerName $computerName -Full

								# Remove the Everyone permission
								$newPermissionSDDL = $printer.SecurityDescriptorSDDL -replace "";$everyoneSID"", ''

								# Update the printer permissions
								Set-Printer -Name $printerName -ComputerName $computerName -PermissionSDDL $newPermissionSDDL                                
								";

				PowerShellInstance.AddScript(script).AddArgument(printerName);

				try
				{
					Collection<PSObject> PSOutput = PowerShellInstance.Invoke();

					foreach (ErrorRecord error in PowerShellInstance.Streams.Error)
					{
						LogError(error.ToString());
					}

					if (PSOutput.Count > 0)
					{
						LogOutput(PSOutput[0].ToString());
					}
					else
					{
						LogMessage("No output from the PowerShell script.");
					}
				}
				catch (Exception ex)
				{
					LogError(ex.Message);
				}
			}
		}

		
		private static void LogError(string errorMessage)
		{
			MessageBox.Show($"Error: {errorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private static void LogOutput(string outputMessage)
		{
			MessageBox.Show(outputMessage, "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private static void LogMessage(string message)
		{
			MessageBox.Show(message, "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}


		public static void AddNewGroupPermission(string printerName, SecurityIdentifier newUserOrGroupSID)
		{
			using (PowerShell PowerShellInstance = PowerShell.Create())
			{
				string script = @"
								$printerName = $args[0]
								$computerName = $env:COMPUTERNAME
								$newUserOrGroupSID = $args[1]  # Passed as an argument

								# Check if the printer exists
								$printer = Get-Printer -Name $printerName -ComputerName $computerName -Full

							    # Construct the new permission SDDL
				                $newPermission = ""(A;OIIO;0x1200a9;;;${newUserOrGroupSID})""

				                # Update the printer permissions
				                Set-Printer -Name $printerName -ComputerName $computerName -PermissionSDDL $newPermission
								";

				PowerShellInstance.AddScript(script).AddArgument(printerName).AddArgument(newUserOrGroupSID.ToString());

				try
				{
					Collection<PSObject> PSOutput = PowerShellInstance.Invoke();

					if (PowerShellInstance.HadErrors)
					{
						foreach (ErrorRecord error in PowerShellInstance.Streams.Error)
						{
							MessageBox.Show($"Error: {error.CategoryInfo.Category} - {error.Exception.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
