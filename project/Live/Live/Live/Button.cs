using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Live
{
    class Button
    {
        public Texture2D actualTextureToDisplay;
        private Texture2D defaultTexture;
        private Texture2D hoveringTexture;
        private Texture2D clickedTexture;
        public Vector2 position = new Vector2();
        private object parent = null;
        
        /// <summary>
        /// Event that fires when the text button is clicked with the mouse over it.
        /// </summary>
        public event EventHandler MouseClicked;

        /// <summary>
        /// Event that fires when the mouse is hovering over the text button.
        /// </summary>
        public event EventHandler MouseHover;


        /// <summary>  
        ///   
        /// </summary>  
        /// <param name="game">объект игры</param>  
        /// <param name="defaultTexture">путь к обычной текстуре</param>  
        /// <param name="hoveringTexture">путь к текстуре, когда мышь наведена на эту кнопку</param>  
        /// <param name="clickedTexture">путь к текстуре, когда мышь кликнута на кнопку</param>  
        /// <param name="position">расположение центра кнопки на экране</param>  
        public Button(Vector2 position, object parent)
        {
            this.parent = parent;
            this.position = position;
            MouseClicked += new EventHandler(MouseClickedEvent);
            MouseHover += new EventHandler(MouseHoverEvent);
        }

        public void Load(Texture2D defaults,Texture2D hover,Texture2D clicked)
        {            
            this.defaultTexture = defaults;
            this.hoveringTexture = hover;
            this.clickedTexture = clicked;
            this.actualTextureToDisplay = this.defaultTexture;
        }
           /// <summary>
        /// This determines if the Mouse is over the text.
        /// </summary>
        /// <returns>returns true if hovering, false if not.</returns>
        public virtual bool OnMouseHover()
        {
            if (
                 Mouse.GetState().X > (this.position.X - this.actualTextureToDisplay.Width / 2) &&
                 Mouse.GetState().X < (this.position.X + this.actualTextureToDisplay.Width / 2) &&
                 Mouse.GetState().Y > (this.position.Y - this.actualTextureToDisplay.Height / 2) &&
                 Mouse.GetState().Y < (this.position.Y + this.actualTextureToDisplay.Height / 2)
               )
            {
                MouseHover.Invoke(parent, EventArgs.Empty);
                return true;
            }
            return false;
        }

        /// <summary>
        /// This determines if the Mouse has been clicked over the text.
        /// </summary>
        /// <returns>returns true if clicked, false if not.</returns>
        public virtual bool OnMouseClicked()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (
                     Mouse.GetState().X > (this.position.X - this.actualTextureToDisplay.Width / 2) &&
                     Mouse.GetState().X < (this.position.X + this.actualTextureToDisplay.Width / 2) &&
                     Mouse.GetState().Y > (this.position.Y - this.actualTextureToDisplay.Height / 2) &&
                     Mouse.GetState().Y < (this.position.Y + this.actualTextureToDisplay.Height / 2)
                   )
                {
                    MouseClicked.Invoke(parent, EventArgs.Empty);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// this event fires everytime the mouse is hovered over the textbutton.
        /// </summary>
        /// <param name="sender">the object which sends this event.</param>
        /// <param name="e">the object arguments which sends this event.</param>
        protected virtual void MouseHoverEvent(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// this event fires everytime the mouse clicks over the textbutton.
        /// </summary>
        /// <param name="sender">the object which sends this event.</param>
        /// <param name="e">the object arguments which sends this event.</param>
        protected virtual void MouseClickedEvent(object sender, EventArgs e)
        {
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if(OnMouseHover())
            {
                actualTextureToDisplay = hoveringTexture;
            }
            if (OnMouseClicked())
            {
                actualTextureToDisplay = clickedTexture;
            }
            if (!OnMouseHover())
            {
                this.actualTextureToDisplay = this.defaultTexture;
            }
           
            
            if (this.actualTextureToDisplay != null)
            {
               spriteBatch.Draw(this.actualTextureToDisplay,
                                     new Vector2(this.position.X - this.actualTextureToDisplay.Width/2,
                                                 this.position.Y - this.actualTextureToDisplay.Height/2), Color.White);
            }
        }
    }
}
