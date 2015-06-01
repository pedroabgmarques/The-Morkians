using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDJ_ProjectoFinal.entidades
{
    /// <summary>
    /// Faz a gestão de tiros e mísseis.
    /// Aquando do Load são criadas centenas de instâncias destes objetos, que são recicladas de modo a evitar 
    /// criar objetos em runtime.
    /// </summary>
    public static class WeaponsManager
    {
        /// <summary>
        /// Lista de balas que estão neste momento ativas no jogo
        /// </summary>
        public static List<Bala> balasAtivas;
        /// <summary>
        /// Lista de balas à espera de serem usadas
        /// </summary>
        public static List<Bala> balasMortas;
        /// <summary>
        /// Lista de misseis ativos no jogo
        /// </summary>
        public static List<Missil> misseisAtivos;
        /// <summary>
        /// Lista de mísses à espera de serem usados
        /// </summary>
        public static List<Missil> misseisMortos;

        private static Bala balaTemp;
        private static Missil missilTemp;

        private static ContentManager content;
        private static Texture2D balaplayer, balainimigo, missilplayer, missilinimigo, baladefesas;

        private static Vector2 vectorTemp;

        /// <summary>
        /// Carrega os assets necessários e cria centenas de instâncias
        /// </summary>
        /// <param name="contents">Instância de ContentManager</param>
        public static void LoadContent(ContentManager contents)
        {

            content = contents;

            balasAtivas = new List<Bala>(500);
            balasMortas = new List<Bala>(500);

            misseisAtivos = new List<Missil>(500);
            misseisMortos = new List<Missil>(500);

            balaplayer = contents.Load<Texture2D>("balaplayer");
            balainimigo = contents.Load<Texture2D>("balainimigo");
            missilplayer = contents.Load<Texture2D>("missilPlayer");
            missilinimigo = contents.Load<Texture2D>("missil");
            baladefesas = contents.Load<Texture2D>("baladefesas");

            vectorTemp = Vector2.Zero;
            
            //gerar 100 balas vazias
            for (int i = 0; i < 500; i++)
            {
                balaTemp = new Bala(content, "balaplayer", 0, OrigemBala.player, DireccaoBala.EmFrente);
                balasMortas.Add(balaTemp);
            }

            //gerar 50 misseis vazios
            for (int i = 0; i < 500; i++) 
            {
                missilTemp = new Missil(content, "missilPlayer", TipoMissil.EmFrente, 0, OrigemBala.player);
                misseisMortos.Add(missilTemp);
            }
        }

        /// <summary>
        /// Recicla um missil que é necessário para o jogo
        /// </summary>
        /// <param name="assetname">Textura do missil a utilizar</param>
        /// <param name="tipoMissil">Tipo de missil - inimigo ou jogador</param>
        /// <param name="direcao">Direção do ecrã em que o missil se move</param>
        /// <param name="origemBala">Entidade que originou o missil</param>
        /// <param name="alvo">Alvo do missil teleguiado</param>
        /// <returns>Missil reciclado</returns>
        public static Missil addMissil(string assetname, TipoMissil tipoMissil, int direcao, OrigemBala origemBala, FlyingEntity alvo = null)
        {
            missilTemp = misseisMortos.First();
            if (assetname == "missilPlayer")
            {
                missilTemp.image = missilplayer;
            }
            else
            {
                missilTemp.image = missilinimigo;
            }
            vectorTemp.X = 1f;
            vectorTemp.Y = (float)missilTemp.image.Height / (float)missilTemp.image.Width;
            missilTemp.size = vectorTemp;
            missilTemp.spriteEffects = direcao > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            missilTemp.tipoMissil = tipoMissil;
            missilTemp.direccao = direcao;
            missilTemp.alvo = alvo;
            missilTemp.origemBala = origemBala;
            
            misseisAtivos.Add(missilTemp);
            misseisMortos.Remove(missilTemp);
            return missilTemp;
        }

        /// <summary>
        /// Recicla um missil que já não é necessário
        /// </summary>
        /// <param name="missil">Instância de missil a remover dos misseis ativos</param>
        public static void removeMissil(Missil missil)
        {
            misseisAtivos.Remove(missil);
            misseisMortos.Add(missil);
        }

        /// <summary>
        /// Recicla uma bala que é necessária para o jogo
        /// </summary>
        /// <param name="assetname">Nome da textura a utilizar</param>
        /// <param name="direcao">Direção do ecrã em que a bala se movimenta</param>
        /// <param name="origemBala">Entidade que originou a bala</param>
        /// <param name="direcaoBala">Tipo de bala - Em frente, cima ou baixo</param>
        /// <param name="parent">Turret que disparou a bala</param>
        /// <returns>Uma bala reciclada</returns>
        public static Bala addBala(string assetname, int direcao, OrigemBala origemBala, DireccaoBala direcaoBala, Defence parent = null)
        {
            balaTemp = balasMortas.First();
            if (assetname == "balaplayer")
            {
                balaTemp.image = balaplayer;
            }
            else if (assetname == "baladefesas")
            {
                balaTemp.image = baladefesas;
            }
            else
            {
                balaTemp.image = balainimigo;
            }

            balaTemp.parent = parent;

            vectorTemp.X = 1f;
            vectorTemp.Y = (float)balaTemp.image.Height / (float)balaTemp.image.Width;
            balaTemp.size = vectorTemp;
            balaTemp.spriteEffects = direcao > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            balaTemp.direccao = direcao;
            balaTemp.origemBala = origemBala;
            balaTemp.direccaobala = direcaoBala;
            balaTemp.direction = Vector2.Zero;
            balasAtivas.Add(balaTemp);
            balasMortas.Remove(balaTemp);
            return balaTemp;
        }

        /// <summary>
        /// Recicla uma bala que já não é necessária no jogo
        /// </summary>
        /// <param name="bala">Instância de bala a reciclar</param>
        public static void removeBala(Bala bala)
        {

            if (balasAtivas.Contains(bala))
            {
                balasAtivas.Remove(bala);
                balasMortas.Add(bala);
            }
            
        }

    }
}
