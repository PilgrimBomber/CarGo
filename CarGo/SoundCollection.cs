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
        Crash_Dummy
    }
    public class SoundCollection
    {
        private List<SoundEffect> effects = new List<SoundEffect>();

        public SoundCollection(ContentManager content)
        {
            foreach (SoundType soundType in Enum.GetValues(typeof (SoundType)).Cast<SoundType>().ToList<SoundType>())
            {
                switch (soundType)
                {
                    case SoundType.Crash_Dummy:
                        effects.Add(content.Load<SoundEffect>("sounds/Enemy_Monster_Hit"));
                        break;
                }
            }
        }

        public SoundEffectInstance getInstance(SoundType soundType)
        {
            return effects[(int)soundType].CreateInstance();
        }


    }
}
