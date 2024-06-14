using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace DllLoadTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DllLoadTestController(ILogger<DllLoadTestController> logger) : ControllerBase
    {
        private readonly ILogger<DllLoadTestController> _logger = logger;

        [HttpGet(Name = "LoadFanSelectDll")]
        public string GetFanSelect()
        {
            string dllPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FanSelectDll", "FANselect.dll");
            string result;
            if (System.IO.File.Exists(dllPath))
            {
                IntPtr fanSelectDllHandle = LoadLibrary(dllPath);
                result = fanSelectDllHandle != IntPtr.Zero ? $"Dll handle: {fanSelectDllHandle}" : $"Error: {Marshal.GetLastWin32Error()}";
            }
            else
            {
                result = "Error: dll file does not exist.";
            }
            return result;
        }

        [DllImport("Kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern IntPtr LoadLibrary(string lpLibFileName);
    }
}
