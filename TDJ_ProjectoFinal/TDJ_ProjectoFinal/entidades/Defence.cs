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
        public float rotatio;
        

        public Defence(ContentManager contents, string assetName): base(contents, assetName) 
        {
            this.rotation = rotatio;
            this.SetRotation((float)Math.PI / 4);

        }
        public override void Update(GameTime gameTime)
        {
           
            Vector2 tpos =this.position;
            float a = (float)this.scene.player.position.Y - tpos.Y;
            float l = (float)this.scene.player.position.X - tpos.X;
            float rot = -(float)Math.Atan2(a, l);
            rot += (float)Math.PI / 2f;
            SetRotation(rot);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

    }
}
