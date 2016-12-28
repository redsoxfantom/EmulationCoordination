using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationCoordination.Controllers
{
    public class ControllerManager
    {
        private static ControllerManager mInstance = null;

        public static ControllerManager Instance
        {
            get
            {
                if(mInstance == null)
                {
                    mInstance = new ControllerManager();
                }
                return mInstance;
            }
        }

        private ControllerManager()
        {
            var state = GamePad.GetState(0);
            if(state.IsConnected)
            {
                string name = GamePad.GetName(0);
                var capabilities = GamePad.GetCapabilities(0);
            }
        }
    }
}
