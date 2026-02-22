using System;
using MGUI.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGUI.Controls
{
    public abstract class Control
    {
        public string Name{get;set;} = "Control";
        public object UserData{get;set;} = null;
        public int ZOrder{get;set;} = 0;
        public bool IsActive
        {
            get => _isactive;
            set
            {
                if(_isactive != value)
                {
                    _isactive = value;
                    OnActiveChanged?.Invoke();
                }
            }
        }
        private bool _isactive = true;
        public Point Position
        {
            get => _position;
            set
            {
                if(_position != value)
                {
                    _position = value;
                    HandleDirty();
                }
            }
        }
        public Point Size
        {
            get => _size;
            set
            {
                if(_size != value)
                {
                    _size = value;
                    HandleDirty();
                }
            }
        }
        public Rectangle Bounds => _bounds;
        protected Point _position;
        protected Point _size;
        private Rectangle _bounds;
        public Color BackgroundColor{get;set;} = Color.White;
        public int BorderThickness{get;set;} = 3;
        public Color BorderColor{get;set;} = Color.Black;
    

        public Action OnActiveChanged;



        private void HandleDirty()
        {
            _bounds = new Rectangle(
                _position.X,
                _position.Y,
                _size.X,
                _size.Y
            );

            AfterDirty();
        }
        public virtual Control HitTest(Point p)
        {
            return _bounds.Contains(p)? this : null;
        }
        public virtual void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(AssetLoader.Pixel, _bounds, BackgroundColor);

            if(BorderThickness > 0)
            {
                //top
                spritebatch.Draw(AssetLoader.Pixel, new Rectangle(Bounds.X, Bounds.Y, Size.X, BorderThickness), BorderColor);
                //left
                spritebatch.Draw(AssetLoader.Pixel, new Rectangle(Bounds.X, Bounds.Y, BorderThickness, Bounds.Height), BorderColor);
                //right
                spritebatch.Draw(AssetLoader.Pixel, new Rectangle(Bounds.Right, Bounds.Y, BorderThickness, Bounds.Height), BorderColor);
                //bottom
                spritebatch.Draw(AssetLoader.Pixel, new Rectangle(Bounds.X, Bounds.Bottom, Size.X + BorderThickness, BorderThickness), BorderColor);
            }
        }



        protected virtual void AfterDirty(){}
        public virtual void OnMouseEnter(MouseEvent e){}
        public virtual void OnMouseExit(MouseEvent e){}
        public virtual void OnMouseHover(MouseEvent e){}
        public virtual void OnMouseDown(MouseEvent e){}
        public virtual void OnMouseUp(MouseEvent e){}
        public virtual void OnMouseScroll(MouseEvent e){}
        public virtual void OnMouseDrag(MouseEvent e){}


        
    }
}