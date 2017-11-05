using Microsoft.Win32;
using System;
using System.Reflection;
using System.Windows;

namespace HD
{
  public static class StartWithWindows
  {
    // The path to the key where Windows looks for startup applications
    static readonly RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

    const string appName = "Hardly Difficult Miner";

    public static bool shouldStartWithWindows
    {
      get
      {
        return rkApp.GetValue(appName) != null;
      }
      set
      {
        if (value)
        {
          Console.WriteLine(Assembly.GetExecutingAssembly().Location); // TODO remove
          rkApp.SetValue(appName, Assembly.GetExecutingAssembly().Location);
        }
        else
        {
          rkApp.DeleteValue(appName);
        }
      }
    }
  }
}
