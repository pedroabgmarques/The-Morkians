﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDJ_ProjectoFinal.graficos;

namespace TDJ_ProjectoFinal.entidades
{
    /// <summary>
    /// Tipo de bala do Player
    /// </summary>
    public enum TipoBala
    {
        /// <summary>
        /// Tipo de bala simples
        /// </summary>
        Simples,
        /// <summary>
        /// Tipo de ala duplo
        /// </summary>
        Duplo,
        /// <summary>
        /// Tipo de bala triplo
        /// </summary>
        Triplo,
        /// <summary>
        /// Tipo de bala quadruplo
        /// </summary>
        Quadruplo

    }

    /// <summary>
    /// Define um jogador
    /// </summary>
    public class Player : FlyingEntity
    {
        

        
        KeyboardState currentKeyboardState;
        
        private int contador;
        private ContentManager contents;
        private float shootTime;
        /// <summary>
        /// Vida do jogador
        /// </summary>
        public int Vida;
        /// <summary>
        /// Pontuação do jogador
        /// </summary>
        public int pontuacao;
        /// <summary>
        /// Última posição conhecida
        /// </summary>
        public Vector2 lastposition;
        /// <summary>
        /// Boundingbox à volta do jogador
        /// </summary>
        public Rectangle boundingBox;
        
        Sprite collided;
        Vector2 collisionPoint;
        
        /// <summary>
        /// Tipo de balas que o jogador dispara
        /// </summary>
        public TipoBala tipobala;
        private int contadorMisseis;
        private float maxSpeed = Camera.velocidadegeral*3;

