using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDJ_ProjectoFinal.graficos;

namespace TDJ_ProjectoFinal.entidades
{
    /// <summary>
<<<<<<< HEAD
    /// Clasee UI
=======
    /// Elementos de UI, fixos no ecrã
>>>>>>> 01b50400078fc77e05e33bb05b959d284833fed0
    /// </summary>
    public class UI : Sprite
    {

        Vector2 posicao;
        Vector2 offset;
<<<<<<< HEAD
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="content">Instancia de ContentManager</param>
        /// <param name="assetName">Nome da sprite na pasta Contents</param>
        /// <param name="offset">Ponto de offset</param>
=======

        /// <summary>
        /// Construtor do elemento de UI
        /// </summary>
        /// <param name="content">Instância de ContentManager</param>
        /// <param name="assetName">Nome da textura a utilizar</param>
        /// <param name="offset">Offset em relação à posição por defeito das UI</param>/param>
>>>>>>> 01b50400078fc77e05e33bb05b959d284833fed0
        public UI(ContentManager content, string assetName, Vector2 offset)
            : base(content, assetName)
        {
            this.offset = offset;
        }
<<<<<<< HEAD
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="gameTime">Instancia de gametime</param>
=======

        /// <summary>
        /// Atualiza os componentes de UI
        /// </summary>
        /// <param name="gameTime">Instância de gameTime</param>
>>>>>>> 01b50400078fc77e05e33bb05b959d284833fed0
        public override void Update(GameTime gameTime)
        {
            posicao.X = Camera.GetTarget().X - Camera.worldWidth / 2 + 0.2f + offset.X;
            posicao.Y = Camera.GetTarget().Y + 1.4f + offset.Y;
            this.position = posicao;
            base.Update(gameTime);
        }
    }
}
