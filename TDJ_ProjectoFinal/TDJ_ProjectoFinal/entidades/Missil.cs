using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDJ_ProjectoFinal.entidades
{

    public enum TipoMissil
    {
        EmFrente,
        Teleguiado
    }

    public class Missil : FlyingEntity
    {

        public TipoMissil tipoMissil { get; set; }

        public Missil(ContentManager contents, string assetName, TipoMissil tipoMissil) 
            : base(contents, assetName)
        {
            base.spriteEffects = SpriteEffects.FlipHorizontally;
            this.tipoMissil = tipoMissil;
        }

        public override void Update(GameTime gameTime)
        {
            switch (this.tipoMissil)
            {
                case TipoMissil.EmFrente:
                    base.position.X -= Camera.speed * 2;
                    break;
                case TipoMissil.Teleguiado:
                    base.position.X -= Camera.speed * 2;
                    break;
                default:
                    base.position.X -= Camera.speed * 2;
                    break;
            }
        }

    }
}
