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
    class Mouse_Handler
    {
        private Vector2 pos;
        private Texture2D tex;
        ContentManager content;

        public Mouse_Handler(ContentManager content1)
        {

            this.pos.X = Mouse.GetState().X;
            this.pos.Y = Mouse.GetState().Y;
            content = content1;
            
        }

        public void LoadContent()
        {
           
         tex = content.Load<Texture2D>("images\\cursor");
            
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

       public bool ButtonClick(Menu_button b)
       {
           if (this.pos.X >= b.position.X // To the right of the left side
           && this.pos.X <= b.position.X + b.tex.Width //To the left of the right side
           && this.pos.Y >= b.position.Y //Below the top side
           && this.pos.Y <= b.position.Y + b.tex.Height) //Above the bottom side
               return true; //We are; return true.
           else
               return false; //We're not; return false.
       }
     


    }
}


