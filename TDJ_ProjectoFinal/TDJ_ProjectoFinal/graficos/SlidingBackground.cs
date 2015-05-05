﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDJ_ProjectoFinal.graficos
{
    class SlidingBackground:Sprite
    {
        private Texture2D texture;
        private Vector2 position;
        private Vector2 size; //world size
        private Vector2 origin; //center of the image in pixels
        private Vector2 lastCameraPosition;
        private float speed; //sliding speed
        private Scene scene;

        public SlidingBackground(ContentManager contents, string assetName, float speed)
            : base(contents, assetName)
        {
            texture = contents.Load<Texture2D>(assetName);
            //origin = new Vector2(texture.Width / 2f, texture.Height / 2f);
            lastCameraPosition = Camera.GetTarget();
            //size = new Vector2(Camera.worldWidth, Camera.worldWidth * texture.Height / texture.Width);
            position = Camera.GetTarget();
            this.speed = speed;
        }

        public void SetScene(Scene cena)
        {
            this.scene = cena;
        }

        public virtual void Draw(GameTime gameTime)
        {

        }
        public override void Update(GameTime gametime)
        {
            base.position.X += this.speed;
            
        }
        public void Dispose()
        {
            texture.Dispose();
        }
    }
}
