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
    class Trash_spread
    {
        ContentManager Content;
        Trash_pick.Mouse_handler ourCursor;
        SpriteBatch spriteBatch;
        Texture2D garden;
        Rectangle rect;
        int timer;
        public static int score = 0;
        static Random rand = new Random();
        public static int trash_counter;
        int total_trash;
        SpriteFont font_time;
        int temptime;
        int temp_dis;
        int level_meter;
        Can_check can_drop;
        Bottle_check bottle_drop;
        Paper_check paper_drop;
        Food_check food_drop;
        Rectangle red_trash_chk, blue_trash_chk, yellow_trash_chk, orange_trash_chk;

        public Trash_spread(int level)
        {
            level_meter = level;
            rect = new Rectangle(0, 0, 800, 600);
            blue_trash_chk = new Rectangle(640, 530, 45, 100);
            red_trash_chk = new Rectangle(730, 530, 45, 100);
            yellow_trash_chk = new Rectangle(540, 530, 45, 100);
            orange_trash_chk = new Rectangle(440, 530, 45, 100);

        }

        public void LoadContent(ContentManager content, SpriteBatch spriteBatch)
        {
            Content = content;
            this.spriteBatch = spriteBatch;
            ourCursor = new Trash_pick.Mouse_handler(Content);
            ourCursor.LoadContent();
            garden = content.Load<Texture2D>("trashs\\Background");
            font_time = content.Load<SpriteFont>(@"Fonts\ScoreFont");

          

            switch (level_meter)
            {
                case 1: timer = 1200;
                    temptime = 1200;
                    total_trash = 7;
                    trash_counter = 0;
                    can_drop = new Can_check(7, red_trash_chk, blue_trash_chk, yellow_trash_chk, orange_trash_chk);
                    can_drop.LoadContent(Content, spriteBatch);
                    break;

                case 2: timer = 1500;
                    temptime = 1500;
                    total_trash = 8;
                    trash_counter = 0;
                    can_drop = new Can_check(4, red_trash_chk, blue_trash_chk, yellow_trash_chk, orange_trash_chk);
                    bottle_drop = new Bottle_check(4, red_trash_chk, blue_trash_chk, yellow_trash_chk, orange_trash_chk);
                    can_drop.LoadContent(Content, spriteBatch);
                    bottle_drop.LoadContent(Content, spriteBatch);
                    break;

                case 3: timer = 1800;
                    temptime = 1800;
                    total_trash = 12;
                    trash_counter = 0;
                    can_drop = new Can_check(3, red_trash_chk, blue_trash_chk, yellow_trash_chk, orange_trash_chk);
                    bottle_drop = new Bottle_check(3, red_trash_chk, blue_trash_chk, yellow_trash_chk, orange_trash_chk);
                    paper_drop = new Paper_check(3, red_trash_chk, blue_trash_chk, yellow_trash_chk, orange_trash_chk);
                    food_drop = new Food_check(3, red_trash_chk, blue_trash_chk, yellow_trash_chk, orange_trash_chk);
                    can_drop.LoadContent(Content, spriteBatch);
                    bottle_drop.LoadContent(Content, spriteBatch);
                    paper_drop.LoadContent(Content, spriteBatch);
                    food_drop.LoadContent(Content, spriteBatch);
                    break;
            }

            temp_dis = temptime / 60;
        }

        public void Update(GameTime gameTime, out bool exit)
        {
            exit = false;

            if (timer < 0)
            {
                exit = true;
                return;
            }
            timer--;
            if ((trash_counter == total_trash))
            {
                exit = true;
                return;
            }
            if (level_meter == 3)
            {
                paper_drop.Update(gameTime, ourCursor);
                food_drop.Update(gameTime, ourCursor);
            }
            if ((level_meter == 2) || (level_meter == 3))
            {
                bottle_drop.Update(gameTime, ourCursor);
            }
            can_drop.Update(gameTime, ourCursor);
            ourCursor.Update(gameTime);

        }



        public void Draw()
        {


            spriteBatch.Draw(garden, rect, Color.White);

            switch (level_meter)
            {
                case 2: bottle_drop.Draw();
                    break;
                case 3: bottle_drop.Draw();
                    paper_drop.Draw();
                    food_drop.Draw();
                    break;
            }

            can_drop.Draw();
            if (temptime % 60 == 0)
            {
                temp_dis = temptime / 60;

            }
            spriteBatch.DrawString(font_time, "Timer: " + temp_dis, new Vector2(50, 530), Color.Red);
            temptime--;

            ourCursor.Draw(this.spriteBatch);
        }


    }
}
