using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDJ_ProjectoFinal.graficos;

namespace TDJ_ProjectoFinal.entidades
{
    /// <summary>
    /// Faz a gestão dos NPC's - cria instâncias aquando do load e reciclas durante o gameplay, para evitar criar
    /// instâncias em runtime
    /// </summary>
    public static class EnemyManager
    {

        private static Texture2D kamikaze, bombardeiro, caça;
        /// <summary>
        /// Lista de kamikazes ativos no jogo
        /// </summary>
        public static List<NPC> kamikazesVivos;
        /// <summary>
        /// Lista de kamikazes que não estão a ser usados pelo jogo
        /// </summary>
        public static List<NPC> kamikazesMortos;

        /// <summary>
        /// Lista de bombardeiros ativos no jogo
        /// </summary>
        public static List<NPC> bombardeirosVivos;
        /// <summary>
        /// Lista de bombardeiros que não estão a ser usados pelo jogo
        /// </summary>
        public static List<NPC> bombardeirosMortos;

        /// <summary>
        /// Lista de caças ativos no jogo
        /// </summary>
        public static List<NPC> caçasVivos;
        /// <summary>
        /// Lista de caças que não estão a ser usados pelo jogo
        /// </summary>
        public static List<NPC> caçasMortos;

        private static NPC npcTemp;

        private static ContentManager content;

        /// <summary>
        /// Carrega os assets necessários e cria instâncias de NPC's que posteriormente são recicladas
        /// </summary>
        /// <param name="contents">Instância de ContentManager</param>
        /// <param name="random">Instâcia de random</param>
        public static void LoadContent(ContentManager contents, Random random)
        {
            content = contents;

            kamikazesVivos = new List<NPC>(50);
            kamikazesMortos = new List<NPC>(50);

            bombardeirosVivos = new List<NPC>(50);
            bombardeirosMortos = new List<NPC>(50);

            caçasVivos = new List<NPC>(50);
            caçasMortos = new List<NPC>(50);

            kamikaze = contents.Load<Texture2D>("Kamikaze");
            bombardeiro = contents.Load<Texture2D>("bombardeiro");
            caça = contents.Load<Texture2D>("caça");
            
            //gerar 50 naves de cada tipo
            for (int i = 0; i < 50; i++)
            {
                npcTemp = new NPC(content, "Kamikaze", TipoNave.Interceptor, 0.3f, random);
                kamikazesMortos.Add(npcTemp);
            }
            for (int i = 0; i < 50; i++)
            {
                npcTemp = new NPC(content, "bombardeiro", TipoNave.Bomber, 0.5f, random);
                bombardeirosMortos.Add(npcTemp);
            }
            for (int i = 0; i < 50; i++)
            {
                npcTemp = new NPC(content, "caça", TipoNave.Interceptor, 0.4f, random);
                caçasMortos.Add(npcTemp);
            }
        }

        
        /// <summary>
        /// Recila um kamikaze que é necessário para o jogo
        /// </summary>
        /// <returns>Instância de NPC do tipo kamikaze</returns>
        public static NPC addKamikaze()
        {
            npcTemp = kamikazesMortos.First();
            kamikazesVivos.Add(npcTemp);
            kamikazesMortos.Remove(npcTemp);
            return npcTemp;
        }

        /// <summary>
        /// Recila um bombardeiro que é necessário para o jogo
        /// </summary>
        /// <returns>Instância de NPC do tipo bombardeiro</returns>
        public static NPC addBombardeiro()
        {
            npcTemp = bombardeirosMortos.First();
            bombardeirosVivos.Add(npcTemp);
            bombardeirosMortos.Remove(npcTemp);
            return npcTemp;
        }

        /// <summary>
        /// Recila um caça que é necessário para o jogo
        /// </summary>
        /// <returns>Instância de NPC do tipo caça</returns>
        public static NPC addCaça()
        {
            npcTemp = caçasMortos.First();
            caçasVivos.Add(npcTemp);
            caçasMortos.Remove(npcTemp);
            return npcTemp;
        }

        /// <summary>
        /// Recicla um NPC que já não é necessário para o jogo
        /// </summary>
        /// <param name="enemy"></param>
        public static void removeEnemy(NPC enemy)
        {

            switch (enemy.tipoNave)
            {
                case TipoNave.Interceptor:
                    kamikazesVivos.Remove(enemy);
                    kamikazesMortos.Add(enemy);
                    break;
                case TipoNave.Hunter:
                    caçasVivos.Remove(enemy);
                    caçasMortos.Add(enemy);
                    break;
                case TipoNave.Bomber:
                    bombardeirosVivos.Remove(enemy);
                    bombardeirosMortos.Add(enemy);
                    break;
                case TipoNave.Mothership:
                    break;
                default:
                    break;
            }
            
            
        }

    }
}
