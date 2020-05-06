using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SampleConfigXF.Services
{
    public interface IAssemblyService
    {
        Assembly GetPlatformAssembly();
    }
}
