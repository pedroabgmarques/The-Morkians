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

    /// <summary>
    /// Tipo de missil - em frente ou teleguiado
    /// </summary>
    public enum TipoMissil
    {
        /// <summary>
        /// Apenas se desloca em frente
        /// </summary>
        EmFrente,
        /// <summary>
        /// Desloca-se em direção a um alvo
        /// </summary>
        Teleguiado
    }

    /// <summary>
    /// Descreve um missil - simples ou teleguiado
    /// </summary>
    public class Missil : FlyingEntity
    {

        /// <summary>
        /// Tipo de missil - simples ou teleguiado
        /// </summary>
        public TipoMissil tipoMissil { get; set; }
        /// <summary>
        /// Animação de jato do missil
        /// </summary>
        public AnimatedSprite thrust;
        private Vector2 thrustPosition;
        /// <summary>
        /// Direção no ecrã em que o missil se move
        /// </summary>
        public int direccao;
        /// <summary>
        /// Alvo a atingir
        /// </summary>
        public FlyingEntity alvo;
        bool passouPeloAlvo;
        Vector2 direction = Vector2.UnitX;
        /// <summary>
        /// Entidade que deu origem ao missil
        /// </summary>
        public OrigemBala origemBala;
        private float aceleracao;
        private float speedInicial;



        /// <summary>
        /// Construtor do Missil
        /// </summary>
        /// <param name="contents">Instância de ContenteManager</param>
        /// <param name="assetName">Nome da textura a utilizar</param>
        /// <param name="tipoMissil">Tipo de missil - simples ou teleguiado</param>
        /// <param name="direccao">Direção do ecrã em que o missil se desloca</param>
        /// <param name="origemBala">Entidade que originou o missil</param>
        /// <param name="alvo">Alvo a atingir</param>
        public Missil(ContentManager contents, string assetName, TipoMissil tipoMissil, int direccao, OrigemBala origemBala, FlyingEntity alvo = null) 
            : base(contents, assetName)
        {
            base.spriteEffects = direccao > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            this.tipoMissil = tipoMissil;
            this.speed = Camera.velocidadegeral * 5;
            this.speedInicial = this.speed;
            this.aceleracao = this.speed / 20;
            this.direccao = direccao;
            this.alvo = alvo;
            this.passouPeloAlvo = false;
            this.origemBala = origemBala;
            this.EnableCollisions();
        }

        /// <summary>
        /// Atualiza o estado do missil
        /// </summary>
        /// <param name="gameTime">Instância de gameTime</param>
        public override void Update(GameTime gameTime)
        {
            this.speed = Camera.velocidadegeral * 5;

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
            if (this.position.X > (Camera.target.X + (Camera.worldWidth/2)) ||
                this.position.X < (Camera.target.X - (Camera.worldWidth / 2)))
            {
                this.thrust.Destroy();
                this.Destroy();
            }
        }

        /// <summary>
        /// Atualiza a animação de jato do missil
        /// </summary>
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
