using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDJ_ProjectoFinal.entidades
{
    /// <summary>
    /// Define o tipo de PowerUp.Mais vida ou melhoramento das armas
    /// </summary>
       public enum TipoPowerUp
        {
            Vida,
            Armas
        }
    public class PowerUp : FlyingEntity
    {
        private int direcao;
        public Rectangle boundingBox;
        Vector2 worldPixels;
        public TipoPowerUp tipoPowerUp {get; set;}
        /// <summary>
        /// Construtor da classe PowerUp, herda de Flying Entity.
        /// </summary>
        /// <param name="contents"></param>
        /// <param name="assetName"></param>
        /// <param name="tipoPowerUp"></param>
        /// <param name="direcao"></param>
        /// <param name="Scl"></param>
        /// <param name="posY"></param>
        public PowerUp(ContentManager contents, string assetName,TipoPowerUp tipoPowerUp, int direcao, float Scl, float posY) : base(contents, assetName)
        {
            base.spriteEffects = direcao > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            this.direcao = direcao;
            
            worldPixels = Camera.WorldPoint2Pixels((int)this.position.X, (int)this.position.Y);
            boundingBox = new Rectangle((int)worldPixels.X, (int)worldPixels.Y, (int)this.image.Width, (int)this.image.Height);
            this.Scl(Scl);
            this.position.X = Camera.worldWidth;
            this.position.Y = posY;
            this.tipoPowerUp = tipoPowerUp;
            this.EnableCollisions();
        }

        public override void Update(GameTime gameTime)
        {

            this.speed = Camera.velocidadegeral * 0.3f;
            
            worldPixels = Camera.WorldPoint2Pixels((int)this.position.X, (int)this.position.Y);
            this.boundingBox.X = (int)worldPixels.X;
            this.boundingBox.Y = (int)worldPixels.Y;
            switch (this.tipoPowerUp)
            {
                case TipoPowerUp.Vida:
                    base.position.Y += speed * direcao;
                    break;
                case TipoPowerUp.Armas:
                    base.position.Y += speed * direcao;
                    
                    break;
            }

          
                
        }

    }
}
