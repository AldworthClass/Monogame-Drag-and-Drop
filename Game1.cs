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

        bool isDraggingAsteroid;
        bool isDraggingCar;
        bool isDraggingRocket;

        Texture2D asteroidTexture;
        Rectangle asteroidRect;

        Texture2D carTexture;
        Rectangle carRect;

        Texture2D rocketTexture;
        Rectangle rocketRect;


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
            isDraggingAsteroid = false;
            
            carRect = new Rectangle(200, 200, 75, 25);
            isDraggingCar = false;

            rocketRect = new Rectangle(400, 100, 40, 75);
            isDraggingRocket = false;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            asteroidTexture = Content.Load<Texture2D>("asteroid");
            carTexture = Content.Load<Texture2D>("fast_car");
            rocketTexture = Content.Load<Texture2D>("rocket");

        }

        // Returns true when a click occurs
        protected bool NewClick()
        {
            return currentMouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released;
        }
        protected override void Update(GameTime gameTime)
        {
            prevMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();


            // Asteroid Dragging Code
            // ++++++++++++++++++++++
            // Check to see if user clicks on target to begin dragging
            if (NewClick() && asteroidRect.Contains(currentMouseState.Position))
                isDraggingAsteroid = true;
            // User releases object
            else if (isDraggingAsteroid && currentMouseState.LeftButton == ButtonState.Released)
                isDraggingAsteroid = false;
            // Asteroid is dragging and needs to follow the mouse         
            else if (isDraggingAsteroid)
                asteroidRect.Offset(currentMouseState.X - prevMouseState.X, currentMouseState.Y - prevMouseState.Y);


            // Rocket Dragging Code (Vertical)
            // +++++++++++++++++++++++++++++++++
            // Check to see if user clicks on target to begin dragging
            if (NewClick() && rocketRect.Contains(currentMouseState.Position))
                isDraggingRocket = true;
            // User releases object
            else if (isDraggingRocket && currentMouseState.LeftButton == ButtonState.Released)
                isDraggingRocket = false;
            // Object is dragging and needs to follow the mouse vertically         
            else if (isDraggingRocket)
                rocketRect.Offset(0, currentMouseState.Y - prevMouseState.Y);

            // Car Dragging Code (Horizontal)
            // +++++++++++++++++++++++++++++++++
            // Check to see if user clicks on target to begin dragging
            if (NewClick() && carRect.Contains(currentMouseState.Position))
                isDraggingCar = true;
            // User releases object
            else if (isDraggingCar && currentMouseState.LeftButton == ButtonState.Released)
                isDraggingCar = false;
            // Object is dragging and needs to follow the mouse horizontally         
            else if (isDraggingCar)
                carRect.Offset(currentMouseState.X - prevMouseState.X, 0);

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
            _spriteBatch.Draw(carTexture, carRect, Color.White);
            _spriteBatch.Draw(rocketTexture, rocketRect, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}