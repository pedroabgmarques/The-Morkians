using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDJ_ProjectoFinal.graficos;

namespace TDJ_ProjectoFinal.entidades
{
    
    /// <summary>
    /// Representa qualquer coisa que voe no jogo
    /// </summary>
    public class FlyingEntity : Sprite
    {
        /// <summary>
        /// Instancia de Sprite
        /// </summary>
        protected Sprite sprite;
        /// <summary>
        /// Velocidade da sprite
        /// </summary>
        public float speed;
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="contents">Instancia de ContentManager</param>
        /// <param name="assetName">Nome da sprite na pasta Content</param>
        public FlyingEntity(ContentManager contents, string assetName) 
            : base(contents, assetName)
        {
            //this.sprite = new Sprite(contents, assetName);
            
        }
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="gameTime">Instancia de gametime</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="gameTime">Instancia de gametime</param>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
