using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace Bing_Bong
{
    class bubble_game
    {
        ContentManager Content;
        SpriteBatch spriteBatch;
        GameObject cleaner;
        int bubbleNo;
        const float maxBubbleHiegh = 0.0001f;
        const float minBubbleHiegh = 1.0f;
        const float maxBubbleVelocity = 3.0f;
        const float minBubbleVelocity = 1.0f;
        Random random = new Random();
        GameObject[] bubbles;
        public static int score;
        SpriteFont font;
        int temptime;
        int temp_dis;
        static Random rand = new Random();
        Vector2 spritePosition = Vector2.Zero;
        Vector2 mousePosition = Vector2.Zero;
        MouseState mouseStateCurrent, mouseStatePrevious;
        HorizontallyScrollingBackground mScrollingBackground;
        GameObject sun;
        SoundEffect bubbleVoice, warningVoice;
        int timer;
        Rectangle viewPortRect;
        int level_meter;
        public bubble_game(ContentManager theContent,int level_meter)
        {
            
            Content = theContent;
            score = 1;
            this.level_meter = level_meter;
            if (this.level_meter == 1)
            { timer = temptime=600;
                bubbleNo = 5;
                score = 0;
            }
            else
            { timer = temptime = 1500; 
                bubbleNo = 9; }
        }

        public void LoadContent(SpriteBatch thespriteBatch,Viewport viewport)
        {
            this.spriteBatch = thespriteBatch;
            viewPortRect = new Rectangle(0, 0, 800, 600);
            //Calling the Horizontal Background 
            mScrollingBackground = new HorizontallyScrollingBackground(viewport);

            // add the background picture 
            mScrollingBackground.AddBackground("Sprites\\sky");

            //Load the content for the Scrolling background
            mScrollingBackground.LoadContent(this.Content);

            // load the cleaner picture and cast to GameObject  
            cleaner = new GameObject(Content.Load<Texture2D>("Sprites\\greenClean1"));

            // load the sun picture and cast to GameObject  
            sun = new GameObject(Content.Load<Texture2D>("Sprites\\sun"));

            //load the content of the audio 
            bubbleVoice = Content.Load<SoundEffect>("Audios\\boble3");


            warningVoice = Content.Load<SoundEffect>("Audios\\warn");


            //create a new array for the bubbles of a size bubbleNo
            bubbles = new GameObject[bubbleNo];

            //load the content of the font 
          
            font = Content.Load<SpriteFont>(@"Fonts\ScoreFont");
            temp_dis = temptime / 60;

            //load the content of the bubles picture and add them to the array 
            for (int i = 0; i < bubbleNo; i++)
            {
                bubbles[i] = new GameObject(Content.Load<Texture2D>("Sprites\\bub" + (i + 1).ToString()));

            }


            //set the bubbles type to bad and good 
            for (int z = 0; z < bubbleNo; z++)
            {
                if (z < 5)
                {
                    bubbles[z].bad = true;
                }
                else
                {
                    bubbles[z].bad = false;
                }
            }
 
        }

        public void Update(GameTime gameTime,out bool exit)
        {
            exit = false;

            if (timer < 0)
            {
                exit = true; return;
            }


            mScrollingBackground.Update(gameTime, 100, HorizontallyScrollingBackground.
                HorizontalScrollDirection.Left);

#if !IXBOX
            mouseStateCurrent = Mouse.GetState();

            if (mouseStateCurrent.LeftButton == ButtonState.Pressed)
            {
                UpdateShooting();
            }

#endif

            

#if !XBOX

            mouseStatePrevious = mouseStateCurrent;

#endif

           
            timer--;
            UpdateMouse();
            UpdateBubbles();
            UpdateSprites();
            
        }

        // Update the mouse movement 
        public void UpdateMouse()
        {
            MouseState mouse = Mouse.GetState();
            mousePosition.X = mouse.X;
            mousePosition.Y = mouse.Y;
        }

        // Update the cleaner position 
        public void UpdateSprites()
        {
            spritePosition.X = mousePosition.X;
            spritePosition.Y = mousePosition.Y;
        }

        // shoot the bubbles 
        public void UpdateShooting()
        {

            Rectangle cleanerRec = new Rectangle((int)spritePosition.X+70, (int)spritePosition.Y,
                    cleaner.sprite.Width-100 , cleaner.sprite.Height - 120);

            foreach (GameObject bb in bubbles)
            {
                Rectangle bubbleRec = new Rectangle((int)bb.position.X, (int)bb.position.Y,
                    bb.sprite.Width, bb.sprite.Height);
                if (cleanerRec.Intersects(bubbleRec))
                {

                    bb.alive = false;
                    bb.killed = true;

                    if (bb.bad == true)
                    {
                        score = score + 1;
                        bubbleVoice.Play();
                        break;

                    }
                    else
                    {
                        score = score - 1;
                        warningVoice.Play();
                        break;
                    }
                }
            }
        }

        //============================================================


        //update the bubbles positions

        public void UpdateBubbles()
        {

            foreach (GameObject bubble in bubbles)
            {
                if (bubble.alive)
                {
                    bubble.position += bubble.velocity;
                    if (!viewPortRect.Contains(new Point((int)bubble.position.X,
                        (int)bubble.position.Y)))
                    {
                        bubble.alive = false;
                        continue;
                    }
                }

                else
                {
                    bubble.alive = true;
                    bubble.position = new Vector2(MathHelper.Lerp(0, (float)viewPortRect.Width,
                        (float)random.NextDouble()), viewPortRect.Bottom);

                    bubble.velocity = new Vector2(MathHelper.Lerp((float)minBubbleVelocity,
                        (float)-maxBubbleVelocity, (float)random.NextDouble()), 0);


                }

            }
        }


        public void Draw()
        {
            //Draw the scrolling background
            mScrollingBackground.Draw(spriteBatch);

            
            spriteBatch.Draw(sun.sprite, new Vector2(-110, -80), Color.White);



            foreach (GameObject bubb in bubbles)
            {
                if (bubb.alive)
                {
                    spriteBatch.Draw(bubb.sprite, bubb.position, Color.White);
                    bubb.position.Y = bubb.position.Y - 1;

                }
            }

            spriteBatch.Draw(cleaner.sprite, spritePosition, null, Color.White);

            if (temptime % 60 == 0)
            {
                temp_dis = temptime / 60;

            }
            spriteBatch.DrawString(font, "Timer: " + temp_dis, new Vector2(50, 530), Color.Red);

            spriteBatch.DrawString(font, "Score: " + score, new Vector2(690, 530), Color.Red);
            temptime--;
            
          
        }
    }
}
