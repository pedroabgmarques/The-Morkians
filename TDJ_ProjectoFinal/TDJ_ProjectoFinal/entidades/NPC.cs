﻿using Microsoft.Xna.Framework;
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

            base.Update(gameTime);
        }

        new public NPC At(Vector2 p)
        {
            this.SetPosition(p);
            return this;
        }

    }
}
