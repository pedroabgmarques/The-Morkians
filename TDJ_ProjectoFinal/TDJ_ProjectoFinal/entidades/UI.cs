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
    /// Elementos de UI, fixos no ecrã
    /// </summary>
    public class UI : Sprite
    {

        Vector2 posicao;
        Vector2 offset;

        /// <summary>
        /// Construtor do elemento de UI
        /// </summary>
        /// <param name="content">Instância de ContentManager</param>
        /// <param name="assetName">Nome da textura a utilizar</param>
        /// <param name="offset">Offset em relação à posição por defeito das UI</param>/param>
        public UI(ContentManager content, string assetName, Vector2 offset)
            : base(content, assetName)
        {
            this.offset = offset;
        }

        /// <summary>
        /// Atualiza os componentes de UI
        /// </summary>
        /// <param name="gameTime">Instância de gameTime</param>
        public override void Update(GameTime gameTime)
        {
            posicao.X = Camera.GetTarget().X - Camera.worldWidth / 2 + 0.2f + offset.X;
            posicao.Y = Camera.GetTarget().Y + 1.4f + offset.Y;
            this.position = posicao;
            base.Update(gameTime);
        }
    }
}
