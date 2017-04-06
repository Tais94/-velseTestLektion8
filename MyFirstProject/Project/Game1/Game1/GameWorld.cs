using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Game1
{ 
    enum DIRECTION {Left, Right };
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameWorld:Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        static GameWorld instance;
        public static GameWorld Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameWorld();
                }
                return instance;
            }
        }

        //
        private List<GameObject> gameObjects;
        
        internal List<GameObject> GameObjects { get { return gameObjects; } }
        internal List<GameObject> RemoveGameObjects { get; set; }
        GameObject player;
        public float deltaTime;
        GameObject enemy;
        Vector2 cameraOffset;


        internal List<Collider> colliders { get; set; }

        private GameWorld()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            gameObjects = new List<GameObject>();
            RemoveGameObjects = new List<GameObject>();
            colliders = new List<Collider>();



            //Director dir = new Director(new PlayerBuilder());
            //player = dir.Construct(new Vector2(-50));
            //gameObjects.Add(player);
            //Camera.Instance.Pos = Vector2.Zero;
            //Camera.Instance.Zoom = 0.1f;


            Director dir = new Director(new TreeBuilder());
            gameObjects.Add(dir.Construct(new Vector2(0, -290)));
            gameObjects.Add(dir.Construct(new Vector2(-450, -290)));
            gameObjects.Add(dir.Construct(new Vector2(-950, -290)));         
            gameObjects.Add(dir.Construct(new Vector2(850, -290)));
            gameObjects.Add(dir.Construct(new Vector2(300, -290)));
            gameObjects.Add(dir.Construct(new Vector2(1000, -290)));
            gameObjects.Add(dir.Construct(new Vector2(1400, -290)));
            gameObjects.Add(dir.Construct(new Vector2(-1300, -290)));
            gameObjects.Add(dir.Construct(new Vector2(-1500, -290)));
            gameObjects.Add(dir.Construct(new Vector2(-1800, -290)));
            gameObjects.Add(dir.Construct(new Vector2(-2000, -290)));
           


            dir = new Director(new TabletBuilder());
            gameObjects.Add(dir.Construct(new Vector2(2000, 95)));
            gameObjects.Add(dir.Construct(new Vector2(1600, 95)));

            dir = new Director(new TabletBuilder2());
            gameObjects.Add(dir.Construct(new Vector2(1000, 95)));

            dir = new Director(new AltarBuilder());
            gameObjects.Add(dir.Construct(new Vector2(2400, -20)));

            dir = new Director(new PottyBuilder());
            gameObjects.Add(dir.Construct(new Vector2(100, -100)));


            dir = new Director(new PlayerBuilder());
            player = dir.Construct(new Vector2(-50));
            gameObjects.Add(player);
            Camera.Instance.Pos = Vector2.Zero;
            Camera.Instance.Zoom = 0.7f;

            dir = new Director(new EnemyBuilder());
            enemy = dir.Construct(new Vector2(-900,-50));
            gameObjects.Add(enemy);

            
            //dir = new Director(new TestBlockBuilder());
            //gameObjects.Add(dir.Construct(new Vector2(-290)));
            //gameObjects.Add(dir.Construct(new Vector2(50, -290)));
            //gameObjects.Add(dir.Construct(new Vector2(-50, -290)));

          

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            foreach (GameObject go in gameObjects)
            {
                go.LoadContent(this.Content);
            }

            cameraOffset.X = (player.GetComponent("SpriteRenderer") as SpriteRenderer).sprite.Width / (player.scaleFactor * 5f);
            cameraOffset.Y = (player.GetComponent("SpriteRenderer") as SpriteRenderer).sprite.Height / (player.scaleFactor * 2.5f);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            foreach (GameObject go in gameObjects)
            {
                go.Update();
            }

            foreach (GameObject obj in RemoveGameObjects)
            {
                gameObjects.Remove(obj);
            }
            Camera.Instance.Pos = -(player.Transform.Position + cameraOffset);

            base.Update(gameTime);
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Multiply(Color.MidnightBlue, 0.3f));

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.FrontToBack,BlendState.AlphaBlend, null,null,null,null,Camera.Instance.GetTransformation(GraphicsDevice));
            
            foreach (GameObject go in gameObjects)
            {
                go.Draw(spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        internal GameObject FindGameObjectWithTag(string tag)
        {
            return gameObjects.Find(x => x.Tag == tag);
        }
       
    }
}
