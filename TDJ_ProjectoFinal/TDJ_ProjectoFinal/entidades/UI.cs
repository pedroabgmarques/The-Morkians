using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDJ_ProjectoFinal.graficos;

namespace TDJ_ProjectoFinal.entidades
{
    public class UI : Sprite
    {

        Vector2 posicao;
        Vector2 offset;

        public UI(ContentManager content, string assetName, Vector2 offset)
            : base(content, assetName)
        {
            this.offset = offset;
        }

        public override void Update(GameTime gameTime)
        {
            posicao.X = Camera.GetTarget().X - Camera.worldWidth / 2 + 0.2f + offset.X;
            posicao.Y = Camera.GetTarget().Y + 1.4f + offset.Y;
            this.position = posicao;
            base.Update(gameTime);
        }
    }
}
