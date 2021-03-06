﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDJ_ProjectoFinal.entidades;
using TDJ_ProjectoFinal.graficos;

namespace TDJ_ProjectoFinal
{
    /// <summary>
    /// Disponibiliza métodos para verificar colisões entre vários tipos de sprites
    /// </summary>
    public static class Colisoes
    {

        static private Sprite collided;
        static private Vector2 collisionPoint;

        /// <summary>
        /// Verifica a existência de colisões pixel a pixel
        /// </summary>
        /// <param name="cManager">Instância de ContentManager</param>
        /// <param name="scene">Instância de scene</param>
        /// <param name="sprite">Sprite a verificar</param>
        /// <param name="origemBala">Entidade que deu origem à bala</param>
        static public void Colision(ContentManager cManager, Scene scene, Sprite sprite, OrigemBala origemBala)
        {
            //Colisao com enimigos
            if (scene.Collides(sprite, out collided, out collisionPoint, scene.inimigos))
            {
                if (origemBala == OrigemBala.player)
                {

                    if (sprite is Missil)
                    {
                        Missil missil = (Missil)sprite;
                        missil.thrust.Destroy();
                        missil.Destroy();
                    }
                    
                    //cria explosao
                    scene.AddExplosao(new AnimatedSprite(cManager, "explosao", 9, 9, false, sprite.position, 0.2f));

                    if (collided is NPC)
                    {
                        NPC inimigo = (NPC)collided;
                        if (sprite is Missil)
                        {
                            scene.AddExplosao(new AnimatedSprite(cManager, "explosao", 9, 9, false, sprite.position, 0.9f));
                            inimigo.Destroy();
                        }
                        else
                        {
                            inimigo.Vida--;
                        }
                        
                    }

                    if (collided is Boss)
                    {
                        Boss boss = (Boss)collided;
                        if (sprite is Missil)
                        {
                            scene.AddExplosao(new AnimatedSprite(cManager, "explosao", 9, 9, false, sprite.position, 0.9f));
                            boss.Vida -= 10;
                        }
                        else
                        {
                            boss.Vida--;
                        }

                    }

                    sprite.Destroy();

                }

                if (collided is Missil)
                {
                    Missil missil = (Missil)collided;
                    scene.AddExplosao(new AnimatedSprite(cManager, "explosao", 9, 9, false, sprite.position, 0.2f));
                    
                    if(missil.thrust != null)
                        missil.thrust.Destroy();
                    missil.Destroy();
                }

            }


            if (scene.Collides(sprite, out collided, out collisionPoint, scene.sprites))
            {
                if (collided is Cenario && sprite is Bala)
                {
                    scene.AddExplosao(new AnimatedSprite(cManager, "explosao", 9, 9, false, sprite.position, 0.2f));
                }

                if (collided is Player)
                {
                    Player player = (Player)collided;
                    if (sprite is Missil)
                    {
                        Missil missil = (Missil)sprite;
                        missil.thrust.Destroy();
                        missil.Destroy();
                        player.Vida -= 2;
                        scene.AddExplosao(new AnimatedSprite(cManager, "explosao", 9, 9, false, sprite.position, 0.4f));
                        
                        
                    }
                    else
                    {
                        player.Vida--;
                        scene.AddExplosao(new AnimatedSprite(cManager, "explosao", 9, 9, false, sprite.position, 0.2f));
                    }

                }

                if(!(collided is Bala)) sprite.Destroy();
            }

        }

    }
}
