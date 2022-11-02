# MetaMaui

This tool is a proof-of-concept and learning opportunity (to learn about MAUI techonology stack).
Its purpose is a client-side replacement for the cloud-native Metadata Discovery feature (still in beta)

## Building

Pretty straight forward, the .NET SDK is needed beforehand. Then go to the root of the project, and simply
```dotnet build```

Should be enough. Most of the time though I build using Visual Studio 2022 to debug. I removed the Android and iOS builds just to speed up the inner development loop, but there is nothing Windows-specific in the application.

## CI

Did not have the time to implement a proper CI system. PRs welcome!
