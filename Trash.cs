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

namespace WindowsGame1
{
    class Trash
    {
        public Texture2D sprite;
        public Vector2 position;
        public Vector2 end;
        public bool caught;
        public Vector2 velocity;
        public float bottom_value;

        public Trash(Texture2D Trash_texture)
        {
            position = Vector2.Zero;
            sprite = Trash_texture;
            end = new Vector2(position.X + 40, position.Y + 40);
            bottom_value = position.Y + 40;
            velocity = Vector2.Zero;
            caught = false;
        }

    }
}
