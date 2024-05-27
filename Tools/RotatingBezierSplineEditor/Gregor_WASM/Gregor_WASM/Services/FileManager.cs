using System.Globalization;
using System.Reflection;

namespace Gregor_WASM.Services
{
    public class FileManager
    {
        public static string Root { get => "Gregor_WASM.Properties"; }

        public static byte[] GetBytes(string path)
        {
            var p = Path.Combine(Root, path.Trim('/').Trim('\\')).Replace("/", ".").Replace("\\", ".").Trim('.');
            var names = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            p = names.ToList().Find(n => n.ToLower() == p.ToLower());
            using (var s = Assembly.GetExecutingAssembly().GetManifestResourceStream(p))
            {
                if (s == null)
                {
                    throw new FileNotFoundException($"The file {path} was not found in the embedded resources");
                }
                using (var ms = new MemoryStream())
                {
                    s.CopyTo(ms);
                    return ms.ToArray();
                }
            }
        }
        public static Stream GetStream(string path)
        {
            return new MemoryStream(GetBytes(path));
        }
    }
}
