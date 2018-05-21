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
    class Food_check
    {
        ContentManager Content;
        SpriteBatch spriteBatch;
        Texture2D add_score,minus_score;
        Texture2D food_tex, yellow_trash;
        Rectangle yellow_rect;
         int no_of_foods;
        Food []food;
        List<Food> food_list;
       Random rand_food = new Random();
        static int food_x, food_y;
        MouseState mPreviousMouseState;
        Rectangle blue_trash_chk, red_trash_chk, yellow_trash_chk, orange_trash_chk;
        bool draw_add, draw_minus;

        public Food_check(int no_of_food, Rectangle red, Rectangle blue, Rectangle yellow, Rectangle orange)
        {
            
            yellow_rect = new Rectangle(415, 500, 95, 100);
            red_trash_chk = red;
            blue_trash_chk = blue;
            yellow_trash_chk = yellow;
            orange_trash_chk = orange;
            
             no_of_foods = no_of_food;          
            
            food = new Food[no_of_foods];
            this.food_list=new List<Food>();
            draw_add = false;

            
            draw_minus = false;
        }

        public void LoadContent(ContentManager content, SpriteBatch spriteBatch)
        {
            Content = content;
            this.spriteBatch = spriteBatch;
            food_tex=Content.Load<Texture2D>("trashs\\Food");
            yellow_trash = content.Load<Texture2D>("trashs\\GameRecycleOrange");
            add_score = content.Load<Texture2D>("trashs\\plus10");
            minus_score = content.Load<Texture2D>("trashs\\minus5");

            for (int i = 0; i < no_of_foods; i++)
            {
                food_x = rand_food.Next(0, 650)+rand_food.Next(55);
                food_y = rand_food.Next(100, 450)+rand_food.Next(60);
               
                food[i] = new Food(new Vector2(food_x, food_y), food_tex);
                food_list.Add(food[i]);
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

            foreach (Food f in food_list)
            {

                if (ourCursor.ButtonClick(f))
                {
                    if ((aCurrentMouseState.LeftButton == ButtonState.Pressed) & (mPreviousMouseState.LeftButton == ButtonState.Released))
                    {  f.Selecting = true;
                    f.position.X = aCurrentMouseState.X;
                    f.position.Y = aCurrentMouseState.Y;
                        mPreviousMouseState = aCurrentMouseState;


                    }
                    else if (aCurrentMouseState.LeftButton == ButtonState.Pressed & mPreviousMouseState.LeftButton == ButtonState.Pressed)
                    {
                       f.position.X = aCurrentMouseState.X;
                        f.position.Y = aCurrentMouseState.Y;
                        mPreviousMouseState = aCurrentMouseState;

                    }
                    else if (aCurrentMouseState.LeftButton == ButtonState.Released && mPreviousMouseState.LeftButton == ButtonState.Pressed)
                    {
                       f.Selecting = false;
                        mPreviousMouseState = aCurrentMouseState;
                    }
                    else
                            f.Selecting = false;
                    
                }

                if (no_of_foods == 3)
                {
                    if ((f.food_rect.Intersects(blue_trash_chk))
                        ||(f.food_rect.Intersects(yellow_trash_chk))
                        || (f.food_rect.Intersects(red_trash_chk)))
                    {
                        Trash_spread.trash_counter++;
                        Trash_spread.score = Trash_spread.score - 5;
                        draw_minus = true;  
                        f.position = new Vector2(-500, 0);
                    }                    
                }

                if (f.food_rect.Intersects(orange_trash_chk))
                {
                    Trash_spread.trash_counter++;
                    Trash_spread.score = Trash_spread.score + 10;
                    draw_add = true;
                    f.position = new Vector2(-500, 0);
                }

                
            }
               }

        public void Draw()
        {         

            spriteBatch.Draw(yellow_trash, yellow_rect, Color.White);
            foreach (Food c in food)
            {
                c.Draw(spriteBatch, c.position, c.tex);
            }

            if (draw_add)
            {
                spriteBatch.Draw(add_score, new Rectangle(430, 460, add_score.Width,add_score.Height), Color.White);
                draw_add = false;
            }
            else if(draw_minus)
            {
                draw_minus = false;
                spriteBatch.Draw(minus_score, new Rectangle(430, 460,minus_score.Width,minus_score.Height), Color.White);
            }
        }


    }
}
