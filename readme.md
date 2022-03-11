# Purpose

The purpose of this module is ensure `ContinuousIntegration.exe` can run successfully if the connection string is set in environment variables.

The CMS allows the connection string to be defined in Azure's Configuration Settings. This value overrides the settings in web.config
Unfortuately, in that scenario `ContinuousIntegration.exe` does not pick up the same connection string as the CMS.

# Installation

# Verifying it works

* view the event log
* `ContinuousIntegration.exe` and see the connection string being displayed.


```
Continuous Integration Console
Kentico Software

CMSConnectionString environment variable is not set. Nothing to do.

Restoring objects...
Optimizing file repository...
```

$Env:CMSConnectionString="Data Source=.;Initial Catalog=k13-demo;Integrated Security=False;Persist Security Info=False;User ID=boilerplate13;Password=DSKJHFDJKHDFFDDFDFKFD;Connect Timeout=60;Encrypt=False;Current Language=English;"
```
Continuous Integration Console
Kentico Software

Overriding CMSConnectionString using environment variables
CMSConnectionString=Data Source=.;Initial Catalog=k13-demo;Integrated Security=False;Persist Security Info=False;User ID=boilerplate13;Password=******;Connect Timeout=60;Encrypt=False;Current Language=English

Restoring objects...
Optimizing file repository...
```