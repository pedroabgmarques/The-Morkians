using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDJ_ProjectoFinal.graficos
{
    /// <summary>
    /// Descreve uma animação
    /// </summary>
    public class AnimatedSprite : Sprite
    {
        private int ncols, nrows;
        private Point currentFrame;
        /// <summary>
        /// FPS da animação
        /// </summary>
        public float animationInterval = 1f / 60f;
        /// <summary>
        /// Temporizador de cada frame
        /// </summary>
        public float animationTimer = 0f;
        /// <summary>
        /// Indica se a animação está em loop
        /// </summary>
        public bool Loop;
        /// <summary>
        /// Tempo até iniciar a animação
        /// </summary>
        public float delay;
        /// <summary>
        /// Tempo decorrido desde a criação da animação (para verificar se se deve iniciar a animação)
        /// </summary>
        public float timeToExplode;
        
        /// <summary>
        /// Construtor de nova animação
        /// </summary>
        /// <param name="content">Instância de ContentManager</param>
        /// <param name="filename">Nome da spritesheet a utilizar</param>
        /// <param name="nrows">Nº de linhas da spritesheet</param>
        /// <param name="ncols">Nº de colunas da spritesheet</param>
        /// <param name="loop">Em loop?</param>
        /// <param name="position">Posição inicial da animação</param>
        /// <param name="scale">Escala da animação</param>
        /// <param name="delay">Tempo a decorrer antes do inicio da animação</param>
        public AnimatedSprite(ContentManager content,
            string filename, int nrows, int ncols, bool loop, Vector2 position, float scale,float delay=0) :
            base(content, filename)
        {
            this.ncols = ncols;
            this.nrows = nrows;
            this.pixelsize.X = this.pixelsize.X / ncols;
            this.pixelsize.Y = this.pixelsize.Y / nrows;
            this.size = new Vector2(1f,
                (float)pixelsize.Y / (float)pixelsize.X);
            this.currentFrame = Point.Zero;
            this.Loop = loop;
            this.position = position;
            this.delay = delay;
            this.Scale(scale);
            

        }

        /// <summary>
        /// Passa para o próximo frame da animação
        /// </summary>
        public void nextFrame()
        {
            if (currentFrame.X < ncols - 1)
            {
                currentFrame.X++;
            }
            else if (currentFrame.Y < nrows - 1)
            {
                currentFrame.X = 0;
                currentFrame.Y++;
            }
            else
            {
                if (Loop)
                {
                    currentFrame = Point.Zero;
                }
                else
                {
                    Destroy();
                }

            }
        }

        /// <summary>
        /// Atualiza o estado de uma animação
        /// </summary>
        /// <param name="gameTime">Instância de gameTime</param>
        public override void Update(GameTime gameTime)
        {
            timeToExplode += 
                (float)gameTime.ElapsedGameTime.Milliseconds;
            animationTimer +=
                (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timeToExplode >= delay) 
            {
                if (animationTimer > animationInterval)
                {
                    
                    animationTimer = 0f;
                    nextFrame();

                }
                

            } 
            
            
            base.Update(gameTime);
        }

        /// <summary>
        /// Desenha uma animação
        /// </summary>
        /// <param name="gameTime">Instância de gameTime</param>
        public override void Draw(GameTime gameTime)
        {
            if (timeToExplode >= delay)
            {

                source = new Rectangle((int)(currentFrame.X * pixelsize.X),
                    (int)(currentFrame.Y * pixelsize.Y), (int)pixelsize.X,
                    (int)pixelsize.Y);
                base.Draw(gameTime);
            }

            
        }

        /// <summary>
        /// Permite que a animação tenha colisões com restantes sprites
        /// </summary>
        public override void EnableCollisions()
        {
            this.HasCollisions = true;

            this.radius = (float)Math.Sqrt(Math.Pow(size.X / 2, 2) +
                                           Math.Pow(size.Y / 2, 2));

            pixels = new Color[(int)(pixelsize.X * pixelsize.Y)];
            image.GetData<Color>(0, new Rectangle(
                    (int)(currentFrame.X * pixelsize.X),
                    (int)(currentFrame.Y * pixelsize.Y),
                    (int)pixelsize.X,
                    (int)pixelsize.Y),
                 pixels, 0,
                (int)(pixelsize.X * pixelsize.Y));
        }
    }
}
