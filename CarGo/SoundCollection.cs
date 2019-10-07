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
            switch (soundType)
            {

                case SoundType.Car_Accelerate:
                    soundEffect.Add(SoundType.Car_Accelerate, content.Load<SoundEffect>("sounds/Car_Accelerate"));
                    break;
                case SoundType.Car_Background:
                    soundEffect.Add(SoundType.Car_Background, content.Load<SoundEffect>("sounds/Car_Background"));
                    break;
                case SoundType.Car_Boost:
                    soundEffect.Add(SoundType.Car_Boost, content.Load<SoundEffect>("sounds/Car_Boost"));
                    break;
                case SoundType.Car_Horn:
                    soundEffect.Add(SoundType.Car_Horn, content.Load<SoundEffect>("sounds/Car_Horn"));
                    break;
                case SoundType.Car_Horn2:
                    soundEffect.Add(SoundType.Car_Horn2, content.Load<SoundEffect>("sounds/Car_Horn2"));
                    break;
                case SoundType.Car_Horn3:
                    soundEffect.Add(SoundType.Car_Horn3, content.Load<SoundEffect>("sounds/Car_Horn3"));
                    break;
                case SoundType.Enemy_Hit:
                    soundEffect.Add(SoundType.Enemy_Hit, content.Load<SoundEffect>("sounds/Enemy_Monster_Hit"));
                    break;
                case SoundType.Enemy_Growl:
                    soundEffect.Add(SoundType.Enemy_Growl, content.Load<SoundEffect>("sounds/Enemy_Growl"));
                    break;
                case SoundType.FlameThrower:
                    soundEffect.Add(SoundType.FlameThrower, content.Load<SoundEffect>("sounds/FlameThrower"));
                    break;
                case SoundType.RocketLauncher_Launch:
                    soundEffect.Add(SoundType.RocketLauncher_Launch, content.Load<SoundEffect>("sounds/RocketLauncher_Launch"));
                    break;
                case SoundType.RocketLauncher_Explosion:
                    soundEffect.Add(SoundType.RocketLauncher_Explosion, content.Load<SoundEffect>("sounds/RocketLauncher_EXPLOSION!!!"));
                    break;
                case SoundType.Shockwave:
                    soundEffect.Add(SoundType.Shockwave, content.Load<SoundEffect>("sounds/Shockwave"));
                    break;
                case SoundType.Trap_Launch:
                    soundEffect.Add(SoundType.Trap_Launch, content.Load<SoundEffect>("sounds/Trap_Launch"));
                    break;
                case SoundType.Menu_Music:
                    soundEffect.Add(SoundType.Menu_Music, content.Load<SoundEffect>("sounds/Lone_Wolf"));
                    break;
            }
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
