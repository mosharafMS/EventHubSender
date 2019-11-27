
$rgName=Read-Host "Please enter resourceGroup name"

$rg=Get-AzResourceGroup -Name $rgName
if($null -eq $rg)
{
    ThrowError -errorId 1978 -ExceptionName "ApplicationException" -ExceptionMessage "Can not locate Resource Group"
    exit
}
$namespace= Read-Host "Please enter Event hub namespace"
$location="CanadaCentral"
$eventHubName="DeviceIngestion"
$partitionCount=32
$messageRetentionInDays=1

$EHNamespace=New-AzEventHubNamespace -ResourceGroupName $rgName -NamespaceName $namespace -Location $location

New-AzEventHub -ResourceGroupName $rgName -Namespace $EHNamespace.Name -Name $eventHubName -PartitionCount $partitionCount -MessageRetentionInDays $messageRetentionInDays

$senderRule = New-AzEventHubAuthorizationRule -ResourceGroupName $rgName -Namespace $EHNamespace.Name -EventHub $eventHubName -Name Sender01 -Rights @("Send")
$senderSAS=New-AzEventHubAuthorizationRuleSASToken -AuthorizationRuleId $senderRule.Id -KeyType primary -ExpiryTime ((Get-Date).AddDays(30))
Write-Host
Write-Host "Event Hub Name: " + $eventHubName
$senderSAS.SharedAccessSignature


