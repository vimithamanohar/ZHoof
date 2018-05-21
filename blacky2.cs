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

namespace WindowsGame2
{
    class blacky2
    {
        Rectangle[] rect;
        int no_of_rect;
        Texture2D rect_texure;
        Texture2D background;
        Texture2D burst;

        //ways rectangle
        Rectangle entry;
        Rectangle[] points_loc;
        Rectangle[] point_eatten;
        Rectangle closedoor;

        static int k = 0;
        int[] indexes;

        SoundEffect foot_hit, big_foot,bulb_burst;

        Texture2D pac_Up;
        Texture2D pac_Down;
        Texture2D pac_Left;
        Texture2D pac_Left2;
        Texture2D pac_Right;
        Texture2D pac_Right2;
        Texture2D pac_power;
        Texture2D close_door;
        Texture2D window_background;

        Texture2D points;

        int pac_X = 100;
        int pac_Y = 299;
        int movx = 0;
        int movy = 0;

        int no_of_points;
        int secondsPassed = 0;

        static int timelimit = 2400;
        int timecounter = 0;

        int temptime = timelimit;
        int temp_dis;
        SpriteFont font;



        //for the problems

        bool[] gtChange;
        bool[] changeDirection;

        enum pac_direction
        {
            up,
            down,
            right1,
            right2,
            left1,
            left2
        }

        bool walk_first_right = false;
        bool walk_first_left = false;
        int randomSeed = 0;
        double storedSecs = 0;
        public static int score;
        pac_direction pac_move;

        

        static int bulb_changed;

        Texture2D carbon;
        Texture2D carbon_change;
        bool[] carbonAlive;
        int[] carbonX;
        int[] carbonY;
        int[] carbonmoveX;
        int[] carbonmoveY;
        int[] carbonLastDirection;
        int no_of_carbons;
        bool[] forceCarbons;
        bool forced = false;
        bool endGame = false;
        double firstRun = 0;
        bool firstCheck = false;
        bool OpenDoor = false;
        bool shutDoor = false;
        bool edibleCarbons = false;

        public blacky2(int no_of_carbon)
        {
            score = 0;
            pac_move = new pac_direction();
            pac_move = pac_direction.right1;
            no_of_carbons = no_of_carbon;

            bulb_changed = 0;
            CreateCarbon(no_of_carbons);

            create_out_of_bound();

            create_point();
        }

