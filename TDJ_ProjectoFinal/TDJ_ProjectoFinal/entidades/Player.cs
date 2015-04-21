using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDJ_ProjectoFinal.entidades
{
    public class Player : FlyingEntity
    {
        public Player(ContentManager contents, string assetName) 
            : base(contents, assetName){
        }

        public override void Update(GameTime gameTime)
        {
            //Todo: atualizar o player de acordo com o teclado
            base.position.X += Camera.speed / 2;
            base.Update(gameTime);
        }
    }
}
