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

        public override void Draw(GameTime gameTime)
        {
            //Os NPC's têm um Draw próprio para desenhar a nave rodada horizontalmente

            Rectangle pos = Camera.WorldSize2PixelRectangle(this.position, this.size);
            // scene.SpriteBatch.Draw(this.image, pos, Color.White);
            scene.SpriteBatch.Draw(this.image, pos, source, Color.White,
                this.rotation, new Vector2(pixelsize.X / 2, pixelsize.Y / 2),
                SpriteEffects.FlipHorizontally, 0);
        }

    }
}
