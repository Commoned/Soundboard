using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobbieRotten.Model
{
    public class SoundManager
    {
        public static void GetAllSounds(ObservableCollection<Sound> sounds)
        {
            var allsounds = getSounds();
            sounds.Clear();
            allsounds.ForEach(p => sounds.Add(p));
        }

        public static void GetSoundsByCategory(ObservableCollection<Sound> sounds, SoundCategory soundCategory)
        {
            var allSounds = getSounds();
            var filteredSounds = allSounds.Where(p => p.Category == soundCategory).ToList();
            sounds.Clear();
            filteredSounds.ForEach(p => sounds.Add(p));
        }

        public static void GetSoundsByName(ObservableCollection<Sound> sounds, string name)
        {
            var allSounds = getSounds();
            var filteredSounds = allSounds.Where(p => p.Name == name).ToList();
            sounds.Clear();
            filteredSounds.ForEach(p => sounds.Add(p));
        }

        private static List<Sound> getSounds()
        {
            var sounds = new List<Sound>();

            
            //***************************************************************************************************************
            string[] dank = Directory.GetFiles("Assets/Audio/Dank").Select(file =>
            Path.GetFileNameWithoutExtension(file)).ToArray();
            foreach (string fileName in dank)
                sounds.Add(new Sound(fileName, SoundCategory.Dank));
            //***************************************************************************************************************
            string[] earrape = Directory.GetFiles("Assets/Audio/Earrape").Select(file =>
            Path.GetFileNameWithoutExtension(file)).ToArray();
            foreach (string fileName in earrape)
                sounds.Add(new Sound(fileName, SoundCategory.Earrape));
            //***************************************************************************************************************
            string[] games = Directory.GetFiles("Assets/Audio/Games").Select(file =>
            Path.GetFileNameWithoutExtension(file)).ToArray();
            foreach (string fileName in games)
                sounds.Add(new Sound(fileName, SoundCategory.Games));
            //***************************************************************************************************************
            string[] overwatch = Directory.GetFiles("Assets/Audio/Overwatch").Select(file =>
            Path.GetFileNameWithoutExtension(file)).ToArray();
            foreach (string fileName in overwatch)
                sounds.Add(new Sound(fileName, SoundCategory.Overwatch));
            //***************************************************************************************************************
            string[] sovereign = Directory.GetFiles("Assets/Audio/Sovereign").Select(file =>
            Path.GetFileNameWithoutExtension(file)).ToArray();
            foreach (string fileName in sovereign)
                sounds.Add(new Sound(fileName, SoundCategory.Sovereign));
            //***************************************************************************************************************
            string[] dwarf = Directory.GetFiles("Assets/Audio/Dwarf").Select(file =>
            Path.GetFileNameWithoutExtension(file)).ToArray();
            foreach (string fileName in dwarf)
                sounds.Add(new Sound(fileName, SoundCategory.Dwarf));
            //***************************************************************************************************************
            string[] arnold = Directory.GetFiles("Assets/Audio/Arnold").Select(file =>
            Path.GetFileNameWithoutExtension(file)).ToArray();
            foreach (string fileName in arnold)
                sounds.Add(new Sound(fileName, SoundCategory.Arnold));


            return sounds;
        }
        
        
    }
}
