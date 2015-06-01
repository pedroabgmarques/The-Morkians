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
    /// Define o tipo de PowerUp - Mais vida ou melhoramento das armas
    /// </summary>
       public enum TipoPowerUp
        {
           /// <summary>
           /// Mais vida
           /// </summary>
            Vida,
           /// <summary>
           /// Melhoramento de armas
           /// </summary>
            Armas
        }

    /// <summary>
    /// Descreve um powerup que pode ser apanahado pelo jogador e lhe confere uma vantagem
    /// </summary>
    public class PowerUp : FlyingEntity
    {
        private int direcao;
        /// <summary>
        /// Boudingbox do powerup, para verificação de colisões
        /// </summary>
        public Rectangle boundingBox;
        Vector2 worldPixels;
        /// <summary>
        /// Tipo de powerup
        /// </summary>
        public TipoPowerUp tipoPowerUp {get; set;}
        /// <summary>
        /// Construtor da classe PowerUp, herda de Flying Entity.
        /// </summary>
        /// <param name="contents">ContentManager</param>
        /// <param name="assetName">Nome da asset na pasta Content</param>
        /// <param name="tipoPowerUp">Define o tipo de Power UP</param>
        /// <param name="direcao">Define a direcao do Power Up</param>
        /// <param name="Scl">Faz o scale da sprite</param>
        /// <param name="posY">Define a posicao y da sprite</param>
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
        /// <summary>
        /// Update dos power ups
        /// </summary>
        /// <param name="gameTime"></param>
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
