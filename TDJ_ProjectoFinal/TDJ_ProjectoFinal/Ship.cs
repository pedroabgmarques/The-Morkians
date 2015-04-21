using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDJ_ProjectoFinal.graficos;

namespace TDJ_ProjectoFinal
{
    class Ship
    {
        private Sprite ship;
        private float velocity;
        private Vector2 position  {  set; get; }

        

        public Ship(ContentManager contents, float velocity,Vector2 position) 
        {
            this.ship = new Sprite(contents, "nave");
            this.velocity = velocity;
            this.position = position;
        }

        

 
    }
}
