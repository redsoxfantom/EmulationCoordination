using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Utilities
{
    public class FileUtilities
    {

        public static String GetRootDirectory()
        {
            String root = Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            root = Path.Combine(root, "EmulationCoordination");

            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

            return root;
        }

        public static T LoadFile<T>(String pathToFile) where T:new()
        {
            String fullyQualifiedPath = Path.Combine(GetRootDirectory(), pathToFile);

            if(!File.Exists(fullyQualifiedPath))
            {
                T obj = new T();
                WriteFile(obj, fullyQualifiedPath);
            }

            String fileText = File.ReadAllText(fullyQualifiedPath);
            return JsonConvert.DeserializeObject<T>(fileText);
        }

        public static void WriteFile(Object obj, String pathToFile)
        {
            String fullyQualifiedPath = Path.Combine(GetRootDirectory(), pathToFile);

            String serializedObj = JsonConvert.SerializeObject(obj, Formatting.Indented);
            File.WriteAllText(fullyQualifiedPath, serializedObj);
        }
    }
}
