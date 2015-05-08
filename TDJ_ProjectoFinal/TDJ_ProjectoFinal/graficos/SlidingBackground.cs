using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDJ_ProjectoFinal.graficos
{
    class SlidingBackground : Sprite
    {

        private float speed; //sliding speed


        public SlidingBackground(ContentManager contents, string assetName, float speed)
            : base(contents, assetName)
        {

            this.speed = speed;
        }

        public override void Update(GameTime gametime)
        {
            

            base.position.X += Camera.velocidadegeral / speed;
            
        }

    }
}
