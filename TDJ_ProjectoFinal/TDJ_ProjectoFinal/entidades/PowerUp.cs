using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDJ_ProjectoFinal.entidades
{
       public enum TipoPowerUp
        {
            Vida,
            BalaDupla,
            BalaTripla
        }
    class PowerUp : FlyingEntity
    {
        private int direcao;
        private float speed;
          

        public TipoPowerUp tipoPowerUp {get; set;}
        public PowerUp(ContentManager contents, string assetName,TipoPowerUp tipoPowerUp, int direcao) : base(contents, assetName)
        {
            base.spriteEffects = direcao > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            this.direcao = direcao;
            this.speed = Camera.speed * 0.3f;
        }

        public override void Update(GameTime gameTime)
        {
            base.position.X += speed * direcao;

            switch (this.tipoPowerUp)
            {
                case TipoPowerUp.Vida:
                    base.position.X += speed * direcao;
                    break;
                case TipoPowerUp.BalaDupla:
                    base.position.X += speed * direcao;
                    break;
                case TipoPowerUp.BalaTripla:
                    base.position.X += speed * direcao;
                    break;
            }
                
        }

    }
}
