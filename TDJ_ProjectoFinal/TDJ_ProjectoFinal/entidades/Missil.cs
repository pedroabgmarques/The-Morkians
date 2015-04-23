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
        private AnimatedSprite thrust;
        private Vector2 thrustPosition;
        private float speed;
        private int direccao;
        private FlyingEntity alvo;

        public Missil(ContentManager contents, string assetName, TipoMissil tipoMissil, int direccao, FlyingEntity alvo = null) 
            : base(contents, assetName)
        {
            base.spriteEffects = direccao > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            this.tipoMissil = tipoMissil;
            this.speed = Camera.speed * 3;
            this.direccao = direccao;
            this.alvo = alvo;

        }

        public override void Update(GameTime gameTime)
        {
            switch (this.tipoMissil)
            {
                case TipoMissil.EmFrente:
                    base.position.X += speed * direccao;
                    break;
                case TipoMissil.Teleguiado:
                    base.position = Vector2.SmoothStep(position, alvo.position, 0.1f);
                    
                    
                    break;
                default:
                    base.position.X += speed * direccao;
                    break;
            }

            UpdateThrust();
        }

        public void UpdateThrust()
        {
            if (thrust == null)
            {
                thrust = new AnimatedSprite(cManager, "thrust", 8, 1);
                thrustPosition.X = position.X;
                thrustPosition.Y = position.Y;
                thrust.SetPosition(thrustPosition);
                thrust.Scale(.3f);
                thrust.Loop = true;
                thrust.spriteEffects = direccao > 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                scene.AddSprite(thrust);
            }
            thrustPosition.X = direccao > 0 ? position.X - 0.2f : position.X + 0.2f;
            thrustPosition.Y = position.Y;
            thrust.SetPosition(thrustPosition);
        }

    }
}
