using EmulationCoordination.Emulators.Emulators;
using EmulationCoordination.Emulators.Interfaces;
using EmulationCoordination.Roms;
using EmulationCoordination.Utilities;
using System;
using System.Collections.Generic;
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

            loadedConfig = FileUtilities.LoadFile<EmulatorManagerConfigDictionary>("EmulatorManager.json");
            foreach (var configuredEmulator in loadedConfig.Keys)
            {
                var matchingEmulator = availableEmulators.Values.Where(f => 
                    f.EmulatorName == configuredEmulator.EmulatorName && 
                    f.Version == configuredEmulator.EmulatorVersion
                ).First();
                matchingEmulator.Installed = loadedConfig[configuredEmulator].Installed;
            }

            foreach(var availableEmulator in availableEmulators.Values)
            {
                String emulatorSpecificInstallDir = Path.Combine(EmulatorInstallDir, 
                    availableEmulator.EmulatorName, availableEmulator.Version);
                availableEmulator.InstallDirectory = emulatorSpecificInstallDir;
            }
        }

        public List<IReadOnlyEmulator> GetAvailableEmulators()
        {
            return availableEmulators.Keys.ToList();
        }

        public bool DownloadAndInstallEmulator(IReadOnlyEmulator emulator)
        {
            bool installResult = availableEmulators[emulator].DownloadAndInstall();
            UpdateConfigProperty(emulator, installResult);

            return installResult;
        }

        public bool DeleteEmulator(IReadOnlyEmulator emulator)
        {
            bool uninstallResult= availableEmulators[emulator].Delete();
            UpdateConfigProperty(emulator, !uninstallResult);

            return uninstallResult;
        }

        public void RunEmulator(IReadOnlyEmulator emulator, RomData rom)
        {
            IEmulator emu = availableEmulators[emulator];
            if (emu.Installed)
            {
                emu.ExecuteRom(rom);
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
            FileUtilities.WriteFile(loadedConfig, "EmulatorManager.json");
        }
    }
}
