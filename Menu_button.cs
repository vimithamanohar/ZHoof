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
    class Menu_button
    {
        public Vector2 position;
        public Texture2D tex;
        public Texture2D hover;
        public Texture2D click; 
        private bool clicking; //Are we clicking?
        private bool hovering; //Are we hovering?
        private string button_function;

        public string Button_Function
        {
            set
            {
                button_function = value;
            }
            get
            {
                return button_function;
            }
        }
      

        public bool Clicking
        {
            set
            {
                clicking = value;
            }
            get
            {
                return clicking;
            }
        }

        public bool Hovering
        {
            set
            {
                hovering = value;
            }
            get
            {
                return hovering;
            }
        }



        public Menu_button(Vector2 position, Texture2D tex,Texture2D tex_hover,Texture2D tex_click,string button_name) //Our constructor
        {
                this.position = position; //Position in 2D
                this.tex = tex; //Our texture to draw
                hover = tex_hover;
                click = tex_click;
                clicking = false;
                hovering = false;
                if (button_name.Equals("Button0"))
                    button_function = "Start";
                else if (button_name.Equals("Button1"))
                    button_function = "Help";
                else
                    if (button_name.Equals("Button3"))
                        button_function = "Next";
                    else
                        if (button_name.Equals("Button4"))
                            button_function = "NextLevel";
                        else
                            if (button_name.Equals("Button5"))
                                button_function = "Restart";
                            else
                                button_function="Exit";


        }
        public void Draw(SpriteBatch batch,Vector2 position,Texture2D tex) //Draw function, same as mousehandler one.
        {
            int tempx = Convert.ToInt32(position.X);
            int tempy = Convert.ToInt32(position.Y);

            if(button_function.Equals("Exit")||button_function.Equals("Start")||button_function.Equals("Help"))
                batch.Draw(tex, new Rectangle(tempx, tempy, 162, 168), Color.White);         
            else
                batch.Draw(tex, new Rectangle(tempx, tempy, 120, 45), Color.White);         
        }
    }

       
}
