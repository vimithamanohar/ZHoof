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

namespace Trash_pick
{
    class Cans
    {
        public Vector2 position;
        public Texture2D tex;
        public Rectangle can_rect;
        private bool select;
       

        public bool Selecting
        {
            set
            {
                select = value;
            }
            get
            {
                return select;
            }
        }

        public Cans(Vector2 position, Texture2D tex) //Our constructor
        {
            this.position = position; //Position in 2D
            this.tex = tex; //Our texture to draw
            select = false;
        }

        public void Draw(SpriteBatch batch, Vector2 position, Texture2D tex) //Draw function, same as mousehandler one.
        {
            int tempx = Convert.ToInt32(position.X);
            int tempy = Convert.ToInt32(position.Y);
            can_rect=new Rectangle(tempx, tempy, 22, 39);
              batch.Draw(tex,can_rect, Color.White);
        }


    }
}
