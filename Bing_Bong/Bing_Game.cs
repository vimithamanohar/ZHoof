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

namespace Bing_Bong
{
    class Bing_Game
    {
        ContentManager Content;
        Rectangle viewPortRect;
        SpriteBatch spriteBatch;
        GameObject bag;
        GameObject stick;
        GameObject can;
        GameObject leaf; 
        GameObject backGround; 
        int[,] items;
        static Random rand = new Random();
        static int ballX;
        static int ballY;
        int timer_limit;
        public static int score;
        SpriteFont font;
        int temptime;
        int temp_dis;
        int rowNum;
        int colNum;
        int timer;
        Rectangle rectangle;
        Rectangle recLeaf;
        Rectangle recStick;
        KeyboardState previousKeyboardState = Keyboard.GetState();
        SoundEffect hit, scoreSound;


         
        
        //Constructor 
        public Bing_Game(ContentManager theContent,int row,int col, int timeLimit)
        {
           
            timer = 0;
            timer_limit = timeLimit;
            temptime = timeLimit;
            rowNum = row;
            colNum = col;
            Content = theContent;

 
        }



        //Load the content 
        public void LoadContent(SpriteBatch theSpriteBatch)
        {
           
            this.spriteBatch = theSpriteBatch;

            viewPortRect = new Rectangle(0, 0, 800,600);

            //load the content of the hit sound 
            hit = Content.Load<SoundEffect>("Audios\\Hit");


            //load the content of the score sound 
            scoreSound = Content.Load<SoundEffect>("Audios\\Hit");

            // load the Stick picture and cast to GameObject  
            stick = new GameObject(Content.Load<Texture2D>("Sprites\\stick"));

            // load the paper bag picture and cast to GameObject  
            bag = new GameObject(Content.Load<Texture2D>("Sprites\\PaperBag"));

            // load the leaf picture and cast to GameObject  
            leaf = new GameObject(Content.Load<Texture2D>("Sprites\\leafy"));

            // load the leaf picture and cast to GameObject  
            can = new GameObject(Content.Load<Texture2D>("Sprites\\can"));

            
            //load the background
            backGround = new GameObject(Content.Load<Texture2D>("Sprites\\BackGround"));

            //do the array of the items 
            stick.velocity.X = 10;

            leaf.velocity.X = (int)rand.Next(3, 7);

            leaf.velocity.Y = (int)rand.Next(3, 7);

            stick.position = new Vector2(350, 573);

            leaf.position = new Vector2(stick.position.X, stick.position.Y - 100);

            items = new int[rowNum, colNum];

            //create a dynamic values for the game matrix. 
            for (int i = 0; i < rowNum; i++)
            {
                for (int y = 0; y < colNum; y++)
                {

                    items[i, y] = (int)rand.Next(1, 12);
                }
            }


            font = Content.Load<SpriteFont>(@"Fonts\ScoreFont");
            temp_dis = temptime / 60;


        }//end load 

        //=================================================================

        public void Update(GameTime gameTime, out bool exit)
        {
            timer_limit--;

            exit = false;

            if (timer_limit < 0)
            {
                exit = true; return;
            }

           

#if !IXBOX
            KeyboardState keyboardState = Keyboard.GetState();
            updateStick();

#endif
            UpdateLeaf(ref exit);
            killItems();

           
        }


        //============================================================
        public void killItems()
        {

            recLeaf = new Rectangle((int)leaf.position.X, (int)leaf.position.Y, 31, 34);

            ballX = 0;
            ballY = 0;
            //bool done = false; 

            for (int i = 0; i < rowNum; i++)
            {
                for (int y = 0; y < colNum; y++)
                {
                    rectangle = new Rectangle(ballX, ballY, 50, 55);

                    if (items[i, y] == 1)
                    {
                        if (recLeaf.Intersects(rectangle))
                        {

                            leaf.velocity.Y *= -1;

                        }
                    }
                    else
                    {
                        if (recLeaf.Intersects(rectangle))
                        {
                            items[i, y] = 0;

                        }
                    }

                    ballX = ballX + 50;


                }

                ballX = 0;
                ballY = ballY + 55;
            }

        }//end killItems 

