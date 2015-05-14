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
    
    

    class Boss:AnimatedSprite
    {

        public int Vida;
        private float shootTime;
        private float posY;
        int contador,contador2;
        bool missil1Lancado = false;
        public Boss(ContentManager content,string filename, int nrows, int ncols, bool loop, Vector2 position, float scale):
            base(content,filename,nrows,ncols,loop,position,scale)
        {
            this.animationTimer = 0f;
            this.animationInterval = 7f / Camera.velocidadegeral;
            this.position.X = Camera.worldWidth;
            this.EnableCollisions();
            this.contador = 0;
            this.Vida = 500;
            this.shootTime = 7f / Camera.velocidadegeral;
        }

        public override void Update(GameTime gameTime)
        {
            posY += Camera.velocidadegeral * 2.5f;
            //this.shootTime = 25f / Camera.velocidadegeral;

            base.position.Y = ((float)Math.Cos(posY)*0.5f) + 0.1f;
            base.position.X = Camera.target.X + 1.7f;

            //Verificar vida
            if (this.Vida <= 0)
            {
                this.scene.AddExplosao(new AnimatedSprite(cManager, "explosao", 9, 9, false, position, 0.9f));
                som.playExplosao();
                Camera.addShake(100);
                this.Destroy();

            }

            animationTimer +=
                (float)gameTime.ElapsedGameTime.Milliseconds;
            if (animationTimer > animationInterval)
            {
                animationTimer = 0f;
                nextFrame();

            }

            if (contador >= shootTime && missil1Lancado==false)
            {
                if (this.scene.player != null)
                {
                    scene.AddInimigo(WeaponsManager.addMissil("missil", TipoMissil.Teleguiado, -1, OrigemBala.inimigo, this.scene.player).Scl(0.15f).
                            At(new Vector2(this.position.X - 1f, this.position.Y - 0.5f)));
                }
                
                
                scene.AddSprite(WeaponsManager.addBala("balainimigo", -1, OrigemBala.inimigo, DireccaoBala.EmFrente).Scl(0.09f).
                    At(new Vector2(position.X - 0.4f, position.Y)));
                missil1Lancado = true;
                
            }
            else if (contador>=shootTime*2)
            {
                if (this.scene.player != null)
                {
                    scene.AddInimigo(WeaponsManager.addMissil("missil", TipoMissil.Teleguiado, -1, OrigemBala.inimigo, this.scene.player).Scl(0.15f).
                        At(new Vector2(this.position.X - 1f, this.position.Y + 0.5f)));
                    missil1Lancado = false;
                }

                scene.AddSprite(WeaponsManager.addBala("balainimigo", -1, OrigemBala.inimigo, DireccaoBala.EmFrente).Scl(0.09f).
                    At(new Vector2(position.X - 0.4f, position.Y)));
                contador = 0;
            }
            
            contador += gameTime.ElapsedGameTime.Milliseconds;
            contador2 += gameTime.ElapsedGameTime.Milliseconds;


            base.Update(gameTime);
        }


    }
}
