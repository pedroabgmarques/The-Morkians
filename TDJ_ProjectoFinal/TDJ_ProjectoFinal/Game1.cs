using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using TDJ_ProjectoFinal.entidades;
using TDJ_ProjectoFinal.graficos;

namespace TDJ_ProjectoFinal
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Scene scene;
        
        
        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 1000;
            graphics.ApplyChanges();

            Camera.SetGraphicsDeviceManager(graphics);
            Camera.SetTarget(Vector2.Zero);
            Camera.SetWorldWidth(5);

            
            
           
            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            scene = new Scene(spriteBatch);
            
            //Fundo do universo (imóvel)
            scene.AddSprite(new Sprite(Content, "universe").Scl(6000 * Camera.worldWidth / graphics.PreferredBackBufferHeight).
                At(new Vector2(Camera.worldWidth, 0f)));
            
            //Nave do jogador
            scene.AddSprite(new Player(Content, "nave").Scl(0.5f));

            newEnemyWave();
         
        }

            

        protected override void UnloadContent()
        {
            
        }

       
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            scene.Update(gameTime);

            Camera.Update();

          

            scene.Update(gameTime);
            
            base.Update(gameTime);
        }

      
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

           

            scene.Draw(gameTime);

            base.Draw(gameTime);
        }

    
        public void newEnemyWave()
        {

            //Alguns inimigos
            scene.AddSprite(new NPC(Content, "nave", TipoNave.Hunter).Scl(0.5f).
                At(new Vector2(Camera.worldWidth, 0f)));
            scene.AddSprite(new NPC(Content, "nave", TipoNave.Hunter).Scl(0.5f).
                At(new Vector2(Camera.worldWidth, 1f)));
            scene.AddSprite(new NPC(Content, "nave", TipoNave.Hunter).Scl(0.5f).
                At(new Vector2(Camera.worldWidth, -1f)));

            scene.AddSprite(new NPC(Content, "nave", TipoNave.Bomber).Scl(0.5f).
                At(new Vector2(Camera.worldWidth, 0f)));
            scene.AddSprite(new NPC(Content, "nave", TipoNave.Bomber).Scl(0.5f).
                At(new Vector2(Camera.worldWidth, 1f)));
            scene.AddSprite(new NPC(Content, "nave", TipoNave.Bomber).Scl(0.5f).
                At(new Vector2(Camera.worldWidth, -1f)));

            scene.AddSprite(new NPC(Content, "nave", TipoNave.Mothership).Scl(0.5f).
                At(new Vector2(Camera.worldWidth, 0f)));

            //Missil
            scene.AddSprite(new Missil(Content, "missil", TipoMissil.EmFrente, -1).Scl(0.2f).
                At(new Vector2(Camera.worldWidth, 0f)));

            //PowerUP
            scene.AddSprite(new PowerUp(Content, "PowerUp-Vida", -1).Scl(0.3f).At(new Vector2(Camera.worldWidth/2, 0f)));
        }
    }
}
