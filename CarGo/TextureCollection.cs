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
        Menu_Yes,
        Menu_No,
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
            Texture2D texture=null;
            switch (textureType)
            {
                case TextureType.Active_FlameThrower: texture = content.Load<Texture2D>("textures/Cargo"); break;//noch benötigt
                case TextureType.Active_FlamethrowerAnimation: texture = content.Load<Texture2D>("textures/Flamethrower_Animation"); break;
                case TextureType.Active_RocketLauncher: texture = content.Load<Texture2D>("textures/Rocket"); break;
                case TextureType.Active_Shockwave: texture = content.Load<Texture2D>("textures/Shockwave"); break;
                case TextureType.Active_ShockwaveAnimation: texture = content.Load<Texture2D>("textures/Shockwave_Animation"); break;
                case TextureType.Active_Trap: texture = content.Load<Texture2D>("textures/TurtleMine"); break;
                case TextureType.Background_Sand: texture = content.Load<Texture2D>("textures/Background_Sand"); break;
                case TextureType.Background_Street_Top: texture = content.Load<Texture2D>("textures/Background_Street_Top"); break;
                case TextureType.Background_Street_Middle: texture = content.Load<Texture2D>("textures/Background_Street_Middle"); break;
                case TextureType.Background_Street_Bottom: texture = content.Load<Texture2D>("textures/Background_Street_Bottom"); break;
                case TextureType.Cargo: texture = content.Load<Texture2D>("textures/Cargo"); break;
                case TextureType.Car_Big: texture = content.Load<Texture2D>("textures/Car_BigSize"); break;
                case TextureType.Car_Medium: texture = content.Load<Texture2D>("textures/Car_MediumSize"); break;
                case TextureType.Car_Small: texture = content.Load<Texture2D>("textures/Car_SmallSize"); break;
                case TextureType.Dummy: texture = content.Load<Texture2D>("textures/Enemy_Dummy"); break;
                case TextureType.Enemy_Zombie_Slow: texture = content.Load<Texture2D>("textures/Enemy_Zombie_Slow"); break;
                case TextureType.Enemy_Zombie_Slow_Animation: texture = content.Load<Texture2D>("textures/Enemy_Zombie_Slow_Animation"); break;
                case TextureType.Enemy_Zombie_Fast: texture = content.Load<Texture2D>("textures/Enemy_Zombie_Fast"); break;
                case TextureType.Enemy_Zombie_Fast_Animation: texture = content.Load<Texture2D>("textures/Enemy_Zombie_Fast_Animation"); break;
                case TextureType.Explosion: texture = content.Load<Texture2D>("textures/Explosion"); break;
                case TextureType.Explosion_Animation: texture = content.Load<Texture2D>("textures/Explosion_Animation"); break;
                case TextureType.Front_Bumper: texture = content.Load<Texture2D>("textures/Mod_Front_Bumper"); break;
                case TextureType.Front_Small_Bumper: texture = content.Load<Texture2D>("textures/Mod_Front_Small_Bumper"); break;
                case TextureType.Front_Big_Bumper: texture = content.Load<Texture2D>("textures/Mod_Front_Big_Bumper"); break;
                case TextureType.Front_Spikes: texture = content.Load<Texture2D>("textures/Mod_Front_Spikes"); break;
                case TextureType.Front_Small_Spikes: texture = content.Load<Texture2D>("textures/Mod_Front_Small_Spikes"); break;
                case TextureType.Front_Big_Spikes: texture = content.Load<Texture2D>("textures/Mod_Front_Big_Spikes"); break;
                case TextureType.WorldObject_Cactus: texture = content.Load<Texture2D>("textures/Cactus"); break;
                case TextureType.WorldObject_CactusRip: texture = content.Load<Texture2D>("textures/CactusRip"); break;
                case TextureType.WorldObject_Skull: texture = content.Load<Texture2D>("textures/Skull"); break;
                case TextureType.WorldObject_SkullRip: texture = content.Load<Texture2D>("textures/SkullRip"); break;
                case TextureType.WorldObject_CactusCompletlyRip: texture = content.Load<Texture2D>("textures/CactusCompletlyRip"); break;
                case TextureType.WorldObject_Rock: texture = content.Load<Texture2D>("textures/Rock"); break;
                case TextureType.Menu_Background: texture = content.Load<Texture2D>("textures/Menu_Background"); break;
                case TextureType.MainMenuBackground: texture = content.Load<Texture2D>("textures/Splashscreen_0"); break;
                case TextureType.MainMenuCarrier: texture = content.Load<Texture2D>("textures/MainMenuCarrier_0"); break;
                case TextureType.Menu_VictoryScreen: texture = content.Load<Texture2D>("textures/Menu_VictoryScreen"); break;
                case TextureType.Menu_DefeatScreen: texture = content.Load<Texture2D>("textures/Menu_DefeatScreen"); break;
                case TextureType.Menu_Controls: texture = content.Load<Texture2D>("textures/Menu_Controls"); break;
                case TextureType.Menu_Select_Flamethrower: texture = content.Load<Texture2D>("textures/Menu_Select_Flamethrower"); break;
                case TextureType.Menu_Select_Shockwave: texture = content.Load<Texture2D>("textures/Menu_Select_Shockwave"); break;
                case TextureType.Menu_Selection_BoxBox: texture = content.Load<Texture2D>("textures/Menu_Selection_BoxBox"); break;
                case TextureType.CreditScreen: texture = content.Load<Texture2D>("textures/CreditScreen"); break;
                case TextureType.Menu_Yes: texture = content.Load<Texture2D>("textures/Menu_Yes"); break;
                case TextureType.Menu_No: texture = content.Load<Texture2D>("textures/Menu_No"); break;
            }
            if (!textures.ContainsKey(textureType))textures.Add(textureType, texture);
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
