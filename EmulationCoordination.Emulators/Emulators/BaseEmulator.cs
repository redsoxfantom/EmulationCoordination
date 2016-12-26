﻿using EmulationCoordination.Emulators.Interfaces;
using EmulationCoordination.Roms;
using EmulationCoordination.Utilities;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Emulators.Emulators
{
    public abstract class BaseEmulator : IEmulator
    {
        public virtual List<EmulatorConsoles> ConsoleNames => new List<EmulatorConsoles>();

        public virtual string EmulatorName => String.Empty;

        public bool Installed { get; set; }

        public virtual string Version => String.Empty;
        
        public string InstallDirectory { get; set; }

        public override bool Equals(object obj)
        {
            IReadOnlyEmulator other = obj as IReadOnlyEmulator;
            if(other != null)
            {
                return (other.EmulatorName == EmulatorName &&
                        other.Version == Version);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return EmulatorName.GetHashCode() ^ Version.GetHashCode();
        }

        public bool Delete()
        {
            if (!Installed)
            {
                return false;
            }

            bool deletionSuccessful = ChildSpecificDelete();
            if(deletionSuccessful)
            {
                Installed = false;
            }
            else
            {
                Installed = true;
            }

            return deletionSuccessful;
        }

        protected abstract bool ChildSpecificDelete();

        public bool DownloadAndInstall()
        {
            if (Installed)
            {
                return true;
            }

            Directory.CreateDirectory(InstallDirectory);
            bool installationSuccessful = ChildSpecificInstall();
            if(installationSuccessful)
            {
                Installed = true;
            }
            else
            {
                Installed = false;
            }

            return Installed;
        }

        protected abstract bool ChildSpecificInstall();

        public void ExecuteRom(IRomData rom)
        {
            String commandLine = CreateCommandLine(rom);
            Process.Start(commandLine);
        }

        protected abstract string CreateCommandLine(IRomData rom);

        protected bool BasicDownloadAndUnzip(String downloadUrl)
        {
            try
            {
                String targetFile = Path.Combine(InstallDirectory, "download.zip");
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(downloadUrl, targetFile);
                }
                using (ZipFile file = ZipFile.Read(targetFile))
                {
                    file.ExtractAll(InstallDirectory);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected bool BasicDelete()
        {
            try
            {
                Directory.Delete(InstallDirectory, true);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
