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
        int contador;
        private int shootTime = 4000;
        public NPC(ContentManager contents, string assetName, TipoNave tipoNave, int direcao, float Scl) 
            : base(contents, assetName)
        {
            base.spriteEffects = SpriteEffects.FlipHorizontally;
            this.tipoNave = tipoNave;
            this.EnableCollisions();
            this.Scl(Scl);
            this.position.X = Camera.worldWidth;
            
            this.contador = 0;
            switch (tipoNave)
            {
                case TipoNave.Hunter:
                    this.Vida = 3;
                    break;
                case TipoNave.Bomber:
                    this.Vida = 5;
                    break;
                case TipoNave.Mothership:
                    this.Vida = 10;
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

            
            //Verificar vida
            if (this.Vida <= 0)
            {
                this.scene.AddExplosao(new AnimatedSprite(cManager, "explosao", 9, 9, false, position, 0.9f));
                this.Destroy();
            }

            if (contador >= shootTime)
            {
                scene.AddSprite(new Bala(this.cManager, "balainimigo", -1, OrigemBala.enimiga, DireccaoBala.EmFrente).Scl(0.09f).
                        At(new Vector2(position.X - 0.4f, position.Y - 0.05f)));
                contador = 0;
            }
            contador += gameTime.ElapsedGameTime.Milliseconds;

            //enimgos sao destruidos se passarem o limite esquerdo da camara
            if (this.position.X < (Camera.target.X - (Camera.worldWidth / 2)))
            {
                this.Destroy();
            }

            base.Update(gameTime);
        }

        new public NPC At(Vector2 p)
        {
            this.SetPosition(p);
            return this;
        }

    }
}
