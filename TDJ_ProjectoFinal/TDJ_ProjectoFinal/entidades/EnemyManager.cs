using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDJ_ProjectoFinal.entidades
{
    public static class EnemyManager
    {

        private static Texture2D kamikaze, bombardeiro, caça;

        public static List<NPC> kamikazesVivos;
        public static List<NPC> kamikazesMortos;

        public static List<NPC> bombardeirosVivos;
        public static List<NPC> bombardeirosMortos;

        public static List<NPC> caçasVivos;
        public static List<NPC> caçasMortos;

        private static NPC npcTemp;

        private static ContentManager content;

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

        public static NPC addKamikaze()
        {
            npcTemp = kamikazesMortos.First();
            kamikazesVivos.Add(npcTemp);
            kamikazesMortos.Remove(npcTemp);
            return npcTemp;
        }

        public static NPC addBombardeiro()
        {
            npcTemp = bombardeirosMortos.First();
            bombardeirosVivos.Add(npcTemp);
            bombardeirosMortos.Remove(npcTemp);
            return npcTemp;
        }

        public static NPC addCaça()
        {
            npcTemp = caçasMortos.First();
            caçasVivos.Add(npcTemp);
            caçasMortos.Remove(npcTemp);
            return npcTemp;
        }

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
