using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        protected Sprite sprite;

        public Defence(ContentManager contents, string assetName): base(contents, assetName) 
        {
            this.sprite = new Sprite(contents, assetName);

        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

    }
}
