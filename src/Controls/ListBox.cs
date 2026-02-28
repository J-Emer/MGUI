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
        private Rectangle _scrollRect = new Rectangle();

        public ListBoxItem SelectedItem{get; private set;}
        public Action<ListBoxItem> OnItemSelected;


        private int _scrollOffset = 0;
        private int _maxScroll = 0;
        private int _scrollSpeed = 20;



        public ListBox() : base()
        {
            BorderThickness = 0;
            BackgroundColor = Theme.Background;
            Items.OnControlsChanged += AfterDirty;

            Size = new Point(200, 200);
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
            int visibleHeight = Bounds.Height;
            int contentHeight = GetChildrenHeight();

            _maxScroll = Math.Max(0, contentHeight - visibleHeight);

            _scrollOffset = Math.Clamp(_scrollOffset, 0, _maxScroll);

            int width = Bounds.Width - (_padding + _padding);

            _scrollRect = new Rectangle(
                Position.X,
                Position.Y - _scrollOffset, 
                width,
                contentHeight
            );

            _layout.HandleLayout(_scrollRect, Items.Controls.ToList<Control>(), _padding);

            foreach (var item in Items.Controls)
            {
                if(Bounds.Contains(item.Bounds))
                {
                    item.IsActive = true;
                }
                else
                {
                    item.IsActive = false;
                }
            }
        }
        public override Control HitTest(Point p)
        {
            if(InputManager.ScrollDelta != 0) //if were scrolling then hit check the ListBox first
            {
                var hitBounds = Bounds.Contains(p);

                if(hitBounds != false)
                {
                    return this;
                }
            }

            for (int i = 0; i < Items.Controls.Count; i++) // if not then just hit test normally
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
        private int GetChildrenHeight()
        {
            int height = 0;

            foreach (var item in Items.Controls)
            {
                height += item.Size.Y + _padding;
            }

            return height;
        }
        public override void OnMouseScroll(MouseEvent e)
        {
            if (!IsActive)
                return;

            _scrollOffset -= e.ScrollDelta > 0 ? _scrollSpeed : -_scrollSpeed;

            _scrollOffset = Math.Clamp(_scrollOffset, 0, _maxScroll);

            AfterDirty();
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
            if(!IsActive){return;}
            BackgroundColor = HoverColor;
        }        
        public override void OnMouseExit(MouseEvent e)
        {
            if(!IsActive){return;}
            BackgroundColor = NormalColor;
        }
        public override void OnMouseDown(MouseEvent e)
        {
            if(!IsActive){return;}
            Callback?.Invoke(this);
        }        
    }
}