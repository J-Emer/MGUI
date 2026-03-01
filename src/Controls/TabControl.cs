using System;
using System.Collections.Generic;
using System.Linq;
using MGUI.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGUI.Controls
{
    public class TabControl : Control
    {
        private List<Button> _buttons = new List<Button>();
        private Rectangle _buttonsRect = new Rectangle();
        private Layout _buttonsLayout = new HorizontalLayout();


        private List<Panel> _panels = new List<Panel>();
        private Rectangle _panelsRect = new Rectangle();
        private Layout _panelsLayout = new StretchLayout();



        private int ButtonsHeight = 30;
        private int _selected = 0;



        public TabControl() : base()
        {
            BackgroundColor = Color.Transparent;
            BorderThickness = 0;
        }
        public Panel GetPanel(int index)
        {
            if(index > _panels.Count){return null;}
            return _panels[index];                                                                                                                                                                                                                                                                 return _panels[index];
        }
        public Panel Add(string text)
        {
            _buttons.Add(new Button
            {
                Text = text,
                OnClick = ButtonClicked,
            });

            Panel _panel = new Panel
            {
                Size = new Point(300, 300),
                Layout = new RowLayout(),
                BorderThickness = 0
            };

            _panels.Add(_panel);

            AfterDirty();

            return _panel;
        }
        private void ButtonClicked(Button button, MouseEvent @event)
        {
            _selected = _buttons.IndexOf(button);
            AfterDirty();
        }
        public override Control HitTest(Point p)
        {
            foreach (var item in _buttons)
            {
                var hit = item.HitTest(p);

                if(hit != null)
                {
                    return hit;
                }
            }

            foreach (var item in _panels)
            {
                var hit = item.HitTest(p);

                if(hit != null)
                {
                    return hit;
                }
            }   

            return base.HitTest(p);
        }
        protected override void AfterDirty()
        {
            _buttonsRect = new Rectangle(Position.X, Position.Y, Size.X, ButtonsHeight);
            
            _panelsRect = new Rectangle(Position.X, _buttonsRect.Bottom, Size.X, Size.Y - ButtonsHeight);

            _buttonsLayout.HandleLayout(_buttonsRect, _buttons.ToList<Control>(), 0);

            for (int i = 0; i < _panels.Count; i++)
            {
                if(i == _selected)
                {
                    _panels[i].IsActive = true;
                }
                else
                {
                    _panels[i].IsActive = false;
                }
            }

            if(_panels.Count > 0)
            {
                _panelsLayout.HandleLayout(_panelsRect, new List<Control>{_panels[_selected]}, 0);                
            }

        }
        public override void Draw(SpriteBatch spritebatch)
        {
            base.Draw(spritebatch);

            foreach (var item in _buttons)
            {
                item.Draw(spritebatch);
            }

            if(_panels.Count > 0)
            {
                _panels[_selected].Draw(spritebatch);                
            }
        }

    }
}