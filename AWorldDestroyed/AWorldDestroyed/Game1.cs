﻿using AWorldDestroyed.Models;
using AWorldDestroyed.Scenes;
using AWorldDestroyed.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AWorldDestroyed
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        //public static Sprite Sprite;
        //public static Sprite Sprite2;
        //public static Texture2D TestTileset;
        //public static Texture2D Pixel;
        //public static Texture2D TileSet_01;

        private SimpleFps simpleFps;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = false;
            //graphics.IsFullScreen = true;
            //graphics.PreferredBackBufferHeight = 200;
            //graphics.PreferredBackBufferWidth = 300;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            simpleFps = new SimpleFps();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            ContentManager.Init(Content, GraphicsDevice);
            ContentManager.AddTexture("Enemy", "Sprites/Enemies/Monster");
            ContentManager.AddTexture("EnemySprites", "Sprites/Enemies/MonsterSpritesheet");
            ContentManager.AddTexture("Player", "Sprites/Player/Player_spriteSheet");

            spriteBatch = new SpriteBatch(GraphicsDevice);

            SceneManager.AddScene("1", new FirstScene(spriteBatch));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            InputManager.Update();
            simpleFps.Update(gameTime);
            Window.Title = simpleFps.msg;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed 
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            SceneManager.ActiveScene.Update(gameTime.ElapsedGameTime.TotalMilliseconds);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            SceneManager.ActiveScene.Draw();
            
            base.Draw(gameTime);
        }
    }
}
