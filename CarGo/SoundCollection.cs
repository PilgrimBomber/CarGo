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
        Enemy_Hit,
        Enemy_Growl,
        RocketLauncher_Explosion,
        RocketLauncher_Launch,
        Shockwave    
    }
    public class SoundCollection
    {
        private List<SoundEffect> soundEffect = new List<SoundEffect>();
        private static SoundCollection instance;

        private SoundCollection()
        {

        }

        public void LoadSounds(ContentManager content)
        {
            foreach (SoundType soundType in Enum.GetValues(typeof (SoundType)).Cast<SoundType>().ToList<SoundType>())
            {
                switch (soundType)
                {
                
                    case SoundType.Car_Accelerate:
                        soundEffect.Add(content.Load<SoundEffect>("sounds/Car_Accelerate"));
                        break;
                    case SoundType.Car_Background:
                        soundEffect.Add(content.Load<SoundEffect>("sounds/Car_Background"));
                        break;
                    case SoundType.Car_Boost:
                        soundEffect.Add(content.Load<SoundEffect>("sounds/Car_Boost"));
                        break;
                    case SoundType.Car_Horn:
                        soundEffect.Add(content.Load<SoundEffect>("sounds/Car_Horn"));
                        break;
                    case SoundType.Car_Horn2:
                        soundEffect.Add(content.Load<SoundEffect>("sounds/Car_Horn2"));
                        break;
                    case SoundType.Enemy_Hit:
                        soundEffect.Add(content.Load<SoundEffect>("sounds/Enemy_Monster_Hit"));
                        break;
                    case SoundType.Enemy_Growl:
                        soundEffect.Add(content.Load<SoundEffect>("sounds/Enemy_Growl"));
                        break;
                    case SoundType.RocketLauncher_Launch:
                        soundEffect.Add(content.Load<SoundEffect>("sounds/RocketLauncher_Launch"));
                        break;
                    case SoundType.RocketLauncher_Explosion:
                        soundEffect.Add(content.Load<SoundEffect>("sounds/RocketLauncher_EXPLOSION!!!"));
                        break;
                    case SoundType.Shockwave:
                        soundEffect.Add(content.Load<SoundEffect>("sounds/Shockwave"));
                            break;
                }
            }
        }

        public static SoundCollection getInstance()
        {
            if (instance == null) instance = new SoundCollection();
            return instance;
        }

        public SoundEffectInstance GetSoundInstance(SoundType soundType)
        {
            if(soundEffect==null) throw new ContentLoadException();
            return soundEffect[(int)soundType].CreateInstance();
        }


    }
}
