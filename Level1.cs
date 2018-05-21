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
    class Level1 
    {
         public static int bag_count = 0;
        private Texture2D level1_Ground;
        //Mouse character
        private Vector2 pos;
        private Texture2D tex;
       SpriteFont font;

        static private int TimeOutLimit = 600;
        private double timeoutCount = 0;
        int temptime = TimeOutLimit;
        int temp_dis;
        Rectangle viewportRect = new Rectangle(40, 45, 750, 600);

        const int trash_number = 2;
        List<Trash> trash_list;
        Trash[] trash = new Trash[trash_number];
        const float maxTrashboundry = 0.8f;
        const float maxyTrashboundry = 0.2f;
        const float minTrashboundry = 0.1f;
        Random random = new Random();
        ContentManager content;

        SoundEffect drop;

        public Level1(ContentManager content1)
        {
            this.pos.X = 5;
            this.pos.Y = 450;
            content = content1;           
            
        }


        public void LoadContent(string theAssetName)
        {
            tex = content.Load<Texture2D>(@"images\gal_character");
            level1_Ground = content.Load<Texture2D>(theAssetName);
            font = content.Load<SpriteFont>(@"Fonts\ScoreFont");
            drop = content.Load<SoundEffect>("Audios\\litter_catch");

            this.trash_list = new List<Trash>(); //Actually creating the list
            for (int i = 0; i <= trash_number - 1; i++)
            {
                trash[i] = new Trash(content.Load<Texture2D>("images\\trash1"));
                //Actually creating the button.
                this.trash_list.Add(trash[i]);
                
            }
            temp_dis = temptime / 60;
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(level1_Ground, new Rectangle(0, 0, 800, 600), Color.White);
            theSpriteBatch.Draw(this.tex, this.pos, Color.White);

            if (temptime % 60 == 0)
            {
                temp_dis = temptime / 60;
                
            }
            theSpriteBatch.DrawString(font, "Timer: " + temp_dis, new Vector2(50, 530), Color.Red);
           temptime--;
            int temp = 50;
            foreach (Trash trashs in trash)
            {
                if (!trashs.caught)
                {
                    int x1 = Convert.ToInt32(trashs.position.X);
                    int y1 = Convert.ToInt32(trashs.position.Y);
                    theSpriteBatch.Draw(trashs.sprite, trashs.position, Color.White);
                              temp = temp + 20;
                }
            }

            theSpriteBatch.DrawString(font, "Bin: " + bag_count, new Vector2(690, 530), Color.Red);
        }

        public void Update(out bool timechk,GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState(); //Needed to find the most current mouse states.
            //pos.X = mouseState.X;
            //pos.Y = mouseState.Y;
                               
            if (mouseState.X > 705)
                this.pos.X = 705;
            else
                this.pos.X = mouseState.X; //Change x pos to mouseX*/
            timeoutCount++;
            if (timeoutCount > TimeOutLimit)
            {
                timechk = true;
                return;
            }
            
            UpdateTrash();
            bin_caught();
            timechk = false;

        }

        public void UpdateTrash()
        {
            float newx, newy = 650;
            foreach (Trash trashs in trash)
            {
                if (!trashs.caught)
                {
                    trashs.position += trashs.velocity; 
                    if (!viewportRect.Contains(new Point((int)trashs.position.X, (int)trashs.position.Y)))
                    {
                        
                        trashs.caught = true;
                    }
                }

                else
                {
                    trashs.caught = false;
                    newx = Math.Abs(MathHelper.Lerp(
                       (float)viewportRect.Width * minTrashboundry,
                       (float)viewportRect.Width * maxTrashboundry,
                       (float)random.NextDouble()));

                    newy = Math.Abs(MathHelper.Lerp(
                        (float)viewportRect.Height * minTrashboundry,
                        (float)viewportRect.Height * maxyTrashboundry,
                        (float)random.NextDouble()) * random.Next(5));


                    trashs.position = new Vector2(newx, newy);
                    trashs.velocity = new Vector2(0,3);
                }
            }
        }


        void bin_caught()
        {
            int bin_x=(int)pos.X+100;
            int bin_y = (int)pos.Y+101;

           Rectangle charater_rect = new Rectangle((int)pos.X+110,(int)pos.Y+101,50,10);

            foreach (Trash bin in trash)
            {
                Rectangle bin_rect = new Rectangle((int)bin.position.X,(int)bin.position.Y,
                                        bin.sprite.Width,bin.sprite.Height);

                if (charater_rect.Intersects(bin_rect))
                {
                    bin.caught=true;
                    drop.Play();
                    bag_count++;                    
                    break;
                }

            }

        }


    }
}
