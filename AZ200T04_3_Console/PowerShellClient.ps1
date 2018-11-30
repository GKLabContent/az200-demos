#[Net.ServicePointManager]::SecurityProtocol =  [Net.SecurityProtocolType]::Tls11
Invoke-RestMethod -Method Get -Uri "https://<Your web app>.azurewebsites.net/api/echo/Test"
