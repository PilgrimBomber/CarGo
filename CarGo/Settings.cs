using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGo
{
    public class Settings
    {
        private static Settings instance=null;
        private float volumeMusic;
        private float volumeSound;
        private Vector2 screenSize;


        private Settings()
        {

        }

        public static Settings Instance
        {
            get
            {
                if (instance == null) instance = new Settings();
                return instance;
            }
        }

        public float VolumeMusic
        {
            get => volumeMusic;
            set
            {
                if (value >= 0 && value <= 1)
                {
                    volumeMusic = value;
                }
            }
        }
        public float VolumeSound
        {
            get => volumeSound;
            set
            {
                if (value >= 0 && value <= 1)
                {
                    volumeSound = value;
                }
            }
        }

        public Vector2 ScreenSize { get => screenSize; set => screenSize = value; }

        public void loadSettings()
        {
            //ToDo: load settings from File.
        }

        public void saveSettings()
        {
            //ToDo: save settings to File.
        }



    }
}
