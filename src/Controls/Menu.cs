using System;
using System.Linq;
using MGUI.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGUI.Controls
{
    public class Menu : DockableControl
    {
        private ControlCollection<MenuItem> Items = new ControlCollection<MenuItem>();
        private Layout Layout = new ColumnLayout();
        private int Padding = 5;
        public Action<MenuItem> OnItemClicked;
        public Action<string> OnDropDownSelected;
        public MenuItem SelectedItem{get; private set;}

        public Menu() : base()
        {
            ZOrder = 5;
            Size = new Point(300, 30);
            Dock = DockStyle.Top;
            BorderThickness = 0;
            BackgroundColor = Color.Black; //Theme.WindowBackground;
            Items.OnControlsChanged += AfterDirty;
        }
        protected override void AfterDirty()
        {
            Layout.HandleLayout(Bounds, Items.Controls.ToList<Control>(), Padding);
        }
        public override Control HitTest(Point p)
        {
            if(!IsActive){return null;}

            for (int i = 0; i < Items.Controls.Count; i++)
            {
                var hit = Items.Controls[i].HitTest(p);

                if(hit != null)
                {
                    return hit;
                }
            }
            return Bounds.Contains(p)? this : null;
        }
        public override void Draw(SpriteBatch spritebatch)
        {
            base.Draw(spritebatch);

            foreach (var control in Items.Controls.OrderByDescending(c => c.ZOrder))
            {
                control.Draw(spritebatch);
            }            
        }
        private void MenuItemClicked(MenuItem item)
        {
            SelectedItem = item;
            OnItemClicked?.Invoke(SelectedItem);
        }
        private void DropDownItemSelected(MenuItem item)
        {
            //note: this method does nothing
        }
        public MenuItem Add(string text)
        {
            MenuItem _item = new MenuItem(text, MenuItemClicked);
            _item.Size = new Point(100, 30);
            Items.Add(_item);
            return _item;
        }
        public MenuItem Add(string text, object userdata)
        {
            MenuItem _item = new MenuItem(text, MenuItemClicked)
            {
                UserData = userdata,
                Size = new Point(100, 30)
            };

            Items.Add(_item);
            return _item;
        }
        public DropDownMenuItem AddDropDown(string text)
        {
            DropDownMenuItem _item = new DropDownMenuItem(text, DropDownItemSelected);
            _item.OnSelected += DropDownItemSelected;
            _item.Size = new Point(100, 30);
            Items.Add(_item);
            return _item;
        }
        private void DropDownItemSelected(string obj)
        {
            OnDropDownSelected?.Invoke(obj);
        }
        public void Remove(MenuItem item)
        {
            Items.Remove(item);
        }
        public MenuItem Find(string name)
        {
            return Items.Find(name);
        }
    
    }

    public class MenuItem : Control
    {
        public SpriteFont Font{get;set;} = AssetLoader.DefaultFont;
        public string Text{get;set;} = "Button";
        public Color FontColor{get;set;} = Color.White;
        protected Vector2 _textPos = Vector2.Zero;
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
        private Color _normalColor = Color.Transparent;
        private Action<MenuItem> Callback;

        public MenuItem(string text, Action<MenuItem> callback) : base()
        {
            Name = text;
            Text = text;
            BorderThickness = 0;
            Callback = callback;
            BackgroundColor = _normalColor;
        }

        protected override void AfterDirty()
        {
            Vector2 _halfText = Font.MeasureString(Text) * 0.5f;
            _textPos = Bounds.Center.ToVector2() - _halfText;
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
            Callback?.Invoke(this);
        }        
    }
    public class DropDownMenuItem : MenuItem
    {
        private Color _normalColor = Color.Black;
        private Panel _panel;
        private bool _showPanel = false;
        public Action<string> OnSelected;
        public string Selected{get; private set;}

        public DropDownMenuItem(string text, Action<MenuItem> callback) : base(text, callback)
        {
            _panel = new Panel
            {
                BackgroundColor = Theme.HeaderBackground,
                IsActive = false,
                Layout = new RowLayout(),
                Padding = 0,
                Size = new Point(300, 300),
                BorderColor = Theme.Border,
                BorderThickness = 1,
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
            if(!IsActive){return null;}

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
            HandlePanel();
        }        
        private void HandlePanel()
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
        public void Add(string text, object userdata)
        {
            if(_panel.Children.Controls.Count == 0)
            {
                Text = text;
            }

            _panel.Children.Add(new Button
            {
                Text = text,
                UserData = userdata,
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
            Text = Selected;
            HandlePanel();
        }
    }
}


