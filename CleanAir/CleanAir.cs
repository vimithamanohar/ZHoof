using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Deformable_Terrain
{
    class CleanAir
    {
        private Vector2 mousePosition;
        HorizontallyScrollingBackground mScrollingBackground;
        private MouseState currentMouseState;
        private uint[] pixelDeformData;
        private uint[] chk_pixelLevelData;
        private uint[] pixelLevelData;
        public TimeOut timey;
        public static int score;

        int temptime = 600;
        int temp_dis;
        SpriteFont font;

        Texture2D textureLevel;
        Texture2D textureDeform;
        Texture2D textureman;
        ContentManager content;
        Vector2 manPosition = new Vector2();

        SoundEffect windy;

        public CleanAir()
        {

            timey = new TimeOut();
        }
        public void LoadContent(ContentManager Content, WindowsGame1.Game1 gaming)
        {
            content = Content;
            gaming.IsMouseVisible = true;
            textureLevel = Content.Load<Texture2D>(@"images\level_chk");
            textureDeform = Content.Load<Texture2D>(@"images\deform");
            textureman = Content.Load<Texture2D>(@"images\man3");
            font = Content.Load<SpriteFont>(@"Fonts\ScoreFont");
            windy = Content.Load<SoundEffect>("Audios\\wind");
            mScrollingBackground = new HorizontallyScrollingBackground(gaming.GraphicsDevice.Viewport);
            mScrollingBackground.AddBackground(@"images\Clear1");

            // Declare an array to hold the pixel data
            pixelDeformData = new uint[(textureDeform.Width) * (textureDeform.Height)];
            chk_pixelLevelData = new uint[textureLevel.Width * textureLevel.Height];
                     
            pixelLevelData = new uint[textureLevel.Width * textureLevel.Height];


            // Populate the array

            textureLevel.GetData(chk_pixelLevelData, 0, textureLevel.Width * textureLevel.Height);
            // Populate the array
            textureDeform.GetData(pixelDeformData, 0, textureDeform.Width * textureDeform.Height);


            //Load the content for the Scrolling background
            mScrollingBackground.LoadContent(Content);
            temp_dis = temptime / 60;

        }

        
        public void restart_level()
        {
           textureLevel = content.Load<Texture2D>(@"images\level_chk");
            textureDeform = content.Load<Texture2D>(@"images\deform");
            pixelLevelData = chk_pixelLevelData;
            textureLevel.SetData((pixelLevelData));
        }

              
        public void Update(GameTime gameTime, out bool exit)
        {
            exit = false;
            if (timey.CheckTime() == false)
            {
                score = score * 5 / 1000;
                exit = true;
                return;

            }


            UpdateMouse();

            // TODO: Add your update logic here       
            //Update the scrolling backround. You can scroll to the left or to the right by changing the scroll direction
            mScrollingBackground.Update(gameTime, 160, HorizontallyScrollingBackground.HorizontalScrollDirection.Left);
        }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
            mScrollingBackground.Draw(spriteBatch);

            spriteBatch.Draw(textureLevel, new Vector2(0, 0), Color.White);
            // spriteBatch.Draw(textureDeform, mousePosition, Color.White);
            manPosition.X = mousePosition.X + 10;
            manPosition.Y = mousePosition.Y - 200;
            spriteBatch.Draw(textureman, manPosition, Color.White);

            if (temptime % 60 == 0)
            {
                temp_dis = temptime / 60;

            }
           spriteBatch.DrawString(font, "Timer: " + temp_dis, new Vector2(50, 530), Color.Red);
            temptime--;

        }

        protected void UpdateMouse()
        {

            MouseState previousMouseState = currentMouseState;
            
            currentMouseState = Mouse.GetState();

            // This gets the mouse co-ordinates

            // relative to the upper left of the game window

            mousePosition = new Vector2(currentMouseState.X, currentMouseState.Y);
            // Here we make sure that we only call the deform level function

            // when the left mouse button is released

            if (previousMouseState.LeftButton == ButtonState.Pressed &&

              currentMouseState.LeftButton == ButtonState.Released)
            {
                windy.Play();
                DeformLevel();

            }



        }
        /// <summary>

        /// 16777215 = Alpha

        /// 4294967295 = White

        /// </summary>

        protected void DeformLevel()
        {

            // Declare an array to hold the pixel data
            
            pixelLevelData = new uint[textureLevel.Width * textureLevel.Height];

            
            // Populate the array

            textureLevel.GetData(pixelLevelData, 0, textureLevel.Width * textureLevel.Height);
            for (int x = 0; x < textureDeform.Width; x++)
            {

                for (int y = 0; y < textureDeform.Height; y++)
                {

                    // Do some error checking so we dont draw out of bounds of the array etc..

                    if (((mousePosition.X + x) < (textureLevel.Width)) && ((mousePosition.Y + y) < (textureLevel.Height)))
                    {

                        if ((mousePosition.X + x) >= 0 && (mousePosition.Y + y) >= 0)
                        {

                            // Here we check that the current co-ordinate of the deform texture is not an alpha value

                            // And that the current level texture co-ordinate is not an alpha value(is not transparent

                            //note from msdn for me : Alpha blending uses the alpha channel of the source color to create a transparency effect so that the destination color appears through the source color.

                            if (pixelDeformData[x + y * textureDeform.Width] != 16777215 && pixelLevelData[((int)mousePosition.X + x) + ((int)mousePosition.Y + y) * textureLevel.Width] != 16777215)
                            {



                                // We then check to see if the deform texture's current pixel is white (4294967295)               

                                if (pixelDeformData[x + y * textureDeform.Width] == 4294967295)
                                {

                                    // It's white so we replace it with an Alpha pixel

                                    pixelLevelData[((int)mousePosition.X + x) + ((int)mousePosition.Y + y) * textureLevel.Width] = 16777215;



                                }

                                else
                                {

                                    // Its not white so just set the level texture pixel to the deform texture pixel

                                    pixelLevelData[((int)mousePosition.X + x) + ((int)mousePosition.Y + y) * textureLevel.Width] = pixelDeformData[x + y * textureDeform.Width];

                                    score += 1;
                                }
                            }
                        }
                    }
                }
            }

            // Update the texture with the changes made above

            textureLevel.SetData(pixelLevelData);

        }

    }
}
