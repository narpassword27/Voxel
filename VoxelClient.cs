using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Speech.Recognition;
using Voxel;
using Voxel.Networking;

namespace Voxel
{
    public class VoxelClient
    {
        private IServerResolver _sr;
        private WakeEngine _we;

        public VoxelClient(IServerResolver sr, WakeEngine we)
        {
            _sr = sr;
            _we = we;

            we.AddListener(VoiceContentDetected);
        }

        public void VoiceContentDetected(object sender, SpeechRecognizedEventArgs e)
        {
            var temp = e.Result.Audio;
            



        }
    }
}