        public void LoadContent(ContentManager theContentManager)
        {
            pac_Right = theContentManager.Load<Texture2D>("images\\pac1");
            pac_Right2 = theContentManager.Load<Texture2D>("images\\pac2");
            pac_Left = theContentManager.Load<Texture2D>("images\\pac3");
            pac_Left2 = theContentManager.Load<Texture2D>("images\\pac4");
            pac_Up = theContentManager.Load<Texture2D>("images\\pac5");
            pac_Down = theContentManager.Load<Texture2D>("images\\pac6");
            pac_power = theContentManager.Load<Texture2D>("images\\power");
            background = theContentManager.Load<Texture2D>("images\\back_of_wall");
            window_background = theContentManager.Load<Texture2D>("images\\score_background1");
            rect_texure = theContentManager.Load<Texture2D>("images\\rect");
            points = theContentManager.Load<Texture2D>("images\\footprint");
            close_door = theContentManager.Load<Texture2D>("images\\door");
            carbon = theContentManager.Load<Texture2D>("images\\bulb");
            carbon_change = theContentManager.Load<Texture2D>("images\\bulb_green");
            burst = theContentManager.Load<Texture2D>("images\\burst");

            foot_hit = theContentManager.Load<SoundEffect>("Audios\\try_bulb");
            big_foot = theContentManager.Load<SoundEffect>("Audios\\big_foot");
            bulb_burst = theContentManager.Load<SoundEffect>("Audios\\bulb_kill");

            font = theContentManager.Load<SpriteFont>(@"Fonts\ScoreFont");
            temp_dis = temptime / 60;
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
        
            theSpriteBatch.Draw(window_background, new Rectangle(0, 0, 800, 600), Color.White);
            theSpriteBatch.Draw(background, new Rectangle(138, 15, background.Width - 150, background.Height + 80), Color.White);

            if (OpenDoor || forced)
            {
                if (!shutDoor)
                    theSpriteBatch.Draw(close_door, closedoor, Color.White);
            }


            for (int i = 0; i < no_of_points; i++)
            {
                Rectangle tester = new Rectangle(1, 1, 1, 1); // this checks to see if the rectangle should be drawn. 
                if (points_loc[i] != tester)
                    theSpriteBatch.Draw(points, points_loc[i], Color.White);
            }

            for (int i = 0; i < no_of_carbons; i++)
            {
                if (carbonAlive[i])
                {
                    Texture2D tempcarbon;

                    if (!edibleCarbons)
                        tempcarbon = carbon;
                    else
                        tempcarbon = carbon_change;

                    theSpriteBatch.Draw(tempcarbon, new Rectangle(carbonX[i], carbonY[i], 20, 20), Color.White);

                }

                else
                {
                    if (bulb_changed < no_of_carbons)
                    {
                        theSpriteBatch.Draw(burst, new Rectangle(carbonX[i],carbonY[i],20,20), Color.White);
                        //bulb_burst.Play();
                        bulb_changed++;
                    }
                }

              
            }
            switch (pac_move)
            {
                case pac_direction.right1:
                    theSpriteBatch.Draw(pac_Right, new Rectangle(pac_X, pac_Y, pac_Right.Width-2, pac_Right.Height), Color.White);
                    break;
                case pac_direction.right2:
                    theSpriteBatch.Draw(pac_Right2, new Rectangle(pac_X, pac_Y, pac_Right2.Width-2, pac_Right2.Height), Color.White);
                    break;
                case pac_direction.left1:
                    theSpriteBatch.Draw(pac_Left, new Rectangle(pac_X, pac_Y, pac_Left.Width-2, pac_Left.Height), Color.White);
                    break;
                case pac_direction.left2:
                    theSpriteBatch.Draw(pac_Left2, new Rectangle(pac_X, pac_Y, pac_Left2.Width-2, pac_Left2.Height), Color.White);
                    break;
                case pac_direction.up:
                    theSpriteBatch.Draw(pac_Up, new Rectangle(pac_X, pac_Y, pac_Up.Width-3, pac_Up.Height), Color.White);
                    break;
                case pac_direction.down:
                    theSpriteBatch.Draw(pac_Down, new Rectangle(pac_X, pac_Y, pac_Down.Width-3, pac_Down.Height), Color.Black);
                    break;

            }

            for (int i = 0; i < no_of_rect; i++)
            {
                theSpriteBatch.Draw(rect_texure, rect[i], Color.White);
            }

            if (temptime % 60 == 0)
            {
                temp_dis = temptime / 60;

            }
            theSpriteBatch.DrawString(font, "Timer: " + temp_dis, new Vector2(15, 530), Color.Red);
            temptime--;


        }

