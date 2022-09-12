using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Drag_and_Drop
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        MouseState currentMouseState;
        MouseState prevMouseState;

        bool isDragging;

        Texture2D asteroidTexture;
        Rectangle asteroidRect;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            asteroidRect = new Rectangle(10, 10, 50, 50);
            isDragging = false;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            asteroidTexture = Content.Load<Texture2D>("asteroid");
        }

        protected override void Update(GameTime gameTime)
        {
            prevMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            // Check to see if user clicks on target to begin dragging
            if (currentMouseState.LeftButton == ButtonState.Pressed && asteroidRect.Contains(currentMouseState.Position) && prevMouseState.LeftButton == ButtonState.Released){
                isDragging = true;
            }
            // User releases object
            else if (isDragging && currentMouseState.LeftButton == ButtonState.Released)
                isDragging = false;
            // Asteroid is dragging and needs to follow the mouse         
            else if (isDragging)
            {
                asteroidRect.Offset(currentMouseState.X - prevMouseState.X, currentMouseState.Y - prevMouseState.Y);
            }


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _spriteBatch.Draw(asteroidTexture, asteroidRect, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}