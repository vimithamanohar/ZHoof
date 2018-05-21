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
    class Help
    {
        private Texture2D tex;
        ContentManager content;

        public Help(ContentManager thecontent)
        {
            content = thecontent;
        }

        public void LoadContent()
        {
            tex = content.Load<Texture2D>("images\\Help");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, new Rectangle(0, 0, 800, 600), Color.White);
        }
    }
}
