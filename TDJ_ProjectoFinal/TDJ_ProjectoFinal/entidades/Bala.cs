using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDJ_ProjectoFinal.entidades;

namespace TDJ_ProjectoFinal
{
    
    class Bala : FlyingEntity
    {
       
        private float speed;
        private int direccao;
        private TipoBala tipobala;
        


        public Bala(ContentManager contents, string assetName, int direccao, TipoBala tipobala ) 
            : base(contents,"balasimples")
        {
            base.spriteEffects = direccao > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            this.speed = Camera.speed * 9;
            this.tipobala = tipobala;
            this.direccao = direccao;
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
            switch ( tipobala )
            {
                case TipoBala.Simples:
                    this.position.X += speed * direccao;
                    this.position.Y += speed * direccao;

                    break;
                case TipoBala.Duplo:
                    this.position.X += speed * direccao;
                    this.position.Y += speed * direccao;

                    break;
                case TipoBala.Triplo:
                    this.position.X += speed * direccao;
                    this.position.Y += speed * direccao;
                   


                    break;
                default:
                    //base.position.X += speed * direccao;
                    break;
            }
        }
    }
}
