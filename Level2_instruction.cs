using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace WindowsGame1
{
    class Level2_instruction
    {
        public Vector2 Position = new Vector2(0, 0);

        //The texture object used when drawing the sprite
        private Texture2D level1_final_background;
        private Texture2D inst_image;

        public void LoadContent(ContentManager theContentManager, string theAssetName, string level_name)
        {
            level1_final_background = theContentManager.Load<Texture2D>(theAssetName);
            inst_image = theContentManager.Load<Texture2D>(level_name);
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(level1_final_background, new Rectangle(0, 0, 800, 600), Color.White);

            theSpriteBatch.Draw(inst_image, new Rectangle(50, 50, 650, 400), Color.White);
        }
    }
}