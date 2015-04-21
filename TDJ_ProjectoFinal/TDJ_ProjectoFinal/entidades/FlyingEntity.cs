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
        protected Sprite sprite;
        protected float speed;
        public FlyingEntity(ContentManager contents, string assetName) 
            : base(contents, assetName)
        {
            this.sprite = new Sprite(contents, assetName);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
