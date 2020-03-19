using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace AWorldDestroyed.Models
{
    /// <summary>
    /// Button class to make buttons ;)
    /// 
    /// *Credit goes to Oyyou*
    /// </summary>
    public class Button
    {
        #region Fields
        private MouseState _currentMouse;
        private SpriteFont _font;
        private bool _isHoovering;
        private MouseState _previousMouse;
        private Texture2D _texture;
        #endregion

        #region Properties
        public event EventHandler Click;
        public bool Clicked { get; private set; }
        public Color PenColor { get; set; }
        public Vector2 Position { get; set; }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }
        public string Text { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Contruct
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="font">A font for the sprite</param>
        public Button(Texture2D texture, SpriteFont font)
        {
            _texture = texture;
            _font = font;
            PenColor = Color.Black;
        }

        /// <summary>
        /// Draw method to create the button.
        /// With mouse hoovering function.
        /// With string on it.(Only if the string is not empty)
        /// </summary>
        /// <param name="gameTime">Snapshot of the game timing state expressed in values that can be used by variable-step (real time) or fixed-step (game time) games.</param>
        /// <param name="spriteBatch">Helper class for drawing text strings and sprites in one or more optimized batches.</param>
        public void DrawButton(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Color color = Color.White;
            if (_isHoovering) color = Color.Brown;
            spriteBatch.Draw(_texture, Rectangle, color);
            if (!string.IsNullOrEmpty(Text))
            {
                float x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(Text).X / 2);
                float y = (Rectangle.Y + (Rectangle.Height / 2)) - (_font.MeasureString(Text).Y / 2);
                spriteBatch.DrawString(_font, Text, new Vector2(x, y), PenColor);
            }
        }

        /// <summary>
        /// Gets the current state of the mouse.
        /// Checks if the mouse is on or clicked
        /// </summary>
        /// <param name="gameTime">Snapshot of the game timing state expressed in values that can be used by variable-step (real time) or fixed-step (game time) games.</param>
        public void UpdateMouseState(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();
            Rectangle mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);
            _isHoovering = false;
            if (mouseRectangle.Intersects(Rectangle))
            {
                _isHoovering = true;
                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }
        #endregion
    }
}
