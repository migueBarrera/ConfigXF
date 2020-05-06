# Use a config file for Xamarin Forms

Helper que ayuda a usar un archivo de configuración para tus app Xamarin Forms. Soporta diferentes archivos para Debug y Release.
Tambien puedes usar configuraciones diferentes por plataformas Android, IOS, UWP, etc... 

 ===================


ConfigXF for Xamarin.Forms

Install on .Net Standard Library.

## NuGet
* Available on NuGet: [ConfigXF](https://www.nuget.org/packages/ConfigXF/)

----------

How use it
-------------

* Crea un archivo de configuración en cualquier sitio de tu proyecto compartido.
![Config files](images/configfiles.png)
* Crea una clase con las propiedades que tiene nuestro archivo de configuración. Puedes usar [quicktype](https://quicktype.io/csharp/) o similar
* Inicializa ConfigXF en App.cs 
```
ConfigManager<AppConfig>.Init(Assembly.GetExecutingAssembly());
```
> - sustituye AppConfig por tu clase creada en el paso anterior
> - puedes pasarle un parametro Newtonsoft.Json.Required para personalizar el comportamiento.
* Usala en cualquier sitio 
```
ConfigManager<AppConfig>.CurrentConfig.YOURPROPERTY
```



## Config files
> - El archivo de configuración por defecto debe llamarse:
> - Debug : Config_Debug.json
> - Release : Config_Release.json
> - Override Config : Config.json Remplaza cualquier archivo de configuración anterior
> - Puedes llamar a Init con un objeto ConfigManagerSettings y configurar diferentes nombres para tus ficheros de configuracion


## Different configuration files for each platform

> - En lugar de tener los ficheros de configuración igual para todas las plataformas tu puedes tener diferentes para cada plataforma
> - Solo necesitas pasarle el Assembly del proyecto de la plataforma. Crea un pequeño DependecyService .
```
public interface IAssemblyService
    {
        Assembly GetPlatformAssembly();
    }
```
> - En cada plataforma implementa la interfaz
```
public class AssemblyService : IAssemblyService
    {
        public Assembly GetPlatformAssembly()
        {
            return Assembly.GetExecutingAssembly();
        }
    }
```
> - Usalo al iniciar ConfigXF
```
var platformAsembly = DependencyService.Get<IAssemblyService>().GetPlatformAssembly();

ConfigManager<AppConfig>.Init(platformAsembly);
```

## Más flexibilidad
> - Puedes configurar más parametros usando ConfigManagerSettings
```
var platformAsembly = DependencyService.Get<IAssemblyService>().GetPlatformAssembly();

ConfigManager<AppConfig>.Init(
                new ConfigManagerSettings()
                {
                    Assembly = platformAsembly,
                    Required = Newtonsoft.Json.Required.Always,
                    DebugFile = "Config_Debug.json",
                    ReleaseFile = "MyReleaseFile.json",
                    MasterFile = "MyMasterFile.json",
                });
```




# Contributing
Este proyecto acepta cualquier tipo de aporte de todos los usuarios. Solo tienes que hacer tu Pull Request.

O también puedes contribuir ayudandome con un coffee

<a href='https://ko-fi.com/Y8Y41MNBQ' target='_blank'><img height='36' style='border:0px;height:36px;' src='https://cdn.ko-fi.com/cdn/kofi1.png?v=2' border='0' alt='Buy Me a Coffee at ko-fi.com' /></a>