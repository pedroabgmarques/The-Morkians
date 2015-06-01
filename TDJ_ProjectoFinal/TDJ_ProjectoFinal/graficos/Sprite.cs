using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDJ_ProjectoFinal.entidades;

namespace TDJ_ProjectoFinal.graficos
{
    /// <summary>
    /// Descreve uma sprite
    /// </summary>
    public class Sprite
    {
        /// <summary>
        /// 
        /// </summary>
        public bool HasCollisions { protected set; get; }
        /// <summary>
        /// Textura da imagem
        /// </summary>
        public Texture2D image;
        /// <summary>
        /// Posicao da imagem
        /// </summary>
        public Vector2 position;
        /// <summary>
        /// Raio da bounding box
        /// </summary>
        protected float radius; // raio da "bounding box"
        /// <summary>
        /// Tamanho da sprite
        /// </summary>
        public Vector2 size;
        /// <summary>
        /// rotação da sprite
        /// </summary>
        protected float rotation;
        /// <summary>
        /// Cena da sprite
        /// </summary>
        protected Scene scene;
        /// <summary>
        /// 
        /// </summary>
        public Vector2 pixelsize;
        /// <summary>
        /// Rectângulo de source da imagem
        /// </summary>
        protected Rectangle? source = null;
        /// <summary>
        /// Array de cores
        /// </summary>
        protected Color[] pixels;
        /// <summary>
        /// Instância de ContentManager
        /// </summary>
        protected ContentManager cManager;

        /// <summary>
        /// Instância de SpriteEffects
        /// </summary>
        public SpriteEffects spriteEffects;
        /// <summary>
        /// Construtor da classe Sprite
        /// </summary>
        /// <param name="contents">Instância de ContentManager</param>
        /// <param name="assetName">Textura</param>
        public Sprite(ContentManager contents, String assetName)
        {
            this.cManager = contents;
            this.HasCollisions = false;
            this.rotation = 0f;
            this.position = Vector2.Zero;
            this.image = contents.Load<Texture2D>(assetName);
            this.pixelsize = new Vector2(image.Width, image.Height);
            this.size = new Vector2(1f, (float)image.Height / (float)image.Width);
            this.spriteEffects = SpriteEffects.None;
        }

        /// <summary>
        /// Verifica a existência de colisões entre esta sprite e outra
        /// </summary>
        /// <param name="other">Sprite com a qual é necessário verificar a colisão</param>
        /// <param name="collisionPoint">Ponto de colisão</param>
        /// <returns>True se houve colisão</returns>
        public bool CollidesWith(Sprite other, out Vector2 collisionPoint)
        {
            collisionPoint = position; // Calar o compilador

            if (!this.HasCollisions) return false;
            if (!other.HasCollisions) return false;

            float distance = (this.position - other.position).Length();

            if (distance > this.radius + other.radius) return false;

            return this.PixelTouches(other, out collisionPoint);
        }
        /// <summary>
        /// Activa colisoes num objecto
        /// </summary>
        public virtual void EnableCollisions()
        {
            this.HasCollisions = true;
            this.radius = (float)Math.Sqrt(Math.Pow(size.X / 2, 2) +
                                             Math.Pow(size.Y / 2, 2));

            pixels = new Color[(int)(pixelsize.X * pixelsize.Y)];
            image.GetData<Color>(pixels);
        }

        /// <summary>
        /// Devolve a cor de um determinado pixel / posição
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Color GetColorAt(int x, int y)
        {
            // Se nao houver collider, da erro!!!
            return pixels[x + y * (int)pixelsize.X];
        }


        private Vector2 ImagePixelToVirtualWorld(int i, int j)
        {
            float x = i * size.X / (float)pixelsize.X;
            float y = j * size.Y / (float)pixelsize.Y;
            return new Vector2(position.X + x - (size.X * 0.5f),
                               position.Y - y + (size.Y * 0.5f));
        }

        private Vector2 VirtualWorldPointToImagePixel(Vector2 p)
        {
            Vector2 delta = p - position;
            float i = delta.X * pixelsize.X / size.X;
            float j = delta.Y * pixelsize.Y / size.Y;

            i += pixelsize.X * 0.5f;
            j = pixelsize.Y * 0.5f - j;

            return new Vector2(i, j);
        }

