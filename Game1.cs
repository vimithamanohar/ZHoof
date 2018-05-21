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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GraphicsDevice device;
        public bool exit_game,restart_game;


        private Vector2 pos;

        public enum game_state
        {
            initial_state,
            runnning_state,
            exit_state
        }

        public game_state gamestate;

        Handling_screens handle_screen;

        public Game1()
        {
            exit_game = false;
            restart_game = false;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            //   create_object();
        }

        private void create_object()
        {
            handle_screen = new Handling_screens(Content);
        }


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = "Sustainable Kid's Game";
            //  this.IsMouseVisible = true;
            this.pos.X = Mouse.GetState().X;
            this.pos.Y = Mouse.GetState().Y;
            gamestate = game_state.initial_state;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            device = graphics.GraphicsDevice;

            // TODO: use this.Content to load your game content here
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // ms.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {

            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 




        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
 KeyboardState newState = Keyboard.GetState();

            if (newState.IsKeyDown(Keys.Escape))
            { graphics.IsFullScreen = false;
                graphics.ApplyChanges();}

            else if (newState.IsKeyDown(Keys.F1))
            {  graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            }


            switch (gamestate)
            {

                case game_state.initial_state: create_object();
                    handle_screen.LoadContent(spriteBatch);

                    gamestate = game_state.runnning_state;
                    break;
                case game_state.runnning_state: handle_screen.Update(gameTime, this);
                    break;
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Bisque);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

           

            switch (gamestate)
            {

                case game_state.runnning_state: handle_screen.Draw(out exit_game,out restart_game, gameTime, this);
                    if (exit_game)
                    {
                       
                       gamestate = game_state.initial_state;
                        Exit();
                    }
                    if (this.restart_game)
                    {
                        restart_game = false;
                        gamestate = game_state.initial_state;
                    }
                    break;

            }


            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
