using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
namespace CarGo
{
    public class Settings
    {
        private static Settings instance=null;
        private float volumeMusic;
        private float volumeSound;
        private Vector2 screenSize;
        private Configuration config;

        private Settings()
        {
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

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
                    config.AppSettings.Settings["VolumeMusic"].Value = value.ToString();
                    
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
                    config.AppSettings.Settings["VolumeSound"].Value = value.ToString();
                }
            }
        }

        public Vector2 ScreenSize { get => screenSize; set => screenSize = value; }

        public void loadSettings()
        {
            AppSettingsReader settingsReader = new AppSettingsReader();
            VolumeMusic = Convert.ToSingle( config.AppSettings.Settings["VolumeMusic"].Value);
            VolumeSound = (float)settingsReader.GetValue("VolumeSound", typeof(float));
        }

        public void saveSettings()
        {
            config.Save(ConfigurationSaveMode.Full);
        }



    }
}
