# Purpose

The purpose of this module is ensure `ContinuousIntegration.exe` can run successfully if the connection string is set in environment variables.

Installing this module means you do not have to store database connection information in `web.config` in your source code repository.

The CMS allows the connection string to be defined in Azure's Configuration Settings. This value overrides the settings in `web.config`
Unfortuately, in that scenario `ContinuousIntegration.exe` does not pick up the same connection string as the CMS. Using this module means both the CMS and `ContinuousIntegration.exe` will both use the same connection string.

## Solution

This is a Kentico custom module that looks to see if the `CMSConnectionString` environment variable is set updates the connection string.

This Kentico Module was inspired by the answer given in this StackOverflow question.

https://stackoverflow.com/questions/66820668/how-do-i-allow-continuousintegration-exe-to-use-a-connection-string-in-azure-key


## Installation

From VisualStudio's package manager console type:
```
Install-Package KenticoCommunity.ConnectionStringModule -Version 0.0.1 -Project CMSApp
```

## Verifying it works

Type `ContinuousIntegration.exe` and see the connection string from web.config is being used.


```
Continuous Integration Console
Kentico Software

CMSConnectionString environment variable is not set. Nothing to do.

Restoring objects...
Optimizing file repository...
```

Using a PowerShell window, Set the `CMSConnectionString` environment variable.

```PowerShell
$Env:CMSConnectionString="Data Source=.;Initial Catalog=k13-demo;Integrated Security=False;Persist Security Info=False;User ID=boilerplate13;Password=DSKJHFDJKHDFFDDFDFKFD;Connect Timeout=60;Encrypt=False;Current Language=English;"
```

Now when you Type `ContinuousIntegration.exe`, you will the see the connection string being used comes from the `CMSConnectionString` environment variable.
```
Continuous Integration Console
Kentico Software

Overriding CMSConnectionString using environment variables
CMSConnectionString=Data Source=.;Initial Catalog=k13-demo;Integrated Security=False;Persist Security Info=False;User ID=boilerplate13;Password=******;Connect Timeout=60;Encrypt=False;Current Language=English

Restoring objects...
Optimizing file repository...
```

## License
This project uses a standard MIT license which can be found here.

## Contribution
Contributions are welcome. Feel free to submit pull requests to the repo.

## Support
Please report bugs as issues in this GitHub repo. We'll respond as soon as possible.