        public void Update(GameTime gametime, out bool exit)
        {
            timecounter++;
            exit = false;
            if (timecounter > timelimit)
            {
                exit = true;
                return;
            }
            if (randomSeed > 2147483)
                randomSeed = 0;

            if (firstCheck == false)
            {
                firstCheck = true;
                firstRun = gametime.TotalGameTime.TotalSeconds;
            }



            KeyboardState currentstate = Keyboard.GetState();
            Keys[] currentkey = currentstate.GetPressedKeys();

            shutDoor = false;

            if (forced == true)
            {
                shutDoor = true;

                for (int i = 0; i < no_of_carbons; i++)
                {
                    if (forceCarbons[i] == true)
                        shutDoor = false;
                }

                if (shutDoor == true)
                    rect[42] = new Rectangle(458, 276, 42, 9);



            }

            if (!endGame)
            {
                if (forced == false)
                {
                    if (storedSecs == 0)
                        storedSecs = gametime.TotalGameTime.TotalSeconds;

                    if (gametime.TotalGameTime.TotalSeconds < storedSecs + 2)
                    {
                        if (gametime.TotalGameTime.Milliseconds % 250 == 0)
                        {
                            if (OpenDoor)
                            {
                                OpenDoor = false;
                            }
                            else
                            {
                                OpenDoor = true;
                            }
                        }
                    }
                    else
                    {
                        rect[42] = new Rectangle(1, 1, 1, 1);
                        for (int i = 0; i < no_of_carbons; i++)
                        {
                            forceCarbons[i] = true;
                        }
                        forced = true;
                    }
                }
            }

            Movecarbon();

            int startsecs = 100;
            int[] moveSecs = new int[no_of_carbons];

            for (int i = 0; i < no_of_carbons; i++)
            {
                moveSecs[i] = startsecs + i * 50;
            }

            for (int i = 0; i < no_of_carbons; i++)
            {
                if (gametime.TotalGameTime.Milliseconds % moveSecs[i] == 0)
                    gtChange[i] = true;
            }


            foreach (Keys keys in currentkey)
            {
                if (keys == Keys.Up)
                {
                    pac_move = pac_direction.up;
                    movy = -5;
                    movx = 0;
                    if (CheckWalls(pac_X, pac_Y, movx, movy, pac_Up, true, 0)) { pac_Y += movy; }
                }

                if (keys == Keys.Down)
                {
                    pac_move = pac_direction.down;
                    movy = 5;
                    movx = 0;
                    if (CheckWalls(pac_X, pac_Y, movx, movy, pac_Down, true, 0)) { pac_Y += movy; }
                }
                if (keys == Keys.Left)
                {
                    movy = 0;
                    movx = -5;
                    if (walk_first_left)
                    {
                        pac_move = pac_direction.left1;
                        walk_first_left = false;
                        if (CheckWalls(pac_X, pac_Y, movx, movy, pac_Left, true, 0)) { pac_X += movx; }
                    }
                    else
                    {
                        pac_move = pac_direction.left2;
                        walk_first_left = true;
                        if (CheckWalls(pac_X, pac_Y, movx, movy, pac_Left2, true, 0)) { pac_X += movx; }
                    }



                }
                if (keys == Keys.Right)
                {

                    movy = 0;
                    movx = 5;
                    if (walk_first_right)
                    {
                        pac_move = pac_direction.right1;
                        walk_first_right = false;
                        if (CheckWalls(pac_X, pac_Y, movx, movy, pac_Right, true, 0)) { pac_X += movx; }
                    }
                    else
                    {
                        pac_move = pac_direction.right2;
                        walk_first_right = true;
                        if (CheckWalls(pac_X, pac_Y, movx, movy, pac_Right, true, 0)) { pac_X += movx; }
                    }
                }
            }

            if (gametime.TotalGameTime.Milliseconds % 1000 == 0)
            {
                if (edibleCarbons)
                {
                    secondsPassed++;
                    if (secondsPassed == 5) // number of seconds that Carbons are edible! 
                    {
                        secondsPassed = 0;
                        edibleCarbons = false;
                    }
                }
            }

            //check the boundry of the screen
            if (pac_X < 0) pac_X = 0;
            if (pac_Y < 0) pac_Y = 0;
            if (pac_X + movx > 760) pac_X = 760;
            if (pac_Y + movy > 560) pac_Y = 560;

        }

