using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Speech.Recognition;
using System.Reflection;

namespace Voxel
{
    public class RecognitionEngine
    {
        private SpeechRecognitionEngine _wake;
        private SpeechRecognitionEngine _commands;
        public enum EngineType { Client, Server }

        public RecognitionEngine(EngineType type)
        {
            _wake = new SpeechRecognitionEngine();
            _commands = new SpeechRecognitionEngine();
        }

        private void Start()
        {
            _wake.SetInputToDefaultAudioDevice();
            _commands.SetInputToDefaultAudioDevice();

            var skills = Assembly.GetCallingAssembly().GetTypes()
                .Where(t => !t.IsInterface && t.FullName.StartsWith("Voxel.Modules") && !t.Attributes.HasFlag(TypeAttributes.NestedPrivate | TypeAttributes.Sealed))
                .Select(t => new
                {
                    Instance = t.GetConstructors().First().Invoke(new object[] { }) as Modules.ISkill,
                    Name = t.Name
                })
                .Select(skill =>
                {
                    Grammar skillGrammar = skill.Instance.GetGrammar();
                    skillGrammar.Name = skill.Name;
                    skillGrammar.SpeechRecognized += skill.Instance.SpeechRecognized;
                    _commands.LoadGrammar(skillGrammar);
                    return skill;
                })
                .ToList();
        }
    }
}
