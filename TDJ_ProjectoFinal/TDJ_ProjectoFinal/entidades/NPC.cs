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
        int contador;
        private int shootTime;
        private float posY;
        public NPC(ContentManager contents, string assetName, TipoNave tipoNave, int direcao, float Scl, Random random) 
            : base(contents, assetName)
        {
            base.spriteEffects = SpriteEffects.FlipHorizontally;
            this.tipoNave = tipoNave;
            this.Scl(Scl);
            this.position.X = Camera.worldWidth;
            this.EnableCollisions();
            
            this.contador = 0;
            switch (tipoNave)
            {
                case TipoNave.Hunter:
                    this.Vida = 3;
                    this.shootTime = 3000;
                    break;
                case TipoNave.Bomber:
                    this.Vida = 5;
                    this.shootTime = random.Next(6000, 10000);
                    break;
                case TipoNave.Mothership:
                    this.Vida = 50;
                    this.shootTime = 4000;
                    break;
                default:
                    this.Vida = 1;
                    break;
            }
        }

        public override void Update(GameTime gameTime)
        {
            // variavel usada pelo coseno
            // Contradominio de cos(x)=[-1,1]
            posY+=0.01f;

            switch (this.tipoNave)
            {
                case TipoNave.Hunter:
                    base.position.X -= Camera.speed;
                    // algoritmo para criar o efeito de onda nos NPC'S
                    // coseno(x)+posição do npc

                    base.position.Y = (float)Math.Cos(posY) + 0.1f;
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
                som.playExplosao(cManager);
                Camera.addShake(100);
                this.Destroy();
                
            }

            if (contador >= shootTime)
            {
                if (this.tipoNave == TipoNave.Hunter || this.tipoNave == TipoNave.Mothership)
                {
                    //Caças e Nave Mãe disparam balas
                    scene.AddSprite(new Bala(this.cManager, "balainimigo", -1, OrigemBala.inimigo, DireccaoBala.EmFrente).Scl(0.09f).
                        At(new Vector2(position.X - 0.4f, position.Y - 0.05f)));
                }
                else
                {
                    if (this.scene.player.position.X < this.position.X)
                    {
                        //Bombardeiros disparam misseis teleguidados
                        scene.AddInimigo(new Missil(this.cManager, "missil", TipoMissil.Teleguiado, -1, OrigemBala.inimigo, this.scene.player).Scl(0.15f).
                            At(this.position));
                    }
                }
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
