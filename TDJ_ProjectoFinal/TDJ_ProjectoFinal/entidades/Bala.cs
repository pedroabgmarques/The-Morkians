﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDJ_ProjectoFinal.entidades;
using TDJ_ProjectoFinal.graficos;

namespace TDJ_ProjectoFinal
{

    public enum DireccaoBala 
    {
        EmFrente,
        Cima,
        Baixo
    }
    public enum OrigemBala
    {
        inimigo,
        player,
        boss,
        defesa
    }
    public class Bala : FlyingEntity
    {
       
        public int direccao;
        public DireccaoBala direccaobala;
        public OrigemBala origemBala;
        

        public Bala(ContentManager contents, string assetName, int direccao, OrigemBala origemBala, DireccaoBala direccaobala)
            : base(contents, assetName)
        {
            base.spriteEffects = direccao > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            this.speed =Camera.velocidadegeral;
            this.direccaobala = direccaobala;
            this.direccao = direccao;
            this.origemBala = origemBala;
            this.EnableCollisions();
            //else
            //{
            //    som.playTiroEnimigo(contents);
            //}
        }

        //public void UpdatePowerUp(TipoBala tipobala) 
        //{
        //    if (tipobala == TipoBala.Duplo) 
        //    {
        //        scene.AddSprite(new Bala(this.cManager, "balasimples", TipoBala.Simples, 1).Scl(0.09f).
        //            At(new Vector2(position.X, position.Y - 0.1f)));
        //    }
        //}
        public override void Update(GameTime gameTime)
        {

            if (origemBala == OrigemBala.player)
            {
                speed = Camera.velocidadegeral / 0.12f;
            }
            else
            {
                speed = Camera.velocidadegeral / 0.20f;
            }
            
            
            switch ( direccaobala )
                {
                case DireccaoBala.EmFrente:
                    if(origemBala==OrigemBala.player)
                        this.position.X += speed * direccao;
                    else
                        this.position.X += speed * direccao;
                    break;
                case DireccaoBala.Cima:
                    this.position.X += speed * direccao;
                    this.position.Y -= speed * 0.2f * direccao;

                    break;
                case DireccaoBala.Baixo:
                    this.position.X += speed *direccao;
                    this.position.Y += speed * 0.2f * direccao;
                   


                    break;
                default:
                    base.position.X += speed * direccao;
                    break;
                }
            
            //deteta colisao das balas com os enimigos
            BulletColision();

            //Destroi bala quando sai ddo limite da camara
            if(this.position.X > (Camera.target.X + (Camera.worldWidth/2))
                || this.position.X < (Camera.target.X - (Camera.worldWidth / 2)))
            {
                this.Destroy();
            }
            if (origemBala == OrigemBala.defesa) 
            {
                this.position.X += (float)Math.PI / 2f;
                this.position.Y -= (float)Math.PI / 2f;
            }
   
        }

        private void BulletColision()
        {
            Colisoes.Colision(cManager, this.scene, this, origemBala);
        }
    }
}
