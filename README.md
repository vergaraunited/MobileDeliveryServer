# MobileDeliveryServer
## United Mobile Delivery Server - Abstract DLL for Mobile Delivery Server Hosting components

.netStandard2.0

nuget pack -IncludeReferencedProjects -Build -Symbols -Properties Configuration=Release

##### nuget.config file
```xml
<configuration>
  <packageSources>
    <add key="UMDNuget" value="https://pkgs.dev.azure.com/unitedwindowmfg/1e4fcdac-b7c9-4478-823a-109475434848/_packaging/UMDNuget/nuget/v3/index.json" />
  </packageSources>
  <packageSourceCredentials>
    <UMDNuget>
        <add key="Username" value="any" />
        <add key="ClearTextPassword" value="w75dbjeqggfltkt5m65yf3e33fryf2olu22of55jxj4b3nmfkpaa" />
      </UMDNuget>
  </packageSourceCredentials>
</configuration>
```

## NuGet Package References
Package Name            |  Version  |  Description
--------------------    |  -------  |  -----------
MobileDeliveryGeneral   |   1.4.3   |  Mobile Delivery General Code with Symbols


**ToDo**<br/>
