using System;
using MGUI.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGUI.Controls
{
    public class Window : ContainerControl
    {
        public int HeaderHeight{get;set;} = 30;
        public SpriteFont Font{get;set;} = AssetLoader.DefaultFont;
        public string HeaderText{get;set;}
        private Rectangle _headerRect = new Rectangle();
        private Rectangle _bodyRect = new Rectangle();
        public Color HeaderColor{get;set;} = Theme.HeaderBackground;
        public Color FontColor{get;set;} = Theme.TextPrimary;
        private Vector2 _textPos = Vector2.Zero;
        private bool _isDragging = false;
        private bool _isResizing = false;
        private Point _offset = Point.Zero;
        private Rectangle _grabRect = new Rectangle();
        public int GrabberSize{get;set;} = 30;
        public Color GrabberColor{get;set;} = Color.White * 0.5f;
        public Point MinSize{get;set;} = new Point(400, 300);
        public DockStyle Dock
        {
            get => dock;
            set
            {
                if(dock != value)
                {
                    dock = value;
                    UIManager.Instance.ChildDockChanged();
                }
            }

        }
        private DockStyle dock = DockStyle.None;
        private Button _closeButton;



        public Window(string name) : base()
        {
            _closeButton = new Button
            {
                Text = "X",
                BorderThickness = 0,
                Size = new Point(HeaderHeight, HeaderHeight),
                NormalColor = Color.Transparent,
                HoverColor = Color.Red,
                FontColor = Color.White,
                OnClick = CloseBtnClicked
            };

            Name = name;
            HeaderText = name;
            BackgroundColor = Theme.Background;
            BorderColor = Theme.BorderLight;
            Size = new Point(300, 400);

            Logger.Log(this, "//todo: Add a MinSize check");
        }

        private void CloseBtnClicked(Button button, MouseEvent @event)
        {
            UIManager.Instance.Remove(this);
        }
        public override Control HitTest(Point p)
        {
            if(_closeButton.Bounds.Contains(p))
            {
                return _closeButton;
            }

            return base.HitTest(p);
        }
        protected override void AfterDirty()
        {           
            _headerRect = new Rectangle(Position.X, Position.Y, Size.X, HeaderHeight);
            _bodyRect = new Rectangle(
                Position.X,
                Position.Y + HeaderHeight,
                Size.X,
                Size.Y - HeaderHeight
            );

            Vector2 _halfText = Font.MeasureString(HeaderText) * 0.5f;
            _textPos = _headerRect.Center.ToVector2() - _halfText;

            _grabRect = new Rectangle(
                Bounds.Right - GrabberSize,
                Bounds.Bottom - GrabberSize,
                GrabberSize,
                GrabberSize
            );

            _closeButton.Position = new Point(Bounds.Right - HeaderHeight, Position.Y);

            Layout.HandleLayout(_bodyRect, Children.Controls, Padding);
        }
        public override void Draw(SpriteBatch spritebatch)
        {           
            base.Draw(spritebatch);
            spritebatch.Draw(AssetLoader.Pixel, _headerRect, HeaderColor);
            spritebatch.DrawString(Font, HeaderText, _textPos, FontColor);
            _closeButton.Draw(spritebatch);
            if(Dock == DockStyle.None)
            {
                spritebatch.Draw(AssetLoader.Pixel, _grabRect, GrabberColor);
            } 
        }




        public override void OnMouseDown(MouseEvent e)
        {
            if(_headerRect.Contains(e.Position))
            {
                _isDragging = true;
                _offset = e.Position - Position;
                Size = MinSize;
                dock = DockStyle.None;
            }

            if(_grabRect.Contains(e.Position) && Dock == DockStyle.None)
            {
                _isResizing = true;
            }
        }
        public override void OnMouseDrag(MouseEvent e)
        {
            if(_isDragging)
            {
                Position = e.Position - _offset;
                UIManager.Instance.ShowDropTargets();
            }
            if(_isResizing)
            {
                Size = e.Position - Position;
            }
        }
        public override void OnMouseUp(MouseEvent e)
        {
            if(_isDragging)
            {
                UIManager.Instance.CheckDock(this);
                UIManager.Instance.HideDropTargets();
            }
            _isDragging = false;
            _isResizing = false;
        }
    
    
    }
}