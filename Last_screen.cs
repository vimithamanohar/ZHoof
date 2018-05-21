using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace WindowsGame1
{
    class Last_screen
    {
       // private Vector2 pos;
        private Texture2D tex;
        ContentManager content;
        SpriteFont font;
       

        public Last_screen(ContentManager thecontent)
        {
            content = thecontent;
        }

        public void LoadContent()
        {
            tex = content.Load<Texture2D>("images\\last_page");
            font = content.Load<SpriteFont>("myFonts");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, new Rectangle(0, 0, 800, 600), Color.White);

            if (Level1_final.total_score > Level1_final.max_total_score)
                Level1_final.max_total_score = Level1_final.total_score;

            spriteBatch.DrawString(font, "Max. Score: " + Level1_final.max_total_score, new Vector2(250, 100), Color.OrangeRed);
            spriteBatch.DrawString(font, "Your Score: " + Level1_final.total_score, new Vector2(300, 180), Color.OrangeRed);
        }
    }
}