        //to check the walls 
        bool CheckWalls(int pac_current_x, int pac_current_y, int more_x, int more_y, Texture2D character, bool isSprite, int carbonID)
        {
            Rectangle chk_rect = new Rectangle(pac_current_x + more_x, pac_current_y + more_y, character.Width, character.Height);

            bool ret = true;

            if (isSprite)
            {
                for (int i = 0; i < no_of_points; i++)
                {
                    if (chk_rect.Intersects(points_loc[i]))
                    {
                        if ((points_loc[i] == new Rectangle(177 + (0 * 24) - 4, 42 + (0 * 21) - 4, 16, 15)) ||
                               (points_loc[i] == new Rectangle(177 + (20 * 24) - 4, 42 + (0 * 21) - 4, 16, 15)) ||
                               (points_loc[i] == new Rectangle(177 + (0 * 24) - 4, 42 + (22 * 21) - 4, 16, 15)) ||
                               (points_loc[i] == new Rectangle(177 + (20 * 24) - 4, 42 + (25 * 21) - 4, 16, 15)))
                        {
                            big_foot.Play();
                            score += 25;
                            CarbonsAreEdible(true);
                        }
                        else
                        {
                            foot_hit.Play();
                            score += 10;
                        }

                        point_eatten[k] = points_loc[i];
                        indexes[k++] = i;
                        points_loc[i] = new Rectangle(1, 1, 1, 1);
                        score = score + 10;
                    }
                }
            }

            for (int i = 0; i < no_of_rect; i++)
            {
                if (chk_rect.Intersects(rect[i]))
                    ret = false;
            }

            if (!isSprite)
            {
                if (chk_rect.Intersects(new Rectangle(pac_X, pac_Y, pac_Right.Width, pac_Right.Height)))
                {
                    ret = false;
                    if (edibleCarbons)
                    {                      
                        carbonAlive[carbonID] = false;
                        bulb_burst.Play();
                    }
                }

                // to re create the carbon foot-prints
                for (int i = 0; i < k; i++)
                {
                    if (chk_rect.Intersects(point_eatten[i]))
                    {
                        points_loc[indexes[i]] = point_eatten[i];
                        point_eatten[i] = new Rectangle(1, 1, 1, 1);
                        score = score - 20;
                    }
                }

            }


            return ret;
        }


        void create_out_of_bound()
        {
            no_of_rect = 43;
            rect = new Rectangle[no_of_rect];
            rect[0] = new Rectangle(138, 15, 25, 207);
            rect[1] = new Rectangle(200, 66, 78, 42);
            rect[2] = new Rectangle(320, 66, 102, 42);
            rect[3] = new Rectangle(536, 66, 102, 42);
            rect[4] = new Rectangle(200, 150, 78, 21);
            rect[5] = new Rectangle(392, 150, 174, 21);
            rect[6] = new Rectangle(138, 0, 568, 25);
            rect[7] = new Rectangle(680, 0, 25, 207);
            rect[8] = new Rectangle(464, 15, 30, 93);
            rect[9] = new Rectangle(138, 213, 140, 21);
            rect[10] = new Rectangle(320, 213, 102, 21);
            rect[11] = new Rectangle(536, 213, 102, 21);
            rect[12] = new Rectangle(138, 414, 25, 249);
            rect[13] = new Rectangle(680, 200, 25, 400);
            rect[14] = new Rectangle(320, 150, 30, 147);
            rect[15] = new Rectangle(608, 150, 30, 147);
            rect[16] = new Rectangle(464, 150, 30, 84);
            rect[17] = new Rectangle(608, 339, 30, 84);
            rect[18] = new Rectangle(320, 339, 30, 84);
            rect[19] = new Rectangle(392, 402, 174, 21);
            rect[20] = new Rectangle(200, 465, 78, 21);
            rect[21] = new Rectangle(138, 276, 140, 21);
            rect[22] = new Rectangle(138, 339, 140, 21);
            rect[23] = new Rectangle(138, 402, 140, 21);
            rect[24] = new Rectangle(248, 339, 30, 84);
            rect[25] = new Rectangle(248, 213, 30, 84);
            rect[26] = new Rectangle(320, 465, 102, 21);
            rect[27] = new Rectangle(536, 465, 102, 21);
            rect[28] = new Rectangle(410, 591, 290, 21);
            rect[29] = new Rectangle(138, 591, 300, 21);
            rect[30] = new Rectangle(392, 528, 174, 21);
            rect[31] = new Rectangle(150, 528, 56, 21);
            rect[32] = new Rectangle(248, 469, 30, 80);
            rect[33] = new Rectangle(464, 406, 30, 80);
            rect[34] = new Rectangle(464, 532, 30, 80);
            rect[35] = new Rectangle(320, 528, 30, 80);
            rect[36] = new Rectangle(608, 528, 30, 80);
            rect[37] = new Rectangle(392, 276, 9, 84);
            rect[38] = new Rectangle(557, 276, 9, 84);
            rect[39] = new Rectangle(392, 351, 170, 9);
            rect[40] = new Rectangle(392, 276, 66, 9);
            rect[41] = new Rectangle(500, 276, 66, 9);
            rect[42] = new Rectangle(458, 276, 42, 9);

            entry = new Rectangle(200, 297, 10, 42);//entrance 
            // exit = new Rectangle(750, 297, 10, 42);//exit
            closedoor = new Rectangle(458, 276, 42, 9);//problem place
        }


