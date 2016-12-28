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

        }
    }
}
