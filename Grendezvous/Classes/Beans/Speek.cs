using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;

namespace Grendezvous.Classes.Beans
{
    class Speek
    {
        SpeechSynthesizer sps = new SpeechSynthesizer();
        public void speek(String message)
        {
            sps.SpeakAsync(message);
        }
    }
}
