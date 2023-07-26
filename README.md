# Mx.NET.SDK.SmartSend
⚡ MultiversX .NET NativeAuth Server SDK: Library for validating native login Token for server app

## How to install?
The content is delivered via nuget packages:
##### RemarkableTools.Mx.NativeAuthServer [![Package](https://img.shields.io/nuget/v/RemarkableTools.Mx.NativeAuthServer)](https://www.nuget.org/packages/RemarkableTools.Mx.NativeAuthServer/)

## Main Features
- Validate login token on server side

## Compatibility
- [NativeAuthClient](https://www.nuget.org/packages/RemarkableTools.Mx.NativeAuthClient/)
- xPortal Hub
##### _*Note: The validation is a bit changed from the original [MultiversX NativeAuthServer](https://github.com/multiversx/mx-sdk-js-native-auth-server). In this SDK the ExtraInfo is mandatory and is containing the valid timestamp for the block_

## Quick start guide
### Basic example
```csharp
var nativeAuthServerConfig = new NativeAuthServerConfig();
nativeAuthServerConfig.AcceptedOrigins = new[] { "https://yourwebsite.com" }; // optional 
var nativeAuthServer = new NativeAuthServer(nativeAuthServerConfig);
try
{
    var nativeAuthToken = nativeAuthServer.Validate(accessToken);
    // token is valid
}
catch (Exception ex) // catching all exceptions
{
    // token is not valid
}
```
