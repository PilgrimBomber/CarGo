using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CarGo
{
    public enum TextureType {
        Explosion,
        Explosion_Animation,
        Background_Sand,
        Background_Street_Top,
        Background_Street_Middle,
        Background_Street_Bottom,
        Cargo,
        Car_Big,
        Car_Medium,
        Car_Small,
        Dummy,
        Enemy_Zombie_Slow,
        Enemy_Zombie_Slow_Animation,
        Enemy_Zombie_Fast,
        Enemy_Zombie_Fast_Animation,
        Front_Spikes,
        Front_Small_Spikes,
        Front_Small_Bumper,
        Front_Bumper,
        WorldObject_Cactus,
        WorldObject_CactusRip,
        WorldObject_CactusCompletlyRip,
        WorldObject_Skull,
        WorldObject_SkullRip,
        WorldObject_Rock,
        Active_RocketLauncher,
        Active_Shockwave,
        Active_Trap
    }

    public class TextureCollection
    {
        private List<Texture2D> textures;

        private static TextureCollection instance;

        private TextureCollection()
        { }


        public void loadTextures(ContentManager content)
        {
            if(textures==null)
            {
                textures = new List<Texture2D>();

                foreach (TextureType textureType in Enum.GetValues(typeof(TextureType)).Cast<TextureType>().ToList())
                {
                    switch (textureType)
                    {
                        case TextureType.Active_RocketLauncher: textures.Add(content.Load<Texture2D>("textures/Rocket")); break;
                        case TextureType.Active_Shockwave: textures.Add(content.Load<Texture2D>("textures/Shockwave")); break; //noch benötigt
                        case TextureType.Active_Trap: textures.Add(content.Load<Texture2D>("textures/Rocket")); break; // noch benötigt
                        case TextureType.Background_Sand: textures.Add(content.Load<Texture2D>("textures/Background_Sand")); break;
                        case TextureType.Background_Street_Top: textures.Add(content.Load<Texture2D>("textures/Background_Street_Top")); break;
                        case TextureType.Background_Street_Middle: textures.Add(content.Load<Texture2D>("textures/Background_Street_Middle")); break;
                        case TextureType.Background_Street_Bottom: textures.Add(content.Load<Texture2D>("textures/Background_Street_Bottom")); break;
                        case TextureType.Cargo: textures.Add(content.Load<Texture2D>("textures/Cargo")); break;
                        case TextureType.Car_Big: textures.Add(content.Load<Texture2D>("textures/Car_BigSize")); break;
                        case TextureType.Car_Medium: textures.Add(content.Load<Texture2D>("textures/Car_MediumSize")); break;
                        case TextureType.Car_Small: textures.Add(content.Load<Texture2D>("textures/Car_SmallSize")); break;
                        case TextureType.Dummy: textures.Add(content.Load<Texture2D>("textures/Enemy_Dummy")); break;
                        case TextureType.Enemy_Zombie_Slow: textures.Add(content.Load<Texture2D>("textures/Enemy_Zombie_Slow")); break;
                        case TextureType.Enemy_Zombie_Slow_Animation: textures.Add(content.Load<Texture2D>("textures/Enemy_Zombie_Slow_Animation")); break;
                        case TextureType.Enemy_Zombie_Fast: textures.Add(content.Load<Texture2D>("textures/Enemy_Zombie_Fast")); break;
                        case TextureType.Enemy_Zombie_Fast_Animation: textures.Add(content.Load<Texture2D>("textures/Enemy_Zombie_Fast_Animation")); break;
                        case TextureType.Explosion: textures.Add(content.Load<Texture2D>("textures/Explosion")); break;
                        case TextureType.Explosion_Animation: textures.Add(content.Load<Texture2D>("textures/Explosion_Animation")); break;
                        case TextureType.Front_Bumper: textures.Add(content.Load<Texture2D>("textures/Mod_Front_Bumper")); break;
                        case TextureType.Front_Small_Bumper: textures.Add(content.Load<Texture2D>("textures/Mod_Front_Small_Bumper")); break;
                        case TextureType.Front_Spikes: textures.Add(content.Load<Texture2D>("textures/Mod_Front_Spikes")); break;
                        case TextureType.Front_Small_Spikes: textures.Add(content.Load<Texture2D>("textures/Mod_Front_Small_Spikes")); break;
                        case TextureType.WorldObject_Cactus: textures.Add(content.Load<Texture2D>("textures/Cactus")); break;
                        case TextureType.WorldObject_CactusRip: textures.Add(content.Load<Texture2D>("textures/CactusRip")); break;
                        case TextureType.WorldObject_Skull: textures.Add(content.Load<Texture2D>("textures/Skull")); break;
                        case TextureType.WorldObject_SkullRip: textures.Add(content.Load<Texture2D>("textures/SkullRip")); break;
                        case TextureType.WorldObject_CactusCompletlyRip: textures.Add(content.Load<Texture2D>("textures/CactusCompletlyRip")); break;
                        case TextureType.WorldObject_Rock: textures.Add(content.Load<Texture2D>("textures/Rock")); break;
                    }
                }
            }
            
        }

        public static TextureCollection getInstance()
        {
            if (instance == null) instance = new TextureCollection();
            return instance;
        }


        public Texture2D GetTexture(TextureType textureType)
        {
            if (textures == null) throw new ContentLoadException(); //exception if the textures arent loaded
            return textures[(int)textureType];
        }

    }
}
