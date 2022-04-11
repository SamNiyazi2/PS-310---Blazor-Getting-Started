
# 04/07/2022 06:14 am - SSN - Copied from "C:\Sams_P\CEC_Routing_v03\CEC_Routing_v03-master\CEC.RoutingSample\dotnet_add_package_CEC.Routing-plain_local.ps1"


$erroractionpreference = "stop"

$error.clear()


0..10|%{""}

get-date 
""


function write-section-header{


	param (
		$caption
	)

	write-host""
	write-host""
	write-host "$caption"
	write-host""

}

$projectName = "$psscriptroot\ps_310_BethanysPieShopHRM.App_BEFORECONVERSION.csproj"


#$packageName = "CEC_Routing_v03-plain"
$packageName = "CEC_Routing_v03"
#$packageName = "CEC.Routing_plain_v2"

$project = get-childitem  $projectName -errorAction silentlycontinue | where name -match '\.*[^_\d*].csproj' 


if ( $null -eq $project ) {
	write-host $projectName -foregroundcolor yellow
	write-error "File not found"
}

dotnet remove $projectName package $packageName 

. "C:\sams\ps\XML\XML_RemoveReferenceFromProject.ps1"  -fileName $project.fullname  -referenceName  "CEC_Routing_v03-plain"


write-section-header Calling dotnet add package....


dotnet add $projectName package $packageName  -s c:\sams_nuget\packages


write-section-header "Done."


