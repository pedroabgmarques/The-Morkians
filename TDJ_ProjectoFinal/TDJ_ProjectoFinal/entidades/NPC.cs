using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDJ_ProjectoFinal.graficos;

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
        public int Vida;
        Sprite collided;
        Vector2 collisionPoint;
        public NPC(ContentManager contents, string assetName, TipoNave tipoNave, int direcao, float Scl, float posY) 
            : base(contents, assetName)
        {
            base.spriteEffects = SpriteEffects.FlipHorizontally;
            this.tipoNave = tipoNave;
            this.EnableCollisions();
            this.Scl(Scl);
            this.position.X = Camera.worldWidth;
            this.position.Y = posY;
            switch (tipoNave)
            {
                case TipoNave.Hunter:
                    this.Vida = 1;
                    break;
                case TipoNave.Bomber:
                    this.Vida = 3;
                    break;
                case TipoNave.Mothership:
                    this.Vida = 4;
                    break;
                default:
                    this.Vida = 1;
                    break;
            }
        }

        public override void Update(GameTime gameTime)
        {

            switch (this.tipoNave)
            {
                case TipoNave.Hunter:
                    base.position.X -= Camera.speed;
                    break;
                case TipoNave.Bomber:
                    base.position.X -= Camera.speed / 3;
                    break;
                case TipoNave.Mothership:
                    base.position.X -= Camera.speed / 4;
                    break;
                default:
                    base.position.X -= Camera.speed;
                    break;
            }
            Console.WriteLine(this.Vida);
            //quando enimigo colide com bala
            if (this.scene.Collides(this, out this.collided, out this.collisionPoint, this.scene.balas))
            {
                this.Vida -= 1;
                if(this.Vida==0)
                {
                    this.Destroy();
                    
                }
            }

            base.Update(gameTime);
        }

    }
}
