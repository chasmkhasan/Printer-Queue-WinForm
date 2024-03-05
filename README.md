# Printer-Queue-WinForm

# How to read Local Group Name and Get SID code: 

$groupName = "Administrators"
$group = Get-LocalGroup -Name $groupName
if ($group) {
    $groupSID = $group.SID.Value
    Write-Output "SID of $groupName group: $groupSID"
} else {
    Write-Output "Group '$groupName' not found."
}

# Remove Everyone Permission from Printer Security-Its working with powershell without error. But Not Update in Printer Security. there may be other factors at play, such as group policies or network configurations.

$ScriptBlock = {
     "Executing on {0}" -f $env:COMPUTERNAME
     $PrinterName = "Kyocera Mita CS-1650 KX"  # Replace 'YOUR_PRINTER_NAME' with the name of your specific printer
     $Printer = Get-Printer -Name $PrinterName
     if ($Printer) {
         "Analyzing printer {0}...." -f $PrinterName
         "Printer Details:"
         $Printer
         # Get the printer's security descriptor
         $SecurityDescriptor = $Printer.PSecurityDescriptor
         # Check if 'Everyone' has access to the printer
         $EveryoneEntry = $SecurityDescriptor.DiscretionaryAcl | Where-Object {$_.SecurityIdentifier.Value -eq "S-1-1-0"}
         if ($EveryoneEntry) {
             # Remove 'Everyone' entry from the security descriptor
             $SecurityDescriptor.DiscretionaryAcl.Remove($EveryoneEntry)
             Set-Printer -Name $PrinterName -PermissionSDDL $SecurityDescriptor.GetSddlForm('All')
             "Permissions removed for printer {0}." -f $PrinterName
             # Display updated permissions
             "Updated Printer Permissions:"
             Get-Printer -Name $PrinterName | Select-Object -ExpandProperty PSecurityDescriptor
         } else {
             "Printer {0} does not have 'Everyone' permission." -f $PrinterName
         }
     } else {
         "Printer {0} not found." -f $PrinterName
     }
     "Complete" } Invoke-Command -ScriptBlock $ScriptBlock

# IF does not run in ur machine Need to check Execution policy

You can set the execution policy within your PowerShell script or as a command line parameter when launching PowerShell. To set the execution policy within your PowerShell script, you can use the Set-ExecutionPolicy cmdlet. For example, to set the execution policy to RemoteSigned, you can include the following line at the beginning of your script:
powershell: 
'Set-ExecutionPolicy RemoteSigned -Scope Process -Force'

This command sets the execution policy to RemoteSigned for the current PowerShell session only (-Scope Process) and forcefully (-Force) without prompting for confirmation.


# Run this project in Windows Server
Application must be run by 'Run as a Administrator' Otherwise it will run but will not execute in the system. 
