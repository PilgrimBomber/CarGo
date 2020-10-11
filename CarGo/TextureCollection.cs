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
        Front_Small_Spikes,
        Front_Spikes,
        Front_Big_Spikes,
        Front_Small_Bumper,
        Front_Bumper,
        Front_Big_Bumper,
        WorldObject_Cactus,
        WorldObject_CactusRip,
        WorldObject_CactusCompletlyRip,
        WorldObject_Skull,
        WorldObject_SkullRip,
        WorldObject_Rock,
        Active_FlameThrower,
        Active_FlamethrowerAnimation,
        Active_RocketLauncher,
        Active_Shockwave,
        Active_ShockwaveAnimation,
        Active_Trap,
        MainMenuBackground,
        MainMenuCarrier,
        Menu_Background,
        Menu_VictoryScreen,
        Menu_DefeatScreen,
        Menu_Controls,
        Menu_Select_Flamethrower,
        Menu_Select_Shockwave,
        Menu_Selection_BoxBox,
        CreditScreen
    }

    public class TextureCollection
    {
        private SortedList<TextureType, Texture2D> textures;
        private ContentManager content;
        private static TextureCollection instance;

        public static TextureCollection Instance
        { get
            {
                if (instance == null) instance = new TextureCollection();
                return instance;
            }
        }

        private TextureCollection()
        {
            textures = new SortedList<TextureType, Texture2D>();
        }


        public void SetContent(ContentManager content)
        {
            this.content = content;
        }

        private void LoadTexture(TextureType textureType)
        {
            if (textures.ContainsKey(textureType)) return;
            switch (textureType)
            {
                case TextureType.Active_FlameThrower: textures.Add(TextureType.Active_FlameThrower, content.Load<Texture2D>("textures/Cargo")); break;//noch benötigt
                case TextureType.Active_FlamethrowerAnimation: textures.Add(TextureType.Active_FlamethrowerAnimation,content.Load<Texture2D>("textures/Flamethrower_Animation")); break;
                case TextureType.Active_RocketLauncher: textures.Add(TextureType.Active_RocketLauncher, content.Load<Texture2D>("textures/Rocket")); break;
                case TextureType.Active_Shockwave: textures.Add(TextureType.Active_Shockwave,content.Load<Texture2D>("textures/Shockwave")); break;
                case TextureType.Active_ShockwaveAnimation: textures.Add(TextureType.Active_ShockwaveAnimation,content.Load<Texture2D>("textures/Shockwave_Animation")); break;
                case TextureType.Active_Trap: textures.Add(TextureType.Active_Trap,content.Load<Texture2D>("textures/TurtleMine")); break;
                case TextureType.Background_Sand: textures.Add(TextureType.Background_Sand,content.Load<Texture2D>("textures/Background_Sand")); break;
                case TextureType.Background_Street_Top: textures.Add(TextureType.Background_Street_Top,content.Load<Texture2D>("textures/Background_Street_Top")); break;
                case TextureType.Background_Street_Middle: textures.Add(TextureType.Background_Street_Middle,content.Load<Texture2D>("textures/Background_Street_Middle")); break;
                case TextureType.Background_Street_Bottom: textures.Add(TextureType.Background_Street_Bottom,content.Load<Texture2D>("textures/Background_Street_Bottom")); break;
                case TextureType.Cargo: textures.Add(TextureType.Cargo,content.Load<Texture2D>("textures/Cargo")); break;
                case TextureType.Car_Big: textures.Add(TextureType.Car_Big,content.Load<Texture2D>("textures/Car_BigSize")); break;
                case TextureType.Car_Medium: textures.Add(TextureType.Car_Medium,content.Load<Texture2D>("textures/Car_MediumSize")); break;
                case TextureType.Car_Small: textures.Add(TextureType.Car_Small,content.Load<Texture2D>("textures/Car_SmallSize")); break;
                case TextureType.Dummy: textures.Add(TextureType.Dummy,content.Load<Texture2D>("textures/Enemy_Dummy")); break;
                case TextureType.Enemy_Zombie_Slow: textures.Add(TextureType.Enemy_Zombie_Slow,content.Load<Texture2D>("textures/Enemy_Zombie_Slow")); break;
                case TextureType.Enemy_Zombie_Slow_Animation: textures.Add(TextureType.Enemy_Zombie_Slow_Animation,content.Load<Texture2D>("textures/Enemy_Zombie_Slow_Animation")); break;
                case TextureType.Enemy_Zombie_Fast: textures.Add(TextureType.Enemy_Zombie_Fast,content.Load<Texture2D>("textures/Enemy_Zombie_Fast")); break;
                case TextureType.Enemy_Zombie_Fast_Animation: textures.Add(TextureType.Enemy_Zombie_Fast_Animation,content.Load<Texture2D>("textures/Enemy_Zombie_Fast_Animation")); break;
                case TextureType.Explosion: textures.Add(TextureType.Explosion,content.Load<Texture2D>("textures/Explosion")); break;
                case TextureType.Explosion_Animation: textures.Add(TextureType.Explosion_Animation,content.Load<Texture2D>("textures/Explosion_Animation")); break;
                case TextureType.Front_Bumper: textures.Add(TextureType.Front_Bumper,content.Load<Texture2D>("textures/Mod_Front_Bumper")); break;
                case TextureType.Front_Small_Bumper: textures.Add(TextureType.Front_Small_Bumper,content.Load<Texture2D>("textures/Mod_Front_Small_Bumper")); break;
                case TextureType.Front_Big_Bumper: textures.Add(TextureType.Front_Big_Bumper,content.Load<Texture2D>("textures/Mod_Front_Big_Bumper")); break;
                case TextureType.Front_Spikes: textures.Add(TextureType.Front_Spikes,content.Load<Texture2D>("textures/Mod_Front_Spikes")); break;
                case TextureType.Front_Small_Spikes: textures.Add(TextureType.Front_Small_Spikes,content.Load<Texture2D>("textures/Mod_Front_Small_Spikes")); break;
                case TextureType.Front_Big_Spikes: textures.Add(TextureType.Front_Big_Spikes,content.Load<Texture2D>("textures/Mod_Front_Big_Spikes")); break;
                case TextureType.WorldObject_Cactus: textures.Add(TextureType.WorldObject_Cactus,content.Load<Texture2D>("textures/Cactus")); break;
                case TextureType.WorldObject_CactusRip: textures.Add(TextureType.WorldObject_CactusRip,content.Load<Texture2D>("textures/CactusRip")); break;
                case TextureType.WorldObject_Skull: textures.Add(TextureType.WorldObject_Skull,content.Load<Texture2D>("textures/Skull")); break;
                case TextureType.WorldObject_SkullRip: textures.Add(TextureType.WorldObject_SkullRip,content.Load<Texture2D>("textures/SkullRip")); break;
                case TextureType.WorldObject_CactusCompletlyRip: textures.Add(TextureType.WorldObject_CactusCompletlyRip,content.Load<Texture2D>("textures/CactusCompletlyRip")); break;
                case TextureType.WorldObject_Rock: textures.Add(TextureType.WorldObject_Rock,content.Load<Texture2D>("textures/Rock")); break;
                case TextureType.Menu_Background: textures.Add(TextureType.Menu_Background,content.Load<Texture2D>("textures/Menu_Background")); break;
                case TextureType.MainMenuBackground: textures.Add(TextureType.MainMenuBackground,content.Load<Texture2D>("textures/Splashscreen_0")); break;
                case TextureType.MainMenuCarrier: textures.Add(TextureType.MainMenuCarrier,content.Load<Texture2D>("textures/MainMenuCarrier_0")); break;
                case TextureType.Menu_VictoryScreen: textures.Add(TextureType.Menu_VictoryScreen,content.Load<Texture2D>("textures/Menu_VictoryScreen")); break;
                case TextureType.Menu_DefeatScreen: textures.Add(TextureType.Menu_DefeatScreen,content.Load<Texture2D>("textures/Menu_DefeatScreen")); break;
                case TextureType.Menu_Controls: textures.Add(TextureType.Menu_Controls,content.Load<Texture2D>("textures/Menu_Controls")); break;
                case TextureType.Menu_Select_Flamethrower: textures.Add(TextureType.Menu_Select_Flamethrower,content.Load<Texture2D>("textures/Menu_Select_Flamethrower")); break;
                case TextureType.Menu_Select_Shockwave: textures.Add(TextureType.Menu_Select_Shockwave,content.Load<Texture2D>("textures/Menu_Select_Shockwave")); break;
                case TextureType.Menu_Selection_BoxBox: textures.Add(TextureType.Menu_Selection_BoxBox,content.Load<Texture2D>("textures/Menu_Selection_BoxBox")); break;
                case TextureType.CreditScreen: textures.Add(TextureType.CreditScreen,content.Load<Texture2D>("textures/CreditScreen")); break;

            }

        }

        public Texture2D GetTexture(TextureType textureType)
        {
            Texture2D returntexture;
            if (!textures.TryGetValue(textureType, out returntexture))
            {
                LoadTexture(textureType);
                textures.TryGetValue(textureType, out returntexture);
            }
            return returntexture;
        }

        


    }
}
