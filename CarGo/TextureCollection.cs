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
    public enum TextureType { Explosion, Cargo, Car_Big, Car_Medium, Car_Small, Dummy, Enemy_Zombie, Enemy_Zombie_Animation, Enemy_Fast ,Front_Spikes, Front_Bumper,WorldObject_Rock, WorldObject_Cactus, WorldObject_CactusRip, Active_RocketLauncher}
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
                        case TextureType.Cargo: textures.Add(content.Load<Texture2D>("textures/Cargo")); break;
                        case TextureType.Car_Big: textures.Add(content.Load<Texture2D>("textures/Car_BigSize")); break;
                        case TextureType.Car_Medium: textures.Add(content.Load<Texture2D>("textures/Car_MediumSize")); break;
                        case TextureType.Car_Small: textures.Add(content.Load<Texture2D>("textures/Car_SmallSize")); break;
                        case TextureType.Dummy: textures.Add(content.Load<Texture2D>("textures/Enemy_Dummy")); break;
                        case TextureType.Enemy_Zombie: textures.Add(content.Load<Texture2D>("textures/Enemy_Zombie")); break;
                        case TextureType.Enemy_Zombie_Animation: textures.Add(content.Load<Texture2D>("textures/Enemy_Zombie_Slow_Animation")); break;
                        case TextureType.Enemy_Fast: textures.Add(content.Load<Texture2D>("textures/EnemyFast")); break;
                        case TextureType.Front_Bumper: textures.Add(content.Load<Texture2D>("textures/Mod_Front_Bumper")); break;
                        case TextureType.Front_Spikes: textures.Add(content.Load<Texture2D>("textures/Mod_Front_Spikes")); break;
                        case TextureType.WorldObject_Cactus: textures.Add(content.Load<Texture2D>("textures/Cactus")); break;
                        case TextureType.WorldObject_CactusRip: textures.Add(content.Load<Texture2D>("textures/CactusRip")); break;
                        case TextureType.WorldObject_Rock: textures.Add(content.Load<Texture2D>("textures/Rock")); break;
                        case TextureType.Active_RocketLauncher: textures.Add(content.Load<Texture2D>("textures/Rocket")); break;
                        case TextureType.Explosion: textures.Add(content.Load<Texture2D>("textures/Explosion")); break;
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
