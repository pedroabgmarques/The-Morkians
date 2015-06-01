using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDJ_ProjectoFinal.graficos;

namespace TDJ_ProjectoFinal.entidades
{
    

    /// <summary>
    /// Tipo de defesa
    /// </summary>
    public enum TipoDefesa
    {
        /// <summary>
        /// Dispara uma rajada de tiros consecutivos
        /// </summary>
        Metrelhadora,
        /// <summary>
        /// Dispara apenas um tiro de tempo a tempo
        /// </summary>
        Laser
    }

    /// <summary>
    /// Descreve uma defesa do nivel 2, tipo turret
    /// </summary>
    public class Defence : Sprite
    {
        /// <summary>
        /// Tipo de defesa
        /// </summary>
        public TipoDefesa tipodefesa { get; set; }
        private float shootTimeM ;
        private float shootTimeL;
        private Vector2 direction = Vector2.Zero;
        private Vector2 posBala;
        private float contadordisparo ;
        /// <summary>
        /// Rotação da turret
        /// </summary>
        public float rot;
        /// <summary>
        /// Tempo de disparo
        /// </summary>
        public float shootTimeLaser;


        /// <summary>
        /// Construtor da classe Defesa
        /// </summary>
        /// <param name="contents">Instância de ContentManager</param>
        /// <param name="assetName">Nome da textura a utilizar</param>
        /// <param name="tipodefesa">Tipo de defesa</param>
        /// <param name="shootTimeLaser">Tempo de disparo</param>
        public Defence(ContentManager contents, string assetName, TipoDefesa tipodefesa, float shootTimeLaser=0)
            : base(contents, assetName) 
        {
            
            this.SetRotation((float)Math.PI / 4);
            this.tipodefesa = tipodefesa;
            this.shootTimeLaser = shootTimeLaser;
            
            this.EnableCollisions();
            

        }
        private float AimAhead(Vector2 delta, Vector2 vr, float muzzleV)
        {
            // Quadratic equation coefficients a*t^2 + b*t + c = 0
            float a = Vector2.Dot(vr, vr) - muzzleV * muzzleV;
            float b = 2f * Vector2.Dot(vr, delta);
            float c = Vector2.Dot(delta, delta);

            float det = b * b - 4f * a * c;

            // If the determinant is negative, then there is no solution
            if (det > 0f)
            {
                return 2f * c / ((float)Math.Sqrt(det) - b);
            }
            else
            {
                return -1f;
            }
        }

        /// <summary>
        /// Atualiza uma defesa
        /// </summary>
        /// <param name="gameTime">Instância de gameTime</param>
        public override void Update(GameTime gameTime)
        {
            switch (this.tipodefesa) 
            {
                case TipoDefesa.Metrelhadora:
                    contadordisparo = 0.009f / Camera.velocidadegeral;
            posBala = this.position + direction;


            Vector2 futurepoint = Vector2.Zero;
            if (this.scene.player != null)
            {
                Vector2 deltaP = this.scene.player.position - this.position;
                Vector2 deltaV = this.scene.player.getVectorVelocity();
                float t = AimAhead(deltaP, deltaV, Camera.velocidadegeral / 0.5f);
                
                if (t > 0)
                {
                    futurepoint = this.scene.player.position + t * (deltaV);
                }
            }
            
            
            

            Vector2 tpos =this.position;
            float a = (float)futurepoint.Y - tpos.Y;
            float l = (float)futurepoint.X - tpos.X;
             rot = -(float)Math.Atan2(a, l);
            
            rot += (float)Math.PI / 2f;
            SetRotation(rot);

            shootTimeM += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (shootTimeM >= contadordisparo && futurepoint != Vector2.Zero)
            {
                Vector2 pos = this.position
                         + new Vector2((float)Math.Sin(rot) * size.Y / 2,
                                       (float)Math.Cos(rot) * size.Y / 2);


              
                scene.AddSprite(WeaponsManager.addBala("baladefesas", 2, OrigemBala.defesa, DireccaoBala.EmFrente, this).Scl(0.03f).At(new Vector2(pos.X, pos.Y)));

                shootTimeM = 0f;
            }
                    break;

                case TipoDefesa.Laser:
                    shootTimeL += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    contadordisparo = 0.009f / Camera.velocidadegeral;

                    this.rotation = 3.15f;
                    if (scene.player != null && shootTimeL >= contadordisparo && scene.player.position.X <= this.position.X + 1.5f)
                    {
                        scene.AddSprite(WeaponsManager.addBala("baladefesas", 1, OrigemBala.defesa, DireccaoBala.EmFrente, this)
                            .Scl(0.03f).At(new Vector2(this.position.X, this.position.Y - 0.2f)));

                        if (shootTimeL >= contadordisparo + shootTimeLaser) 
                        {
                            shootTimeL = 0f;
                        }
                    }
                    break;
            }
    
            base.Update(gameTime);
        }

        /// <summary>
        /// Desenha uma defesa
        /// </summary>
        /// <param name="gameTime">Instância de gameTime</param>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

    }
}
