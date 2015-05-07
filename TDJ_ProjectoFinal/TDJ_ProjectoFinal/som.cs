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
        private static Dictionary<string, SoundEffect> soundEffects;
        private static Dictionary<string, SoundEffectInstance> loopedSounds;
        private static Dictionary<string, Song> songs;

        public static void Initialize(ContentManager contentManager)
        {
            content = contentManager;
            soundEffects = new Dictionary<string, SoundEffect>();
            loopedSounds = new Dictionary<string, SoundEffectInstance>();
            songs = new Dictionary<string, Song>();
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

        public static void AddSongs(string songName, Song song)
        {
            if (songs.ContainsKey(songName))
            {
                songs[songName].Dispose();
                songs[songName] = song;
            }
            else
            {
                songs.Add(songName, song);
            }
        }

        public static void PlaySong(Song song)
        {
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;
        }

        public static void AddSoundEffect(string effectName, SoundEffect soundEffect)
        {
            if (soundEffects.ContainsKey(effectName))
            {
                soundEffects[effectName].Dispose();
                soundEffects[effectName] = soundEffect;
            }
            else
            {
                soundEffects.Add(effectName, soundEffect);
            }
        }

        public static void PlayLoopedSoundEffect(string effectName, string instanceName)
        {
            if (soundEffects.ContainsKey(effectName) && !loopedSounds.ContainsKey(instanceName))
            {
                SoundEffectInstance instance = soundEffects[effectName].CreateInstance();

                instance.IsLooped = true;
                loopedSounds.Add(instanceName, instance);
                instance.Play();
            }
            else if (soundEffects.ContainsKey(effectName) && loopedSounds.ContainsKey(instanceName))
            {
                if (loopedSounds[instanceName].State == SoundState.Playing)
                {
                    loopedSounds[instanceName].Stop();
                }

                loopedSounds[instanceName].Dispose();

                loopedSounds[instanceName] = soundEffects[effectName].CreateInstance();
                loopedSounds[instanceName].IsLooped = true;
                loopedSounds[instanceName].Play();
            }
        }
    }
}
