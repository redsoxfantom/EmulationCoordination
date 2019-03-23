using EmulationCoordination.Emulators.Emulators;
using EmulationCoordination.Emulators.Interfaces;
using EmulationCoordination.Roms;
using EmulationCoordination.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Emulators
{
    public class EmulatorManager
    {
        private String EmulatorInstallDir;

        public static EmulatorManager Instance
        {
            get
            {
                if(mInstance == null)
                {
                    mInstance = new EmulatorManager();
                    mInstance.Initialize();
                }
                return mInstance;
            }
        }

        private static EmulatorManager mInstance = null;
        private static RomManager romMgr;
        private Dictionary<IReadOnlyEmulator, IEmulator> availableEmulators;
        private Dictionary<EmulatorManagerConfigKey, EmulatorManagerConfig> loadedConfig;
        private List<IKnownEmulator> knownEmulatorTypes;

        private EmulatorManager()
        {
            currentOs = Environment.OSVersion;
        }

        private void Initialize()
        {
            availableEmulators = new Dictionary<IReadOnlyEmulator, IEmulator>();

            EmulatorInstallDir = Path.Combine(FileUtilities.GetRootDirectory(),"Emulators");

            loadedConfig = FileUtilities.LoadFile<EmulatorManagerConfigDictionary>("EmulatorManager.json",new ConsoleConverter());
            foreach (var configuredEmulator in loadedConfig.Keys)
            {
                var emuCfg = loadedConfig[configuredEmulator].CustomConfig;
                CustomEmulator emu = new CustomEmulator(emuCfg.PathToExecutable, emuCfg.CommandLineArgs, 
                    configuredEmulator.EmulatorVersion, configuredEmulator.EmulatorName, emuCfg.Consoles);
                availableEmulators.Add(emu,emu);
            }

            romMgr = RomManager.Instance;
        }

        public List<IReadOnlyEmulator> GetAvailableEmulators()
        {
            return availableEmulators.Keys.ToList();
        }

        public List<IReadOnlyEmulator> GetAvailableEmulators(EmulatorConsoles console)
        {
            var allEmulators = GetAvailableEmulators();
            var emulatorsForConsole = allEmulators.Where(f => f.ConsoleNames.Contains(console)).ToList();
            return emulatorsForConsole;
        }

        private void RemoveCustomEmulator(IReadOnlyEmulator emulator)
        {
            EmulatorManagerConfigKey key = new EmulatorManagerConfigKey()
            {
                EmulatorName = emulator.EmulatorName,
                EmulatorVersion = emulator.Version
            };
            if(loadedConfig.ContainsKey(key))
            {
                loadedConfig.Remove(key);
            }
            if(availableEmulators.ContainsKey(emulator))
            {
                availableEmulators.Remove(emulator);
            }
            UpdateConfiguration();
        }

        public void AddCustomEmulator(CustomEmulator emulator)
        {
            EmulatorManagerConfigKey key = new EmulatorManagerConfigKey()
            {
                EmulatorName = emulator.EmulatorName,
                EmulatorVersion = emulator.Version
            };
            EmulatorManagerConfig cfg;
            if(!loadedConfig.TryGetValue(key, out cfg))
            {
                cfg = new EmulatorManagerConfig()
                {
                    Installed = true,
                    CustomConfig = new CustomEmulatorConfig()
                    {
                        PathToExecutable = emulator.PathToExecutable,
                        CommandLineArgs = emulator.CommandLineArguments,
                        Consoles = emulator.ConsoleNames
                    }
                };
                loadedConfig.Add(key, cfg);
            }

            if(!availableEmulators.ContainsKey(emulator))
            {
                availableEmulators.Add(emulator, emulator);
            }
            else
            {
                availableEmulators[emulator] = emulator;
            }

            UpdateConfiguration();
        }

        public bool DeleteEmulator(IReadOnlyEmulator emulator)
        {
            RemoveCustomEmulator(emulator);
            return true;
        }

        public void RunEmulator(RomData rom)
        {
            var romConsole = rom.Console;
            var emulatorsForConsole = GetAvailableEmulators(romConsole);
            var selectedEmulator = emulatorsForConsole.First();
            RunEmulator(selectedEmulator, rom);
        }

        public void RunEmulator(IReadOnlyEmulator emulator, RomData rom)
        {
            IEmulator emu;
            if (availableEmulators.TryGetValue(emulator, out emu))
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                emu.ExecuteRom(rom);
                timer.Stop();
                rom.TimePlayed += timer.Elapsed;
                romMgr.UpdateRomData(rom);
            }
            else
            {
                throw new EmulatorManagerException(String.Format("The selected emulator {0} has not been installed", emulator.EmulatorName));
            }
        }

        private void UpdateConfigProperty(IReadOnlyEmulator emulator, Boolean installed)
        {
            EmulatorManagerConfig cfg;
            var key = new EmulatorManagerConfigKey()
            {
                EmulatorName = emulator.EmulatorName,
                EmulatorVersion = emulator.Version
            };
            if (!loadedConfig.TryGetValue(key, out cfg))
            {
                cfg = new EmulatorManagerConfig();
                loadedConfig.Add(key, cfg);
            }
            cfg.Installed = installed;

            UpdateConfiguration();
        }

        private void UpdateConfiguration()
        {
            FileUtilities.WriteFile(loadedConfig, "EmulatorManager.json",new ConsoleConverter());
        }
    }
}
