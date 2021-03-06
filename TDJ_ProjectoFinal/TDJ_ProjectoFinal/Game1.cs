﻿using Microsoft.Xna.Framework;
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
    /// Nivel em que o jogo se encontra.
    /// </summary>
    public enum GameState
    {
        /// <summary>
        /// Menu inicial
        /// </summary>
        Menu,
        /// <summary>
        /// 1º animação
        /// </summary>
        Bridge0,
        /// <summary>
        /// Nível 1
        /// </summary>
        Nivel1,
        /// <summary>
        /// 2º animação
        /// </summary>
        Bridge1,
        /// <summary>
        /// Nivel 2
        /// </summary>
        Nivel2,
        /// <summary>
        /// 3ª animação
        /// </summary>
        Bridge2,
        /// <summary>
        /// Nivel 3
        /// </summary>
        Nivel3,
        /// <summary>
        /// Animação final
        /// </summary>
        End
    }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {


        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Scene scene,scene2,scene3;
        Player player;
        /// <summary>
        /// Instância de Random partilhada por todo o jogo
        /// </summary>
        public static Random random;
        bool novopowerup = false;
        float timer;
        Scene Cena;
        Random randomShake;
        GameState gamestate;
        SpriteFont font;
        int kbLimit;
        int timerTextos;
        KeyValuePair<string, Vector2> texto;
        KeyValuePair<string, Vector2> textoBridge1,textoBridge2,textoEnd;
        int timeToRestart;
        bool RestartReady = false;
        int endTimer = 0;
        bool endTimerBool = false;

        /// <summary>
        /// Construtor do jogo
        /// </summary>
        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 1000;
            
        }

        /// <summary>
        /// Inicializa o jogo
        /// </summary>
        protected override void Initialize()
        {

            random = new Random();
            randomShake = new Random();
            
            Camera.SetGraphicsDeviceManager(graphics);
            Camera.SetTarget(Vector2.Zero);
            Camera.SetWorldWidth(5);

            som.Initialize(Content);
            
            base.Initialize();
        }

       /// <summary>
       /// Carrega os assets do jogo e o estado inicial
       /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            scene = new Scene(spriteBatch);
            Cena = scene;
            font = Content.Load<SpriteFont>("MyFont");
            WeaponsManager.LoadContent(Content);
            EnemyManager.LoadContent(Content, random);
            kbLimit = 0;
            timeToRestart = 0;
            timerTextos = 0;
            texto = new KeyValuePair<string, Vector2>("", Vector2.Zero);
            textoBridge1 = new KeyValuePair<string, Vector2>("", Vector2.Zero);
            textoBridge2 = new KeyValuePair<string, Vector2>("", Vector2.Zero);
            textoEnd = new KeyValuePair<string, Vector2>("", Vector2.Zero);

            LoadLevel(GameState.Menu);
            
        }

        private void LoadLevel(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Menu:

                    Cena.Clear();

                    gamestate = GameState.Menu;

                    Camera.SetTarget(Vector2.Zero);

                    Camera.velocidadegeral = 0;
                    Cena.AddSprite(new Sprite(Content, "mainMenu").At(Vector2.Zero).Scl(5f));
                    Cena.AddSprite(new Sprite(Content, "backgroundTextoMenu").At(new Vector2(0, -0.12f)).Scl(5f));

                    som.playMusicaMenu();
                    
                    
                    break;
                case GameState.Bridge0:

                    Cena.Clear();

                    gamestate = GameState.Bridge0;

                    Camera.SetTarget(new Vector2(24, 0));
                    Camera.velocidadegeral = -0.002f;

                    //Fundo do universo (imóvel)
                    Cena.AddSprite(new SlidingBackground(Content, "universe", 4f).Scl(6000 * Camera.worldWidth / graphics.PreferredBackBufferHeight).
                        At(new Vector2(Camera.worldWidth, 0f)));
                    //lua
                    Cena.AddSprite(new SlidingBackground(Content, "fullMoon", 6f).Scl(1f).At(new Vector2(17, 0f)));
                    //planeta
                    Cena.AddSprite(new SlidingBackground(Content, "planeta", 9f).Scl(8f).At(new Vector2(14, -2.7f)));
                    //estacao espacial
                    Cena.AddSprite(new SlidingBackground(Content, "spaceStaion", 500f).Scl(4f).At(new Vector2(19f, 0f)));

                    Cena.AddSprite(new Sprite(Content, "backgroundTextoMenuGigante").At(new Vector2(7, -0.62f)).Scl(40f));

                    Cena.AddTexto("First contact wasn't how we expected.", new Vector2(110, 400));
                    Cena.AddTexto("From outer space, a single message:", new Vector2(135, 400));
                    Cena.AddTexto("\"WE ARE COMING.\"", new Vector2(320, 400));
                    Cena.AddTexto("In two months they arrived at Jupiter,", new Vector2(108, 400));
                    Cena.AddTexto("with a four mile wide mothership", new Vector2(150, 400));
                    Cena.AddTexto("carrying hundreds of fighters and bombers.", new Vector2(90, 400));
                    Cena.AddTexto("In the time it took them to get here", new Vector2(120, 400));
                    Cena.AddTexto("and decelerate into low earth orbit, ", new Vector2(125, 400));
                    Cena.AddTexto("a brave team of engineers from IPCA", new Vector2(125, 400));
                    Cena.AddTexto("infiltrated the Morkian mainframes", new Vector2(160, 400));
                    Cena.AddTexto("and reverse-engineered their technology.", new Vector2(105, 400));
                    Cena.AddTexto("First they will have to defeat ", new Vector2(170, 400));
                    Cena.AddTexto("All the spaceships defending the mothership. ", new Vector2(95, 400));
                    Cena.AddTexto("They are Earth's only hope! ", new Vector2(280, 400));
                    Cena.AddTexto("FIM - ESTE NÃO APARECE ", new Vector2(0, 0));

                    

                    som.playMusicaBridge0();
                    
                    break;
                case GameState.Nivel1:

                    Cena.Clear();

                    gamestate = GameState.Nivel1;

                    Camera.SetTarget(new Vector2(0, 0));
                    Camera.velocidadegeral = 0.007f;

                    //Fundo do universo (imóvel)
                    Cena.AddSprite(new SlidingBackground(Content, "universe", 4f).Scl(6000 * Camera.worldWidth / graphics.PreferredBackBufferHeight).
                        At(new Vector2(Camera.worldWidth, 0f)));
                    //lua
                    Cena.AddSprite(new SlidingBackground(Content, "fullMoon", 6f).Scl(1f).At(new Vector2(10, 0f)));
                    //planeta
                    Cena.AddSprite(new SlidingBackground(Content, "planeta", 9f).Scl(8f).At(new Vector2(8, -2.7f)));
                    //estacao espacial
                    Cena.AddSprite(new SlidingBackground(Content, "spaceStaion", 500f).Scl(4f).At(new Vector2(14f, 0.8f)));
                    //Nave do jogador
                    player = new Player(Content, "nave", TipoBala.Simples);
                    Cena.AddSprite(player.Scl(0.50f));
                    Cena.player = player;

                    newEnemyWave(Cena);

                    //PowerUP
                    Cena.AddPowerUp(new PowerUp(Content, "PowerUp-Vida", TipoPowerUp.Vida, -1, 0.3f, 1f));
                    //scene.AddPowerUp(new PowerUp(Content, "PowerUp-Bala", TipoPowerUp.Armas, -1, 0.3f, 1.5f));

                    som.playMusicaNivel1();
                    
                    break;
                case GameState.Bridge1:
                    
                    timerTextos = 0;
                    Cena.Clear();
                    
                    gamestate = GameState.Bridge1;

                    scene2 = new Scene(spriteBatch);
                    Cena = scene2;
                    Cena.active = true;
                    Camera.SetTarget(new Vector2(0, 0));
                    Camera.velocidadegeral = 0.002f;

                    Cena.AddSprite(new SlidingBackground(Content, "universe", 4f).Scl(6000 * Camera.worldWidth / graphics.PreferredBackBufferHeight).
                       At(new Vector2(Camera.worldWidth, 0f)));
                    Cena.AddSprite(new SlidingBackground(Content, "nave", 0.7f).Scl(0.4f).At(new Vector2(-1.5f, 0.3f)));
                    Cena.AddSprite(new Sprite(Content, "naveMaeCenario").Scl(10f).At(new Vector2(6.5f, 0)));

                    //Cena.textos.Dequeue();
                    Cena.AddSprite(new Sprite(Content, "backgroundTextoMenuGigante").At(new Vector2(7, -0.62f)).Scl(40f));
                    Cena.AddTexto("The defence ships have been destroyed. Good job!", new Vector2(50, 400));
                    Cena.AddTexto("Now we must get into the mother ship and destroy it,", new Vector2(10, 400));
                    Cena.AddTexto("Tech team is detecting several traps.", new Vector2(160, 400));
                    Cena.AddTexto("Get inside and stay sharp. Good luck.", new Vector2(120, 400));
                    Cena.AddTexto("fim, nao aparece", new Vector2(60, 400));

                    som.playMusicaBridge0();

                    break;
                case GameState.Nivel2:

                    Cena.Clear();

                    Camera.velocidadegeral = 0.007f;

                    gamestate = GameState.Nivel2;

                    Cena.sprites.Clear();
                    Cena.inimigos.Clear();
                    Cena.powerUps.Clear();
                    Cena.explosoes.Clear();
                    Cena.active = false;
                    GC.Collect();

                    //scene2 = new Scene(spriteBatch);
                    //Cenas.Add(scene2);
                    //Cenas[1].active = true;
                    Camera.SetTarget(Vector2.Zero);

                    Cena.AddSprite(new SlidingBackground(Content, "fundoFinal", 3f).Scl(45f).
                       At(new Vector2(Camera.worldWidth, 0f)));
                    Cena.AddSprite(new Cenario(Content, "mapaFinalCima", 40f).At(new Vector2(5, 0.1f)));
                    Cena.AddSprite(new Cenario(Content, "mapaFinalBaixo", 40f).At(new Vector2(5, -0.1f)));
                    player = new Player(Content, "nave", TipoBala.Simples);
                    Cena.AddSprite(player.Scl(0.50f));
                    
                    Defesas();

                    Cena.player = player;
                    player.position.X = Camera.target.X - (Camera.worldWidth / 2) + 0.1f;

                    som.playMusicaNivel2();

                    break;
                case GameState.Bridge2:

                    
                    timerTextos = 0;
                    Cena.Clear();
                    
                    gamestate = GameState.Bridge2;

                    scene3 = new Scene(spriteBatch);
                    Cena = scene3;
                    Cena.active = true;
                    Camera.SetTarget(new Vector2(0, 0));
                    Camera.velocidadegeral = 0.002f;

                    Cena.AddSprite(new SlidingBackground(Content, "fundoFinal", 4f).Scl(6000 * Camera.worldWidth / graphics.PreferredBackBufferHeight).
                       At(new Vector2(Camera.worldWidth, 0f)));
                    Cena.AddSprite(new SlidingBackground(Content, "nave", 0.4f).Scl(0.65f).At(new Vector2(-1.5f, 0.3f)));
                    
                    
                    Cena.AddSprite(new Sprite(Content, "backgroundTextoMenuGigante").At(new Vector2(7, -0.62f)).Scl(40f));
                    Cena.AddTexto("That was close. Great job avoiding the defences!", new Vector2(50, 400));
                    Cena.AddTexto("Tech team found the Morkians leader chamber,", new Vector2(60, 400));
                    Cena.AddTexto("This is our chance to end this!", new Vector2(180, 400));
                    Cena.AddTexto("Destroy him and save mankind.", new Vector2(200, 400));
                    Cena.AddTexto("fim, nao aparece", new Vector2(60, 400));

                    som.playMusicaBridge0();
                    break;
                case GameState.Nivel3:

                    Cena.Clear();

                    Camera.velocidadegeral = 0.007f;

                    gamestate = GameState.Nivel3;

                    scene3 = new Scene(spriteBatch);
                    Cena = scene3;
                    Camera.SetTarget(Vector2.Zero);

                    Cena.AddSprite(new Sprite(Content, "fundoFinal").Scl(45f).
                       At(new Vector2(Camera.worldWidth, 0f)));
                    //Cenas[2].AddInimigo(new NPC(Content, "boss", TipoNave.Mothership, 2f, random).At(new Vector2(3f, 0f)));
                    //Cenas[2].AddInimigo(new AnimatedSprite(Content,"bossSheet",1,3,true,new Vector2(1f,1f),2f));
                    Cena.AddInimigo(new Boss(Content, "bossSheet2", 1, 2, true, new Vector2(1f, 1f), 2f));
                    player = new Player(Content, "nave", TipoBala.Simples);
                    Cena.AddSprite(player.Scl(0.50f));
                    Cena.player = player;
                    player.position.X = Camera.target.X - (Camera.worldWidth / 2) + 0.1f;
                    player.tipobala = TipoBala.Quadruplo;
                    
                    som.playMusicaNivel3();

                    break;
                case GameState.End:

                   
                    Cena.Clear();
                    Camera.velocidadegeral = 0.007f;

                    gamestate = GameState.End;

                    scene3 = new Scene(spriteBatch);
                    Cena = scene3;
                    Cena.AddSprite(new SlidingBackground(Content, "universe", 4f).Scl(6000 * Camera.worldWidth / graphics.PreferredBackBufferHeight).
                       At(new Vector2(Camera.worldWidth, 0f)));

                    Cena.AddSprite(new SlidingBackground(Content, "nave", 0.7f).Scl(0.4f).At(new Vector2(Camera.target.X- 1.5f, 0.3f)));


                    Cena.AddSprite(new Sprite(Content, "naveMaeCenario").Scl(10f).At(new Vector2(Camera.target.X - 3.9f, 0f)));
                    Cena.AddExplosao(new AnimatedSprite(Content, "explosao", 9, 9, false, new Vector2(Camera.target.X -0.6f, 0.8f), 0.5f, 50f));
                    Cena.AddExplosao(new AnimatedSprite(Content, "explosao", 9, 9, false, new Vector2(Camera.target.X - 0.0f, 0f), 0.5f, 1000f));
                    Cena.AddExplosao(new AnimatedSprite(Content, "explosao", 9, 9, false, new Vector2(Camera.target.X - 0.6f, -0.8f), 1.1f, 800f));
                    Cena.AddExplosao(new AnimatedSprite(Content, "explosao", 9, 9, false, new Vector2(Camera.target.X - 0.6f, -0.8f), 0.5f, 50f));
                    Cena.AddExplosao(new AnimatedSprite(Content, "explosao", 9, 9, false, new Vector2(Camera.target.X - 1.6f, 0.8f), 0.9f, 500f));

                    Cena.AddExplosao(new AnimatedSprite(Content, "explosao", 9, 9, false, new Vector2(Camera.target.X - 0.6f, 0.8f), 0.5f, 500f));
                    Cena.AddExplosao(new AnimatedSprite(Content, "explosao", 9, 9, false, new Vector2(Camera.target.X - 0.0f, 0f), 0.5f, 1200f));
                    Cena.AddExplosao(new AnimatedSprite(Content, "explosao", 9, 9, false, new Vector2(Camera.target.X - 0.9f, -0.3f), 1.1f, 1800f));
                    Cena.AddExplosao(new AnimatedSprite(Content, "explosao", 9, 9, false, new Vector2(Camera.target.X - 0.3f, -0.4f), 0.5f, 150f));
                    Cena.AddExplosao(new AnimatedSprite(Content, "explosao", 9, 9, false, new Vector2(Camera.target.X - 1.6f, 0.5f), 0.9f, 1500f));

                    Cena.AddExplosao(new AnimatedSprite(Content, "explosao", 9, 9, false, new Vector2(Camera.target.X - 0.9f, 0.9f), 0.5f, 1900f));
                    Cena.AddExplosao(new AnimatedSprite(Content, "explosao", 9, 9, false, new Vector2(Camera.target.X - 0.0f, 0f), 1.5f, 2500f));
                    Cena.AddExplosao(new AnimatedSprite(Content, "explosao", 9, 9, false, new Vector2(Camera.target.X - 0.1f, 0f), 1.1f, 2100f));
                    Cena.AddExplosao(new AnimatedSprite(Content, "explosao", 9, 9, false, new Vector2(Camera.target.X - 0.5f, -0.4f), 1.5f, 2500f));
                    Cena.AddExplosao(new AnimatedSprite(Content, "explosao", 9, 9, false, new Vector2(Camera.target.X - 1.9f, 0.5f), 2.5f, 2800f));

                    Cena.AddExplosao(new AnimatedSprite(Content, "explosao", 9, 9, false, new Vector2(Camera.target.X - 1.2f, 0f), 2.6f, 2800f));
                    Cena.AddExplosao(new AnimatedSprite(Content, "explosao", 9, 9, false, new Vector2(Camera.target.X - 0.91f, -0.9f), 2.6f, 2900f));
                    Cena.AddExplosao(new AnimatedSprite(Content, "explosao", 9, 9, false, new Vector2(Camera.target.X - 1.2f, 0.1f), 2.6f, 3000f));
                    Cena.AddExplosao(new AnimatedSprite(Content, "explosao", 9, 9, false, new Vector2(Camera.target.X - 0.1f, -0.1f), 2.6f, 3200f));

                    Cena.AddExplosao(new AnimatedSprite(Content, "explosao", 9, 9, false, new Vector2(Camera.target.X - 1.2f, 0f), 2.6f, 3100f));
                    Cena.AddExplosao(new AnimatedSprite(Content, "explosao", 9, 9, false, new Vector2(Camera.target.X - 0.91f, -0.9f), 2.6f, 3500f));
                    Cena.AddExplosao(new AnimatedSprite(Content, "explosao", 9, 9, false, new Vector2(Camera.target.X - 1.2f, 0.1f), 2.6f, 3700f));
                    Cena.AddExplosao(new AnimatedSprite(Content, "explosao", 9, 9, false, new Vector2(Camera.target.X - 0.1f, 0.1f), 2.6f, 4000f));
                    Cena.AddExplosao(new AnimatedSprite(Content, "explosao", 9, 9, false, new Vector2(Camera.target.X - 0.1f, 0.1f), 2.6f, 4500f));
                    Cena.AddExplosao(new AnimatedSprite(Content, "explosao", 9, 9, false, new Vector2(Camera.target.X - 0.1f, 0.1f), 2.6f, 4700f));
                    Cena.AddExplosao(new AnimatedSprite(Content, "explosao", 9, 9, false, new Vector2(Camera.target.X - 0.1f, 0.1f), 2.6f, 5200f));
                    Cena.AddExplosao(new AnimatedSprite(Content, "explosao", 9, 9, false, new Vector2(Camera.target.X , 0.1f), 2.6f, 5700f));
                    Cena.AddExplosao(new AnimatedSprite(Content, "explosao", 9, 9, false, new Vector2(Camera.target.X + 0.8f, 0.1f), 2.6f, 6500f));
                    
                    Cena.AddTexto("", new Vector2(160, 400));
                    Cena.AddTexto("Congratulations!!!", new Vector2(310,400));
                    Cena.AddTexto("You saved humanity", new Vector2(300, 400));
                    Cena.AddTexto("Wow, we never tought you could do it... ", new Vector2(145, 400));
                    Cena.AddTexto("Well Done.", new Vector2(370, 400));
                    Cena.AddTexto("MISSION ACCOMPLISHED!", new Vector2(310, 400));
                    Cena.AddTexto("fim, nao aparece", new Vector2(200, 400));


                    break;
                default:
                    break;
            }
        }

            
        /// <summary>
        /// Descarrega os assets e recursos utilizados pelo jogo
        /// </summary>
        protected override void UnloadContent()
        {
            
        }

       /// <summary>
       /// faz o update ao jogo em cada frame.
       /// </summary>
       /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Cena.bossLevelClear) 
            {
                timer += gameTime.ElapsedGameTime.Milliseconds;
            }

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
                        LoadLevel(GameState.Nivel3);
                        Camera.velocidadegeral = 0.007f;
                        break;
                    case GameState.Nivel3:
                        Camera.velocidadegeral = 0.007f;
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
            if(gamestate == GameState.End)
                endTimer += gameTime.ElapsedGameTime.Milliseconds;
            
            UpdateScenes(gameTime);

            if (gamestate == GameState.End && endTimer > 5000 && !endTimerBool)
            {
                Cena.AddSprite(new Sprite(Content, "backgroundTextoMenuGigante").At(new Vector2(7, -0.62f)).Scl(40f));
                endTimerBool = true;
            }

            base.Update(gameTime);
        }

        private void UpdateScenes(GameTime gameTime)
        {


            if ((gamestate == GameState.Nivel1 || gamestate == GameState.Nivel2 || gamestate == GameState.Nivel3) && Cena.player == null)
            {
                if (!RestartReady) RestartReady = true;
                reStart();
            }
            Camera.Update(randomShake);

            Cena.Update(gameTime);
                
          
            ChangeScenes();
        }

        private void ChangeScenes()
        {
            if (Cena.inimigos.Count <= 5 && Cena.active == true && Camera.target.X < 18f && gamestate == GameState.Nivel1)
            {
                //Matámos todos os inimigos, nova ronda
                newEnemyWave(Cena);
                novopowerup = false;
            }

            if (!novopowerup && Cena.active == true && gamestate == GameState.Nivel1)
            {
                // os inimigos estão a terminar...
                // nova ronda surgitrá
                // (apenas teste)
                Cena.AddPowerUp(new PowerUp(Content, "PowerUp-Bala", TipoPowerUp.Armas, -1, 0.3f, 0f));

                novopowerup = true;

            }
            //muda para cena2
            if (Camera.target.X >= 25f && gamestate == GameState.Nivel1)
            {
                player.position.X += 0.02f;

                if (player.position.X >= (Camera.GetTarget().X + Camera.worldWidth / 2) - 0.5f)
                {
                    Cena.active = false;
                   
                    LoadLevel(GameState.Bridge1);

                }
            }
            //muda para cena 3
            if (Camera.target.X >= 25f && gamestate == GameState.Nivel2)
            {
                player.position.X += 0.02f;

                if (player.position.X >= (Camera.GetTarget().X + Camera.worldWidth / 2) - 0.5f)
                {
                    Cena.active = false;
                    LoadLevel(GameState.Bridge2);
                }
            }


            
            if (gamestate == GameState.Nivel3)
            {
                if (Cena.bossKilled) 
                {
                    player.position.X += 0.02f;
                }
                

                if (timer>=1900f&& player.position.X>Camera.worldWidth+1.8f) 
                {
                    
                    

                    //if (player.position.X >= (Camera.GetTarget().X + Camera.worldWidth / 2) - 0.5f)
                    //{
                    LoadLevel(GameState.End);
                    //Cena.active = false;
                       
                    //}
                }
            }
            
        }

        /// <summary>
        /// Desenha os elementos do Jogo.
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Cena.Draw(gameTime);
            
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
                        texto = Cena.GetTexto();
                    }

                    spriteBatch.Begin();
                    spriteBatch.DrawString(font, texto.Key, texto.Value, Color.White);
                    spriteBatch.End();

                    if (timerTextos >= 5000 && Cena.textos.Count > 0)
                    {
                        texto = Cena.GetTexto();
                    }
                    if (timerTextos > 5000)
                    {
                        timerTextos = 0;
                        if (Cena.textos.Count == 0)
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
                    
                    if (timerTextos == 0)
                    {
                        textoBridge1 = Cena.GetTexto();
                    }

                    spriteBatch.Begin();
                    spriteBatch.DrawString(font, textoBridge1.Key, textoBridge1.Value, Color.White);
                    spriteBatch.End();

                    if (timerTextos >= 5000 && Cena.textos.Count > 0)
                    {
                        textoBridge1 = Cena.GetTexto();
                    }
                    if (timerTextos > 5000)
                    {
                        timerTextos = 0;
                        if (Cena.textos.Count == 0)
                        {
                            //Acabaram os textos, seguir
                            LoadLevel(GameState.Nivel2);
                        }
                    }
                    timerTextos += gameTime.ElapsedGameTime.Milliseconds;
                    break;
                case GameState.Nivel2:
                    break;
                case GameState.Bridge2:
                     if (timerTextos == 0)
                    {
                        textoBridge2 = Cena.GetTexto();
                    }

                    spriteBatch.Begin();
                    spriteBatch.DrawString(font, textoBridge2.Key, textoBridge2.Value, Color.White);
                    spriteBatch.End();

                    if (timerTextos >= 5000 && Cena.textos.Count > 0)
                    {
                        textoBridge2 = Cena.GetTexto();
                    }
                    if (timerTextos > 5000)
                    {
                        timerTextos = 0;
                        if (Cena.textos.Count == 0)
                        {
                            //Acabaram os textos, seguir
                            LoadLevel(GameState.Nivel3);
                        }
                    }
                    timerTextos += gameTime.ElapsedGameTime.Milliseconds;
                    break;
                case GameState.Nivel3:
                    break;
                case GameState.End:
                    if (timerTextos == 0)
                    {
                        textoEnd = Cena.GetTexto();
                    }

                    spriteBatch.Begin();
                    spriteBatch.DrawString(font, textoEnd.Key, textoEnd.Value, Color.White);
                    spriteBatch.End();

                    if (timerTextos >= 5000 && Cena.textos.Count > 0)
                    {
                       
                        textoEnd = Cena.GetTexto();
                    }
                    if (timerTextos > 5000)
                    {
                        timerTextos = 0;
                        if (Cena.textos.Count == 0)
                        {
                            //Acabaram os textos, seguir
                            LoadLevel(GameState.Menu);
                        }
                    }
                    timerTextos += gameTime.ElapsedGameTime.Milliseconds;
                    break;
                default:
                    break;
            }

            //Desenhar a UI
            Cena.UIs.Clear();

            if ((gamestate == GameState.Nivel1)
                && Cena.player != null && Cena.player.Vida > 0)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(font, "" + Cena.enemiesKilled * 100, new Vector2(800f, 25f), Color.Green);
                spriteBatch.End();
            }
            
            
            if (Cena.player != null && Cena.player.Vida > 0)
            {
                if (gamestate == GameState.Nivel1 || gamestate == GameState.Nivel2 || gamestate == GameState.Nivel3)
                {
                    //UI
                    
                    Cena.AddUI(new UI(Content, "UI\\GreenUIv2", new Vector2(2.3f, -0.2f)).Scl(4.5f));//-0.2f
                    for (int i = 0; i < 5; i++)
                    {
                        Cena.AddUI(new UI(Content, "UI\\vida4" /*+ (i + 1)*/, new Vector2(2.1f + i * 0.04f, -0.153f)).Scl(0.02f));//y=-0.153
                    }
                    for (int i = 0; i < Cena.player.Vida; i++)
                    {
                        Cena.AddUI(new UI(Content, "UI\\vida5versao2" /*+ (i + 1)*/, new Vector2(2.1f + i * 0.04f, -0.153f)).Scl(0.03f));
                    }
                    
                }
            }
            

            base.Draw(gameTime);
        }

        /// <summary>
        /// Recebe por parametro a cena e adiciona novos enimigos ao enemyManager
        /// </summary>
        /// <param name="cena"></param>
        public void newEnemyWave(Scene cena)
        {

            //Alguns inimigos
            cena.AddInimigo(EnemyManager.addKamikaze()
                .At(new Vector2(Camera.worldWidth + player.position.X, -1f)));
            cena.AddInimigo(EnemyManager.addKamikaze()
                .At(new Vector2(Camera.worldWidth + player.position.X+1f, 0f)));
            cena.AddInimigo(EnemyManager.addKamikaze()
                .At(new Vector2(Camera.worldWidth + player.position.X+2f, 1f)));


            cena.AddInimigo(EnemyManager.addBombardeiro()
                .At(new Vector2(Camera.worldWidth + player.position.X + 5, 1f)));
            cena.AddInimigo(EnemyManager.addBombardeiro()
                .At(new Vector2(Camera.worldWidth + player.position.X + 6, -1f)));

            cena.AddInimigo(EnemyManager.addBombardeiro()
                .At(new Vector2(Camera.worldWidth + player.position.X + 2, 0f)));
            cena.AddInimigo(EnemyManager.addBombardeiro()
                .At(new Vector2(Camera.worldWidth + player.position.X + 3, 0.5f)));
            cena.AddInimigo(EnemyManager.addBombardeiro()
                .At(new Vector2(Camera.worldWidth + player.position.X + 4, -0.8f)));

            cena.AddInimigo(EnemyManager.addCaça()
                .At(new Vector2(Camera.worldWidth + player.position.X + 10, 1f)));

            //scene.AddInimigo(new NPC(Content, "nave", TipoNave.Mothership).Scl(0.5f).
            //    At(new Vector2(Camera.worldWidth, 0f)));

            
            
        }
        /// <summary>
        /// Adiciona á lista de sprites da cena elementos do tipo Defense - turrets do segundo nivel.
        /// </summary>
        public void Defesas()
        {
            // conjunto de ddefesas do nivel 2
            // metrelhadoras
            //inferiores
            Cena.AddSprite(new Defence(Content, "turret", TipoDefesa.Metrelhadora).Scl(0.6f).At(new Vector2(player.position.X, -1.20f)));
            Cena.AddSprite(new Defence(Content, "turret", TipoDefesa.Metrelhadora).Scl(0.6f).At(new Vector2(player.position.X + 0.6f, -1.20f)));
            Cena.AddSprite(new Defence(Content, "turret", TipoDefesa.Metrelhadora).Scl(0.6f).At(new Vector2(player.position.X + 10f, -0.90f)));

            Cena.AddSprite(new Defence(Content, "turret", TipoDefesa.Metrelhadora).Scl(0.6f).At(new Vector2(player.position.X + 14f, -1.09f)));

            Cena.AddSprite(new Defence(Content, "turret", TipoDefesa.Metrelhadora).Scl(0.6f).At(new Vector2(player.position.X + 15.5f, -1.10f)));

            Cena.AddSprite(new Defence(Content, "turret", TipoDefesa.Metrelhadora).Scl(0.6f).At(new Vector2(player.position.X + 18.6f, -1.35f)));
            Cena.AddSprite(new Defence(Content, "turret", TipoDefesa.Metrelhadora).Scl(0.6f).At(new Vector2(player.position.X + 23f, -1.30f)));

            // superiores

            Cena.AddSprite(new Defence(Content, "turret", TipoDefesa.Metrelhadora).Scl(0.6f).At(new Vector2(player.position.X, 1.2f)));
            //Cenas[1].AddSprite(new Defence(Content, "turret").Scl(0.6f).At(new Vector2(player.position.X+0.7f,1.35f )));
            Cena.AddSprite(new Defence(Content, "turret", TipoDefesa.Metrelhadora).Scl(0.6f).At(new Vector2(player.position.X + 3.5f, 0.2f)));
            Cena.AddSprite(new Defence(Content, "turret", TipoDefesa.Metrelhadora).Scl(0.6f).At(new Vector2(player.position.X + 5.2f, 0.2f)));

            Cena.AddSprite(new Defence(Content, "turret", TipoDefesa.Metrelhadora).Scl(0.6f).At(new Vector2(player.position.X + 12f, 0.6f)));
            Cena.AddSprite(new Defence(Content, "turret", TipoDefesa.Metrelhadora).Scl(0.6f).At(new Vector2(player.position.X + 18f, 0.98f)));
            Cena.AddSprite(new Defence(Content, "turret", TipoDefesa.Metrelhadora).Scl(0.6f).At(new Vector2(player.position.X + 21f, 0.30f)));
            Cena.AddSprite(new Defence(Content, "turret", TipoDefesa.Metrelhadora).Scl(0.6f).At(new Vector2(player.position.X + 24.8f, 0.5f)));

            // armas de laser

            Cena.AddSprite(new Defence(Content, "laserweapon", TipoDefesa.Laser, 0.20f).Scl(0.15f).At(new Vector2(player.position.X + 4.4f, 0f)));
            Cena.AddSprite(new Defence(Content, "laserweapon", TipoDefesa.Laser, 0.18f).Scl(0.15f).At(new Vector2(player.position.X + 8.4f, 0.85f)));
            Cena.AddSprite(new Defence(Content, "laserweapon", TipoDefesa.Laser, 0.17f).Scl(0.15f).At(new Vector2(player.position.X + 12.4f, 0.5f)));
            Cena.AddSprite(new Defence(Content, "laserweapon", TipoDefesa.Laser, 0.19f).Scl(0.15f).At(new Vector2(player.position.X + 15.4f, 0.6f)));
            Cena.AddSprite(new Defence(Content, "laserweapon", TipoDefesa.Laser, 0.20f).Scl(0.15f).At(new Vector2(player.position.X + 20.4f, 0.15f)));
            Cena.AddSprite(new Defence(Content, "laserweapon", TipoDefesa.Laser, 0.15f).Scl(0.15f).At(new Vector2(player.position.X + 24f, 0.48f)));
            Cena.AddSprite(new Defence(Content, "laserweapon", TipoDefesa.Laser, 0.20f).Scl(0.15f).At(new Vector2(player.position.X + 24.2f, 0.365f)));
            Cena.AddSprite(new Defence(Content, "laserweapon", TipoDefesa.Laser, 0.25f).Scl(0.15f).At(new Vector2(player.position.X + 24.4f, 0.4f)));


        }

        void reStart()
        {
            if (timeToRestart >= 2000)
            {

                Cena.Clear();
                scene = new Scene(spriteBatch);
                Cena = scene;
                timeToRestart = 0;
                RestartReady = false;
                LoadLevel(gamestate);
                
            }
            Camera.velocidadegeral -= Camera.velocidadegeral / 30f;
        }

 
    }
}
