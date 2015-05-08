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
    

    public enum TipoDefesa
    {
        Metrelhadora,
        Laser
    }
    class Defence : Sprite
    {
        public TipoDefesa tipodefesa { get; set; }
        private float shootTime ;
        private Vector2 direction = Vector2.Zero;
        private Vector2 posBala;
        private int contadordisparo = 10;
        
        

        public Defence(ContentManager contents, string assetName): base(contents, assetName) 
        {
            
            this.SetRotation((float)Math.PI / 4);
            
            this.EnableCollisions();

        }
        public override void Update(GameTime gameTime)
        {
            posBala = this.position + direction;
           
            Vector2 tpos =this.position;
            float a = (float)this.scene.player.position.Y - tpos.Y;
            float l = (float)this.scene.player.position.X - tpos.X;
            float rot = -(float)Math.Atan2(a, l);
            rot += (float)Math.PI / 2f;
            SetRotation(rot);

            shootTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (shootTime >= contadordisparo)
            {
                Vector2 pos = this.position
                         + new Vector2((float)Math.Sin(rot) * size.Y / 2,
                                       (float)Math.Cos(rot) * size.Y / 2);
                Bala bala = new Bala(cManager, "baladefesas", 1, OrigemBala.defesa, DireccaoBala.EmFrente);
                bala.At(new Vector2(pos.X,pos.Y));
                bala.Scale(0.03f);
                scene.AddSprite(bala);
                shootTime = 0f;
            }
            

            

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

    }
}
