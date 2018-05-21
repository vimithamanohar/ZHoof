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
    class Paper_check
    {
        ContentManager Content;
        SpriteBatch spriteBatch;
        Texture2D add_score,minus_score;
        Texture2D paper_tex, yellow_trash;
        Rectangle yellow_rect;
         int no_of_papers;
        Paper []paper;
        List<Paper> paper_list;
       Random rand_paper = new Random();
        static int paper_x, paper_y;
        MouseState mPreviousMouseState;
        Rectangle blue_trash_chk, red_trash_chk, yellow_trash_chk, orange_trash_chk;
        bool draw_add, draw_minus;

        public Paper_check(int no_of_paper, Rectangle red, Rectangle blue, Rectangle yellow, Rectangle orange)
        {
            
            yellow_rect = new Rectangle(515, 500, 95, 100);
            red_trash_chk = red;
            blue_trash_chk = blue;
            yellow_trash_chk = yellow;
            orange_trash_chk = orange;
            
             no_of_papers = no_of_paper;          
            
            paper = new Paper[no_of_papers];
            this.paper_list=new List<Paper>();
            draw_add = false;

            
            draw_minus = false;
        }

        public void LoadContent(ContentManager content, SpriteBatch spriteBatch)
        {
            Content = content;
            this.spriteBatch = spriteBatch;
            paper_tex=Content.Load<Texture2D>("trashs\\Paper");
            yellow_trash = content.Load<Texture2D>("trashs\\GameRecycleYellow");
            add_score = content.Load<Texture2D>("trashs\\plus10");
            minus_score = content.Load<Texture2D>("trashs\\minus5");

            for (int i = 0; i < no_of_papers; i++)
            {
                paper_x = rand_paper.Next(30, 650);
                paper_y = rand_paper.Next(250, 400);
               
                paper[i] = new Paper(new Vector2(paper_x, paper_y), paper_tex);
                paper_list.Add(paper[i]);
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

            foreach (Paper p in paper_list)
            {

                if (ourCursor.ButtonClick(p))
                {
                    if ((aCurrentMouseState.LeftButton == ButtonState.Pressed) & (mPreviousMouseState.LeftButton == ButtonState.Released))
                    {  p.Selecting = true;
                    p.position.X = aCurrentMouseState.X;
                    p.position.Y = aCurrentMouseState.Y;
                        mPreviousMouseState = aCurrentMouseState;


                    }
                    else if (aCurrentMouseState.LeftButton == ButtonState.Pressed & mPreviousMouseState.LeftButton == ButtonState.Pressed)
                    {
                       p.position.X = aCurrentMouseState.X;
                        p.position.Y = aCurrentMouseState.Y;
                        mPreviousMouseState = aCurrentMouseState;

                    }
                    else if (aCurrentMouseState.LeftButton == ButtonState.Released && mPreviousMouseState.LeftButton == ButtonState.Pressed)
                    {
                       p.Selecting = false;
                        mPreviousMouseState = aCurrentMouseState;
                    }
                    else
                            p.Selecting = false;
                    
                }

                if (no_of_papers == 3)
                {
                    if ((p.paper_rect.Intersects(blue_trash_chk))
                        ||(p.paper_rect.Intersects(orange_trash_chk))
                        || (p.paper_rect.Intersects(red_trash_chk)))
                    {
                        Trash_spread.trash_counter++;
                        Trash_spread.score = Trash_spread.score - 5;
                        draw_minus = true;  
                        p.position = new Vector2(-500, 0);
                    }                    
                }

                if (p.paper_rect.Intersects(yellow_trash_chk))
                {
                    Trash_spread.trash_counter++;
                    Trash_spread.score = Trash_spread.score + 10;
                    draw_add = true;
                    p.position = new Vector2(-500, 0);
                }

                
            }
               }

        public void Draw()
        {         

            spriteBatch.Draw(yellow_trash, yellow_rect, Color.White);
            foreach (Paper c in paper)
            {
                c.Draw(spriteBatch, c.position, c.tex);
            }

            if (draw_add)
            {
                spriteBatch.Draw(add_score, new Rectangle(530, 460, add_score.Width,add_score.Height), Color.White);
                draw_add = false;
            }
            else if(draw_minus)
            {
                draw_minus = false;
                spriteBatch.Draw(minus_score, new Rectangle(530, 460,minus_score.Width,minus_score.Height), Color.White);
            }
        }


    }
}
