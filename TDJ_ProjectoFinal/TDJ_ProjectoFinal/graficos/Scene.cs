﻿using Microsoft.Xna.Framework;
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
    /// <summary>
    /// A classe Scene cria uma nova cena, cria listas para armezenar sprites genericas,powerups,enimigos,explosoes,elementos de UI e
    /// uma queue de textos.
    /// playerKilled - player morre fica true, else fica false.enemiesKilled - conta os enimigos abatidos;active - diz nos se a cena está activa ou nao;
    /// bossKilled - diz nos se boss já foi eliminado;bossLevelClear - diz nos se o nivel do boss ja foi completado.
    /// </summary>
    public class Scene
    {
        /// <summary>
        /// Instância de spriteBatch
        /// </summary>
        public SpriteBatch SpriteBatch { get; private set; }
        /// <summary>
        /// Lista de sprites
        /// </summary>
        public List<Sprite> sprites;
        /// <summary>
        /// Lista de powerups
        /// </summary>
        public List<Sprite> powerUps;
        /// <summary>
        /// Lista de inimigos
        /// </summary>
        public List<Sprite> inimigos;
        /// <summary>
        /// Lista de explosões
        /// </summary>
        public List<Sprite> explosoes;
        /// <summary>
        /// Lista de elementos da UI
        /// </summary>
        public List<Sprite> UIs;
        /// <summary>
        /// Instância do jogador
        /// </summary>
        public Player player;
        /// <summary>
        /// Indica se o player está vivo
        /// </summary>
        public bool playerKilled=false;
        /// <summary>
        /// Cena ativa?
        /// </summary>
        public bool active = true;
        /// <summary>
        /// Numero de inimigos mortos nesta cena
        /// </summary>
        public int enemiesKilled;
        /// <summary>
        /// Indica se o boss final está vivo
        /// </summary>
        public bool bossKilled ;
        /// <summary>
        /// Indica se o jogador passou o 3º nivel
        /// </summary>
        public bool bossLevelClear = false;
        /// <summary>
        /// Instância de GraphicsDevice
        /// </summary>
        public GraphicsDevice gDevice;
        /// <summary>
        /// Queue de textos a apresentar nesta cena
        /// </summary>
        public Queue<KeyValuePair<string, Vector2>> textos;
        /// <summary>
        /// Indica se o fundo do texto já está a ser desenhado
        /// </summary>
        public bool fundoTexto;

        /// <summary>
        /// Construtor da classe Scene
        /// </summary>
        /// <param name="sb">Instância de spriteBatch</param>
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
        /// <summary>
        /// Limpa as lista de sprites,powerups,enimigos,explosoes, textos e UI. Usa o garbage collector para libertar memoria.
        /// </summary>
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
        /// <summary>
        /// Limpa a lista de UI.
        /// </summary>
        public void ClearUI()
        {
            this.UIs.Clear();
        }
        /// <summary>
        /// Recebe por parametro uma string e um posicao e adiciona-o á queue.
        /// </summary>
        /// <param name="texto">Texto a escrever no ecrã</param>
        /// <param name="posicao">Posição do texto, em pixels</param>
        public void AddTexto(string texto, Vector2 posicao)
        {
            textos.Enqueue(new KeyValuePair<string, Vector2>(texto, posicao));
        }
        /// <summary>
        /// Este metodo devolve os textos que estão na Queue
        /// </summary>
        /// <returns>Queue de textos</returns>
        public KeyValuePair<string, Vector2> GetTexto()
        {
            return textos.Dequeue();
            
        }
        /// <summary>
        /// recebe por parametro uma sprite, adiciona-a á lista de sprites e atribui a sprite á cena
        /// </summary>
        /// <param name="s">Sprite a adicionar</param>
        public void AddSprite(Sprite s)
        {
            this.sprites.Add(s);
            s.SetScene(this);
        }
        /// <summary>
        /// Recebe por parametro um powerUp, adiciona-o á lista de power ups e atribui a sprite á cena.
        /// </summary>
        /// <param name="s">Powerup a adicionar</param>
        public void AddPowerUp(PowerUp s)
        {
            this.powerUps.Add(s);
            s.SetScene(this);
        }
        /// <summary>
        /// Recebe uma sprite por parametrom adiciona-a á lista de enimigos e atribui a sprite á cena.
        /// </summary>
        /// <param name="s">Sprite a adicionar</param>
        public void AddInimigo(Sprite s)
        {
            this.inimigos.Add(s);
            s.SetScene(this);
        }
        /// <summary>
        /// Recebe uma animated sprite por parametro, adiciona-a á lista de explosões e atrbui a sprite ´cena.
        /// </summary>
        /// <param name="s">Animação a adicionar</param>
        public void AddExplosao(AnimatedSprite s)
        {
            this.explosoes.Add(s);
            s.SetScene(this);
        }
        /// <summary>
        /// Recebe uma sprite por parametro, adiciona-a á lista de UI's e atribui a sprite á cena.
        /// </summary>
        /// <param name="s">Sprite a adicionar</param>
        public void AddUI(Sprite s)
        {
            this.UIs.Add(s);
            s.SetScene(this);
        }

        
        /// <summary>
        /// Recebe uma sprite por parametro e dependendo do tipo, remove-a da respetiva lista.
        /// No caso de o jogador morrer faz também o fade out da musica.
        /// </summary>
        /// <param name="s">Sprite a remover</param>
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
        /// <summary>
        /// O metodo update percorre todas as listas e faz o update aos objectos.
        /// </summary>
        /// <param name="gameTime">Instância de gameTime</param>
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
        /// <summary>
        /// O metodo Draw percorre todas as listas e desenha-as
        /// </summary>
        /// <param name="gameTime">Instância de gameTime</param>
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

        /// <summary>
        /// Verifica a colisão pixel a pixel entre duas sprites
        /// </summary>
        /// <param name="s">Sprite cuja colisão vai ser verificada</param>
        /// <param name="collided">Sprite com a qual existe uma colisão</param>
        /// <param name="collisionPoint">Coordenadas da colisão</param>
        /// <param name="list">Lista de sprites a verificar</param>
        /// <returns></returns>
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
        /// <summary>
        /// Este metodo faz o dispose de todas as sprites presentes na lista.
        /// </summary>
        public void Dispose()
        {
            foreach (var sprite in sprites)
            {
                sprite.Dispose();
            }
        }
    }
}
