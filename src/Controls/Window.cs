using System;
using System.Collections.Generic;
using MGUI.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGUI.Util
{
    public class Window : ContainerControl
    {
        public int HeaderHeight{get;set;} = 30;
        public string Text{get;set;} = "Window";
        public SpriteFont Font{get;set;} = AssetLoader.DefaultFont;
        public Color HeaderColor{get;set;} = Theme.HeaderBackground;
        public Color HeaderTextColor{get;set;} = Theme.TextPrimary;

        private Rectangle _headerRect = new Rectangle();
        private Rectangle _bodyRect = new Rectangle();
        private Vector2 _textPos = Vector2.Zero;

        private bool _isDragging = false;
        private Point _offset = Point.Zero;



        //---resize stuff
        public int GrabHandleSize{get;set;} = 30;
        private Rectangle _resizeHandle = new Rectangle();
        private bool _isResizing = false;
        private Point _resizeOffset = Point.Zero;
        private Point _resizeStartMouse;
        private Point _resizeStartSize;
        public Color GrabHandleColor{get;set;} = Color.White * 0.35f;



        public Window(string name) : base()
        {
            Name = name;
            Text = name;
        }
        protected override void AfterDirty()
        {
            _resizeHandle = new Rectangle(
                Bounds.Right - GrabHandleSize,
                Bounds.Bottom - GrabHandleSize,
                GrabHandleSize,
                GrabHandleSize                
            );

            _headerRect = new Rectangle(Position.X, Position.Y, Size.X, HeaderHeight);
            _bodyRect = new Rectangle(Position.X, Position.Y + HeaderHeight, Size.X, Size.Y - HeaderHeight);

            Vector2 _halfText = Font.MeasureString(Text) / 2f;
            _textPos = _headerRect.Center.ToVector2() - _halfText;

            Layout.HandleLayout(_bodyRect, Controls, Padding);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(AssetLoader.Pixel, _headerRect, HeaderColor);
            spriteBatch.Draw(AssetLoader.Pixel, _bodyRect, BackgroundColor);
            spriteBatch.DrawString(Font, Text, _textPos, HeaderTextColor);

            if (BorderThickness > 0)
            {
                // Top
                spriteBatch.Draw(AssetLoader.Pixel,new Rectangle(Bounds.X, Bounds.Y, Bounds.Width, BorderThickness), BorderColor);
                // Bottom
                spriteBatch.Draw(AssetLoader.Pixel, new Rectangle(Bounds.X, Bounds.Y + Bounds.Height - BorderThickness, Bounds.Width, BorderThickness), BorderColor);
                // Left
                spriteBatch.Draw(AssetLoader.Pixel, new Rectangle(Bounds.X, Bounds.Y, BorderThickness, Bounds.Height), BorderColor);
                // Right
                spriteBatch.Draw(AssetLoader.Pixel, new Rectangle(Bounds.X + Bounds.Width - BorderThickness, Bounds.Y, BorderThickness, Bounds.Height), BorderColor);
            }  

            for (int i = 0; i < Controls.Count; i++)
            {
                Controls[i].Draw(spriteBatch);
            }

            DebugDrawGrabHandles(spriteBatch);
        }
        public void DebugDrawGrabHandles(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(AssetLoader.Pixel, _resizeHandle, GrabHandleColor);
        }


        public override void OnMouseDown(MouseEvent e)
        {
            if (_headerRect.Contains(InputManager.MousePosition))
            {
                _isDragging = true;
                _offset = InputManager.MousePosition - Position;
            }
            else if (_resizeHandle.Contains(InputManager.MousePosition))
            {
                _isResizing = true;
                _resizeStartMouse = InputManager.MousePosition;
                _resizeStartSize = Size;
            }
        }
        public override void OnMouseHover(MouseEvent e)
        {
            if(_isResizing)
            {
                Point delta = InputManager.MousePosition - _resizeStartMouse;
                Size = new Point(
                    Math.Max(100, _resizeStartSize.X + delta.X),  // minimum width = 100
                    Math.Max(50, _resizeStartSize.Y + delta.Y)    // minimum height = 50
                );

                // AfterDirty();                
            }
            if (_isDragging)
            {
                Position = InputManager.MousePosition - _offset;
                UIManager.DockManager?.HandleDock(this, false);
            }
        }
        public override void OnMouseUp(MouseEvent e)
        {
            _isDragging = false;
            _isResizing = false;
            UIManager.DockManager?.HandleDock(this, true);
        }
    
    
    
    }
}