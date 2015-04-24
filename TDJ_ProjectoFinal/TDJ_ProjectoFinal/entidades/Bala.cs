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
    
    class Bala : FlyingEntity
    {
       
        private float speed;
        private int direccao;
        private DireccaoBala direccaobala;
        Sprite collided;
        Vector2 collisionPoint;
        AnimatedSprite explosao;

        public Bala(ContentManager contents, string assetName, int direccao, DireccaoBala direccaobala) 
            : base(contents,"balasimples")
        {
            base.spriteEffects = direccao > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            this.speed = Camera.speed * 9;
            this.direccaobala = direccaobala;
            this.direccao = direccao;
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
                    this.position.X += speed * direccao;
                    

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
            if (this.scene.Collides(this, out collided, out collisionPoint, this.scene.enimigos))
            {
                //destroi bala
                this.Destroy();
                //cria explosao
                explosao = new AnimatedSprite(cManager, "explosao", 9, 9);
                explosao.position.X = this.position.X;
                explosao.position.Y = this.position.Y;
                explosao.Loop = false;
                explosao.Scl(0.2f);


                this.scene.AddSprite(explosao);
            }
        }
    }
}
