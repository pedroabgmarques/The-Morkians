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
        /// <summary>
        /// Velocidade de deslocação
        /// </summary>
        private float speed; //sliding speed

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="contents">Content Manager</param>
        /// <param name="assetName">Nome da imagem</param>
        /// <param name="speed">Velocidade da sprite</param>
        public SlidingBackground(ContentManager contents, string assetName, float speed)
            : base(contents, assetName)
        {

            this.speed = speed;
        }
        /// <summary>
        /// Actualiza a posicao da sprite
        /// </summary>
        /// <param name="gametime">instancia de gametime</param>
        public override void Update(GameTime gametime)
        {
            

            base.position.X += Camera.velocidadegeral / speed;
            
        }

    }
}
