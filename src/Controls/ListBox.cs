using System;
using System.Linq;
using MGUI.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGUI.Controls
{
    public class ListBox : Control
    {
        public ControlCollection<ListBoxItem> Items{get; private set;} = new ControlCollection<ListBoxItem>();
        private Layout _layout = new RowLayout();
        private int _padding = 0;


        public ListBoxItem SelectedItem{get; private set;}
        public Action<ListBoxItem> OnItemSelected;


        public ListBox() : base()
        {
            BackgroundColor = Theme.Background;
            Items.OnControlsChanged += AfterDirty;
        }
        public void Add(string text)
        {
            Items.Add(new ListBoxItem(ListItemClick)
            {
                Text = text
            });
        }
        public void Add(string text, object userdata)
        {
            Items.Add(new ListBoxItem(ListItemClick)
            {
                Text = text,
                UserData = userdata
            });
        }
        public void Remove(ListBoxItem item)
        {
            Items.Remove(item);
        }   
        private void ListItemClick(ListBoxItem item)
        {
            SelectedItem = item;
            OnItemSelected?.Invoke(SelectedItem);
        }             
        protected override void AfterDirty()
        {
            _layout.HandleLayout(Bounds, Items.Controls.ToList<Control>(), _padding);   
        }
        public override Control HitTest(Point p)
        {
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
            ScissorStack.Push(Bounds);

            base.Draw(spritebatch);

            foreach (var control in Items.Controls.OrderByDescending(c => c.ZOrder))
            {
                control.Draw(spritebatch);
            }

            ScissorStack.Pop();
        }


        public override void OnMouseScroll(MouseEvent e)
        {
            Logger.Log(this, e.ScrollDelta);
        }

    
    
    
    
    }


    public class ListBoxItem : Control
    {
        public SpriteFont Font{get;set;} = AssetLoader.DefaultFont;
        public string Text{get;set;} = "Text";
        public Color FontColor{get;set;} = Theme.TextPrimary;
        private Vector2 _textpos = Vector2.Zero;
        public Color NormalColor
        {
            get => _normalColor;
            set
            {
                _normalColor = value;
                BackgroundColor = _normalColor;
            }
        }
        private Color _normalColor;
        public Color HoverColor{get;set;} = Theme.ButtonHover;

        private Action<ListBoxItem> Callback;

        public ListBoxItem(Action<ListBoxItem> _callback) : base()
        {
            Callback = _callback;
            BorderThickness = 0;
            NormalColor = Color.Transparent;
            Size = new Point(150, 30);
        }
        protected override void AfterDirty()
        {
            Vector2 _halfText = Font.MeasureString(Text) * 0.5f;
            _textpos = Bounds.Center.ToVector2() - _halfText;
        }
        public override void Draw(SpriteBatch spritebatch)
        {
            base.Draw(spritebatch);

            spritebatch.DrawString(Font, Text, _textpos, FontColor);
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
}