using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;

namespace TDJ_ProjectoFinal
{
    static class som
    {
        private static SoundEffect tiro;
        private static SoundEffect explosao;
        private static SoundEffect rocket;
        private static ContentManager content;

        private static SoundEffect musicaMenu;
        private static SoundEffectInstance musicaMenuInstance;
        private static SoundEffect musicaBridge0;
        private static SoundEffectInstance musicaBridge0Instance;
        private static SoundEffect musicaNivel1;
        private static SoundEffectInstance musicaNivel1Instance;
        private static SoundEffect musicaNivel2;
        private static SoundEffectInstance musicaNivel2Instance;
        private static SoundEffect musicaNivel3;
        private static SoundEffectInstance musicaNivel3Instance;

        public static void Initialize(ContentManager contentManager)
        {
            content = contentManager;
        }

        public static void playTiro()
        {
            if (tiro == null)
            {
                tiro = content.Load<SoundEffect>("som\\tiro");
            }
                tiro.Play(0.1f, 1, 0f);
            
        }

        public static void playTiroEnimigo()
        {
            if (tiro == null)
            {
                tiro = content.Load<SoundEffect>("som\\tiro2");
            }
            tiro.Play(0.1f, 1, 0f);
        }

        public static void playExplosao()
        {
            if (explosao == null)
            {
                explosao = content.Load<SoundEffect>("som\\somExplosao");
                
            }
            explosao.Play(0.1f, 1, 0f);
        }

        public static void playRocket()
        {
            if (rocket == null)
            {
                rocket = content.Load<SoundEffect>("som\\rocket");

            }
            rocket.Play(0.1f, 1, 0f);
        }

        public static void playMusicaMenu()
        {
            if (musicaMenu == null)
            {
                musicaMenu = content.Load<SoundEffect>("som\\bridge0");

            }
            if (musicaMenuInstance == null)
            {
                musicaMenuInstance = musicaMenu.CreateInstance();
            }
            musicaMenuInstance.Play();
        }

        public static void playMusicaBridge0()
        {
            if (musicaMenuInstance != null) musicaMenuInstance.Stop();
            if (musicaNivel1Instance != null) musicaNivel1Instance.Stop();
            if (musicaNivel2Instance != null) musicaNivel2Instance.Stop();
            if (musicaBridge0 == null)
            {
                musicaBridge0 = content.Load<SoundEffect>("som\\menu");

            }
            if (musicaBridge0Instance == null)
            {
                musicaBridge0Instance = musicaBridge0.CreateInstance();
            }
            musicaBridge0Instance.Play();
        }

        public static void playMusicaNivel1()
        {
            if (musicaBridge0Instance != null) musicaBridge0Instance.Stop();
            if (musicaNivel1Instance != null) musicaNivel1Instance.Stop();
            if (musicaNivel2Instance != null) musicaNivel2Instance.Stop();

            if (musicaNivel1 == null)
            {
                musicaNivel1 = content.Load<SoundEffect>("som\\nivel1");

            }
            
            musicaNivel1Instance = musicaNivel1.CreateInstance();

            musicaNivel1Instance.Volume = 0.5f;
            musicaNivel1Instance.Play();
        }

        public static void playMusicaNivel2()
        {
            if (musicaNivel1Instance != null) musicaNivel1Instance.Stop();
            if (musicaNivel2Instance != null) musicaNivel2Instance.Stop();

            if (musicaNivel2 == null)
            {
                musicaNivel2 = content.Load<SoundEffect>("som\\nivel2");

            }
            
            musicaNivel2Instance = musicaNivel2.CreateInstance();
            
            musicaNivel2Instance.Volume = 0.5f;
            musicaNivel2Instance.Play();
        }

        public static void playMusicaNivel3()
        {
            if (musicaNivel2Instance != null) musicaNivel2Instance.Stop();
            if (musicaNivel3Instance != null) musicaNivel3Instance.Stop();

            if (musicaNivel3 == null)
            {
                musicaNivel3 = content.Load<SoundEffect>("som\\nivel3");

            }

            musicaNivel3Instance = musicaNivel3.CreateInstance();

            musicaNivel3Instance.Volume = 0.5f;
            musicaNivel3Instance.Play();
        }

    }
}
