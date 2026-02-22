using System;
using MGUI.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGUI.Controls
{
    public class DropDown : Control
    {
        public SpriteFont Font{get;set;} = AssetLoader.DefaultFont;
        public string Text{get;set;} = "";
        public Color FontColor{get;set;} = Color.White;
        private Vector2 _textPos = Vector2.Zero;
        public Color HoverColor{get;set;} = Theme.ButtonHover;
        public Color NormalColor
        {
            get => _normalColor;
            set
            {
                _normalColor = value;
                BackgroundColor = _normalColor;
            }
        }
        private Color _normalColor = Color.Black;
        private Panel _panel;
        private bool _showPanel = false;
        public Action<string> OnSelected;
        public string Selected{get; private set;}


        public DropDown() : base()
        {
            _panel = new Panel
            {
                BackgroundColor = Theme.HeaderBackground,
                IsActive = false,
                Layout = new RowLayout(),
                Padding = 0,
                Size = new Point(300, 300),
                BorderThickness = 0,
            };

            BackgroundColor = _normalColor;
            Size = new Point(200, 30);
        }
        protected override void AfterDirty()
        {
            Vector2 _halfText = Font.MeasureString(Text) * 0.5f;
            _textPos = Bounds.Center.ToVector2() - _halfText;

            int _controlHeight = 0;

            foreach (var item in _panel.Children.Controls)
            {
                _controlHeight += item.Size.Y;
            }

            _panel.Position = new Point(Position.X, Bounds.Bottom);
            _panel.Size = new Point(Size.X, _controlHeight);
        }
        public override Control HitTest(Point p)
        {
            var hit = _panel.HitTest(p);

            if(hit != null)
            {
                return hit;
            }

            return base.HitTest(p);
        }
        public override void Draw(SpriteBatch spritebatch)
        {
            base.Draw(spritebatch);

            spritebatch.DrawString(Font, Text, _textPos, FontColor);
        }
        public override void OnMouseEnter(MouseEvent e)
        {
            BackgroundColor = HoverColor;
        }        
        public override void OnMouseExit(MouseEvent e)
        {
            BackgroundColor = NormalColor;
        }
        public override void OnMouseDown(MouseEvent e)
        {
            _showPanel = !_showPanel;
            _panel.IsActive = _showPanel;

            if(_showPanel)
            {
                UIManager.Instance.ShowOverlay(_panel);
            }
            else
            {
                UIManager.Instance.RemoveOverlay(_panel);
            }
        }        
    
    
    
        public void Add(string text)
        {
            if(_panel.Children.Controls.Count == 0)
            {
                Text = text;
            }

            _panel.Children.Add(new Button
            {
                Text = text,
                NormalColor = Color.Transparent,
                BorderThickness = 0,
                OnClick = ItemSelected
            });

            AfterDirty();
        }

        private void ItemSelected(Button button, MouseEvent @event)
        {
            Selected = button.Text;
            OnSelected?.Invoke(Selected);
        }
    }
}