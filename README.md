# TodoWebApp

Simple todo web app using ASP.Net 6 MVC

## IDEs

### Visual Studio

- Just hit the green play button

### VS Code & others

- Install [.NET 6.0 SDK](https://dotnet.microsoft.com/en-us/download/visual-studio-sdks)
- Add a global.json anywhere in the project folder and insert this:

> This is the only version I know of that works in [Railway.app](https://railway.app/) at the time of creating this project

```json
{ "sdk": { "version": "6.0.400" } )
```

- You can now run this command:

> Profiles are stored in `Properties/launchSettings.json`

```sh
$ dotnet watch run --launch-profile HotReload
```