        //============================================================



        protected void updateStick()
        {

            //the current state of the keyboard
            KeyboardState keyboard = Keyboard.GetState();


            //the keys being pressed by the user 
            Keys[] oneKey = keyboard.GetPressedKeys();


            //loop through array of keys, and move the stick 
            for (int i = 0; i < oneKey.Length; i++)
            {

                //move the stick left and right 
                if (oneKey[i] == Keys.Right)
                {
                    stick.position.X += stick.velocity.X;
                    stick.position.Y = 573;
                    if (stick.position.X > 650)
                    {
                        stick.position.X = 650;
                    }
                }

                if (oneKey[i] == Keys.Left)
                {
                    stick.position.X -= stick.velocity.X;
                    stick.position.Y = 573;
                    if (stick.position.X < 0)
                    {
                        stick.position.X = 0;
                    }
                }

            }
        }

        //=====================================================================

        public void UpdateLeaf(ref bool exit)
        {
            recStick = new Rectangle((int)stick.position.X, (int)stick.position.Y, 153, 27);

            recLeaf = new Rectangle((int)leaf.position.X, (int)leaf.position.Y, 31, 34);


            //update positions

            leaf.position.Y += leaf.velocity.Y;
            leaf.position.X += leaf.velocity.X;

            //check the boundaries

            //check the bottom
            if (leaf.position.Y >= viewPortRect.Height || leaf.position.Y + recLeaf.Height >
                stick.position.Y + recStick.Height)
            {
                {
                    leaf.killed = true;
                    exit = true; return;
                }
            }
            else
            {
                //check if the leaf hits the stick, if yes give a negative value to Y velocity
                if (recStick.Intersects(recLeaf))
                {
                    //play the hit sound 
                    hit.Play();

                    if (leaf.velocity.Y > 0)
                    {
                        leaf.velocity.Y *= -1;
                    }
                    else
                    {
                        leaf.velocity.Y *= 1;
                    }

                    
                }
                
            }

            //check the top
            if (leaf.position.Y <= 0)
            {
                leaf.velocity.Y *= -1;
            }

            //check right 
            if (leaf.position.X > viewPortRect.Width)
            {
                leaf.velocity.X *= -1;
            }

            //check left 
            if (leaf.position.X < 0)
            {
                leaf.velocity.X *= -1;
            }

        }//end of updateLeaf



        //====================================================================


        public void Draw()
        {

            spriteBatch.Draw(backGround.sprite, viewPortRect, Color.White);
           

            if (leaf.killed == false)
            {

                spriteBatch.Draw(leaf.sprite, leaf.position, Color.White);
            }


            //draw the stick and the cans on the screen  
            spriteBatch.Draw(stick.sprite, stick.position, Color.White);

            //draw the Background of the screen 

            ballX = 0;
            ballY = 0;
            score = 0;
            for (int i = 0; i < rowNum; i++)
            {
                for (int y = 0; y < colNum; y++)
                {
                    rectangle = new Rectangle(ballX, ballY, 50, 55);

                    if (items[i, y] == 1)
                    {

                        spriteBatch.Draw(can.sprite, rectangle, Color.White);
                    }
                    else
                    {
                        if (items[i, y] != 0)
                        {
                            spriteBatch.Draw(bag.sprite, rectangle, Color.White);
                        }
                        if (items[i, y] == 0)
                        {
                            score++;
                        }
                    }

                    ballX = ballX + 50;


                }

                ballX = 0;
                ballY = ballY + 55;
            }

            

            if (temptime % 60 == 0)
            {
                temp_dis = temptime / 60;

            }
            spriteBatch.DrawString(font, "Timer: " + temp_dis, new Vector2(15, 530), Color.Red);
            temptime--;

        }

    }
    
}
