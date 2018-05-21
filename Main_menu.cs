using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    class Main_menu
    {
        //The current position of the Sprite
        public Vector2 Position = new Vector2(0,0);
        
        //The texture object used when drawing the sprite
        private Texture2D backGround;

        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            backGround = theContentManager.Load<Texture2D>(theAssetName);
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(backGround, new Rectangle(0,0,800,600), Color.White);
        }


    }
}
