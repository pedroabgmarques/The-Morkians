using Microsoft.Xna.Framework;
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

    public enum TipoBala
    {
        Simples,
        Duplo,
        Triplo

    }

    public class Player : FlyingEntity
    {
        

        
        KeyboardState currentKeyboardState;
        
        private int contador;
        private ContentManager contents;
        private int shootTime = 500;
        public int Vida;

        public Rectangle boundingBox;
        
        Sprite collided;
        Vector2 collisionPoint;
        
        public TipoBala tipobala;


        public TipoBala GetTipoBala() 
        {
            return tipobala;
        }

        public Player(ContentManager contents, string assetName,TipoBala tipobala) 
            : base(contents, assetName){
                contador = 0;
                this.contents = contents;
                this.tipobala = tipobala;
                //Velocidade da nave
                this.speed = 0.008f;
                this.Vida = 5;              
                this.EnableCollisions();
        }

        public override void Update(GameTime gameTime)
        {
            //Todo: atualizar o player de acordo com o teclado
            
            //Movimento da nave atraves do teclado

            //Guarda o estado do teclado
            currentKeyboardState = Keyboard.GetState();
            //Se pressionado esquerda
            if (currentKeyboardState.IsKeyDown(Keys.A)) 
            {

                this.position.X -= speed;

            }
            //Se pressionado cima
            if (currentKeyboardState.IsKeyDown(Keys.W)) 

            {

                this.position.Y += speed;

            }
            //Se pressionado baixo
            if (currentKeyboardState.IsKeyDown(Keys.S)) 
            {

                this.position.Y -= speed;

            }
            //Se pressioando direita
            if (currentKeyboardState.IsKeyDown(Keys.D))
            {

                this.position.X += speed;

            }
            if (currentKeyboardState.IsKeyDown(Keys.Space)) 
            {
                if (contador >= shootTime) { 
                     switch( tipobala)
                     {
                case TipoBala.Simples:
                             scene.AddSprite(new Bala(this.cManager, "balasimples", 1,OrigemBala.player, DireccaoBala.EmFrente).Scl(0.09f).
                             At(new Vector2(position.X+0.3f, position.Y )));
                    
                    break;
                case TipoBala.Duplo:

                    scene.AddSprite(new Bala(this.cManager, "balasimples", 1, OrigemBala.player, DireccaoBala.EmFrente).Scl(0.09f).
                    At(new Vector2(position.X + 0.4f, position.Y + 0.05f)));
                    scene.AddSprite(new Bala(this.cManager, "balasimples", 1, OrigemBala.player, DireccaoBala.EmFrente).Scl(0.09f).
                    At(new Vector2(position.X + 0.4f, position.Y - 0.05f)));
                    
                    
                    break;
                case TipoBala.Triplo:
                    scene.AddSprite(new Bala(this.cManager, "balasimples", 1, OrigemBala.player, DireccaoBala.Cima).Scl(0.09f).
                    At(new Vector2(position.X+0.2f, position.Y - 0.1f)));
                    scene.AddSprite(new Bala(this.cManager, "balasimples", 1, OrigemBala.player, DireccaoBala.EmFrente).Scl(0.09f).
                     At(new Vector2(position.X + 0.3f, position.Y )));
                    scene.AddSprite(new Bala(this.cManager, "balasimples", 1, OrigemBala.player, DireccaoBala.Baixo).Scl(0.09f).
                    At(new Vector2(position.X + 0.2f, position.Y + 0.1f)));
                    
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
                    this.tipobala = (TipoBala)powerUp.tipoPowerUp;
                    if(powerUp.tipoPowerUp==TipoPowerUp.Vida)
                    {
                        this.Vida++;
                    }
                }
                this.collided.Destroy();
            }
            //colisao com inimigos
            if (this.scene.Collides(this, out this.collided, out this.collisionPoint, this.scene.inimigos))
            {
                this.collided.Destroy();
                this.scene.AddExplosao(new AnimatedSprite(cManager, "explosao", 9, 9, false, position+new Vector2(0.2f,0f), 1.5f));
                this.Destroy();
            }

            //Para a nave quando chega ao limite esquerdo da camara.
            if (this.position.X - 0.1f /*- (this.position.X - (this.image.Width/2))*/ <= Camera.target.X - (Camera.worldWidth / 2))
            {
                this.position.X += 0.01f;
                
            }
            //para a nave quando chega ao limite direito da camara
            if (this.position.X + 0.2f >= Camera.target.X + (Camera.worldWidth / 2))
            {
                this.position.X -= 0.01f;

            }
            //para a nave quando chega ao limite superior da camara
            
            if (this.position.Y >= Camera.target.Y + 1.4f )//1.4f -> valor martelado.
            {
                this.position.Y -= 0.01f;

            }
            ////para a nave quando chega ao limite inferior da camara
            if (this.position.Y <= Camera.target.Y - 1.4f)//1.4f -> valor martelado.
            {
                this.position.Y += 0.01f;

            }

            if(this.Vida<=0)
            {
                this.scene.AddExplosao(new AnimatedSprite(cManager, "explosao", 9, 9, false, this.position, 1.5f));
                this.Destroy();
            }

            base.position.X += Camera.speed / 2;

            
            contador += gameTime.ElapsedGameTime.Milliseconds;

            base.Update(gameTime);
        }

        

    }
}
