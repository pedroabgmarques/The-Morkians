using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDJ_ProjectoFinal.graficos;
using TDJ_ProjectoFinal.entidades;
using Microsoft.Xna.Framework.Content;

namespace TDJ_ProjectoFinal
{
    public class Scene
    {
        public SpriteBatch SpriteBatch { get; private set; }
        public List<Sprite> sprites;
        public List<Sprite> powerUps;
        public List<Sprite> inimigos;
        public List<Sprite> explosoes;
        public List<Sprite> UIs;
        public Player player;
        public bool playerKilled=false;
        public bool active = true;
        public int enemiesKilled;
        public bool bossKilled ;
        public bool bossLevelClear = false;
        public GraphicsDevice gDevice;
        public Queue<KeyValuePair<string, Vector2>> textos;
        public bool fundoTexto;
        public Scene(SpriteBatch sb)
        {
            this.SpriteBatch = sb;
            this.sprites = new List<Sprite>();
            this.powerUps = new List<Sprite>();
            this.inimigos = new List<Sprite>();
            this.explosoes = new List<Sprite>();
            this.UIs = new List<Sprite>();
            this.textos = new Queue<KeyValuePair<string, Vector2>>();
            this.enemiesKilled = 0;
            this.fundoTexto = false;
            
        }

        public void Clear()
        {
            this.sprites.Clear();
            this.powerUps.Clear();
            this.inimigos.Clear();
            this.explosoes.Clear();
            this.textos.Clear();
            this.UIs.Clear();
            GC.Collect();
        }

        public void ClearUI()
        {
            this.UIs.Clear();
        }

        public void AddTexto(string texto, Vector2 posicao)
        {
            textos.Enqueue(new KeyValuePair<string, Vector2>(texto, posicao));
        }

        public KeyValuePair<string, Vector2> GetTexto()
        {
            return textos.Dequeue();
            
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

        public void AddInimigo(Sprite s)
        {
            this.inimigos.Add(s);
            s.SetScene(this);
        }

        public void AddExplosao(AnimatedSprite s)
        {
            this.explosoes.Add(s);
            s.SetScene(this);
        }

        public void AddUI(Sprite s)
        {
            this.UIs.Add(s);
            s.SetScene(this);
        }

        

        public void RemoveSprite(Sprite s)
        {
            this.sprites.Remove(s);
            this.powerUps.Remove(s);
            if (s is NPC)
            {
                this.enemiesKilled++;
                
            }
            this.inimigos.Remove(s);
        
            this.explosoes.Remove(s);
            this.UIs.Remove(s);

            if (s is Player)
            {
                this.player = null;
                this.playerKilled = true;
                som.fade(GameState.Nivel1);
                som.fade(GameState.Nivel2);
                som.fade(GameState.Nivel3);
            }
            
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
            foreach (var inimigo in inimigos.ToList())
            {
                inimigo.Update(gameTime);
            }
            foreach (var explosao in explosoes.ToList())
            {
                explosao.Update(gameTime);
            }
            foreach (var UI in UIs)
            {
                UI.Update(gameTime);
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
                
                foreach (PowerUp powerUp in powerUps)
                {
                    powerUp.Draw(gameTime);
                }
                
                foreach (Sprite inimigo in inimigos)
                {
                    inimigo.Draw(gameTime);
                }
                foreach (Sprite explosao in explosoes)
                {
                    explosao.Draw(gameTime);
                }
                foreach (UI UI in UIs)
                {
                    UI.Draw(gameTime);
                }
         
                this.SpriteBatch.End();
            }
        }

        public bool Collides(Sprite s, out Sprite collided,
                                       out Vector2 collisionPoint, List<Sprite> list)
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
