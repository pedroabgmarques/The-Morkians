using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDJ_ProjectoFinal
{
    class som
    {
        private static SoundEffect tiro;
        private static SoundEffect explosao;
        private static SoundEffect rocket;

        public static void playTiro(ContentManager content)
        {
            if (tiro == null)
            {
                tiro = content.Load<SoundEffect>("som\\tiro");
            }
                tiro.Play(0.1f, 1, 0f);
            
        }

        public static void playTiroEnimigo(ContentManager content)
        {
            if (tiro == null)
            {
                tiro = content.Load<SoundEffect>("som\\tiro2");
            }
            tiro.Play(0.1f, 1, 0f);
        }

        public static void playExplosao(ContentManager content)
        {
            if (explosao == null)
            {
                explosao = content.Load<SoundEffect>("som\\somExplosao");
                
            }
            explosao.Play(0.1f, 1, 0f);
        }

        public static void playRocket(ContentManager content)
        {
            if (rocket == null)
            {
                rocket = content.Load<SoundEffect>("som\\rocket");

            }
            rocket.Play(0.1f, 1, 0f);
        }
    }
}
