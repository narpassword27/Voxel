
using System.Speech.Recognition;

namespace Voxel.Modules
{
    interface ISkill
    {
        Grammar GetGrammar();
        void SpeechRecognized(object sender, SpeechRecognizedEventArgs e);
    }
}
