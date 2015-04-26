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
        Player player;
        static public Random random;
        
        
        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 1000;
        }

        protected override void Initialize()
        {

            random = new Random();
            

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
            player=new Player(Content, "nave", TipoBala.Simples);
            scene.AddSprite(player.Scl(0.5f));
            scene.player = player;


            newEnemyWave();

            //PowerUP
            scene.AddPowerUp(new PowerUp(Content, "PowerUp-Vida", TipoPowerUp.Vida, -1, 0.3f, 1f));
            scene.AddPowerUp(new PowerUp(Content, "PowerUp-TiroDuplo", TipoPowerUp.Armas, -1, 0.3f, 1.5f));
            scene.AddPowerUp(new PowerUp(Content, "PowerUp-TiroTriplo", TipoPowerUp.Armas, -1, 0.3f, 0.5f));
         
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

            if (scene.inimigos.Count <= 0)
            {
                //Matámos todos os inimigos, nova ronda
                newEnemyWave();
            }
            
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
            scene.AddInimigo(new NPC(Content, "Kamikaze", TipoNave.Hunter, 1, 0.3f, random).
                At(new Vector2(Camera.worldWidth + player.position.X, -1f)));
            scene.AddInimigo(new NPC(Content, "kamikaze", TipoNave.Hunter, 1, 0.3f, random).
                At(new Vector2(Camera.worldWidth + player.position.X+1f, 0f)));
            scene.AddInimigo(new NPC(Content, "kamikaze", TipoNave.Hunter, 1, 0.3f, random).
                At(new Vector2(Camera.worldWidth + player.position.X+2f, 1f)));


            scene.AddInimigo(new NPC(Content, "bombardeiro", TipoNave.Bomber, 1, 0.5f, random).
                At(new Vector2(Camera.worldWidth + player.position.X + 5, 1f)));
            scene.AddInimigo(new NPC(Content, "nave", TipoNave.Hunter, 1, 0.5f, random).
                At(new Vector2(Camera.worldWidth + player.position.X + 10, -1f)));

            scene.AddInimigo(new NPC(Content, "bombardeiro", TipoNave.Bomber, 1, 0.5f, random).
                At(new Vector2(Camera.worldWidth + player.position.X + 2, 0f)));
            scene.AddInimigo(new NPC(Content, "bombardeiro", TipoNave.Bomber, 1, 0.5f, random).
                At(new Vector2(Camera.worldWidth + player.position.X + 3, 0.5f)));
            scene.AddInimigo(new NPC(Content, "bombardeiro", TipoNave.Bomber, 1, 0.5f, random).
                At(new Vector2(Camera.worldWidth + player.position.X + 4, -0.8f)));

            //scene.AddInimigo(new NPC(Content, "nave", TipoNave.Mothership).Scl(0.5f).
            //    At(new Vector2(Camera.worldWidth, 0f)));

            
            
        }
    }
}