        void CreateCarbon(int howMany)
        {
            no_of_carbons = howMany;
            carbonAlive = new bool[no_of_carbons];
            carbonX = new int[no_of_carbons];
            carbonY = new int[no_of_carbons];
            carbonmoveX = new int[no_of_carbons];
            carbonmoveY = new int[no_of_carbons];
            carbonLastDirection = new int[no_of_carbons];
            gtChange = new bool[no_of_carbons];
            changeDirection = new bool[no_of_carbons];
            forceCarbons = new bool[no_of_carbons];

            for (int i = 0; i < no_of_carbons; i++)
            {
                carbonAlive[i] = true;
                carbonX[i] = 400 + i * 30;
                if (carbonX[i] > 500)
                    carbonX[i] = 400;

                carbonY[i] = 300;
                carbonmoveX[i] = 0;
                carbonmoveY[i] = 0;
                carbonLastDirection[i] = -1;
                gtChange[i] = false;
                changeDirection[i] = false;
                forceCarbons[i] = false;
            }

        }

        void create_point()
        {
            no_of_points = 546; // 29 * 27 [grid of points] 
            points_loc = new Rectangle[no_of_points];
            point_eatten = new Rectangle[no_of_points];
            indexes = new int[no_of_points];

            int counter = -1;
            int x = 177;
            int y = 42;
            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < 21; j++)
                {
                    counter++;

                    Rectangle tempRect;

                    if ((j == 0 && i == 0) || (j == 20 && i == 0) || (j == 0 && i == 22) || (j == 20 && i == 25))
                        tempRect = new Rectangle(x + (j * 24) - 4, y + (i * 21) - 4, 16, 15);
                    else
                        tempRect = new Rectangle(x + (j * 24), y + (i * 21), 12,12);

                    bool flag = false;

                    for (int t = 0; t < no_of_rect; t++)
                    {
                        if (tempRect.Intersects(rect[t]))
                            flag = true;
                    }

                    if (!flag)
                        points_loc[counter] = tempRect;
                    else
                        counter--;
                }
            }

