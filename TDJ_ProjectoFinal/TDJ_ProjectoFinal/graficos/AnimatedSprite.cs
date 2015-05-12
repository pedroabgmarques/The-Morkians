﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDJ_ProjectoFinal.graficos
{
    public class AnimatedSprite : Sprite
    {
        private int ncols, nrows;
        private Point currentFrame;
        public float animationInterval = 1f / 60f;
        public float animationTimer = 0f;
        public bool Loop;
        

        public AnimatedSprite(ContentManager content,
            string filename, int nrows, int ncols, bool loop, Vector2 position, float scale) :
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
            this.Scale(scale);
            

        }
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

        public override void Update(GameTime gameTime)
        {
      
            animationTimer +=
                (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (animationTimer > animationInterval)
            {
                animationTimer = 0f;
                nextFrame();

            }

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {

            source = new Rectangle((int)(currentFrame.X * pixelsize.X),
                (int)(currentFrame.Y * pixelsize.Y), (int)pixelsize.X,
                (int)pixelsize.Y);

            base.Draw(gameTime);
        }

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
