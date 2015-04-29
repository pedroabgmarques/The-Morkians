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
            Armas
        }
    public class PowerUp : FlyingEntity
    {
        private int direcao;
        private float speed;
        public Rectangle boundingBox;
        Vector2 worldPixels;
        public TipoPowerUp tipoPowerUp {get; set;}

        public PowerUp(ContentManager contents, string assetName,TipoPowerUp tipoPowerUp, int direcao, float Scl, float posY) : base(contents, assetName)
        {
            base.spriteEffects = direcao > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            this.direcao = direcao;
            this.speed = Camera.speed * 0.3f;
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
            base.position.X += speed * direcao;
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
