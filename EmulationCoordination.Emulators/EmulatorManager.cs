using EmulationCoordination.Emulators.Emulators;
using EmulationCoordination.Emulators.Interfaces;
using EmulationCoordination.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Emulators
{
    public class EmulatorManager
    {
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

            loadedConfig = FileUtilities.LoadFile<EmulatorManagerConfigDictionary>("EmulatorManager.json");
            foreach (var configuredEmulator in loadedConfig.Keys)
            {
                var matchingEmulator = availableEmulators.Values.Where(f => 
                    f.EmulatorName == configuredEmulator.EmulatorName && 
                    f.Version == configuredEmulator.EmulatorVersion
                ).First();
                matchingEmulator.Installed = loadedConfig[configuredEmulator].Installed;
            }
        }

        public List<IReadOnlyEmulator> GetAvailableEmulators()
        {
            return availableEmulators.Keys.ToList();
        }

        public bool DownloadAndInstallEmulator(IReadOnlyEmulator emulator)
        {
            bool installResult = availableEmulators[emulator].DownloadAndInstall();
            UpdateInstalledProperty(emulator, installResult);

            return installResult;
        }

        public bool DeleteEmulator(IReadOnlyEmulator emulator)
        {
            bool uninstallResult= availableEmulators[emulator].Delete();
            UpdateInstalledProperty(emulator, !uninstallResult);

            return uninstallResult;
        }

        private void UpdateInstalledProperty(IReadOnlyEmulator emulator, Boolean installed)
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
