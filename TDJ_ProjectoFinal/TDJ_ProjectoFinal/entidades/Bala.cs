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
    public enum TipoBala
    {
        Simples,
        Duplo,
        Triplo
        
    }
    class Bala : FlyingEntity
    {
        public TipoBala tipoBala { get; set; }
        private float speed;
        private int direccao;


        public Bala(ContentManager contents, string assetName, TipoBala tipoBala, int direccao) 
            : base(contents, assetName)
        {
            base.spriteEffects = direccao > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            this.tipoBala = tipoBala;
            this.speed = Camera.speed * 9;
            this.direccao = direccao;
        }
        public override void Update(GameTime gameTime)
        {
            switch (this.tipoBala)
            {
                case TipoBala.Simples:
                    base.position.X += speed * direccao;
                    break;
                case TipoBala.Duplo:
                    scene.AddSprite(new Bala(this.cManager, "balasimples", TipoBala.Simples, 1).Scl(0.09f).
                    At(new Vector2(position.X , position.Y-0.1f)));
                    base.position.X += speed * direccao;
                    break;
                case TipoBala.Triplo:
                    
                    base.position.X += speed * direccao;
                    break;
                default:
                    base.position.X += speed * direccao;
                    break;
            }

            
        }
    }
}
