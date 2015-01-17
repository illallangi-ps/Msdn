$ErrorActionPreference="Stop"
$DebugPreference="Continue"

function Export-Msdn
{
    param(
        [Parameter()]
        [string]
        $Path = ((Get-Item -Path .).FullName)
    )

	Begin
	{
        Write-Debug "Export-Msdn -Path ""$($Path)"""
        
		Get-MsdnCategory | Export-MsdnCategory -Path $Path
	}
}

function Export-MsdnCategory
{
    param(
        [Parameter(Mandatory=$true)]
        [string]
        $Path,

        [Parameter(Mandatory=$true,ValueFromPipelineByPropertyName=$true)]
        [string]
        $Brand,

        [Parameter(Mandatory=$true,ValueFromPipelineByPropertyName=$true)]
        [string]
        $Name,

        [Parameter(Mandatory=$true,ValueFromPipelineByPropertyName=$true)]
        [int]
        $ProductGroupId
    )

    Process
    {
        Write-Debug "Export-MsdnCategory -Path ""$($Path)"" -Root ""$($Root)"" -Brand ""$($Brand)"" -Name ""$($Name)"" -ProductGroupId $($ProductGroupId)"

        $target = (Join-Path -Path $Path -ChildPath $Name)

        if (Test-Path -Path "$($target)")
        {
            Write-Debug " - Folder ""$($target)"" Exists"
        }
        else
		{
			Write-Debug " - New-Item -Path ""$($target)"" -Type Directory"
			New-Item -Path "$($target)" -Type Directory | Out-Null
		}
        
        Get-MsdnFamily -ProductGroupId $ProductGroupId | Export-MsdnFamily -Path $target -Root $Path
    }
}

function Export-MsdnFamily
{
     param(
        [Parameter(Mandatory=$true)]
        [string]
        $Path,

        [Parameter(Mandatory=$true)]
        [string]
        $Root,

        [Parameter(Mandatory=$true,ValueFromPipelineByPropertyName=$true)]
        [int]
        $ProductFamilyId,

        [Parameter(Mandatory=$true,ValueFromPipelineByPropertyName=$true)]
        [string]
        $Title,

        [Parameter(Mandatory=$true,ValueFromPipelineByPropertyName=$true)]
        [int]
        $ProductGroupId
    )

    Process
    {
        Write-Debug "Export-MsdnFamily -Path ""$($Path)"" -Root ""$($Root)"" -ProductFamilyId $($ProductFamilyId) -Title ""$($Title)"" -ProductGroupId $($ProductGroupId)"

        $target = (Join-Path -Path $Path -ChildPath $Title)

        if (Test-Path -Path "$($target)")
        {
            Write-Debug " - Folder ""$($target)"" Exists"
        }
        else
		{
			Write-Debug " - New-Item -Path ""$($target)"" -Type Directory"
			New-Item -Path "$($target)" -Type Directory | Out-Null
		}

        Get-MsdnFile -ProductFamilyId $ProductFamilyId | Export-MsdnFileSearch -Path $target -Root $Root
    }
}

function Export-MsdnFileSearch
{
     param(
        [Parameter(Mandatory=$true)]
        [string]
        $Path,

        [Parameter(Mandatory=$true)]
        [string]
        $Root,

        [Parameter(Mandatory=$true,ValueFromPipelineByPropertyName=$true)]
        [int]
        $FileId
    )
    
    Process
    {
        Write-Debug "Export-MsdnFileSearch -Path ""$($Path)"" -Root ""$($Root)"" -FileId $FileId"

        Get-MsdnFile -FileId $FileId | Export-MsdnFile -Path $Path -Root $Root
    }
}

function Export-MsdnFile
{
     param(
        [Parameter(Mandatory=$true)]
        [string]
        $Path,

        [Parameter(Mandatory=$true)]
        [string]
        $Root,

        [Parameter(Mandatory=$true,ValueFromPipelineByPropertyName=$true)]
        [int]
        $FileId,

        [Parameter(Mandatory=$true,ValueFromPipelineByPropertyName=$true)]
        [string]
        $Description,

        [Parameter(Mandatory=$true,ValueFromPipelineByPropertyName=$true)]
        [string]
        $Sha1Hash,

        [Parameter(Mandatory=$true,ValueFromPipelineByPropertyName=$true)]
        [string]
        $FileName,

        [Parameter(ValueFromPipelineByPropertyName=$true)]
        [string]
        $Notes
    )

    Process
    {
        Write-Debug "Export-MsdnFile -Path ""$($Path)"" -Root ""$($Root)"" -FileId $FileId"

        $target = (Join-Path -Path $Path -ChildPath $Description.Trim('\r').Trim('\n').Trim().Replace(":", "").Replace('"',"'"))
        
        if (Test-Path -Path "$($target)")
        {
            Write-Debug " - Folder ""$($target)"" Exists"
        }
        else
		{
			Write-Debug " - New-Item -Path ""$($target)"" -Type Directory"
			New-Item -Path "$($target)" -Type Directory | Out-Null
		}

        $readme = (Join-Path -Path $target -ChildPath "readme.html")

        if (Test-Path -Path "$($readme)")
        {
            Write-Debug " - Readme ""$($readme)"" Exists"
        }
        else
		{
            if (($null -ne $Notes) -and ("" -ne $Notes))
            {
                Write-Debug " - Notes | Out-File -FilePath ""$($readme)"""
                $Notes | Out-File -FilePath $readme -Encoding default
            }
			else
            {
                Write-Debug " - No notes"
            }
		}

        $checksums = (Join-Path -Path $target -ChildPath "checksums.sha1")

        if (Test-Path -Path "$($checksums)")
        {
            Write-Debug " - Checksums ""$($checksums)"" Exists"
        }
        else
		{
            Write-Debug " - Sha1Hash | Out-File -FilePath ""$($checksums)"""
            "$($Sha1Hash) *$($FileName)" | Out-File -FilePath $checksums -Encoding default
		}

        $relativePath = (Join-Path -Path $target -ChildPath $FileName).Substring($Root.Length + 1)

        "$($Sha1Hash) *$($relativePath)" | Out-File -FilePath (Join-Path -Path $Root -ChildPath "checksums.sha1") -Encoding default -Append
    }
}

