using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Live
{
    class TextButton
    {
        private Vector2     m_Position          = new Vector2();
        private string      m_Text              = null;
        private Color       m_TextColor         = Color.White;
        private Color       m_TextHoverColor    = Color.Yellow;
        private SpriteFont  m_Font              = null;
        private float       m_HoverScale        = 1.0f;
        private object      m_Parent            = null;
        

        /// <summary>
        /// Event that fires when the text button is clicked with the mouse over it.
        /// </summary>
        public event EventHandler MouseClicked;

        /// <summary>
        /// Event that fires when the mouse is hovering over the text button.
        /// </summary>
        public event EventHandler MouseHover;

        /// <summary>
        /// This is the  x, y  position of the text.
        /// </summary>
        public virtual Vector2 Position { get { return m_Position; } set { m_Position = value; } }

        /// <summary>
        /// This is the text color when the mouse is not over the text.
        /// </summary>
        public virtual Color TextColor { get { return m_TextColor; } set { m_TextColor = value; } }

        /// <summary>
        /// This is the text color when the mouse is over the text box.
        /// </summary>
        public virtual Color HoverColor { get { return m_TextHoverColor; } set { m_TextHoverColor = value; } }

        /// <summary>
        /// This is the font attached to the TextButton class.
        /// </summary>
        public virtual SpriteFont Font { get { return m_Font; } set { m_Font = value; } }

        /// <summary>
        /// This is the scale of the text when you hover over the text (default 1.0)
        /// </summary>
        public virtual float HoverScale { get { return m_HoverScale; } set { m_HoverScale = value; } }

        /// <summary>
        /// This is the object the textbutton is attached too.
        /// </summary>
        public object Parent { get { return m_Parent; } set { m_Parent = value; } }

        /// <summary>
        /// This determines if the Mouse is over the text.
        /// </summary>
        /// <returns>returns true if hovering, false if not.</returns>
        public virtual bool OnMouseHover()
        {
            if (
                 Mouse.GetState().X > m_Position.X &&
                 Mouse.GetState().X < m_Position.X + m_Font.MeasureString(m_Text).X &&
                 Mouse.GetState().Y > m_Position.Y &&
                 Mouse.GetState().Y < m_Position.Y + m_Font.MeasureString(m_Text).Y
               )
            {
                MouseHover.Invoke(m_Parent, EventArgs.Empty);
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
                     Mouse.GetState().X > m_Position.X &&
                     Mouse.GetState().X < m_Position.X + m_Font.MeasureString(m_Text).X &&
                     Mouse.GetState().Y > m_Position.Y &&
                     Mouse.GetState().Y < m_Position.Y + m_Font.MeasureString(m_Text).Y
                   )
                {
                    MouseClicked.Invoke(m_Parent, EventArgs.Empty);
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

        /// <summary>
        /// TextButton Constructor requires the text to display, and the font to be attached.
        /// </summary>
        /// <param name="text">the string in which you wish to display.</param>
        /// <param name="font">the font that the string will be presented in.</param>
        public TextButton(string text, SpriteFont font, object parent)
        {
            m_Text = text;
            m_Font = font;
            m_Parent = parent;
            MouseClicked += new EventHandler(MouseClickedEvent);
            MouseHover += new EventHandler(MouseHoverEvent);
        }

        /// <summary>
        /// The Draw method draws the TextButton to the screen.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw the text too</param>
        /// <param name="x">The X location of the text.</param>
        /// <param name="y">The Y location of the text.</param>
        public virtual void Draw(SpriteBatch spriteBatch, int x, int y)
        {
            m_Position.X = (float)x;
            m_Position.Y = (float)y;

            if (m_Font != null)
            {
                if (!OnMouseHover())
                {
                    spriteBatch.DrawString(m_Font, m_Text, m_Position, m_TextColor);
                }
                else
                {
                    spriteBatch.DrawString(m_Font, m_Text, m_Position, m_TextHoverColor, 0f, new Vector2(), m_HoverScale, SpriteEffects.None, 1f);
                }
            }
        }
    }
}