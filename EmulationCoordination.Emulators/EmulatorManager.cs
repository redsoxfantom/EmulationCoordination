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
        private OperatingSystem currentOs;
        private Dictionary<IReadOnlyEmulator, IEmulator> availableEmulators;
        private Dictionary<EmulatorManagerConfigKey, EmulatorManagerConfig> loadedConfig;

        private EmulatorManager()
        {
            currentOs = Environment.OSVersion;
        }

        private void Initialize()
        {
            if(currentOs.Platform == PlatformID.Win32NT)
            {
                availableEmulators = EmulatorRetriever.GetWindowsEmulators();
            }
            else
            {
                availableEmulators = new Dictionary<IReadOnlyEmulator, IEmulator>();
            }

            EmulatorInstallDir = Path.Combine(FileUtilities.GetRootDirectory(),"Emulators");

            loadedConfig = FileUtilities.LoadFile<EmulatorManagerConfigDictionary>("EmulatorManager.json",new ConsoleConverter());
            foreach (var configuredEmulator in loadedConfig.Keys)
            {
                if (configuredEmulator.EmulatorType == EmulatorType.BUILTIN)
                {
                    var matchingEmulator = availableEmulators.Values.Where(f =>
                        f.EmulatorName == configuredEmulator.EmulatorName &&
                        f.Version == configuredEmulator.EmulatorVersion
                    ).First();
                    matchingEmulator.Installed = loadedConfig[configuredEmulator].Installed;
                }
                if(configuredEmulator.EmulatorType == EmulatorType.CUSTOM)
                {
                    var emuCfg = loadedConfig[configuredEmulator].CustomConfig;
                    CustomEmulator emu = new CustomEmulator(emuCfg.PathToExecutable, emuCfg.CommandLineArgs, 
                        configuredEmulator.EmulatorVersion, configuredEmulator.EmulatorName, emuCfg.Consoles);
                    availableEmulators.Add(emu,emu);
                }
            }
            
            foreach(var availableEmulator in availableEmulators.Values.Where(f=>f.EmulatorType == EmulatorType.BUILTIN))
            {
                String emulatorSpecificInstallDir = Path.Combine(EmulatorInstallDir, 
                    availableEmulator.EmulatorName, availableEmulator.Version);
                availableEmulator.InstallDirectory = emulatorSpecificInstallDir;
            }

            romMgr = RomManager.Instance;
        }

        public List<IReadOnlyEmulator> GetAvailableEmulators()
        {
            return availableEmulators.Keys.ToList();
        }

        private void RemoveCustomEmulator(IReadOnlyEmulator emulator)
        {
            EmulatorManagerConfigKey key = new EmulatorManagerConfigKey()
            {
                EmulatorName = emulator.EmulatorName,
                EmulatorVersion = emulator.Version,
                EmulatorType = emulator.EmulatorType
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
                EmulatorVersion = emulator.Version,
                EmulatorType = emulator.EmulatorType
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

        public bool DownloadAndInstallEmulator(IReadOnlyEmulator emulator)
        {
            bool installResult = availableEmulators[emulator].DownloadAndInstall();
            UpdateConfigProperty(emulator, installResult);

            return installResult;
        }

        public bool DeleteEmulator(IReadOnlyEmulator emulator)
        {
            bool uninstallResult = false;
            if (emulator.EmulatorType == EmulatorType.BUILTIN)
            {
                uninstallResult = availableEmulators[emulator].Delete();
                UpdateConfigProperty(emulator, !uninstallResult);
            }
            else
            {
                RemoveCustomEmulator(emulator);
                uninstallResult = true;
            }
            return uninstallResult;
        }

        public void RunEmulator(IReadOnlyEmulator emulator, RomData rom)
        {
            IEmulator emu = availableEmulators[emulator];
            if (emu.Installed)
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
                throw new EmulatorManagerException(String.Format("The selected emulator {0} has not been installed",emulator.EmulatorName));
            }
        }

        private void UpdateConfigProperty(IReadOnlyEmulator emulator, Boolean installed)
        {
            EmulatorManagerConfig cfg;
            var key = new EmulatorManagerConfigKey()
            {
                EmulatorName = emulator.EmulatorName,
                EmulatorVersion = emulator.Version,
                EmulatorType = emulator.EmulatorType
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
            FileUtilities.WriteFile(loadedConfig, "EmulatorManager.json");
        }
    }
}
