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
    class Mouse_handler
    {
        private Vector2 pos;
        private Texture2D tex;
        ContentManager content;

        public Mouse_handler(ContentManager content1)
        {

            this.pos.X = Mouse.GetState().X;
            this.pos.Y = Mouse.GetState().Y;
            content = content1;
            
        }

        public void LoadContent()
        {
           
         tex = content.Load<Texture2D>("images\\trash_cursor");
            
        }

       public void Update(GameTime gameTime)
        {
            
            UpdateMouse();
        }

       protected void UpdateMouse()
       {
           MouseState current_mouse = Mouse.GetState();

           // The mouse x and y positions are returned relative to the
           // upper-left corner of the game window.
           pos.X = current_mouse.X;
           pos.Y = current_mouse.Y;

           // Change background color based on mouse position.

       }

       public void Draw(SpriteBatch sp)
       {
           sp.Draw(tex, pos, Color.White);
       }

       public bool ButtonClick(Cans c)
       {
           if (this.pos.X >= c.position.X // To the right of the left side
           && this.pos.X <= c.position.X + 22 //To the left of the right side
           && this.pos.Y >= c.position.Y //Below the top side
           && this.pos.Y <= c.position.Y + 39) //Above the bottom side
               return true; //We are; return true.
           else
               return false; //We're not; return false.
       }

       public bool ButtonClick(Bottle  b)
       {
           if (this.pos.X >= b.position.X // To the right of the left side
           && this.pos.X <= b.position.X + 15 //To the left of the right side
           && this.pos.Y >= b.position.Y //Below the top side
           && this.pos.Y <= b.position.Y + 45) //Above the bottom side
               return true; //We are; return true.
           else
               return false; //We're not; return false.
       }

       public bool ButtonClick(Paper p)
       {
           if (this.pos.X >= p.position.X // To the right of the left side
           && this.pos.X <= p.position.X + 40 //To the left of the right side
           && this.pos.Y >= p.position.Y //Below the top side
           && this.pos.Y <= p.position.Y + 35) //Above the bottom side
               return true; //We are; return true.
           else
               return false; //We're not; return false.
       }

       public bool ButtonClick(Food f)
       {
           if (this.pos.X >= f.position.X // To the right of the left side
           && this.pos.X <= f.position.X + 40 //To the left of the right side
           && this.pos.Y >= f.position.Y //Below the top side
           && this.pos.Y <= f.position.Y + 35) //Above the bottom side
               return true; //We are; return true.
           else
               return false; //We're not; return false.
       }


    }
}
