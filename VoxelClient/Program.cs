using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voxel;
using Voxel.Networking;

namespace VoxelClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerResolver sr = new ServerResolver(ConfigurationManager.AppSettings["serverLocation"]);
            WakeEngine we = new WakeEngine();





        }
    }
}
