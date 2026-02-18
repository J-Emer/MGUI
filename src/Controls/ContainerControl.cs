using System.Collections.Generic;
using System.Linq;
using MGUI.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGUI.Controls
{
    public class ContainerControl : Control
    {
        public UIManager UIManager;
        protected List<Control> Controls = new List<Control>();
        public Layout Layout{get;set;} = new RowLayout();
        public int Padding{get;set;} = 5;
        public DockStyle Dock{get;set;} = DockStyle.None;
        public Vector2 MinSize { get; set; } = new Vector2(150, 100);



        public void Add(Control _control)
        {
            Controls.Add(_control);
            AfterDirty();
        }
        public void Remove(Control _control)
        {
            Controls.Remove(_control);
            AfterDirty();            
        }
        public Control Find(string name)
        {
            return Controls.FirstOrDefault(x => x.Name == name);
        }
        public T Find<T>(string name) where T : Control
        {
            return (T)Controls.FirstOrDefault(x => x.Name == name);
        }        
        protected override void AfterDirty()
        {
            Layout.HandleLayout(Bounds, Controls, Padding);
        }
        public override Control HitTest(Point p)
        {
            //---need to check the child controls first
            for (int i = 0; i < Controls.Count; i++)
            {
                var hit = Controls[i].HitTest(p);
                if(hit != null)
                {
                    return Controls[i];
                }
            }

            return Bounds.Contains(p) ? this : null;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            for (int i = 0; i < Controls.Count; i++)
            {
                Controls[i].Draw(spriteBatch);
            }
        }
    }
}