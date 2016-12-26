using EmulationCoordination.Roms.DataContracts;
using EmulationCoordination.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Roms
{
    public class RomManager
    {
        private static RomManager mInstance = null;
        private String rootDirectory;
        private RomManagerConfig loadedConfig;
        private RomDataConfig loadedRomData;
        private ImageConverter imageConverter;

        public static RomManager Instance
        {
            get
            {
                if(mInstance == null)
                {
                    mInstance = new RomManager();
                }

                return mInstance;
            }
        }

        private RomManager()
        {
            rootDirectory = Path.Combine(FileUtilities.GetRootDirectory(), "Games");
            imageConverter = new ImageConverter();
            
            foreach(var console in EmulatorConsoles.Values)
            {
                Directory.CreateDirectory(Path.Combine(rootDirectory, console.FriendlyName));
            }

            loadedConfig = FileUtilities.LoadFile<RomManagerConfig>("RomManager.json");
            loadedRomData = FileUtilities.LoadFile<RomDataConfig>("RomData.json", imageConverter);
        }

        public List<RomData> GetRoms(EmulatorConsoles ConsoleToSearch)
        {
            List<RomData> returnList = new List<RomData>();
            String pathToSearch = Path.Combine(rootDirectory, ConsoleToSearch.FriendlyName);
            List<String> acceptableExtensions;
            if(!loadedConfig.TryGetValue(ConsoleToSearch.FriendlyName,out acceptableExtensions))
            {
                acceptableExtensions = new List<string>();
            }
            
            foreach(var file in Directory.EnumerateFiles(pathToSearch))
            {
                String fileExtension = Path.GetExtension(file);
                if (acceptableExtensions.Contains(fileExtension))
                {
                    RomData data = RetrieveRomData(file, ConsoleToSearch);
                    returnList.Add(data);
                }
            }

            return returnList;
        }

        public void UpdateRomData(RomData data)
        {
            if(loadedRomData.ContainsKey(data.Path))
            {
                loadedRomData[data.Path] = data;
            }
            else
            {
                loadedRomData.Add(data.Path, data);
            }

            FileUtilities.WriteFile(loadedRomData, "RomData.json", imageConverter);
        }

        private RomData RetrieveRomData(String file, EmulatorConsoles ConsoleToSearch)
        {
            if(loadedRomData.ContainsKey(file))
            {
                return loadedRomData[file];
            }
            else
            {
                return new RomData()
                {
                    Path = file,
                    FriendlyName = Path.GetFileName(file),
                    Console = ConsoleToSearch,
                    IsUpToDate = false,
                    NumPlayers = "Unknown",
                    Description = "None",
                    Developer = "Unknown",
                    Publisher = "Unknown",
                    Rating = 0.0f
                };
            }
        }
    }
}
