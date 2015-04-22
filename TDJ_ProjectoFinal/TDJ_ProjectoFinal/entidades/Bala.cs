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
            this.speed = Camera.speed * 3;
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
                    //TODO: algoritmo para seguir a nave
                    base.position.X += speed * direccao;
                    break;
                case TipoBala.Triplo:
                    //TODO: algoritmo para seguir a nave
                    base.position.X += speed * direccao;
                    break;
                default:
                    base.position.X += speed * direccao;
                    break;
            }

            
        }
    }
}
