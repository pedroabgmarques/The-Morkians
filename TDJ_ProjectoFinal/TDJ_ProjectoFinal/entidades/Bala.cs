using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDJ_ProjectoFinal.entidades;
using TDJ_ProjectoFinal.graficos;

<<<<<<< HEAD
namespace TDJ_ProjectoFinal
{
=======
namespace TDJ_ProjectoFinal{
>>>>>>> 01b50400078fc77e05e33bb05b959d284833fed0

    /// <summary>
    /// Através desta variavel, é nos possivel saber a direção de uma  bala.
    /// É através dela tambem que são criados os efeitos de power up do jogador.
    /// Em relação à sua origem, estas podem mover-se em frente a 0 graus, a +45graus ou a -45graus. 
    /// </summary>

    public enum DireccaoBala 
    {
        /// <summary>
        /// Em frente, 0º
        /// </summary>
        EmFrente,
        /// <summary>
        /// 45º
        /// </summary>
        Cima,
        /// <summary>
        /// -45º
        /// </summary>
        Baixo
    }

    /// <summary>
    /// Em todo jogo, a bala pode ter  quatro origens diferentes sendo elas:
    ///     o jogador
    ///     os inimigos comuns
    ///     as defesas(segundo nivel)
    ///     boss final
    /// 
    /// </summary>
    public enum OrigemBala
    {
        /// <summary>
        /// Morkian inimigo
        /// </summary>
        inimigo,
        /// <summary>
        /// Jogador
        /// </summary>
        player,
        /// <summary>
        /// Boss final
        /// </summary>
        boss,
        /// <summary>
        /// Turrets imóveis
        /// </summary>
        defesa
    }
   /// <summary>
   /// A classe "Bala", como o nome indica, é responsável por todas as balas no jogo, sejam elas do player, inimigo ou defesas.
   /// É composta por um construtor e por dois métodos sendo eles o método "Update" e método "BulletColision"
   /// Esta classe herda caracteristicas de outra classe, FlyingEntity.
   /// 
   /// </summary>
    public class Bala : FlyingEntity
    {
       
        /// <summary>
        /// Direção do ecrã em que a bala se move (esquerda / direita ou direita / esquerda)
        /// </summary>
        public int direccao;
        /// <summary>
        /// Tipo de bala - em frente, cima, baixo
        /// </summary>
        public DireccaoBala direccaobala;
        /// <summary>
        /// Entidade que originou a bala
        /// </summary>
        public OrigemBala origemBala;
        /// <summary>
        /// Vetor de direção da bala (rotação)
        /// </summary>
        public Vector2 direction;
        /// <summary>
        /// Caso tenha sido disparada por uma turret, turret respetiva
        /// </summary>
        public Defence parent;
        
        
        /// <summary>
        ///Construtor da classe "Bala".
        ///É responsavável por todas as variáveis que definem uma bala
        /// </summary>
        /// <param name="contents">Instâcia de ContentManager</param>
        /// <param name="assetName">Nome da textura da bala</param>
        /// <param name="direccao">Direção do ecrã em que a bala se move</param>
        /// <param name="origemBala">Entidade que disparou a bala</param>
        /// <param name="direccaobala">Tipo de bala - frente, cima, baixo</param>
        /// <param name="parent">Turret que originou a bala</param>
        public Bala(ContentManager contents, string assetName, int direccao, OrigemBala origemBala, DireccaoBala direccaobala, Defence parent=null)
            : base(contents, assetName)
        {
            base.spriteEffects = direccao > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            this.speed =Camera.velocidadegeral;
            this.direccaobala = direccaobala;
            this.direccao = direccao;
            this.origemBala = origemBala;
            this.parent = parent;
            direction = Vector2.Zero;
            
            this.EnableCollisions();
            //else
            //{
            //    som.playTiroEnimigo(contents);
            //}
        }

        //public void UpdatePowerUp(TipoBala tipobala) 
        //{
        //    if (tipobala == TipoBala.Duplo) 
        //    {
        //        scene.AddSprite(new Bala(this.cManager, "balasimples", TipoBala.Simples, 1).Scl(0.09f).
        //            At(new Vector2(position.X, position.Y - 0.1f)));
        //    }
        //}


        /// <summary>
        /// Update da classe "Bala"
        /// Este método atualiza a posição da bala assim como a sua velocidade.
        /// </summary>
        /// <param name="gameTime">Instância de GameTime</param>
        public override void Update(GameTime gameTime)

        {
          
           
            if (origemBala == OrigemBala.player)
            {
                speed = Camera.velocidadegeral / 0.12f;
            }
            else  if (origemBala == OrigemBala.defesa) 
            {
                if (direction == Vector2.Zero)
                {
                    direction = new Vector2((float)Math.Sin(parent.rot),
                        (float)Math.Cos(parent.rot));
                }
                
                speed = Camera.velocidadegeral / 0.009f;

            }
            else
            {
                speed = Camera.velocidadegeral / 0.20f;
            }
            
            
            switch ( direccaobala )
                {
                case DireccaoBala.EmFrente:
                        if (origemBala == OrigemBala.player)
                        {
                            this.position.X += speed * direccao;
                        }
                        else if (origemBala == OrigemBala.defesa)
                        {
                            if (parent.tipodefesa == TipoDefesa.Metrelhadora)
                            {
                                this.position += direction * speed
                                    * (float)gameTime.ElapsedGameTime.TotalSeconds;
                            }
                            else if (parent.tipodefesa == TipoDefesa.Laser) 
                            {
                                this.position.Y -= (float)gameTime.ElapsedGameTime.TotalSeconds+0.09f;

                            }
                            
                            
                        }
                        
                        else
                            this.position.X += speed * direccao;
                    break;
                case DireccaoBala.Cima:
                    this.position.X += speed * direccao;
                    this.position.Y -= speed * 0.2f * direccao;

                    break;
                case DireccaoBala.Baixo:
                    this.position.X += speed *direccao;
                    this.position.Y += speed * 0.2f * direccao;
                   


                    break;
                default:
                    base.position.X += speed * direccao;
                    break;
                }
            
            //deteta colisao das balas com os enimigos
            BulletColision();

            //Destroi bala quando sai ddo limite da camara
            if(this.position.X > (Camera.target.X + (Camera.worldWidth/2))
                || this.position.X < (Camera.target.X - (Camera.worldWidth / 2)))
            {
                this.Destroy();
            }
            
   
        }
        /// <summary>
        /// Método "BulletColision" passa por parametro a cena em que a bala está assim como a sua origem.
        /// Desta forma, verificamos se a bala colidiu com alguma sprite do jogo.
        /// </summary>

        private void BulletColision()
        {
            Colisoes.Colision(cManager, this.scene, this, origemBala);
        }
    }
}