            //hiding some unwanted footprints
            points_loc[109] = new Rectangle(1, 1, 1, 1);
            points_loc[110] = new Rectangle(1, 1, 1, 1);
            points_loc[111] = new Rectangle(1, 1, 1, 1);
            points_loc[112] = new Rectangle(1, 1, 1, 1);
            points_loc[113] = new Rectangle(1, 1, 1, 1);
            points_loc[114] = new Rectangle(1, 1, 1, 1);
            points_loc[126] = new Rectangle(1, 1, 1, 1);
            points_loc[127] = new Rectangle(1, 1, 1, 1);
            points_loc[128] = new Rectangle(1, 1, 1, 1);
            points_loc[129] = new Rectangle(1, 1, 1, 1);
            points_loc[130] = new Rectangle(1, 1, 1, 1);
            points_loc[131] = new Rectangle(1, 1, 1, 1);
            points_loc[138] = new Rectangle(1, 1, 1, 1);
            points_loc[139] = new Rectangle(1, 1, 1, 1);
            points_loc[140] = new Rectangle(1, 1, 1, 1);
            points_loc[141] = new Rectangle(1, 1, 1, 1);
            points_loc[142] = new Rectangle(1, 1, 1, 1);
            points_loc[143] = new Rectangle(1, 1, 1, 1);


        }


        void Movecarbon()
        {
            for (int i = 0; i < no_of_carbons; i++)
            {
                if (carbonAlive[i])
                {
                    if (forceCarbons[i] == false)
                    {
                        if (changeDirection[i])
                        {
                            changeDirection[i] = false; // makes sure we can still move

                            randomSeed++;

                            Random rnd = new Random(randomSeed);

                            int randomDirection = rnd.Next(0, 4);

                            if ((carbonLastDirection[i] == 0) || (carbonLastDirection[i] == 1))
                                randomDirection = rnd.Next(2, 4);
                            else
                                randomDirection = rnd.Next(0, 2);

                            if (randomDirection == 0)
                            {
                                carbonmoveY[i] = -3;
                                carbonmoveX[i] = 0;
                            }
                            if (randomDirection == 1)
                            {
                                carbonmoveY[i] = 3;
                                carbonmoveX[i] = 0;
                            }
                            if (randomDirection == 2)
                            {
                                carbonmoveY[i] = 0;
                                carbonmoveX[i] = -3;
                            }
                            if (randomDirection == 3)
                            {
                                carbonmoveY[i] = 0;
                                carbonmoveX[i] = 3;
                            }

                            carbonLastDirection[i] = randomDirection;
                        }

                        if (CheckWalls(carbonX[i], carbonY[i], carbonmoveX[i], carbonmoveY[i], carbon, false, i))
                        {
                            Rectangle tempRect = new Rectangle(carbonX[i], carbonY[i], carbon.Width, carbon.Height);

                            carbonX[i] = carbonX[i] + carbonmoveX[i];

                            carbonY[i] = carbonY[i] + carbonmoveY[i];
                        }
                        else
                            changeDirection[i] = true;


                        if (gtChange[i])
                        {
                            gtChange[i] = false;
                            randomSeed++;
                            Random rndShallWe = new Random(randomSeed);
                            if (rndShallWe.Next(0, 100) > 50)
                                changeDirection[i] = true;
                        }
                    }
                }
            }

            int doorLeft = 458;
            int doorWidth = 42;
            int doorTop = 276;

            for (int i = 0; i < no_of_carbons; i++)
            {
                if (forceCarbons[i] == true)
                {
                    if ((carbonX[i] > doorLeft) && (carbonX[i] + carbon.Width < doorLeft + doorWidth))
                    {
                        carbonmoveX[i] = 0;
                        carbonmoveY[i] = -5;
                    }
                    else if (carbonX[i] > doorLeft)
                    {
                        carbonmoveX[i] = -5;
                        carbonmoveY[i] = 0;
                    }
                    else if (carbonX[i] < doorLeft)
                    {
                        carbonmoveX[i] = 5;
                        carbonmoveY[i] = 0;
                    }

                    carbonX[i] = carbonX[i] + carbonmoveX[i];
                    carbonY[i] = carbonY[i] + carbonmoveY[i];

                    if (carbonY[i] + carbon.Height < doorTop)
                    {
                        carbonLastDirection[i] = 0;
                        forceCarbons[i] = false;
                    }
                }
            }
        }

        void CarbonsAreEdible(bool edible)
        {
            if (edible)
            {
                edibleCarbons = true;
            }
            else
            {
                edibleCarbons = false;
            }
        }
    }
}
