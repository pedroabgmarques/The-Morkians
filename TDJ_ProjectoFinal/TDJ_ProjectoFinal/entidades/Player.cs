﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDJ_ProjectoFinal.graficos;

namespace TDJ_ProjectoFinal.entidades
{
    public class Player : FlyingEntity
    {


        
        KeyboardState currentKeyboardState;
        public Player(ContentManager contents, string assetName) 
            : base(contents, assetName)
        {
            //Velocidade da nave
            this.speed = 0.008f;

        private int contador;
        private ContentManager contents;

        public Player(ContentManager contents, string assetName) 
            : base(contents, assetName){
                contador = 0;
                this.contents = contents;

        }

        public override void Update(GameTime gameTime)
        {
            //Todo: atualizar o player de acordo com o teclado
            
            //Movimento da nave atraves do teclado

            //Guarda o estado do teclado
            currentKeyboardState = Keyboard.GetState();
            //Se pressionado esquerda
            if (currentKeyboardState.IsKeyDown(Keys.A)) 
            {

                this.position.X -= speed;

            }
            //Se pressionado cima
            if (currentKeyboardState.IsKeyDown(Keys.W)) 

            {

                this.position.Y += speed;

            }
            //Se pressionado baixo
            if (currentKeyboardState.IsKeyDown(Keys.S)) 
            {

                this.position.Y -= speed;

            }
            //Se pressioando direita
            if (currentKeyboardState.IsKeyDown(Keys.D))
            {

                this.position.X += speed;

            }

            

            base.position.X += Camera.speed / 2;

            if (contador > 5000)
            {
                scene.AddSprite(new Missil(contents, "missil", TipoMissil.EmFrente, 1).Scl(0.2f).
                At(new Vector2(position.X + 0.2f, position.Y)));
                contador = 0;
            }
            contador += gameTime.ElapsedGameTime.Milliseconds;

            base.Update(gameTime);
        }

    }
}