        /// <summary>
        /// Retorna o tipo de bala em uso pelo Player.
        /// </summary>
        /// <returns>Tipo de bala</returns>
        public TipoBala GetTipoBala() 
        {
            return tipobala;
        }
        /// <summary>
        /// Construtor da classe Player.
        /// </summary>
        /// <param name="contents"></param>
        /// <param name="assetName"></param>
        /// <param name="tipobala"></param>
        public Player(ContentManager contents, string assetName,TipoBala tipobala) 
            : base(contents, assetName){
                contador = 0;
                this.contents = contents;
                this.tipobala = tipobala;
                //Velocidade da nave
                this.speed = Camera.velocidadegeral *3 ;
                this.Vida = 5;              
                this.EnableCollisions();
                this.contadorMisseis = 0;
                this.shootTime = 0; //setted no update
                this.pontuacao = 0;
        }

        
        /// <summary>
        /// Atualiza o estado do jogador
        /// </summary>
        /// <param name="gameTime">Instância de gametime</param>
        public override void Update(GameTime gameTime)
        {
            
            

            this.shootTime = 1.5f / Camera.velocidadegeral;
            this.speed = Camera.velocidadegeral * 3;
            
            //Movimento da nave atraves do teclado

            //Guarda o estado do teclado
            currentKeyboardState = Keyboard.GetState();
            //Se pressionado esquerda
            if (currentKeyboardState.IsKeyDown(Keys.Left)) 
            {
                if(this.position.X - this.size.X / 2 > Camera.GetTarget().X - Camera.worldWidth / 2)
                    this.position.X -= speed;

            }
            //Se pressionado cima
            if (currentKeyboardState.IsKeyDown(Keys.Up)) 

            {
                if (this.position.Y + this.size.Y / 2 < Camera.GetTarget().Y + Camera.GetWorldHeight() / 2)
                    this.position.Y += speed;

            }
            //Se pressionado baixo
            if (currentKeyboardState.IsKeyDown(Keys.Down)) 
            {
                if (-this.position.Y + this.size.Y / 2 < Camera.GetTarget().Y + Camera.GetWorldHeight() / 2)
                    this.position.Y -= speed;

            }
            //Se pressioando direita
            if (currentKeyboardState.IsKeyDown(Keys.Right))
            {
                if (this.position.X + this.size.X / 2 < Camera.GetTarget().X + Camera.worldWidth / 2)
                    this.position.X += speed;
            }
            lastposition = this.position;
            if (currentKeyboardState.IsKeyDown(Keys.W))
            {

                Camera.velocidadegeral += 0.00005f;

            }
            if (currentKeyboardState.IsKeyDown(Keys.S))
            {
                if (Camera.velocidadegeral > 0f) 
                { Camera.velocidadegeral -= 0.00005f; }
                else { Camera.velocidadegeral = 0f; }
                

            }
            if (currentKeyboardState.IsKeyDown(Keys.Space)) 
            {
                if (contador >= shootTime) {
                    som.playTiro();
                     switch( tipobala)
                     {
                case TipoBala.Simples:
                    scene.AddSprite(WeaponsManager.addBala("balaplayer", 1,OrigemBala.player, DireccaoBala.EmFrente).Scl(0.09f).
                             At(new Vector2(position.X+0.4f, position.Y )));
                    
                    break;
                case TipoBala.Duplo:

                    scene.AddSprite(WeaponsManager.addBala("balaplayer", 1, OrigemBala.player, DireccaoBala.EmFrente).Scl(0.09f).
                    At(new Vector2(position.X + 0.4f, position.Y + 0.05f)));
                    scene.AddSprite(WeaponsManager.addBala("balaplayer", 1, OrigemBala.player, DireccaoBala.EmFrente).Scl(0.09f).
                    At(new Vector2(position.X + 0.4f, position.Y - 0.05f)));

                    if (contadorMisseis > 3000)
                    {

                        scene.AddSprite(WeaponsManager.addMissil("missilPlayer", TipoMissil.EmFrente, 1, OrigemBala.player).Scl(0.15f).
                        At(new Vector2(position.X + 0.4f, position.Y)));
                        contadorMisseis = 0;

                    }
                    
                    
                    break;
                case TipoBala.Triplo:
                    scene.AddSprite(WeaponsManager.addBala("balaplayer", 1, OrigemBala.player, DireccaoBala.Cima).Scl(0.09f).
                    At(new Vector2(position.X + 0.4f, position.Y - 0.03f)));

                    //scene.AddSprite(new Bala(this.cManager, "balaplayer", 1, OrigemBala.player, DireccaoBala.EmFrente).Scl(0.09f).
                    //At(new Vector2(position.X + 0.4f, position.Y - 0.05f )));

                    scene.AddSprite(WeaponsManager.addBala("balaplayer", 1, OrigemBala.player, DireccaoBala.EmFrente).Scl(0.09f).
                    At(new Vector2(position.X + 0.4f, position.Y )));

                    scene.AddSprite(WeaponsManager.addBala("balaplayer", 1, OrigemBala.player, DireccaoBala.Baixo).Scl(0.09f).
                    At(new Vector2(position.X + 0.4f, position.Y + 0.03f)));

                    

                    if (contadorMisseis > 3000)
                    {
                        List<Sprite> listaAlvos = this.scene.inimigos.FindAll(x => (x is NPC || x is Missil));
                        if (listaAlvos.Count > 0)
                        {
                            FlyingEntity alvo = (FlyingEntity)listaAlvos[Game1.random.Next(listaAlvos.Count)];
                            scene.AddSprite(WeaponsManager.addMissil("missilPlayer", TipoMissil.Teleguiado, 1, OrigemBala.player, alvo).Scl(0.15f).
                            At(new Vector2(position.X + 0.4f, position.Y)));
                            contadorMisseis = 0;
                        }
                        
                    }
                    break;
                case TipoBala.Quadruplo:
                    scene.AddSprite(WeaponsManager.addBala("balaplayer", 1, OrigemBala.player, DireccaoBala.Cima).Scl(0.09f).
                    At(new Vector2(position.X + 0.4f, position.Y - 0.03f)));

                    scene.AddSprite(WeaponsManager.addBala("balaplayer", 1, OrigemBala.player, DireccaoBala.EmFrente).Scl(0.09f).
                    At(new Vector2(position.X + 0.4f, position.Y - 0.03f )));

                    scene.AddSprite(WeaponsManager.addBala("balaplayer", 1, OrigemBala.player, DireccaoBala.EmFrente).Scl(0.09f).
                    At(new Vector2(position.X + 0.4f, position.Y+0.03f)));

                    scene.AddSprite(WeaponsManager.addBala("balaplayer", 1, OrigemBala.player, DireccaoBala.Baixo).Scl(0.09f).
                    At(new Vector2(position.X + 0.4f, position.Y + 0.03f)));



                    if (contadorMisseis > 3000)
                    {
                        List<Sprite> listaAlvos = this.scene.inimigos.FindAll(x => (x is NPC || x is Missil));
                        if (listaAlvos.Count > 0)
                        {
                            FlyingEntity alvo = (FlyingEntity)listaAlvos[Game1.random.Next(listaAlvos.Count)];
                            scene.AddSprite(WeaponsManager.addMissil("missilPlayer", TipoMissil.Teleguiado, 1, OrigemBala.player, alvo).Scl(0.15f).
                            At(new Vector2(position.X + 0.4f, position.Y)));
                            contadorMisseis = 0;
                        }

                    }
                    break;
                default:
                    break;
                     }
                     contador = 0;
                }
            }
            //colisao com power ups
            if(this.scene.Collides(this,out this.collided,out this.collisionPoint,this.scene.powerUps))
            {
              
                if(this.collided is PowerUp)
                {
                    PowerUp powerUp = (PowerUp)collided;
                    if (powerUp.tipoPowerUp == TipoPowerUp.Vida)
                    {
                        if(this.Vida < 5) this.Vida++;
                    }
                    else
                        if (this.tipobala != TipoBala.Quadruplo)
                        {
                            this.tipobala++;
                        }
                
                }
                this.collided.Destroy();
            }

            //colisao con cenario
            if(this.scene.Collides(this,out this.collided, out this.collisionPoint,this.scene.sprites))
            {
                if(collided is Cenario|| collided is Defence)
                {
                    this.Destroy();
                    this.scene.AddExplosao(new AnimatedSprite(cManager, "explosao", 9, 9, false, position + new Vector2(0.2f, 0f), 1.5f));
                    Camera.addShake(500);
                    
                }
            }

            //colisao com inimigos
            if (this.scene.Collides(this, out this.collided, out this.collisionPoint, this.scene.inimigos))
            {

                if (collided is Missil)
                {
                    Missil missil = (Missil)collided;
                    missil.thrust.Destroy();
                }
                this.collided.Destroy();
                this.scene.AddExplosao(new AnimatedSprite(cManager, "explosao", 9, 9, false, position + new Vector2(0.2f, 0f), 1.5f));
                Camera.addShake(500);
                if (!(collided is Missil)) this.Destroy();
                
                
            }

            if(this.Vida<=0)
            {
                this.scene.AddExplosao(new AnimatedSprite(cManager, "explosao", 9, 9, false, this.position, 1.5f));
                som.playExplosao();
                Camera.addShake(500);
                this.scene.playerKilled = true;
                this.scene.ClearUI();
               
                this.Destroy();
            }

            this.position.X += Camera.speed;


            contador += gameTime.ElapsedGameTime.Milliseconds;
            contadorMisseis += gameTime.ElapsedGameTime.Milliseconds;
            

            base.Update(gameTime);
        }

        /// <summary>
        /// Devolve a velocidade do jogador como um vetor
        /// </summary>
        /// <returns>Vetor de velocidade do jogador</returns>
        public Vector2 getVectorVelocity() 
        {
            return Vector2.Subtract(position, lastposition);
        }

        

    }
}
