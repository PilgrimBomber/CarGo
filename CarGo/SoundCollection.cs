using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace CarGo
{

    public enum SoundType
    {
  
        Car_Accelerate,
        Car_Background,
        Car_Boost,
        Car_Horn,
        Car_Horn2,
        Car_Horn3,
        Enemy_Hit,
        Enemy_Growl,
        FlameThrower,
        RocketLauncher_Explosion,
        RocketLauncher_Launch,
        Shockwave,
        Trap_Launch,
        Menu_Music
    }
    public class SoundCollection
    {
        private SortedList<SoundType,SoundEffect> soundEffect = new SortedList<SoundType,SoundEffect>();
        private static SoundCollection instance;
        private ContentManager content;

        private SoundCollection()
        {

        }
        public void SetContent(ContentManager content)
        {
            this.content = content;
        }

        private void LoadSound(SoundType soundType)
        {
            SoundEffect sound = null;
            switch (soundType)
            {

                case SoundType.Car_Accelerate:
                    sound = content.Load<SoundEffect>("sounds/Car_Accelerate");
                    break;
                case SoundType.Car_Background:
                    sound = content.Load<SoundEffect>("sounds/Car_Background");
                    break;
                case SoundType.Car_Boost:
                    sound = content.Load<SoundEffect>("sounds/Car_Boost");
                    break;
                case SoundType.Car_Horn:
                    sound = content.Load<SoundEffect>("sounds/Car_Horn");
                    break;
                case SoundType.Car_Horn2:
                    sound = content.Load<SoundEffect>("sounds/Car_Horn2");
                    break;
                case SoundType.Car_Horn3:
                    sound = content.Load<SoundEffect>("sounds/Car_Horn3");
                    break;
                case SoundType.Enemy_Hit:
                    sound = content.Load<SoundEffect>("sounds/Enemy_Monster_Hit");
                    break;
                case SoundType.Enemy_Growl:
                    sound = content.Load<SoundEffect>("sounds/Enemy_Growl");
                    break;
                case SoundType.FlameThrower:
                    sound = content.Load<SoundEffect>("sounds/FlameThrower");
                    break;
                case SoundType.RocketLauncher_Launch:
                    sound = content.Load<SoundEffect>("sounds/RocketLauncher_Launch");
                    break;
                case SoundType.RocketLauncher_Explosion:
                    sound = content.Load<SoundEffect>("sounds/RocketLauncher_EXPLOSION!!!");
                    break;
                case SoundType.Shockwave:
                    sound = content.Load<SoundEffect>("sounds/Shockwave");
                    break;
                case SoundType.Trap_Launch:
                    sound = content.Load<SoundEffect>("sounds/Trap_Launch");
                    break;
                case SoundType.Menu_Music:
                    sound = content.Load<SoundEffect>("sounds/Lone_Wolf");
                    break;
            }
            if(!soundEffect.ContainsKey(soundType)) soundEffect.Add(soundType, sound);
        }

        public static SoundCollection Instance
        {
            get
            {
                if (instance == null) instance = new SoundCollection();
                return instance;
            }
        }

        public SoundEffectInstance GetSoundInstance(SoundType soundType)
        {
            if(!soundEffect.ContainsKey(soundType))
            {
                LoadSound(soundType);
            }

            return soundEffect[soundType].CreateInstance();
        }
        

    }
}
