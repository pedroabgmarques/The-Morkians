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

    public enum TipoMissil
    {
        EmFrente,
        Teleguiado
    }

    public class Missil : FlyingEntity
    {

        public TipoMissil tipoMissil { get; set; }
        public AnimatedSprite thrust;
        private Vector2 thrustPosition;
        private int direccao;
        private FlyingEntity alvo;
        bool passouPeloAlvo;
        Vector2 direction = Vector2.UnitX;
        public OrigemBala origemBala;
        private float aceleracao;
        private float speedInicial;



        public Missil(ContentManager contents, string assetName, TipoMissil tipoMissil, int direccao, OrigemBala origemBala, FlyingEntity alvo = null) 
            : base(contents, assetName)
        {
            base.spriteEffects = direccao > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            this.tipoMissil = tipoMissil;
            this.speed = 0.02f;
            this.speedInicial = this.speed;
            this.aceleracao = this.speed / 20;
            this.direccao = direccao;
            this.alvo = alvo;
            this.passouPeloAlvo = false;
            this.origemBala = origemBala;
            this.EnableCollisions();
            som.playRocket();
        }

        public override void Update(GameTime gameTime)
        {
            switch (this.tipoMissil)
            {
                case TipoMissil.EmFrente:
                    base.position.X += speed * direccao;
                    if(this.speed < this.speedInicial * 5)
                        this.speed += this.aceleracao;
                    break;
                    
                case TipoMissil.Teleguiado:

                        if (!passouPeloAlvo && alvo != null)
                        {
                            //Encontrar a direção do alvo
                            direction = alvo.position - this.position;
                        }

                        if ((direction.Length() < 0.7f && !passouPeloAlvo) || alvo == null)
                        {
                            //Já passou pelo alvo, deixamos de atualizar a direção
                            passouPeloAlvo = true;
                        }
                    
                    direction.Normalize();
                    //Mover na direção para onde estamos virados
                    position += direction * speed;
                    if (this.speed < this.speedInicial * 5)
                        this.speed += this.aceleracao;
                    
                    break;
                default:
                    base.position.X += speed * direccao;
                    if (this.speed < this.speedInicial * 5)
                        this.speed += this.aceleracao;
                    break;
            }

            UpdateThrust();

            MissilColision();

            //Destroi missil quando sai ddo limite da camara
            if (this.position.X > (Camera.target.X + (Camera.worldWidth/2)) || this.position.X < 0)
            {
                this.thrust.Destroy();
                this.Destroy();
            }
        }

        public void UpdateThrust()
        {
            if (thrust == null)
            {
                thrust = new AnimatedSprite(cManager, "thrust", 8, 1,true, position, 0.2f);     
                thrust.spriteEffects = direccao > 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                scene.AddSprite(thrust);
            }
            thrustPosition.X = direccao > 0 ? position.X - 0.15f : position.X + 0.15f;
            thrustPosition.Y = position.Y;
            thrust.SetPosition(thrustPosition);
        }

        private void MissilColision()
        {
            Colisoes.Colision(cManager, this.scene, this, origemBala);
        }

    }
}
