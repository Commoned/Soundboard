using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobbieRotten.Model
{
    public class Sound
    {
        public string Name { get; set; }
        public SoundCategory Category { get; set; }
        public string AudioFile { get; set; }
        public string ImageFile { get; set; }

        public Sound(string name, SoundCategory category)
        {
            Name = name;
            Category = category;
            AudioFile = String.Format("/Assets/Audio/{0}/{1}.wav", category, name);
            ImageFile = String.Format("/Assets/Icons/{0}.png", category);
        }
    }
    public enum SoundCategory
    {
        Dank,
        Earrape,
        Games,
        Overwatch,
        Sovereign
    }
}
