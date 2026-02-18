using System;
using MGUI.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGUI.Controls
{
    public class Control
    {
        public string Name{get;set;} = "Control";
        public int ZOrder{get;set;} = 0;
        public object UserData{get;set;} = null;
        public Point Position
        {
            get => _position;
            set
            {
                _position = value;
                HandleDirty();
            }
        }
        private Point _position;
        public Point Size
        {
            get => _size;
            set
            {
                _size = value;
                HandleDirty();
            }
        }
        private Point _size;
        public Rectangle Bounds{get; private set;}
        public Point Center => Bounds.Center;
        public Color BackgroundColor{get;set;} = Theme.Background;
        public bool Visible
        {
            get => _visible;
            set
            {
                if(_visible != value)
                {
                    _visible = value;
                    Logger.Log(this, "Visibility Changed");
                }
            }
        }
        public bool Enabled
        {
            get => _enabled;
            set
            {
                if(_enabled != value)
                {
                    _enabled = value;
                    Logger.Log(this, "Visibility Changed");
                }
            }
        }
        private bool _visible = true;
        private bool _enabled = true;
        public int BorderThickness{get;set;} = 0;
        public Color BorderColor{get;set;} = Theme.Border;



        private void HandleDirty()
        {
            Bounds = new Rectangle(Position.X, Position.Y, Size.X, Size.Y);
            AfterDirty();
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(AssetLoader.Pixel, Bounds, BackgroundColor);

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
        }
        public virtual Control HitTest(Point p)//---did this control have the mouse???
        {
            if (!Visible || !Enabled)
                return null;

            return Bounds.Contains(p) ? this : null;
        }



        protected virtual void AfterDirty(){}
        public virtual void OnMouseDown(MouseEvent e) { }
        public virtual void OnMouseUp(MouseEvent e) { }
        public virtual void OnMouseMove(MouseEvent e) { }
        public virtual void OnMouseEnter(MouseEvent e) { }
        public virtual void OnMouseHover(MouseEvent e) { }
        public virtual void OnMouseLeave(MouseEvent e) { }
        public virtual void OnMouseScroll(MouseEvent e) { }




        public override string ToString()
        {
            return $"Type: {GetType().Name} | Name: {Name}";
        }
    }
}