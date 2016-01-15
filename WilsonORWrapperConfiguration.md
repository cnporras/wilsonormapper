# Configuration #

Configuration is handled by adding a custom section to your application's XML configuration file (i.e. App.config or Web.config).

## Sample Configuration File ##
```
<?xml version="1.0"?>
<configuration>
	<configSections>
		<section name="WilsonORWrapper" type="WilsonORWrapper.ConfigurationSettings,WilsonORWrapper" />
	</configSections>
	<connectionStrings>
		<add name="TestData" providerName="System.Data.SqlClient" 
			connectionString="server=localhost;database=usertest;trusted_connection=true;"/>
	</connectionStrings>
	<WilsonORWrapper
		mappingsFileName="Mappings.config"
		mappingsFileLocation="UserApp.Entities.dll"
		mappingsFileSource="Assembly"
		connectionString="TestData"
		logger="Log4Net"
	/>
</configuration>
```

## Configuration Options ##

The options available in the `WilsonORWrapper` configuration section follow.

  * **mappingsFileName**: The name of the mappings XML file. The default value is `Mappings.config`.
  * **mappingsFileSource**: Specifies where to find the mappings file, based on a `MappingsFileSource` value. Possible values include `FileSystem` and `Assembly`. The default value is `MappingsFileSource.FileSystem`.
  * **mappingsFileLocation**: The path to the mappings file. If `MappingsFileSource` is set to `mappingsFileSource.FileSystem`, this is the folder where the mappings file is located (either absolute or relative path). If `MappingsFileSource` is set to `mappingsFileSource.Assembly`, this is the the name of the assembly that contains the mappings file as an embedded resource.
  * **connectionString**: The name of the connection string to use. Note that this is not the actual connection string but the named connection string in the `connectionStrings` section of the configuration file. The default value is `WilsonORWrapper`.
  * **logger**: The name of the logger to expose to the services. Logging to the event log and logging using the third-party tools Log4Net and NLog are supported. The default is no logging. Possible values include `None`, `EventLog`, `Log4Net`, and `NLog`.
  * **cache**: The name of the cache to expose to the services. Logging using the AspNet cache and the third-party Memcached library are supported. The default is no cache. Possible values include `None`, `AspNet`, and `Memcached'.



