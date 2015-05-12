﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDJ_ProjectoFinal.entidades
{
    public static class WeaponsManager
    {

        public static List<Bala> balasAtivas;
        public static List<Bala> balasMortas;

        public static List<Missil> misseisAtivos;
        public static List<Missil> misseisMortos;

        private static Bala balaTemp;
        private static Missil missilTemp;

        private static ContentManager content;
        private static Texture2D balaplayer, balainimigo, missilplayer, missilinimigo;

        private static Vector2 vectorTemp;

        public static void LoadContent(ContentManager contents)
        {

            content = contents;

            balasAtivas = new List<Bala>(100);
            balasMortas = new List<Bala>(100);

            misseisAtivos = new List<Missil>(50);
            misseisMortos = new List<Missil>(50);

            balaplayer = contents.Load<Texture2D>("balaplayer");
            balainimigo = contents.Load<Texture2D>("balainimigo");
            missilplayer = contents.Load<Texture2D>("missilPlayer");
            missilinimigo = contents.Load<Texture2D>("missil");

            vectorTemp = Vector2.Zero;
            
            //gerar 100 balas vazias
            for (int i = 0; i < 100; i++)
            {
                balaTemp = new Bala(content, "balaplayer", 0, OrigemBala.player, DireccaoBala.EmFrente);
                balasMortas.Add(balaTemp);
            }

            //gerar 50 misseis vazios
            for (int i = 0; i < 50; i++) 
            {
                missilTemp = new Missil(content, "missilPlayer", TipoMissil.EmFrente, 0, OrigemBala.player);
                misseisMortos.Add(missilTemp);
            }
        }

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

        public static void removeMissil(Missil missil)
        {
            misseisAtivos.Remove(missil);
            misseisMortos.Add(missil);
        }

        public static Bala addBala(string assetname, int direcao, OrigemBala origemBala, DireccaoBala direcaoBala)
        {
            balaTemp = balasMortas.First();
            if (assetname == "balaplayer")
            {
                balaTemp.image = balaplayer;
            }
            else
            {
                balaTemp.image = balainimigo;
            }
            vectorTemp.X = 1f;
            vectorTemp.Y = (float)balaTemp.image.Height / (float)balaTemp.image.Width;
            balaTemp.size = vectorTemp;
            balaTemp.spriteEffects = direcao > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            balaTemp.direccao = direcao;
            balaTemp.origemBala = origemBala;
            balaTemp.direccaobala = direcaoBala;
            balasAtivas.Add(balaTemp);
            balasMortas.Remove(balaTemp);
            return balaTemp;
        }

        public static void removeBala(Bala bala)
        {
            balasAtivas.Remove(bala);
            balasMortas.Add(bala);
        }

    }
}