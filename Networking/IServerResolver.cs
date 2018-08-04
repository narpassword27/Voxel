using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Recognition;

namespace Voxel.Networking
{
    public abstract class IServerResolver
    {
        public event EventHandler<ConnectionEventArgs> ServerConnectionEvent;
        public enum ConnectionStatus { Success, ServerBusy, ServerNotFound }

        protected void SendServerConnectionEvent(object sender, ConnectionEventArgs e) => ServerConnectionEvent(sender, e);
    }
}
