using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Bing_Bong
{
    class GameObject
    {
        public float rotation;
        public Texture2D sprite; // to draw 
        public Vector2 position; //two dimensions
        public Vector2 center;
        public Vector2 velocity; // mofiy the position each frame
        public bool alive; // if an object is alive or dead 
        public bool bad;
        public bool killed;
        //public int[,] array; 
       

        //constructor of the game object 
        public GameObject(Texture2D loadedTexture)
        {
            sprite = loadedTexture;
            center = new Vector2(sprite.Width / 2, sprite.Height / 2);
            rotation = 0.0f;
            position = Vector2.Zero;
            velocity = Vector2.Zero;
            alive = false;
            bad = false;
            killed = false;
            //array = new int[0, 0]; 
           
        }





    }
}
