using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDJ_ProjectoFinal.entidades;

namespace TDJ_ProjectoFinal.graficos
{
    class Cenario : Sprite
    {
        public Cenario(ContentManager contents, string assetName) 
            : base(contents, assetName)
        {
            
            this.EnableCollisions();
            
        }

    }
}
