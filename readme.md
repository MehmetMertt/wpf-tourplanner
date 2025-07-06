## App.config Setup (TourPlanner.Presentation)

To configure application settings for the `TourPlanner.Presentation` project, you need to create an `App.config` file in the root of that project.

### 🔧 Steps

1. In **Solution Explorer**, right-click the `TourPlanner.Presentation` project.
2. Select **Add → New Item...**
3. Choose **"Application Configuration File"** and name it `App.config`.
4. Paste the following content into the file:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <appSettings>
    <add key="host" value="" />
    <add key="username" value="" />
    <add key="password" value="" />
    <add key="database" value="" />
    <add key="openroute_key" value="" />
    <add key="openweather_key" value="" />
  </appSettings>
</configuration>
