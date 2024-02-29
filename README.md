# Printer-Queue-WinForm

#How to read Local Group Name and Get SID code: 

$groupName = "Administrators"
$group = Get-LocalGroup -Name $groupName
if ($group) {
    $groupSID = $group.SID.Value
    Write-Output "SID of $groupName group: $groupSID"
} else {
    Write-Output "Group '$groupName' not found."
}

#Remove Everyone Permission from Printer Security-Its working with powershell without error. But Not Update in Printer Security. there may be other factors at play, such as group policies or network configurations.

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
     "Complete"
 }
Invoke-Command -ScriptBlock $ScriptBlock
