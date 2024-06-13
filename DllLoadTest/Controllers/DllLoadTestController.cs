using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
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
            IntPtr fanSelectDllHandle = LoadLibrary(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FanSelectDll", "FANselect.dll"));
            string result = fanSelectDllHandle != IntPtr.Zero ? $"Dll handle: {fanSelectDllHandle}" : $"Error: {Marshal.GetLastWin32Error()}";
            return result;
        }

        [DllImport("Kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern IntPtr LoadLibrary(string lpLibFileName);
    }
}
