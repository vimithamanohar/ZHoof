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
    class Level1_final
    {
        //The current position of the Sprite
        public Vector2 Position = new Vector2(0, 0);

        //The texture object used when drawing the sprite
        private Texture2D level1_final_background;
        private Texture2D Score_image;
        SpriteFont font;
        ContentManager theContentManager;
        int score_of_the_level;
        string message;
        public static int total_score=0;
        public static int max_total_score = 0;
        SoundEffect sound_effect;

        public Level1_final(ContentManager content)
        {
            theContentManager = content;
        }

       public void LoadContent(string theAssetName,string win,string lost,int levelnumber)
        {
            level1_final_background = theContentManager.Load<Texture2D>(theAssetName);
            font = theContentManager.Load<SpriteFont>("myFonts");
           
          
            if (levelnumber == 0)
            {
                total_score = 0;
                score_of_the_level = Level1.bag_count;
                message = "Bin Collected";
                if (Level1.bag_count > 0)
                {
                    sound_effect=theContentManager.Load<SoundEffect>("Audios\\chime");                    
                    Score_image = theContentManager.Load<Texture2D>(win);

                }
                else
                {
                    sound_effect = theContentManager.Load<SoundEffect>("Audios\\sad");
                    Score_image = theContentManager.Load<Texture2D>(lost);
                }

            
                Level1.bag_count = 0;
            }

            if (levelnumber == 1)
            {
                message = "         Amount of \nAir Pollution Removed";
                score_of_the_level = Deformable_Terrain.CleanAir.score;
                sound_effect = theContentManager.Load<SoundEffect>("Audios\\chime");   
                Score_image = theContentManager.Load<Texture2D>(win);
                Deformable_Terrain.CleanAir.score = 0;
            }

            if (levelnumber == 2)
            {
                message = "Score";
                score_of_the_level = Bing_Bong.bubble_game.score;
                if (Bing_Bong.bubble_game.score > 0)
                    Score_image = theContentManager.Load<Texture2D>(win);
                else
                    Score_image = theContentManager.Load<Texture2D>(lost);

                if (score_of_the_level < 15)
                    sound_effect = theContentManager.Load<SoundEffect>("Audios\\sad");
                else
                    sound_effect = theContentManager.Load<SoundEffect>("Audios\\chime");

                Bing_Bong.bubble_game.score = 0;
               
            }

            if (levelnumber == 3)
            {
                message = "Score";
                score_of_the_level = Bing_Bong.bubble_game.score;
                if (Bing_Bong.bubble_game.score > 0)
                    Score_image = theContentManager.Load<Texture2D>(win);
                else
                    Score_image = theContentManager.Load<Texture2D>(lost);

                if (score_of_the_level < 10)
                    sound_effect = theContentManager.Load<SoundEffect>("Audios\\sad");
                else
                    sound_effect = theContentManager.Load<SoundEffect>("Audios\\chime");

                Bing_Bong.bubble_game.score = 0;

            }

            if (levelnumber == 4) 
            {

                message = "Trash Collected";
                score_of_the_level = Trash_pick.Trash_spread.score; //RecycleBinSimple.RecycleFunction.score;

                if (score_of_the_level == 0)
                    sound_effect = theContentManager.Load<SoundEffect>("Audios\\sad");
                else
                    sound_effect = theContentManager.Load<SoundEffect>("Audios\\chime");

                Score_image = theContentManager.Load<Texture2D>(win);
                Trash_pick.Trash_spread.score = 0;
            //    RecycleBinSimple.RecycleFunction.score = 0;

            }

            if (levelnumber == 5)
            {
                message = "Trash Collected";
                score_of_the_level = Trash_pick.Trash_spread.score;//RecycleBin.RecycleFunction.score;
                Score_image = theContentManager.Load<Texture2D>(win);

                if (score_of_the_level < 50)
                    sound_effect = theContentManager.Load<SoundEffect>("Audios\\sad");
                else
                    sound_effect = theContentManager.Load<SoundEffect>("Audios\\chime");

               // RecycleBin.RecycleFunction.score = 0;
                Trash_pick.Trash_spread.score = 0;
            }

            if (levelnumber == 6)
            {
                message = "Trash Collected";
                score_of_the_level = Trash_pick.Trash_spread.score;//RecycleBin.RecycleFunction.score;
                Score_image = theContentManager.Load<Texture2D>(win);

                if (score_of_the_level < 50)
                    sound_effect = theContentManager.Load<SoundEffect>("Audios\\sad");
                else
                    sound_effect = theContentManager.Load<SoundEffect>("Audios\\chime");

                // RecycleBin.RecycleFunction.score = 0;
                Trash_pick.Trash_spread.score = 0;
            }

            if (levelnumber == 7)
            {
                message = "Number of Bags";
                score_of_the_level = Bing_Bong.Bing_Game.score;
                if (score_of_the_level > 9)
                {
                    sound_effect = theContentManager.Load<SoundEffect>("Audios\\chime");
                    Score_image = theContentManager.Load<Texture2D>(win);
                }
                else
                {
                    sound_effect = theContentManager.Load<SoundEffect>("Audios\\sad");
                    Score_image = theContentManager.Load<Texture2D>(lost);
                }

                Bing_Bong.Bing_Game.score = 0;
            }

            if (levelnumber == 8)
            {
                message = "Number of Bags";
                score_of_the_level = Bing_Bong.Bing_Game.score;
                if (score_of_the_level > 75)
                {
                    sound_effect = theContentManager.Load<SoundEffect>("Audios\\chime");
                    Score_image = theContentManager.Load<Texture2D>(win);
                }
                else
                {
                    sound_effect = theContentManager.Load<SoundEffect>("Audios\\sad");
                    Score_image = theContentManager.Load<Texture2D>(lost);
                }
                Bing_Bong.Bing_Game.score = 0;
                 
            }

           if (levelnumber == 9)
            {

                message = "Carbon Footprint Removed";
                score_of_the_level = WindowsGame2.blacky.score;
                if (WindowsGame2.blacky.score > 1500)
                {
                    sound_effect = theContentManager.Load<SoundEffect>("Audios\\chime");
                    Score_image = theContentManager.Load<Texture2D>(win);
                }
                else
                {
                    Score_image = theContentManager.Load<Texture2D>(lost);
                    sound_effect = theContentManager.Load<SoundEffect>("Audios\\sad");
                }
                WindowsGame2.blacky.score = 0;
            }
           
           if (levelnumber == 10) 
           {
               
               score_of_the_level = WindowsGame2.blacky2.score;
               if (WindowsGame2.blacky.score > 4500)
               {                     
                   message = "          Congrats...\n Carbon Footprint Removed";
                   Score_image = theContentManager.Load<Texture2D>(win);
                   
               }
               else
               {                 
                   message = "          Not bad,\n Carbon Footprint Removed";
                   Score_image = theContentManager.Load<Texture2D>(lost);
               }

               if (score_of_the_level > 1000)
                    sound_effect = theContentManager.Load<SoundEffect>("Audios\\clap"); 
               else
                    sound_effect = theContentManager.Load<SoundEffect>("Audios\\sad");

               WindowsGame2.blacky2.score = 0;
           }
           
           total_score = total_score + score_of_the_level;

          

           sound_effect.Play();           
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(level1_final_background, new Rectangle(0, 0, 800, 600), Color.White);
           
            theSpriteBatch.Draw(Score_image, new Rectangle(250,270,325,250), Color.White);

            theSpriteBatch.DrawString(font, message+": "+score_of_the_level, new Vector2(270,175), Color.OrangeRed);
          
        }

    }
}
