using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.Helpers
{
    public static class Resource
    {
        public static void WriteResourceToFile(Type t, string resourceName, string fileName)
        {
            using (var resource = t.Assembly.GetManifestResourceStream(resourceName))
            {
                if (resource != null)
                {
                    using (var file = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                    {
                        resource.CopyTo(file);
                    }
                }
            }
        }
    }
}
