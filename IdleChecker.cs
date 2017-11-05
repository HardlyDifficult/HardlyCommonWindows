using System;
using System.Runtime.InteropServices;

namespace HD
{
  internal struct LASTINPUTINFO
  {
    public uint cbSize;
    public uint dwTime;
  }

  /// <summary>
  /// Helps to find the idle time, (in milliseconds) spent since the last user input
  /// </summary>
  public class IdleTimeFinder
  {
    [DllImport("User32.dll")]
    static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

    [DllImport("Kernel32.dll")]
    static extern uint GetLastError();
    
    public static TimeSpan GetIdleTime()
    {
      LASTINPUTINFO lastInPut = new LASTINPUTINFO();
      lastInPut.cbSize = (uint)Marshal.SizeOf(lastInPut);
      GetLastInputInfo(ref lastInPut);

      return TimeSpan.FromMilliseconds(((uint)Environment.TickCount - lastInPut.dwTime));
    }

    /// <summary>
    /// Get the Last input time in milliseconds
    /// </summary>
    static long GetLastInputTime()
    {
      LASTINPUTINFO lastInPut = new LASTINPUTINFO();
      lastInPut.cbSize = (uint)Marshal.SizeOf(lastInPut);
      if (!GetLastInputInfo(ref lastInPut))
      {
        throw new Exception(GetLastError().ToString());
      }
      return lastInPut.dwTime;
    }
  }
}
