using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDJ_ProjectoFinal.entidades;
using TDJ_ProjectoFinal.graficos;

namespace TDJ_ProjectoFinal
{

    public enum DireccaoBala 
    {
        EmFrente,
        Cima,
        Baixo
    }
    public enum OrigemBala
    {
        enimiga,
        player
    }
    class Bala : FlyingEntity
    {
       
        private float speed;
        private int direccao;
        private DireccaoBala direccaobala;
        private OrigemBala origemBala;
        Sprite collided;
        Vector2 collisionPoint;

        public Bala(ContentManager contents, string assetName, int direccao,OrigemBala origemBala, DireccaoBala direccaobala) 
            : base(contents,"balasimples")
        {
            base.spriteEffects = direccao > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            this.speed = Camera.speed * 9;
            this.direccaobala = direccaobala;
            this.direccao = direccao;
            this.origemBala = origemBala;
            this.EnableCollisions();
        }

        //public void UpdatePowerUp(TipoBala tipobala) 
        //{
        //    if (tipobala == TipoBala.Duplo) 
        //    {
        //        scene.AddSprite(new Bala(this.cManager, "balasimples", TipoBala.Simples, 1).Scl(0.09f).
        //            At(new Vector2(position.X, position.Y - 0.1f)));
        //    }
        //}
        public override void Update(GameTime gameTime)
        {
            
            switch ( direccaobala )
                {
                case DireccaoBala.EmFrente:
                    if(origemBala==OrigemBala.player)
                        this.position.X += speed * direccao;
                    else
                        this.position.X += speed * direccao + 0.03f;
                    break;
                case DireccaoBala.Cima:
                    this.position.X += speed * direccao;
                    this.position.Y -= speed * 0.5f * direccao;

                    break;
                case DireccaoBala.Baixo:
                    this.position.X += speed *direccao;
                    this.position.Y += speed*0.5f * direccao;
                   


                    break;
                default:
                    //base.position.X += speed * direccao;
                    break;
                }
            
            //deteta colisao das balas com os enimigos
            BulletColision();

            //Destroi bala quando sai ddo limite da camara
            if(this.position.X > (Camera.target.X + (Camera.worldWidth/2)))
            {
                this.Destroy();
            }
   
        }

        private void BulletColision()
        {
            //Colisao com enimigos
            if (this.scene.Collides(this, out collided, out collisionPoint, this.scene.inimigos))
            {
                if (origemBala == OrigemBala.player)
                {
                    //destroi bala
                    this.Destroy();
                    //cria explosao
                    this.scene.AddExplosao(new AnimatedSprite(cManager, "explosao", 9, 9, false, position, 0.2f));

                    if (collided is NPC)
                    {
                        NPC inimigo = (NPC)collided;
                        inimigo.Vida--;
                    }
                }
                
            }
            //colisao com player
            if(this.scene.Collides(this, out collided, out collisionPoint, this.scene.sprites))
            {
                if (collided is Player)
                {
                    Player player = (Player)collided;
                    player.Vida--;
                    this.Destroy();
                    this.scene.AddExplosao(new AnimatedSprite(cManager, "explosao", 9, 9, false, position, 0.2f));
                }
            }
           
        }
    }
}