        /// <summary>
        /// Indica se existe colisão entre dois pixeis
        /// </summary>
        /// <param name="other">Sprite contra a qual será verificada a colisão</param>
        /// <param name="collisionPoint">Ponto da colisão</param>
        /// <returns>True caso exista colisão entre dois pixeis</returns>
        public bool PixelTouches(Sprite other, out Vector2 collisionPoint)
        {
            // Se nao houver colisao, o ponto de colisao retornado e'
            // a posicao da Sprite (podia ser outro valor qualquer)
            collisionPoint = position;

            bool touches = false;

            int i = 0;
            while (touches == false && i < pixelsize.X)
            {
                int j = 0;
                while (touches == false && j < pixelsize.Y)
                {
                    if (GetColorAt(i, j).A > 0)
                    {
                        Vector2 CollidePoint = ImagePixelToVirtualWorld(i, j);
                        Vector2 otherPixel = other.VirtualWorldPointToImagePixel(CollidePoint);

                        if (otherPixel.X >= 0 && otherPixel.Y >= 0 &&
                            otherPixel.X < other.pixelsize.X &&
                            otherPixel.Y < other.pixelsize.Y)
                        {
                            if (other.GetColorAt((int)otherPixel.X, (int)otherPixel.Y).A > 0)
                            {
                                touches = true;
                                collisionPoint = CollidePoint;
                            }
                        }

                    }
                    j++;
                }
                i++;
            }
            return touches;
        }
        /// <summary>
        /// Faz o scale da sprite
        /// </summary>
        /// <param name="scale">Valor para scale</param>
        public virtual void Scale(float scale)
        {
            this.size *= scale;
        }
        /// <summary>
        /// Atribui a sprite á cena
        /// </summary>
        /// <param name="s">Cena a ser atibuida</param>
        public virtual void SetScene(Scene s)
        {
            this.scene = s;
        }
        /// <summary>
        /// Escala a sprite
        /// </summary>
        /// <param name="scale">Valor de escala</param>
        /// <returns>Sprite escalada</returns>
        public Sprite Scl(float scale)
        {
            
            this.Scale(scale);
           
            return this;
        }


        /// <summary>
        /// Desenha esta sprite
        /// </summary>
        /// <param name="gameTime">Instância de gameTime</param>
        public virtual void Draw(GameTime gameTime)
        {
            Rectangle pos = Camera.WorldSize2PixelRectangle(this.position, this.size);
            // scene.SpriteBatch.Draw(this.image, pos, Color.White);
            scene.SpriteBatch.Draw(this.image, pos, source, Color.White,
                this.rotation, new Vector2(pixelsize.X / 2, pixelsize.Y / 2),
                spriteEffects, 0);
        }

        /// <summary>
        /// Setter da rotação de uma sprite
        /// </summary>
        /// <param name="r">Valor da rotação</param>
        public virtual void SetRotation(float r)
        {
            this.rotation = r;
        }

        /// <summary>
        /// Atualiza o estado de uma sprite
        /// </summary>
        /// <param name="gameTime">Instância de gameTime</param>
        public virtual void Update(GameTime gameTime) { }

        /// <summary>
        /// Dispõe a sprite
        /// </summary>
        public virtual void Dispose()
        {
            this.image.Dispose();
        }

        /// <summary>
        /// Destrói esta instância de sprite, retirando-a de todas as listas
        /// </summary>
        public void Destroy()
        {
            if (this is Bala) WeaponsManager.removeBala((Bala)this);
            if (this is Missil) WeaponsManager.removeMissil((Missil)this);
            if (this is NPC) EnemyManager.removeEnemy((NPC)this);
            this.scene.RemoveSprite(this);
        }

        /// <summary>
        /// Setter de posição da sprite
        /// </summary>
        /// <param name="position">Nova posição</param>
        public void SetPosition(Vector2 position)
        {
            this.position = position;
        }

        /// <summary>
        /// Coloca a sprite numa determinada posição e devolve a sprite
        /// </summary>
        /// <param name="p">Nova posição</param>
        /// <returns>Sprite reposicionada</returns>
        public Sprite At(Vector2 p)
        {
            this.SetPosition(p);
            return this;
        }
    }
}
