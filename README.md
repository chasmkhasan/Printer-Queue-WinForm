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
