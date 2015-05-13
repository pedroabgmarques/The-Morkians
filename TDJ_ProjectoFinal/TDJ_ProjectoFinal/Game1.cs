using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using TDJ_ProjectoFinal.entidades;
using TDJ_ProjectoFinal.graficos;

namespace TDJ_ProjectoFinal
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {

        enum GameState
        {
            Menu,
            Bridge0,
            Nivel1,
            Bridge1,
            Nivel2,
            Bridge2,
            Nivel3,
            End
        }

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Scene scene,scene2,scene3;
        Player player;
        public static Random random;
        bool novopowerup = false;
        List<Scene> Cenas;
        Random randomShake;
        GameState gamestate;
        SpriteFont font;
        int kbLimit;
        int timerTextos;
        KeyValuePair<string, Vector2> texto;
        int timeToRestart;
        bool RestartReady = false;
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
            font = Content.Load<SpriteFont>("MyFont");
            WeaponsManager.LoadContent(Content);
            EnemyManager.LoadContent(Content, random);
            kbLimit = 0;
            timeToRestart = 0;
            timerTextos = 0;
            texto = new KeyValuePair<string, Vector2>("", Vector2.Zero);
            

            LoadLevel(GameState.Menu);
            
        }

        private void LoadLevel(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Menu:
                    gamestate = GameState.Menu;

                    Camera.velocidadegeral = 0;
                    Cenas[0].AddSprite(new Sprite(Content, "mainMenu").At(Vector2.Zero).Scl(5f));
                    Cenas[0].AddSprite(new Sprite(Content, "backgroundTextoMenu").At(new Vector2(0, -0.12f)).Scl(5f));

                    som.playMusicaMenu();
                    
                    
                    break;
                case GameState.Bridge0:

                    gamestate = GameState.Bridge0;

                    Camera.SetTarget(new Vector2(24, 0));
                    Camera.velocidadegeral = -0.002f;

                    //Fundo do universo (imóvel)
                    Cenas[0].AddSprite(new SlidingBackground(Content, "universe", 4f).Scl(6000 * Camera.worldWidth / graphics.PreferredBackBufferHeight).
                        At(new Vector2(Camera.worldWidth, 0f)));
                    //lua
                    Cenas[0].AddSprite(new SlidingBackground(Content, "fullMoon", 6f).Scl(1f).At(new Vector2(17, 0f)));
                    //planeta
                    Cenas[0].AddSprite(new SlidingBackground(Content, "planeta", 9f).Scl(8f).At(new Vector2(14, -2.7f)));
                    //estacao espacial
                    Cenas[0].AddSprite(new SlidingBackground(Content, "spaceStaion", 500f).Scl(4f).At(new Vector2(19f, 0f)));

                    Cenas[0].AddSprite(new Sprite(Content, "backgroundTextoMenuGigante").At(new Vector2(7, -0.62f)).Scl(40f));

                    Cenas[0].AddTexto("First contact wasn't how we expected.", new Vector2(110, 400));
                    Cenas[0].AddTexto("From outer space, a single message:", new Vector2(135, 400));
                    Cenas[0].AddTexto("\"WE ARE COMING.\"", new Vector2(320, 400));
                    Cenas[0].AddTexto("In two months they arrived at Jupiter,", new Vector2(108, 400));
                    Cenas[0].AddTexto("with a four mile wide mothership", new Vector2(150, 400));
                    Cenas[0].AddTexto("carrying hundreds of fighters and bombers.", new Vector2(90, 400));
                    Cenas[0].AddTexto("In the time it took them to get here", new Vector2(120, 400));
                    Cenas[0].AddTexto("and decelerate into low earth orbit, ", new Vector2(125, 400));
                    Cenas[0].AddTexto("a brave team of engineers from IPCA", new Vector2(125, 400));
                    Cenas[0].AddTexto("infiltrated the Morkian mainframes", new Vector2(160, 400));
                    Cenas[0].AddTexto("and reverse-engineered their technology.", new Vector2(105, 400));
                    Cenas[0].AddTexto("First they will have to defeat ", new Vector2(170, 400));
                    Cenas[0].AddTexto("All the spaceships defending the mothership. ", new Vector2(95, 400));
                    Cenas[0].AddTexto("They are Earth's only hope! ", new Vector2(280, 400));
                    Cenas[0].AddTexto("FIM - ESTE NÃO APARECE ", new Vector2(0, 0));

                    som.playMusicaBridge0();
                    
                    break;
                case GameState.Nivel1:
                    gamestate = GameState.Nivel1;

                    Camera.SetTarget(new Vector2(0, 0));
                    Camera.velocidadegeral = 0.007f;

                    //TODO: Limpar cenas do menu

                    //Fundo do universo (imóvel)
                    Cenas[0].AddSprite(new SlidingBackground(Content, "universe", 4f).Scl(6000 * Camera.worldWidth / graphics.PreferredBackBufferHeight).
                        At(new Vector2(Camera.worldWidth, 0f)));
                    //lua
                    Cenas[0].AddSprite(new SlidingBackground(Content, "fullMoon", 6f).Scl(1f).At(new Vector2(10, 0f)));
                    //planeta
                    Cenas[0].AddSprite(new SlidingBackground(Content, "planeta", 9f).Scl(8f).At(new Vector2(8, -2.7f)));
                    //estacao espacial
                    Cenas[0].AddSprite(new SlidingBackground(Content, "spaceStaion", 500f).Scl(4f).At(new Vector2(14f, 0.8f)));
                    //Nave do jogador
                    player = new Player(Content, "nave", TipoBala.Simples);
                    Cenas[0].AddSprite(player.Scl(0.62f));
                    Cenas[0].player = player;

                    newEnemyWave(0);

                    //PowerUP
                    Cenas[0].AddPowerUp(new PowerUp(Content, "PowerUp-Vida", TipoPowerUp.Vida, -1, 0.3f, 1f));
                    //scene.AddPowerUp(new PowerUp(Content, "PowerUp-Bala", TipoPowerUp.Armas, -1, 0.3f, 1.5f));

                    som.playMusicaNivel1();
                    
                    break;
                case GameState.Bridge1:
                    //TODO: Bridge 1
                    gamestate = GameState.Bridge1;

                     Cenas[0].sprites.Clear();
                    Cenas[0].inimigos.Clear();
                    Cenas[0].powerUps.Clear();
                    Cenas[0].explosoes.Clear();
                    Cenas[0].active = false;
                    GC.Collect();
                    scene2 = new Scene(spriteBatch);
                    Cenas.Add(scene2);
                    Cenas[1].active = true;
                    Camera.SetTarget(new Vector2(0, 0));
                    Camera.velocidadegeral = 0.002f;

                    Cenas[1].AddSprite(new SlidingBackground(Content, "universe", 4f).Scl(6000 * Camera.worldWidth / graphics.PreferredBackBufferHeight).
                       At(new Vector2(Camera.worldWidth, 0f)));
                    break;
                case GameState.Nivel2:

                    gamestate = GameState.Nivel2;

                    Cenas[0].sprites.Clear();
                    Cenas[0].inimigos.Clear();
                    Cenas[0].powerUps.Clear();
                    Cenas[0].explosoes.Clear();
                    Cenas[0].active = false;
                    GC.Collect();

                    //scene2 = new Scene(spriteBatch);
                    //Cenas.Add(scene2);
                    //Cenas[1].active = true;
                    Camera.SetTarget(Vector2.Zero);
            
                    Cenas[1].AddSprite(new SlidingBackground(Content, "fundoFinal",3f).Scl(45f).
                       At(new Vector2(Camera.worldWidth, 0f)));
                    Cenas[1].AddSprite(new Cenario(Content, "mapaFinalCima",40f).At(new Vector2(5,0.1f)));
                    Cenas[1].AddSprite(new Cenario(Content, "mapaFinalBaixo", 40f).At(new Vector2(5,-0.1f)));
                    player = new Player(Content, "nave", TipoBala.Simples);
                    Cenas[1].AddSprite(player.Scl(0.62f));
                    
                    Defesas();
            
                    Cenas[1].player = player;
                    player.position.X = Camera.target.X - (Camera.worldWidth / 2) + 0.1f;

                    som.playMusicaNivel2();

                    break;
                case GameState.Bridge2:
                    //TODO: bridge 2
                    break;
                case GameState.Nivel3:

                    gamestate = GameState.Nivel3;

                    Cenas[1].sprites.Clear();
                    Cenas[1].inimigos.Clear();
                    Cenas[1].powerUps.Clear();
                    Cenas[1].explosoes.Clear();
                    Cenas[1].active = false;
                    GC.Collect();

                    scene3 = new Scene(spriteBatch);
                    Cenas.Add(scene3);
                    Camera.SetTarget(Vector2.Zero);
            
                    Cenas[2].AddSprite(new Sprite(Content, "fundoFinal").Scl(45f).
                       At(new Vector2(Camera.worldWidth, 0f)));
                    //Cenas[2].AddInimigo(new NPC(Content, "boss", TipoNave.Mothership, 2f, random).At(new Vector2(3f, 0f)));
                    //Cenas[2].AddInimigo(new AnimatedSprite(Content,"bossSheet",1,3,true,new Vector2(1f,1f),2f));
                    Cenas[2].AddInimigo(new Boss(Content, "bossSheet2", 1, 2, true, new Vector2(1f, 1f), 2f));
                    player = new Player(Content, "nave", TipoBala.Simples);
                    Cenas[2].AddSprite(player.Scl(0.62f));
                    Cenas[2].player = player;
                    player.position.X = Camera.target.X - (Camera.worldWidth / 2) + 0.1f;
                    player.tipobala = TipoBala.Quadruplo;

                    break;
                case GameState.End:
                    //TODO: End
                    break;
                default:
                    break;
            }
        }

            

        protected override void UnloadContent()
        {
            
        }

       
        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && kbLimit > 1000)
            {

                switch (gamestate)
                {
                    case GameState.Menu:
                        LoadLevel(GameState.Bridge0);
                        break;
                    case GameState.Bridge0:
                        LoadLevel(GameState.Nivel1);
                        Camera.velocidadegeral = 0.007f;
                        break;
                    case GameState.Nivel1:
                        break;
                    case GameState.Bridge1:
                        LoadLevel(GameState.Nivel2);
                        Camera.velocidadegeral = 0.007f;
                        break;
                    case GameState.Nivel2:
                        break;
                    case GameState.Bridge2:
                        break;
                    case GameState.Nivel3:
                        break;
                    case GameState.End:
                        break;
                    default:
                        break;
                }
                kbLimit = 0;
            }
            kbLimit += gameTime.ElapsedGameTime.Milliseconds;
            if(RestartReady)
                timeToRestart += gameTime.ElapsedGameTime.Milliseconds;
            
            UpdateScenes(gameTime);

            base.Update(gameTime);
        }

        private void UpdateScenes(GameTime gameTime)
        {
            List<Scene> copia = new List<Scene>(Cenas);
            foreach (var scene in copia)
            {

                if (scene.active == true)
                {
                    if (scene.playerKilled)
                    {
                        RestartReady = true;
                        reStart();
                    }
                    Camera.Update(randomShake);

                    scene.Update(gameTime);
                }
            }
          
            ChangeScenes();
        }

        private void ChangeScenes()
        {
            if (Cenas[0].inimigos.Count <= 5 && Cenas[0].active == true && Camera.target.X < 18f && gamestate == GameState.Nivel1)
            {
                //Matámos todos os inimigos, nova ronda
                newEnemyWave(0);
                novopowerup = false;
            }

            if (!novopowerup && Cenas[0].active == true)
            {
                // os inimigos estão a terminar...
                // nova ronda surgitrá
                // (apenas teste)
                Cenas[0].AddPowerUp(new PowerUp(Content, "PowerUp-Bala", TipoPowerUp.Armas, -1, 0.3f, 0f));

                novopowerup = true;

            }
            //muda para cena2
            if (Camera.target.X >= 25f && Cenas[0].active == true)
            {
                player.position.X += 0.02f;

                if (player.position.X >= (Camera.GetTarget().X + Camera.worldWidth / 2) - 0.5f)
                {
                    Cenas[0].active = false;
                   
                    LoadLevel(GameState.Bridge1);

                }
            }
            //muda para cena 3
            if (Camera.target.X >= 25f && Cenas.Count > 1 && Cenas[1].active == true)
            {
                player.position.X += 0.02f;

                if (player.position.X >= (Camera.GetTarget().X + Camera.worldWidth / 2) - 0.5f)
                {
                    Cenas[1].active = false;
                    LoadLevel(GameState.Nivel3);
                }
            }


            if (Cenas.Count == 3)
            {
                if (Cenas[2].active == true && Camera.target.X >= 2f)
                {
                    Camera.speed = 0f;
                }
            }
        }

      
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

          
            
            foreach (var scene in Cenas)
            {
                if(scene.active==true)
                scene.Draw(gameTime);
            }

            switch (gamestate)
            {
                case GameState.Menu:
                    spriteBatch.Begin();
                    spriteBatch.DrawString(font, "Press ENTER to play.", new Vector2(300f, 300f), Color.White);
                    spriteBatch.End();
                    break;
                case GameState.Bridge0:
                    

                    if (timerTextos == 0)
                    {
                        texto = Cenas[0].GetTexto();
                    }

                    spriteBatch.Begin();
                    spriteBatch.DrawString(font, texto.Key, texto.Value, Color.White);
                    spriteBatch.End();

                    if (timerTextos >= 5000 && Cenas[0].textos.Count > 0)
                    {
                        texto = Cenas[0].GetTexto();
                    }
                    if (timerTextos > 5000)
                    {
                        timerTextos = 0;
                        if (Cenas[0].textos.Count == 0)
                        {
                            //Acabaram os textos, seguir
                            LoadLevel(GameState.Nivel1);
                        }
                    }
                    timerTextos += gameTime.ElapsedGameTime.Milliseconds;
                    
                    break;
                case GameState.Nivel1:
                    break;
                case GameState.Bridge1:
                    break;
                case GameState.Nivel2:
                    break;
                case GameState.Bridge2:
                    break;
                case GameState.Nivel3:
                    break;
                case GameState.End:
                    break;
                default:
                    break;
            }

            base.Draw(gameTime);
        }

    
        public void newEnemyWave(int cena)
        {

            //Alguns inimigos
            Cenas[cena].AddInimigo(EnemyManager.addKamikaze()
                .At(new Vector2(Camera.worldWidth + player.position.X, -1f)));
            Cenas[cena].AddInimigo(EnemyManager.addKamikaze()
                .At(new Vector2(Camera.worldWidth + player.position.X+1f, 0f)));
            Cenas[cena].AddInimigo(EnemyManager.addKamikaze()
                .At(new Vector2(Camera.worldWidth + player.position.X+2f, 1f)));


            Cenas[cena].AddInimigo(EnemyManager.addBombardeiro()
                .At(new Vector2(Camera.worldWidth + player.position.X + 5, 1f)));
            Cenas[cena].AddInimigo(EnemyManager.addBombardeiro()
                .At(new Vector2(Camera.worldWidth + player.position.X + 6, -1f)));

            Cenas[cena].AddInimigo(EnemyManager.addBombardeiro()
                .At(new Vector2(Camera.worldWidth + player.position.X + 2, 0f)));
            Cenas[cena].AddInimigo(EnemyManager.addBombardeiro()
                .At(new Vector2(Camera.worldWidth + player.position.X + 3, 0.5f)));
            Cenas[cena].AddInimigo(EnemyManager.addBombardeiro()
                .At(new Vector2(Camera.worldWidth + player.position.X + 4, -0.8f)));

            Cenas[cena].AddInimigo(EnemyManager.addCaça()
                .At(new Vector2(Camera.worldWidth + player.position.X + 10, 1f)));

            //scene.AddInimigo(new NPC(Content, "nave", TipoNave.Mothership).Scl(0.5f).
            //    At(new Vector2(Camera.worldWidth, 0f)));

            
            
        }

        public void Defesas()
        {
            // conjunto de ddefesas do nivel 2
            // metrelhadoras
            //inferiores
            Cenas[1].AddSprite(new Defence(Content, "turret", TipoDefesa.Metrelhadora).Scl(0.6f).At(new Vector2(player.position.X, -1.35f)));
            Cenas[1].AddSprite(new Defence(Content, "turret", TipoDefesa.Metrelhadora).Scl(0.6f).At(new Vector2(player.position.X + 0.6f, -1.35f)));
            Cenas[1].AddSprite(new Defence(Content, "turret", TipoDefesa.Metrelhadora).Scl(0.6f).At(new Vector2(player.position.X + 10f, -1.00f)));

            Cenas[1].AddSprite(new Defence(Content, "turret", TipoDefesa.Metrelhadora).Scl(0.6f).At(new Vector2(player.position.X + 14f, -1.19f)));

            Cenas[1].AddSprite(new Defence(Content, "turret", TipoDefesa.Metrelhadora).Scl(0.6f).At(new Vector2(player.position.X + 15.5f, -1.10f)));

            Cenas[1].AddSprite(new Defence(Content, "turret", TipoDefesa.Metrelhadora).Scl(0.6f).At(new Vector2(player.position.X + 18.6f, -1.35f)));
            Cenas[1].AddSprite(new Defence(Content, "turret", TipoDefesa.Metrelhadora).Scl(0.6f).At(new Vector2(player.position.X + 23f, -1.30f)));

            // superiores

            Cenas[1].AddSprite(new Defence(Content, "turret", TipoDefesa.Metrelhadora).Scl(0.6f).At(new Vector2(player.position.X, 1.2f)));
            //Cenas[1].AddSprite(new Defence(Content, "turret").Scl(0.6f).At(new Vector2(player.position.X+0.7f,1.35f )));
            Cenas[1].AddSprite(new Defence(Content, "turret", TipoDefesa.Metrelhadora).Scl(0.6f).At(new Vector2(player.position.X + 3.5f, 0.2f)));
            Cenas[1].AddSprite(new Defence(Content, "turret", TipoDefesa.Metrelhadora).Scl(0.6f).At(new Vector2(player.position.X + 5.2f, 0.2f)));

            Cenas[1].AddSprite(new Defence(Content, "turret", TipoDefesa.Metrelhadora).Scl(0.6f).At(new Vector2(player.position.X + 12f, 0.6f)));
            Cenas[1].AddSprite(new Defence(Content, "turret", TipoDefesa.Metrelhadora).Scl(0.6f).At(new Vector2(player.position.X + 18f, 0.98f)));
            Cenas[1].AddSprite(new Defence(Content, "turret", TipoDefesa.Metrelhadora).Scl(0.6f).At(new Vector2(player.position.X + 21f, 0.30f)));
            Cenas[1].AddSprite(new Defence(Content, "turret", TipoDefesa.Metrelhadora).Scl(0.6f).At(new Vector2(player.position.X + 24.8f, 0.5f)));

            // armas de laser

            Cenas[1].AddSprite(new Defence(Content, "laserweapon", TipoDefesa.Laser,0.20f).Scl(0.15f).At(new Vector2(player.position.X+4.4f, 0f)));
            Cenas[1].AddSprite(new Defence(Content, "laserweapon", TipoDefesa.Laser,0.18f).Scl(0.15f).At(new Vector2(player.position.X + 8.4f, 0.85f)));
            Cenas[1].AddSprite(new Defence(Content, "laserweapon", TipoDefesa.Laser,0.17f).Scl(0.15f).At(new Vector2(player.position.X + 12.4f, 0.5f)));
            Cenas[1].AddSprite(new Defence(Content, "laserweapon", TipoDefesa.Laser,0.19f).Scl(0.15f).At(new Vector2(player.position.X + 15.4f, 0.6f)));
            Cenas[1].AddSprite(new Defence(Content, "laserweapon", TipoDefesa.Laser,0.20f).Scl(0.15f).At(new Vector2(player.position.X + 20.4f, 0.15f)));
            Cenas[1].AddSprite(new Defence(Content, "laserweapon", TipoDefesa.Laser, 0.15f).Scl(0.15f).At(new Vector2(player.position.X + 24f, 0.48f)));
            Cenas[1].AddSprite(new Defence(Content, "laserweapon", TipoDefesa.Laser, 0.20f).Scl(0.15f).At(new Vector2(player.position.X + 24.2f, 0.365f)));
            Cenas[1].AddSprite(new Defence(Content, "laserweapon", TipoDefesa.Laser,0.25f).Scl(0.15f).At(new Vector2(player.position.X + 24.4f, 0.4f)));


        }

        void reStart()
        {
            if (timeToRestart >= 2000)
            {

                Cenas.Clear();
                GC.Collect();
                scene = new Scene(spriteBatch);
                Cenas.Add(scene);
                LoadLevel(gamestate);
                timeToRestart = 0;
            }
        }

 
    }
}
