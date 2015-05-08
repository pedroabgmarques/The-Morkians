using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
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
        Scene scene,scene2,scene3;
        Player player;
        public static Random random;
        bool novopowerup = false;
        List<Scene> Cenas;
        Random randomShake;
        
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
            randomShake = new Random();
            Cenas = new List<Scene>();
            
            Camera.SetGraphicsDeviceManager(graphics);
            Camera.SetTarget(Vector2.Zero);
            Camera.SetWorldWidth(5);

            som.Initialize(Content);

            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            scene = new Scene(spriteBatch);
            Cenas.Add(scene);
            
            
            //Fundo do universo (imóvel)
            Cenas[0].AddSprite(new SlidingBackground(Content, "universe", 4f).Scl(6000 * Camera.worldWidth / graphics.PreferredBackBufferHeight).
                At(new Vector2(Camera.worldWidth, 0f)));
            //lua
            Cenas[0].AddSprite(new SlidingBackground(Content, "fullMoon", 6f).Scl(1f).At(new Vector2(6, 1f)));
            //planeta
            Cenas[0].AddSprite(new SlidingBackground(Content, "planeta", 9f).Scl(8f).At(new Vector2(8,-2.7f)));
            //estacao espacial
            Cenas[0].AddSprite(new SlidingBackground(Content, "spaceStaion", 500f).Scl(4f).At(new Vector2(12f, 0.8f)));
            //Nave do jogador
            player=new Player(Content, "nave", TipoBala.Simples);
            Cenas[0].AddSprite(player.Scl(0.62f));
            Cenas[0].player = player;

            newEnemyWave();

            //PowerUP
            Cenas[0].AddPowerUp(new PowerUp(Content, "PowerUp-Vida", TipoPowerUp.Vida, -1, 0.3f, 1f));
            //scene.AddPowerUp(new PowerUp(Content, "PowerUp-Bala", TipoPowerUp.Armas, -1, 0.3f, 1.5f));


            Song background_cena1 = Content.Load<Song>("som/musicacena1");
            som.PlaySong(background_cena1);
            
         
        }

            

        protected override void UnloadContent()
        {
            
        }

       
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            foreach (var scene in Cenas)
            {

                if (scene.active == true)
                {

                    Camera.Update(randomShake);

                    scene.Update(gameTime);
                }
            }
            
            if (Cenas[0].inimigos.Count <= 5 && Cenas[0].active==true)
            {
                //Matámos todos os inimigos, nova ronda
                newEnemyWave();
                novopowerup = false;
            }

            if (!novopowerup && Cenas[0].active == true) 
            {
                // os inimigos estão a terminar...
                // nova ronda surgitrá
                // (apenas teste)
                Cenas[0].AddPowerUp(new PowerUp(Content, "PowerUp-Bala", TipoPowerUp.Armas, -1, 0.3f,0f ));
                  
                novopowerup = true;

            }
            //muda para cena2
            if ( Camera.target.X >= 25f && Cenas[0].active == true)
            {
                Cenas[0].inimigos.Clear();
                Cenas[0].powerUps.Clear();
                player.position.X += 0.02f;
                
                if (player.position.X >= (Camera.GetTarget().X + Camera.worldWidth/2)-0.5f)
                {
                    Cenas[0].active = false;
                    cena2();
                    
                }
            }

            if(Camera.target.X >= 25f && Cenas.Count > 1 && Cenas[1].active==true)
            {
                player.position.X += 0.02f;

                if(player.position.X >= (Camera.GetTarget().X + Camera.worldWidth/2)-0.5f)
                {
                    Cenas[1].active=false;
                    cena3();
                }
            }


            if (Cenas.Count == 3)
            {
                if (Cenas[2].active == true && Camera.target.X >= 2f)
                {
                    Camera.speed = 0f;
                }
            }

            base.Update(gameTime);
        }

      
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            foreach (var scene in Cenas)
            {
                if(scene.active==true)
                scene.Draw(gameTime);
            }
               
                
            base.Draw(gameTime);
        }

    
        public void newEnemyWave()
        {

            //Alguns inimigos
            Cenas[0].AddInimigo(new NPC(Content, "Kamikaze", TipoNave.Hunter, 1, 0.3f,random).
                At(new Vector2(Camera.worldWidth + player.position.X, -1f)));
            Cenas[0].AddInimigo(new NPC(Content, "kamikaze", TipoNave.Hunter, 1, 0.3f, random).
                At(new Vector2(Camera.worldWidth + player.position.X+1f, 0f)));
            Cenas[0].AddInimigo(new NPC(Content, "kamikaze", TipoNave.Hunter, 1, 0.3f, random).
                At(new Vector2(Camera.worldWidth + player.position.X+2f, 1f)));


            Cenas[0].AddInimigo(new NPC(Content, "bombardeiro", TipoNave.Bomber, 1, 0.5f, random).
                At(new Vector2(Camera.worldWidth + player.position.X + 5, 1f)));
            Cenas[0].AddInimigo(new NPC(Content, "caça", TipoNave.Hunter, 1, 0.4f, random).
                At(new Vector2(Camera.worldWidth + player.position.X + 6, -1f)));

            Cenas[0].AddInimigo(new NPC(Content, "bombardeiro", TipoNave.Bomber, 1, 0.5f, random).
                At(new Vector2(Camera.worldWidth + player.position.X + 2, 0f)));
            Cenas[0].AddInimigo(new NPC(Content, "bombardeiro", TipoNave.Bomber, 1, 0.5f, random).
                At(new Vector2(Camera.worldWidth + player.position.X + 3, 0.5f)));
            Cenas[0].AddInimigo(new NPC(Content, "bombardeiro", TipoNave.Bomber, 1, 0.5f, random).
                At(new Vector2(Camera.worldWidth + player.position.X + 4, -0.8f)));

            Cenas[0].AddInimigo(new NPC(Content, "caça", TipoNave.Hunter, 1, 0.4f, random).
                At(new Vector2(Camera.worldWidth + player.position.X + 10, 1f)));

            //scene.AddInimigo(new NPC(Content, "nave", TipoNave.Mothership).Scl(0.5f).
            //    At(new Vector2(Camera.worldWidth, 0f)));

            
            
        }

        public void cena2()
        {
            scene2 = new Scene(spriteBatch);
            Cenas.Add(scene2);
            Camera.SetTarget(Vector2.Zero);
            
            Cenas[1].AddSprite(new SlidingBackground(Content, "fundoFinal", 2f).Scl(45f).
               At(new Vector2(Camera.worldWidth, 0f)));
            Cenas[1].AddSprite(new Cenario(Content, "mapaFinalCima",40f).At(new Vector2(5,0.1f)));
            Cenas[1].AddSprite(new Cenario(Content, "mapaFinalBaixo", 40f).At(new Vector2(5,-0.1f)));
            player = new Player(Content, "nave", TipoBala.Simples);
            Cenas[1].AddSprite(player.Scl(0.62f));
            Defesas();
            
            Cenas[1].player = player;
            player.position.X = Camera.target.X - (Camera.worldWidth / 2) + 0.1f;
            
        }

        public void cena3()
        {
            scene3 = new Scene(spriteBatch);
            Cenas.Add(scene3);
            Camera.SetTarget(Vector2.Zero);

            Cenas[2].AddSprite(new Sprite(Content, "fundoFinal").Scl(45f).
               At(new Vector2(Camera.worldWidth, 0f)));
            Cenas[2].AddInimigo(new NPC(Content, "boss",TipoNave.Mothership,0,2f,random).At(new Vector2(3f,0f)));
            player = new Player(Content, "nave", TipoBala.Simples);
            Cenas[2].AddSprite(player.Scl(0.62f));
            Cenas[2].player = player;
            player.position.X = Camera.target.X - (Camera.worldWidth / 2) + 0.1f;
        }

        public void Defesas()
        {
            // conjunto de ddefesas do nivel dois
            // metrelhadoras
            //inferiores
            Cenas[1].AddSprite(new Defence(Content, "turret").Scl(0.6f).At(new Vector2(player.position.X, -1.35f)));
            Cenas[1].AddSprite(new Defence(Content, "turret").Scl(0.6f).At(new Vector2(player.position.X + 0.6f, -1.35f)));
            Cenas[1].AddSprite(new Defence(Content, "turret").Scl(0.6f).At(new Vector2(player.position.X + 10f, -1.00f)));

            Cenas[1].AddSprite(new Defence(Content, "turret").Scl(0.6f).At(new Vector2(player.position.X + 14f, -1.19f)));
            
            Cenas[1].AddSprite(new Defence(Content, "turret").Scl(0.6f).At(new Vector2(player.position.X + 15.5f, -1.10f)));

            Cenas[1].AddSprite(new Defence(Content, "turret").Scl(0.6f).At(new Vector2(player.position.X +18.6f, -1.35f)));
            Cenas[1].AddSprite(new Defence(Content, "turret").Scl(0.6f).At(new Vector2(player.position.X + 23f, -1.30f)));

            // superiores

            Cenas[1].AddSprite(new Defence(Content, "turret").Scl(0.6f).At(new Vector2(player.position.X, 1.2f)));

            
            


        }

    }
}
