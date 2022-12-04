# TodoWebApp

Simple todo web app using ASP.Net 6 MVC

## IDEs

### Visual Studio

- Just hit the green play button

### VS Code & others

- Install [.NET 6.0 SDK](https://dotnet.microsoft.com/en-us/download/visual-studio-sdks) (6.0.400). This is the only SDK version I know of that works in [Railway.app](https://railway.app/) at the time of creating this project. `./global.json` is where the SDK version is set.

- You can now run this command:

> Profiles are stored in `Properties/launchSettings.json`

```sh
$ dotnet watch run --launch-profile HotReload
```
