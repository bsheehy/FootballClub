# http://stackoverflow.com/questions/20259251/powershell-script-to-check-the-status-of-a-url
# http://stackoverflow.com/questions/17486205/call-an-url-with-scheduled-powershell-script
$url="https://frrocksgfc.com/"

# https://support.plesk.com/hc/en-us/articles/213372209-How-to-call-PHP-Perl-ASP-NET-scripts-from-scheduled-tasks
# C:\\Windows\\System32\\WindowsPowerShell\\v1.0\\powershell.exe
# Arguments: -c "(new-object system.net.webclient).downloadstring('http://domain.test/script.aspx')"

# $log_file = "C:\temp\KeepWebSiteAlive2.log"
# Start-Transcript -Path $log_file

# Get-Date -UFormat "%d/%m/%Y %R"
# Write-Host "$date [INFO] Exécution de $url" 

# PowerShell 1
(New-Object System.Net.WebClient).DownloadString("$url");


# PowerShell 3
# Invoke-WebRequest -Uri $url -OutFile "myBig.log"


# PowerShell 2
# $request = [System.Net.WebRequest]::Create($url)
# $response = $request.GetResponse()
# $response.Close()