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

namespace Trash_pick
{
    class Bottle_check
    {
        ContentManager Content;
        SpriteBatch spriteBatch;
        Texture2D add_score,minus_score;
        Texture2D bottle_tex, blue_trash;
        Rectangle blue_rect;
         int no_of_bottles;
        Bottle []bottle;
        List<Bottle> bottle_list;
       Random rand_bottle = new Random();
        static int bottle_x, bottle_y;
        MouseState mPreviousMouseState;
        Rectangle blue_trash_chk, red_trash_chk, yellow_trash_chk, orange_trash_chk;
        bool draw_add, draw_minus;

        public Bottle_check(int no_of_bottle, Rectangle red, Rectangle blue, Rectangle yellow, Rectangle orange)
        {
            
            blue_rect = new Rectangle(610, 500, 95, 100);
            red_trash_chk = red;
            blue_trash_chk = blue;
            yellow_trash_chk = yellow;
            orange_trash_chk = orange;
            
             no_of_bottles = no_of_bottle;          
            
            bottle = new Bottle[no_of_bottles];
            this.bottle_list=new List<Bottle>();
            draw_add = false;

            
            draw_minus = false;
        }

        public void LoadContent(ContentManager content, SpriteBatch spriteBatch)
        {
            Content = content;
            this.spriteBatch = spriteBatch;
            bottle_tex=Content.Load<Texture2D>("trashs\\Bottle");
            blue_trash = content.Load<Texture2D>("trashs\\GameRecycleBlue");
            add_score = content.Load<Texture2D>("trashs\\plus10");
            minus_score = content.Load<Texture2D>("trashs\\minus5");

            for (int i = 0; i < no_of_bottles; i++)
            {
                bottle_x = rand_bottle.Next(150, 700);
                bottle_y = rand_bottle.Next(300, 400);
               
                bottle[i] = new Bottle(new Vector2(bottle_x, bottle_y), bottle_tex);
                bottle_list.Add(bottle[i]);
            }

           
            mPreviousMouseState = Mouse.GetState();
        }

        public void Update(GameTime gameTime,Mouse_handler ourCursor)
        {
            
            HandleMouse(ourCursor); 
            
        }

        public void HandleMouse(Mouse_handler ourCursor)
        {  
            MouseState aCurrentMouseState = Mouse.GetState();

            foreach (Bottle b in bottle_list)
            {

                if (ourCursor.ButtonClick(b))
                {
                    if ((aCurrentMouseState.LeftButton == ButtonState.Pressed) & (mPreviousMouseState.LeftButton == ButtonState.Released))
                    {  b.Selecting = true;
                    b.position.X = aCurrentMouseState.X;
                    b.position.Y = aCurrentMouseState.Y;
                        mPreviousMouseState = aCurrentMouseState;


                    }
                    else if (aCurrentMouseState.LeftButton == ButtonState.Pressed & mPreviousMouseState.LeftButton == ButtonState.Pressed)
                    {
                       b.position.X = aCurrentMouseState.X;
                        b.position.Y = aCurrentMouseState.Y;
                        mPreviousMouseState = aCurrentMouseState;

                    }
                    else if (aCurrentMouseState.LeftButton == ButtonState.Released && mPreviousMouseState.LeftButton == ButtonState.Pressed)
                    {
                       b.Selecting = false;
                        mPreviousMouseState = aCurrentMouseState;
                    }
                    else
                            b.Selecting = false;
                    
                }

                if (no_of_bottles == 3)
                {
                    if ((b.bottle_rect.Intersects(yellow_trash_chk))
                        ||(b.bottle_rect.Intersects(orange_trash_chk))
                        || (b.bottle_rect.Intersects(red_trash_chk)))
                    {
                        Trash_spread.trash_counter++;
                        Trash_spread.score = Trash_spread.score - 5;
                        draw_minus = true;  
                        b.position = new Vector2(-500, 0);
                    }                    
                }

                if (no_of_bottles == 4)
                {
                    if ((b.bottle_rect.Intersects(red_trash_chk)))
                    {
                        Trash_spread.trash_counter++;
                        Trash_spread.score = Trash_spread.score - 5;
                        draw_minus = true;
                        b.position = new Vector2(-500, 0);
                    }
                }

                if (b.bottle_rect.Intersects(blue_trash_chk))
                {
                    Trash_spread.trash_counter++;
                    Trash_spread.score = Trash_spread.score + 10;
                    draw_add = true;
                    b.position = new Vector2(-500, 0);
                }

                
            }
               }

        public void Draw()
        {         

            spriteBatch.Draw(blue_trash, blue_rect, Color.White);
            foreach (Bottle c in bottle)
            {
                c.Draw(spriteBatch, c.position, c.tex);
            }

            if (draw_add)
            {
                spriteBatch.Draw(add_score, new Rectangle(630, 460, add_score.Width,add_score.Height), Color.White);
                draw_add = false;
            }
            else if(draw_minus)
            {
                draw_minus = false;
                spriteBatch.Draw(minus_score, new Rectangle(630, 460,minus_score.Width,minus_score.Height), Color.White);
            }
        }


    }
}
