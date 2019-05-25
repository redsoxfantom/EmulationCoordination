using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmulationCoordination.Utilities
{
    public static class FileUtilities
    {
        public static string ExecutingDirectory {get;private set;}
        
        static FileUtilities()
        {
            ExecutingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

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

        public static T LoadFile<T>(String pathToFile, params JsonConverter[] converters) where T:new()
        {
            String fullyQualifiedPath = Path.Combine(GetRootDirectory(), pathToFile);

            if(!File.Exists(fullyQualifiedPath))
            {
                T obj = new T();
                WriteFile(obj, fullyQualifiedPath, converters);
            }

            String fileText = File.ReadAllText(fullyQualifiedPath);
            return JsonConvert.DeserializeObject<T>(fileText,converters);
        }

        public static void WriteFile(Object obj, String pathToFile, params JsonConverter[] converters)
        {
            String fullyQualifiedPath = Path.Combine(GetRootDirectory(), pathToFile);

            String serializedObj = JsonConvert.SerializeObject(obj, Formatting.Indented,converters);
            File.WriteAllText(fullyQualifiedPath, serializedObj);
        }

        public static string UseFilePicker(FilePickerType type, string pickerTitle = null, string extensionFilter = null)
        {
            string selectedPath = null;
            FileDialog dialog = null;

            switch (type)
            {
                case FilePickerType.LOAD:
                    dialog = new OpenFileDialog();
                    break;
                case FilePickerType.SAVE:
                    dialog = new SaveFileDialog();
                    break;
            }

            if (pickerTitle != null)
            {
                dialog.Title = pickerTitle;
            }
            if (extensionFilter != null)
            {
                dialog.Filter = extensionFilter;
            }
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                selectedPath = dialog.FileName;
            }

            dialog.Dispose();

            return selectedPath;
        }

        public enum FilePickerType
        {
            SAVE,
            LOAD
        }
    }
}
