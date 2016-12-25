using EmulationCoordination.Emulators.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Emulators
{
    public class EmulatorManagerConfig
    {
        public bool Installed { get; set; }
    }

    public class EmulatorManagerConfigKey
    {
        public String EmulatorName { get; set; }
        public String EmulatorVersion { get; set; }

        public override bool Equals(object obj)
        {
            EmulatorManagerConfigKey other = obj as EmulatorManagerConfigKey;
            if (other != null)
            {
                return (other.EmulatorName == EmulatorName &&
                        other.EmulatorVersion == EmulatorVersion);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return EmulatorName.GetHashCode() ^ EmulatorVersion.GetHashCode();
        }
    }

    [JsonArray]
    public class EmulatorManagerConfigDictionary: Dictionary<EmulatorManagerConfigKey, EmulatorManagerConfig>
    {
    }
}
