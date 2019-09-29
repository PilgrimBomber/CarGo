using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CarGo
{
    class FontCollection
    {
        public enum Fonttyp { MainMenuButtonFont }

            private List<SpriteFont> fonts = new List<SpriteFont>();
            private static FontCollection fontinstance;

            private FontCollection()
            { }

            public void LoadFonts(ContentManager content)
            {
                foreach (Fonttyp fontTyp in Enum.GetValues(typeof(Fonttyp)).Cast<Fonttyp>().ToList<Fonttyp>())
                {
                    switch (fontTyp)
                    {
                        case Fonttyp.MainMenuButtonFont:
                            fonts.Add(content.Load<SpriteFont>("fonts/Arial"));
                            break;
                    }
                }
            }

            public static FontCollection getInstance()
            {
                if (fontinstance == null) fontinstance = new FontCollection();
                return fontinstance;
            }

        public SpriteFont GetFont(Fonttyp fontType)
        {
            if (fonts == null) throw new ContentLoadException();
            return fonts[(int)fontType];
        }



    }
}
