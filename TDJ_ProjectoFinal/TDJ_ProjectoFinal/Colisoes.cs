using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDJ_ProjectoFinal.entidades;
using TDJ_ProjectoFinal.graficos;

namespace TDJ_ProjectoFinal
{
    public static class Colisoes
    {

        static private Sprite collided;
        static private Vector2 collisionPoint;

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

                    sprite.Destroy();

                }

                if (collided is Missil)
                {
                    Missil missil = (Missil)collided;
                    scene.AddExplosao(new AnimatedSprite(cManager, "explosao", 9, 9, false, sprite.position, 0.2f));
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
