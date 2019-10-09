﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
namespace CarGo
{
    public enum Difficulty {
    Easy = 1,
    Normal = 2,
    Hard =5};

    public class Settings
    {
        private static Settings instance=null;
        private float volumeMusic;
        private float volumeSound;
        private Difficulty difficulty;
        private Vector2 screenSize;
        private Configuration config;

        private Settings()
        {
            loadSettings();
            //config = ConfigurationManager.OpenMachineConfiguration();

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
        public Difficulty Difficulty { get => difficulty; set => difficulty = value; }

        public void loadSettings()
        {
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            VolumeMusic = Convert.ToSingle( config.AppSettings.Settings["VolumeMusic"].Value);
            VolumeSound = Convert.ToSingle(config.AppSettings.Settings["VolumeSound"].Value);
            switch(Convert.ToInt32(config.AppSettings.Settings["Difficulty"].Value))
            {
                case 1: difficulty = Difficulty.Easy;
                    break;
                case 2: difficulty = Difficulty.Normal;
                    break;
                case 3: difficulty = Difficulty.Hard;
                    break;
                default:
                    difficulty = Difficulty.Normal;
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void saveSettings()
        {
            config.Save(ConfigurationSaveMode.Full);
        }
    }
}
