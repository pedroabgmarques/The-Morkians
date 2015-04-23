using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDJ_ProjectoFinal.entidades
{
    class PowerUp : FlyingEntity
    {
        private int direcao;
        private float speed;
        public PowerUp(ContentManager contents, string assetName, int direcao) : base(contents, assetName)
        {
            base.spriteEffects = direcao > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            this.direcao = direcao;
            this.speed = Camera.speed * 0.5f;
        }

        public override void Update(GameTime gameTime)
        {
            base.position.X += speed * direcao;
        }

    }
}
