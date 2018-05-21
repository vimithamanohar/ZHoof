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
    class Can_check
    {
        ContentManager Content;
        SpriteBatch spriteBatch;
        Texture2D add_score, minus_score;
        Texture2D can_tex, red_trash;
        Rectangle red_rect;
        int no_of_cans;
        Cans[] can;
        List<Cans> can_list;
        static Random can_rand = new Random();
        static int can_x, can_y;
        MouseState mPreviousMouseState;
        Rectangle red_trash_chk, blue_trash_chk, yellow_trash_chk, orange_trash_chk;
        bool draw_add, draw_minus;

        public Can_check(int no_of_can, Rectangle red, Rectangle blue, Rectangle yellow, Rectangle orange)
        {

            red_rect = new Rectangle(700, 500, 95, 100);
            red_trash_chk = red;
            blue_trash_chk = blue;
            yellow_trash_chk = yellow;
            orange_trash_chk = orange;

            no_of_cans = no_of_can;

            can = new Cans[no_of_cans];
            this.can_list = new List<Cans>();
            draw_add = false;


            draw_minus = false;
        }

        public void LoadContent(ContentManager content, SpriteBatch spriteBatch)
        {
            Content = content;
            this.spriteBatch = spriteBatch;
            can_tex = Content.Load<Texture2D>("trashs\\Can");
            red_trash = content.Load<Texture2D>("trashs\\GameRecycleRed");
            add_score = content.Load<Texture2D>("trashs\\plus10");
            minus_score = content.Load<Texture2D>("trashs\\minus5");

            for (int i = 0; i < no_of_cans; i++)
            {
                can_x = can_rand.Next(50, 650) + can_rand.Next(40);
                can_y = can_rand.Next(150, 450) + can_rand.Next(60);

                can[i] = new Cans(new Vector2(can_x, can_y), can_tex);
                can_list.Add(can[i]);
            }


            mPreviousMouseState = Mouse.GetState();
        }

        public void Update(GameTime gameTime, Mouse_handler ourCursor)
        {

            HandleMouse(ourCursor);

        }

        public void Draw()
        {

            spriteBatch.Draw(red_trash, red_rect, Color.White);
            foreach (Cans c in can)
            {
                c.Draw(spriteBatch, c.position, c.tex);
            }

            if (draw_add)
            {
                spriteBatch.Draw(add_score, new Rectangle(720, 460, add_score.Width, add_score.Height), Color.White);
                draw_add = false;
            }
            else if (draw_minus)
            {
                draw_minus = false;
                spriteBatch.Draw(minus_score, new Rectangle(720, 460, minus_score.Width, minus_score.Height), Color.White);
            }
        }

        public void HandleMouse(Mouse_handler ourCursor)
        {
            MouseState aCurrentMouseState = Mouse.GetState();

            foreach (Cans c in can_list)
            {

                if (ourCursor.ButtonClick(c))
                {
                    if ((aCurrentMouseState.LeftButton == ButtonState.Pressed) & (mPreviousMouseState.LeftButton == ButtonState.Released))
                    {
                        c.Selecting = true;
                        c.position.X = aCurrentMouseState.X;
                        c.position.Y = aCurrentMouseState.Y;
                        mPreviousMouseState = aCurrentMouseState;


                    }
                    else if (aCurrentMouseState.LeftButton == ButtonState.Pressed & mPreviousMouseState.LeftButton == ButtonState.Pressed)
                    {
                        c.position.X = aCurrentMouseState.X;
                        c.position.Y = aCurrentMouseState.Y;
                        mPreviousMouseState = aCurrentMouseState;

                    }
                    else if (aCurrentMouseState.LeftButton == ButtonState.Released && mPreviousMouseState.LeftButton == ButtonState.Pressed)
                    {
                        c.Selecting = false;
                        mPreviousMouseState = aCurrentMouseState;
                    }
                    else
                        c.Selecting = false;

                }
                if (no_of_cans == 3)
                {
                    if ((c.can_rect.Intersects(yellow_trash_chk))
                        || (c.can_rect.Intersects(orange_trash_chk))
                        || (c.can_rect.Intersects(blue_trash_chk)))
                    {
                        Trash_spread.trash_counter++;
                        Trash_spread.score = Trash_spread.score - 5;
                        draw_minus = true;
                        c.position = new Vector2(-500, 0);
                    }

                }
                if (no_of_cans == 4)
                {
                    if ((c.can_rect.Intersects(blue_trash_chk)))
                    {
                        Trash_spread.trash_counter++;
                        Trash_spread.score = Trash_spread.score - 5;
                        draw_minus = true;
                        c.position = new Vector2(-500, 0);
                    }

                }

                if (c.can_rect.Intersects(red_trash_chk))
                {

                    Trash_spread.score = Trash_spread.score + 10;
                    Trash_spread.trash_counter++;
                    draw_add = true;
                    c.position = new Vector2(-500, 0);
                }

            }
        }
    }
}
