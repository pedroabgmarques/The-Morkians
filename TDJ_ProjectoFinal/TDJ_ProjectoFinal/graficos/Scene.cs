using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDJ_ProjectoFinal.graficos;
using TDJ_ProjectoFinal.entidades;

namespace TDJ_ProjectoFinal
{
    public class Scene
    {
        public SpriteBatch SpriteBatch { get; private set; }
        public List<Sprite> sprites;
        public List<Sprite> powerUps;
        public List<Sprite> enimigos;

        public Scene(SpriteBatch sb)
        {
            this.SpriteBatch = sb;
            this.sprites = new List<Sprite>();
            this.powerUps = new List<Sprite>();
            this.enimigos = new List<Sprite>();
        }

        public void AddSprite(Sprite s)
        {
            this.sprites.Add(s);
            s.SetScene(this);
        }

        public void AddPowerUp(PowerUp s)
        {
            this.powerUps.Add(s);
            s.SetScene(this);
        }

        public void AddEnimigo(NPC s)
        {
            this.enimigos.Add(s);
            s.SetScene(this);
        }

        public void RemoveSprite(Sprite s)
        {
            this.sprites.Remove(s);
            this.powerUps.Remove(s);
            this.enimigos.Remove(s);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var sprite in sprites.ToList())
            {
                sprite.Update(gameTime);
            }
            foreach (var powerUp in powerUps.ToList())
            {
                powerUp.Update(gameTime);
            }
            foreach (var enimigo in enimigos.ToList())
            {
                enimigo.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            if (sprites.Count > 0)
            {
                this.SpriteBatch.Begin();
                foreach (var sprite in sprites)
                {
                    sprite.Draw(gameTime);
                }
                foreach (var powerUp in powerUps.ToList())
                {
                    powerUp.Draw(gameTime);
                }
                
                foreach (var enimigo in enimigos.ToList())
                {
                    enimigo.Draw(gameTime);
                }
                this.SpriteBatch.End();
            }
        }

        public bool Collides(Sprite s, out Sprite collided,
                                       out Vector2 collisionPoint,List<Sprite> list)
        {
            bool collisionExists = false;
            collided = s;  // para calar o compilador
            collisionPoint = Vector2.Zero; // para calar o compilador

            foreach (var sprite in list)
            {
                if (s == sprite) continue;
                if (s.CollidesWith(sprite, out collisionPoint))
                {
                    collisionExists = true;
                    collided = sprite;
                    break;
                }
            }
            return collisionExists;
        }

        public void Dispose()
        {
            foreach (var sprite in sprites)
            {
                sprite.Dispose();
            }
        }
    }
}
