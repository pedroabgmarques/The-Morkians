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
        /// <summary>
        /// Cenario do nivel2
        /// </summary>
        /// <param name="contents">Instancia de ContentManager</param>
        /// <param name="assetName">Nome da sprite</param>
        /// <param name="Scl">Valor para fazer scale</param>
        public Cenario(ContentManager contents, string assetName, float Scl) 
            : base(contents, assetName)
        {
            this.Scl(Scl);
            this.EnableCollisions();
            
        }

    }
}
