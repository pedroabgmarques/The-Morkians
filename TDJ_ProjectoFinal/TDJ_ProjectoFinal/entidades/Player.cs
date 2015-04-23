using Microsoft.Xna.Framework;
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
        
        private int contador;
        private ContentManager contents;
        private int shootTime = 500;
        public Rectangle boundingBox;
        private List<PowerUp> PowerUps;
        
        Sprite collided;
        Vector2 collisionPoint;
        public Player(ContentManager contents, string assetName) 
            : base(contents, assetName){
                contador = 0;
                this.contents = contents;
                //Velocidade da nave
                this.speed = 0.008f;
                              
                this.EnableCollisions();
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
            if (currentKeyboardState.IsKeyDown(Keys.Space)) 
            {
                if (contador >= shootTime) { 
                    
                    scene.AddSprite(new Bala(contents, "balasimples", TipoBala.Triplo, 1).Scl(0.09f).
                    At(new Vector2(position.X + 0.3f, position.Y)));
                    contador = 0;
                    
                }
            }

            if(this.scene.Collides(this,out this.collided,out this.collisionPoint,this.scene.powerUps))
            {
                this.collided.Destroy();
            }
            if (this.scene.Collides(this, out this.collided, out this.collisionPoint, this.scene.sprites))
            {
                this.collided.Destroy();
            }

            base.position.X += Camera.speed / 2;

            
            contador += gameTime.ElapsedGameTime.Milliseconds;

            base.Update(gameTime);
        }

        //public override void Draw(GameTime gameTime)
        //{
        //    //desenhar rectangulo
        //    Texture2D rect = new Texture2D(Camera.gDevManager.GraphicsDevice, boundingBox.Width, boundingBox.Height);

        //    Color[] data = new Color[boundingBox.Width * boundingBox.Height];
        //    for (int i = 0; i < data.Length; ++i) data[i] = Color.Chocolate;
        //    rect.SetData(data);
            
        //    Vector2 coor = new Vector2(this.boundingBox.X, this.boundingBox.Y);
        //    scene.SpriteBatch.Draw(rect, coor, Color.White);

        //    base.Draw(gameTime);

        //}

    }
}
