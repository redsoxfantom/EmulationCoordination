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
    public delegate void NewRomHandler(RomData newData);

    public class RomManager
    {
        private static RomManager mInstance = null;
        private String rootDirectory;
        private Dictionary<string,RomData> loadedRomData;
        private List<RomFileSystemWatcher> romWatchers;
        private ImageConverter imageConverter;
        private ConsoleConverter consoleConverter;

        public event NewRomHandler NewRomAdded;

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
            consoleConverter = new ConsoleConverter();
            romWatchers = new List<RomFileSystemWatcher>();
            
            foreach(var console in EmulatorConsoles.Values)
            {
                String pathToRomDirectory = Path.Combine(rootDirectory, console.FriendlyName);
                Directory.CreateDirectory(pathToRomDirectory);

                foreach (var extension in console.FileExtensions)
                {
                    String extensionFilter = String.Format("*{0}", extension);
                    RomFileSystemWatcher watcher = new RomFileSystemWatcher(pathToRomDirectory,extensionFilter);
                    watcher.associatedConsole = console;
                    watcher.Created += NewRomFound;
                    watcher.EnableRaisingEvents = true;
                    romWatchers.Add(watcher);
                }
            }
            
            loadedRomData = new Dictionary<string, RomData>();
        }

        private void NewRomFound(object sender, FileSystemEventArgs e)
        {
            RomData romData = RetrieveRomData(e.FullPath, ((RomFileSystemWatcher)sender).associatedConsole);
            NewRomAdded?.Invoke(romData);
        }

        public List<RomData> GetRoms(EmulatorConsoles ConsoleToSearch)
        {
            List<RomData> returnList = new List<RomData>();
            String pathToSearch = Path.Combine(rootDirectory, ConsoleToSearch.FriendlyName);
            List<String> acceptableExtensions = ConsoleToSearch.FileExtensions;
            
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

        public Dictionary<EmulatorConsoles,List<RomData>> GetAllRoms()
        {
            Dictionary<EmulatorConsoles, List<RomData>> returnVal = new Dictionary<EmulatorConsoles, List<RomData>>();

            foreach(var console in EmulatorConsoles.Values)
            {
                var consoleData = GetRoms(console);
                if(consoleData.Count > 0)
                {
                    returnVal.Add(console, consoleData);
                }
            }

            return returnVal;
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

            String relativePathToConfig = Path.Combine("Games", data.Console.FriendlyName,
                    String.Format("{0}.data.json", Path.GetFileNameWithoutExtension(data.Path)));
            FileUtilities.WriteFile(data, relativePathToConfig, imageConverter);
        }

        private RomData RetrieveRomData(String file, EmulatorConsoles ConsoleToSearch)
        {
            String possiblePathToConfig = Path.Combine(FileUtilities.GetRootDirectory(),
                                                       "Games",
                                                       ConsoleToSearch.FriendlyName,
                                                       String.Format("{0}.data.json", Path.GetFileNameWithoutExtension(file)));

            if(loadedRomData.ContainsKey(file))
            {
                return loadedRomData[file];
            }
            else if(File.Exists(possiblePathToConfig))
            {
                String relativePathToConfig = Path.Combine("Games", ConsoleToSearch.FriendlyName, 
                    String.Format("{0}.data.json", Path.GetFileNameWithoutExtension(file)));

                RomData loadedData = FileUtilities.LoadFile<RomData>(relativePathToConfig, imageConverter,consoleConverter);
                loadedRomData.Add(loadedData.Path, loadedData);
                return loadedData;
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
                    Description = "No Description",
                    Developer = "Unknown",
                    Publisher = "Unknown",
                    Rating = 0.0f,
                    BoxArt = Resource.DefaultBoxart,
                    Logo = Resource.DefaultIcon,
                    Banner = Resource.DefaultBanner,
                    Background = Resource.DefaultBackground
                };
            }
        }
    }

    class RomFileSystemWatcher : FileSystemWatcher
    {
        public RomFileSystemWatcher()
        {
        }

        public RomFileSystemWatcher(string path) : base(path)
        {
        }

        public RomFileSystemWatcher(string path, string filter) : base(path, filter)
        {
        }

        public EmulatorConsoles associatedConsole { get; set; }
    }
}
