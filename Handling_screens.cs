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
    class Handling_screens
    {
        SpriteBatch spriteBatch;
        Main_menu mm;
        Menu_button[] mainButton = new Menu_button[3];
        Menu_button level_button;
        Menu_button next_level_button;
        Menu_button restart_button;
        Mouse_Handler ourMouse;
        SoundEffect soundEngine;
        SoundEffectInstance soundEngineInstance;
       

        int x, y;
        bool timechk;
        bool help_screen;

        List<Menu_button> buttonlist;
        List<Menu_button> level_buttonlist;
        List<Menu_button> next_level_buttonlist;
        List<Menu_button> restart_buttonlist;

        Level1 catch_bag_l1;
        Level1_final l1_final;
        Level1_instruction l1_instr;

        Deformable_Terrain.CleanAir clean_air_l2;
        Bing_Bong.bubble_game bubble_l3;
        Bing_Bong.bubble_game bubble_l4;
        Trash_pick.Trash_spread Trash_pick_l5;
        Trash_pick.Trash_spread Trash_pick_l6;
        Trash_pick.Trash_spread Trash_pick_l7;
        Bing_Bong.Bing_Game bing_bong_l8;
        Bing_Bong.Bing_Game bing_bong_l9;
        WindowsGame2.blacky blacky_l10;
        WindowsGame2.blacky2 blacky_l11;

        Last_screen last_screen;

        ContentManager Content;
        SoundEffect button_click;

        string[] level_name;
        const int no_of_level = 11;

        enum level_state
        {
            main_screen,
            level_final,
            level_instruction,
            bin_level1_screen, //bin
            clean_air_level2_screen,    //clean
            bubble_1_level3_screen, //bubble
            bubble_2_level4_screen, //bubble
            trash_1_level5_screen, //trash
            trash_3_level6_screen, //trash
            trash_4_level7_screen, //trash
            bin_bong_1_level8_screen, //bing
            bing_bong_3_level9_screen, //bing
            blacky_1_level0_screen, //black
            blacky_3_level11_screen, //black
            last_level
        }

        level_state myLevelScreen;

        int level_number;


        MouseState mouseState;
        Texture2D tex, tex_hover, tex_click;


        public Handling_screens(ContentManager theContentManager)
        { 
           
            Content = theContentManager;
            create_main_screen_objects();            
           
        }

        public void create_main_screen_objects()
        {
            x = -50;
            y = 380;
            level_number = 0;
            level_name = new string[no_of_level];
            this.buttonlist = new List<Menu_button>();
            this.level_buttonlist = new List<Menu_button>();
            this.next_level_buttonlist = new List<Menu_button>();
            this.restart_buttonlist = new List<Menu_button>();

            for (int i = 0; i < no_of_level; i++)
            {
                level_name[i] = "images\\level" + i.ToString();
             }

            soundEngine = Content.Load<SoundEffect>("Audios\\theme");
            
            mm = new Main_menu();

            ourMouse = new Mouse_Handler(Content);
            help_screen = false;

            catch_bag_l1 = new Level1(Content);
            l1_final = new Level1_final(Content);
            l1_instr = new Level1_instruction(); 
            last_screen = new Last_screen(Content);
            button_click = Content.Load<SoundEffect>("Audios\\button");

        }

        public void LoadContent(SpriteBatch theSpriteBatch)
        {
            spriteBatch = theSpriteBatch;
            mm.LoadContent(Content, @"images\main_background");
            ourMouse.LoadContent();

            catch_bag_l1.LoadContent(@"images\level1_background");
            soundEngineInstance=soundEngine.Play(0.1f, 0.0f, 0.0f, true);
            
            for (int i = 0; i <= 5; i++)
            {
                tex = Content.Load<Texture2D>("images\\Button" + i);
                tex_hover = Content.Load<Texture2D>("images\\Button" + i + "a");
                tex_click = Content.Load<Texture2D>("images\\Button" + i + "b");

                x += 180;
                if (i == 3)
                {
                    level_button = new Menu_button(new Vector2(650, 60), tex, tex_hover, tex_click, "Button" + i);
                    this.level_buttonlist.Add(level_button);
                }
                else if (i == 4)
                {
                    next_level_button = new Menu_button(new Vector2(555, 150), tex, tex_hover, tex_click, "Button" + i);
                    this.next_level_buttonlist.Add(next_level_button);
                }
                else if (i == 5)
                {
                     restart_button = new Menu_button(new Vector2(655, 150), tex, tex_hover, tex_click, "Button" + i);
                    this.restart_buttonlist.Add(restart_button);
                }
                else
                { 
                    mainButton[i] = new Menu_button(new Vector2(x, y), tex, tex_hover, tex_click, "Button" + i);
                    this.buttonlist.Add(mainButton[i]);
                }
            }

            myLevelScreen = level_state.main_screen;
        }

        public void Draw(out bool exit_game, out bool restart_game, GameTime gametime, Game1 gaming)
        {

            switch (myLevelScreen)
            {
                case level_state.main_screen: mm.Draw(this.spriteBatch);
                    foreach (Menu_button b in buttonlist)
                    {
                        b.Draw(spriteBatch, b.position, b.tex);

                        if (b.Hovering)
                        {
                            b.Draw(spriteBatch, b.position, b.hover);
                        }

                        if (b.Clicking)
                        {
                            button_click.Play();
                            b.Draw(spriteBatch, b.position, b.click);
                            if (b.Button_Function.Equals("Start"))
                            {
                                myLevelScreen = level_state.level_instruction;
                                    l1_instr.LoadContent(this.Content, "images\\inst_background", level_name[level_number]);

                            }
                            if (b.Button_Function.Equals("Help"))
                            {
                                myLevelScreen = level_state.level_instruction;
                                l1_instr.LoadContent(this.Content, "images\\help","help_screen");
                                help_screen = true;
                                b.Clicking = false;
                            }
                            if (b.Button_Function.Equals("Exit"))
                            {
                                exit_game = true;
                                restart_game = false;
                                return; 
                            }
                        }
                    }
                    ourMouse.Draw(spriteBatch);
                    break;

                case level_state.level_instruction: l1_instr.Draw(spriteBatch);

                    foreach (Menu_button b in level_buttonlist)
                    {
                        b.Draw(spriteBatch, b.position, b.tex);
                        if (b.Hovering)
                        {
                            b.Draw(spriteBatch, b.position, b.hover);
                        }

                        if (b.Clicking)
                        {
                            button_click.Play();
                            b.Draw(spriteBatch, b.position, b.click);
                            if (b.Button_Function.Equals("Next"))
                            {
                                if (help_screen)
                                {
                                    myLevelScreen = level_state.main_screen;
                                    help_screen = false;
                                }
                                else
                                {
                                    switch (level_number)
                                    {
                                        case 0: myLevelScreen = level_state.bin_level1_screen;
                                            break;

                                        case 1: myLevelScreen = level_state.clean_air_level2_screen;                                          
                                            clean_air_l2 = new Deformable_Terrain.CleanAir();
                                            clean_air_l2.LoadContent(Content, gaming);
                                            break;

                                        case 2: myLevelScreen = level_state.bubble_1_level3_screen;
                                            bubble_l3 = new Bing_Bong.bubble_game(Content, 1);
                                            bubble_l3.LoadContent(spriteBatch, gaming.GraphicsDevice.Viewport); 
                                            break;

                                        case 3: myLevelScreen = level_state.bubble_2_level4_screen;
                                            bubble_l4 = new Bing_Bong.bubble_game(Content, 2);  
                                            bubble_l4.LoadContent(spriteBatch, gaming.GraphicsDevice.Viewport);
                                            break;

                                        case 4: myLevelScreen = level_state.trash_1_level5_screen;
                                            Trash_pick_l5 = new Trash_pick.Trash_spread(1);
                                            Trash_pick_l5.LoadContent(Content, spriteBatch);
                                            break;

                                        case 5:myLevelScreen = level_state.trash_3_level6_screen;
                                            Trash_pick_l6 = new Trash_pick.Trash_spread(2);
                                            Trash_pick_l6.LoadContent(Content, spriteBatch);
                                            break;

                                        case 6: myLevelScreen = level_state.trash_4_level7_screen;
                                            Trash_pick_l7 = new Trash_pick.Trash_spread(3);
                                            Trash_pick_l7.LoadContent(Content, spriteBatch);
                                            break;

                                        case 7: myLevelScreen = level_state.bin_bong_1_level8_screen;
                                            bing_bong_l8 = new Bing_Bong.Bing_Game(Content, 1, 16, 1500);
                                              bing_bong_l8.LoadContent(spriteBatch);
                                            break;

                                        case 8: myLevelScreen = level_state.bing_bong_3_level9_screen;
                                            bing_bong_l9 = new Bing_Bong.Bing_Game(Content, 6, 16, 3000);
                                            bing_bong_l9.LoadContent(spriteBatch);
                                            break;

                                        case 9: myLevelScreen = level_state.blacky_1_level0_screen;
                                            blacky_l10 = new WindowsGame2.blacky();
                                            blacky_l10.LoadContent(Content);
                                            break;

                                        case 10: myLevelScreen = level_state.blacky_3_level11_screen;
                                            blacky_l11 = new WindowsGame2.blacky2(5);
                                            blacky_l11.LoadContent(Content);
                                            break;
                                    }
                                }
                                
                                b.Clicking = false; break;
                            }
                        }
                    }

                   ourMouse.Draw(this.spriteBatch);
                    break;

                case level_state.level_final: l1_final.Draw(spriteBatch);

                    foreach (Menu_button b in next_level_buttonlist)
                    {
                        b.Draw(spriteBatch, b.position, b.tex);
                        if (b.Hovering)
                        {
                            b.Draw(spriteBatch, b.position, b.hover);
                        }

                        if (b.Clicking)
                        {
                            button_click.Play();
                            b.Draw(spriteBatch, b.position, b.click);
                            if (b.Button_Function.Equals("NextLevel"))
                            {
                                if (level_number >= no_of_level)
                                {
                                    myLevelScreen = level_state.last_level;
                                    clean_air_l2.restart_level();
                                    soundEngineInstance.Stop();
                                    last_screen.LoadContent();
                                }

                                else if (level_number < no_of_level)
                                {
                                    myLevelScreen = level_state.level_instruction;
                                    l1_instr.LoadContent(Content, "images\\inst_background", level_name[level_number]);
                                }
                                b.Clicking = false; break;
                            }
                        }
                    }
                    ourMouse.Draw(this.spriteBatch);
                    break;

                case level_state.bin_level1_screen: catch_bag_l1.Draw(spriteBatch);
                    break;

                case level_state.clean_air_level2_screen: clean_air_l2.Draw(spriteBatch, gametime);
                    break;

                case level_state.bubble_1_level3_screen: bubble_l3.Draw();
                    break;

                case level_state.bubble_2_level4_screen: bubble_l4.Draw();
                    break;

                case level_state.trash_1_level5_screen: Trash_pick_l5.Draw(); //trash_collect_l5.Draw(spriteBatch);
                    break;

                case level_state.trash_3_level6_screen: Trash_pick_l6.Draw(); //trash_collect_l6.Draw(spriteBatch);
                    break;

                case level_state.trash_4_level7_screen: Trash_pick_l7.Draw(); //trash_collect_l6.Draw(spriteBatch);
                    break;

                case level_state.bin_bong_1_level8_screen: bing_bong_l8.Draw();
                    break;
              
                case level_state.bing_bong_3_level9_screen: bing_bong_l9.Draw();
                    break;

                case level_state.blacky_1_level0_screen: blacky_l10.Draw(spriteBatch);
                    break;

                case level_state.blacky_3_level11_screen: blacky_l11.Draw(spriteBatch);
                    break;

                case level_state.last_level:  last_screen.Draw(spriteBatch);
                    foreach (Menu_button b in restart_buttonlist)
                    {
                        b.Draw(spriteBatch, b.position, b.tex);
                        if (b.Hovering)
                        {
                            b.Draw(spriteBatch, b.position, b.hover);
                        }

                        if (b.Clicking)
                        {
                            button_click.Play();
                            b.Draw(spriteBatch, b.position, b.click);
                            if (b.Button_Function.Equals("Restart"))
                            {
                                    myLevelScreen = level_state.main_screen;
                                    help_screen = false;
                                    restart_game = true;
                                    
                                    exit_game = false;
                                    return;
                            }
                        }
                    }
                    ourMouse.Draw(spriteBatch);
                    break;

            }
            exit_game = false;
            restart_game = false;
        }

        public void Update(GameTime gameTime, Game1 gaming)
        {
            bool exit;
            switch (myLevelScreen)
            {
                case level_state.main_screen: Update_main_screen(gameTime);
                    break;
                case level_state.level_instruction: Update_level_screen(gameTime);
                    break;

                case level_state.level_final: Update_next_level_screen(gameTime);
                    break;
                case level_state.bin_level1_screen: catch_bag_l1.Update(out timechk, gameTime);
                    if (timechk)
                    {
                        myLevelScreen = level_state.level_final; l1_final.LoadContent("images\\score_background1", "images\\full_trash_bin", "images\\empty_trash", level_number);
                        level_number++;
                    }
                    break;

                case level_state.clean_air_level2_screen: clean_air_l2.Update(gameTime, out exit);
                    if (exit)
                    {
                        gaming.IsMouseVisible = false;
                        myLevelScreen = level_state.level_final;
                        l1_final.LoadContent("images\\score_background1", "images\\score2", "images\\score2", level_number);
                        level_number++;
                    }
                    break;


                case level_state.bubble_1_level3_screen: bubble_l3.Update(gameTime, out exit);
                    if (exit)
                    {
                        myLevelScreen = level_state.level_final;
                        l1_final.LoadContent("images\\score_background1", "images\\fulltrashBubble", "images\\empty_trash", level_number);
                        level_number++;
                    }
                    break;

                case level_state.bubble_2_level4_screen: bubble_l4.Update(gameTime, out exit);
                    if (exit)
                    {
                        myLevelScreen = level_state.level_final;
                        l1_final.LoadContent("images\\score_background1", "images\\fulltrashBubble", "images\\empty_trash", level_number);
                        level_number++;
                    }
                    break;

                case level_state.trash_1_level5_screen: Trash_pick_l5.Update(gameTime, out exit); 
                    if (exit)
                    {
                        myLevelScreen = level_state.level_final;
                        l1_final.LoadContent("images\\score_background1", "images\\score1", "images\\score1", level_number);
                        level_number++;
                    }
                    break;


                case level_state.trash_3_level6_screen: Trash_pick_l6.Update(gameTime,out exit); 
                    if (exit)
                    {
                        myLevelScreen = level_state.level_final;
                        l1_final.LoadContent("images\\score_background1", "images\\score1", "images\\score1", level_number);
                        level_number++;
                    }
                    break;

                case level_state.trash_4_level7_screen: Trash_pick_l7.Update(gameTime, out exit);
                    if (exit)
                    {
                        myLevelScreen = level_state.level_final;
                        l1_final.LoadContent("images\\score_background1", "images\\score1", "images\\score1", level_number);
                        level_number++;
                    }
                    break;

                case level_state.bin_bong_1_level8_screen: bing_bong_l8.Update(gameTime, out exit);
                    if(exit)
                    {
                        myLevelScreen=level_state.level_final;
                        l1_final.LoadContent("images\\score_background1", "Sprites\\smily", "Sprites\\sad", level_number);
                        level_number++;
                    }
                    break;

                case level_state.bing_bong_3_level9_screen: bing_bong_l9.Update(gameTime, out exit);
                    if (exit)
                    {
                        myLevelScreen = level_state.level_final;
                        l1_final.LoadContent("images\\score_background1", "Sprites\\smily", "Sprites\\sad", level_number);
                        level_number++;
                    }
                    break;


                case level_state.blacky_1_level0_screen: blacky_l10.Update(gameTime, out exit);
                    if (exit)
                    {
                        myLevelScreen = level_state.level_final;
                        l1_final.LoadContent("images\\score_background1", "images\\blacky_win", "images\\blacky_lost", level_number);
                        level_number++;
                    }
                    break;

                case level_state.blacky_3_level11_screen: blacky_l11.Update(gameTime, out exit);
                    if (exit)
                    {
                        myLevelScreen = level_state.level_final;
                        l1_final.LoadContent("images\\score_background1", "images\\blacky_medal", "images\\blacky_win", level_number);
                        level_number++;
                    }
                    break;

                case level_state.last_level: Update_restart_level_(gameTime);
                    
                    break;

            }

        }

        public void Update_main_screen(GameTime gameTime)
        {
            HandleMouse(buttonlist); //Check clicking
            ourMouse.Update(gameTime);
        }

        public void Update_next_level_screen(GameTime gameTime)
        {

            HandleMouse(next_level_buttonlist); //Check clicking
            ourMouse.Update(gameTime);
        }

        public void Update_restart_level_(GameTime gameTime)
        {

            HandleMouse(restart_buttonlist); //Check clicking
            ourMouse.Update(gameTime);
        }


        public void Update_level_screen(GameTime gameTime)
        {

            HandleMouse(level_buttonlist); //Check clicking
            ourMouse.Update(gameTime);
        }

        private void HandleMouse(List<Menu_button> newbuttonlist)
        {
            mouseState = Mouse.GetState();

            //Get the current state of the mouse
            foreach (Menu_button b in newbuttonlist)
            {

                if (ourMouse.ButtonClick(b))
                {
                    b.Hovering = true;
                    {
                        if (mouseState.LeftButton == ButtonState.Pressed)
                        { b.Clicking = true; break; }
                        else
                            b.Clicking = false;
                    }
                }

                else
                    b.Hovering = false;

            }

        }


    }
}
