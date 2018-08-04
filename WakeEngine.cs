using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Speech.Recognition;

namespace Voxel
{
    public class WakeEngine
    {
        private SpeechRecognitionEngine _wake;
        private SpeechRecognitionEngine _content;

        public WakeEngine()
        {
            _wake.SetInputToDefaultAudioDevice();
            _content.SetInputToDefaultAudioDevice();

            _wake.LoadGrammarAsync(new Grammar(new GrammarBuilder("voxel")) { Name = "Wake" });
            _wake.SpeechRecognized += Wake_SpeechRecognized;
            _wake.InitialSilenceTimeout = TimeSpan.FromSeconds(0);
            _wake.BabbleTimeout = TimeSpan.FromSeconds(0);

            _content.LoadGrammarAsync(new DictationGrammar());
            _content.SpeechRecognized += Wake_SpeechRecognized;
            _content.InitialSilenceTimeout = TimeSpan.FromSeconds(3);
            _content.BabbleTimeout = TimeSpan.FromSeconds(10);
            _content.EndSilenceTimeout = TimeSpan.FromSeconds(2);

            _wake.RecognizeAsync(RecognizeMode.Single);
        }

        private void Wake_SpeechRecognized(object sender, SpeechRecognizedEventArgs e) => _content.RecognizeAsync(RecognizeMode.Single);
        private void Content_SpeechRecognized(object sender, SpeechRecognizedEventArgs e) => _wake.RecognizeAsync(RecognizeMode.Single);

        public void AddListener(EventHandler<SpeechRecognizedEventArgs> handler) => _content.SpeechRecognized += handler;
    }
}
