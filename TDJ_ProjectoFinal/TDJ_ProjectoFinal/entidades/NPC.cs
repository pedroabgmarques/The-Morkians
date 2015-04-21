using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDJ_ProjectoFinal.entidades
{

    public enum TipoNave
    {
        Hunter,
        Bomber,
        Mothership
    }

    public class NPC : FlyingEntity
    {

        public TipoNave tipoNave { get; set; }

        public NPC(ContentManager contents, string assetName, TipoNave tipoNave) 
            : base(contents, assetName)
        {
            base.spriteEffects = SpriteEffects.FlipHorizontally;
            this.tipoNave = tipoNave;
        }

        public override void Update(GameTime gameTime)
        {

            switch (this.tipoNave)
            {
                case TipoNave.Hunter:
                    base.position.X -= Camera.speed;
                    break;
                case TipoNave.Bomber:
                    base.position.X -= Camera.speed / 2;
                    break;
                case TipoNave.Mothership:
                    base.position.X -= Camera.speed / 4;
                    break;
                default:
                    base.position.X -= Camera.speed;
                    break;
            }

            base.Update(gameTime);
        }

    }
}
