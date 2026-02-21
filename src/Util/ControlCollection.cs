using System;
using System.Collections.Generic;
using System.Linq;
using MGUI.Controls;

namespace MGUI.Util
{
    public class ControlCollection
    {
        public List<Control> Controls{get; private set;} = new List<Control>();
        public Action OnControlsChanged;

        public void Add(Control control)
        {
            Controls.Add(control);
            OnControlsChanged?.Invoke();
        }
        public void Remove(Control control)
        {
            Controls.Remove(control);
            OnControlsChanged?.Invoke();
        }
        public Control Find(string name)
        {
            return Controls.FirstOrDefault(x => x.Name == name);
        } 
        public T Find<T>(string name) where T : Control
        {
            return (T)Controls.FirstOrDefault(x => x.Name == name);
        }                         
    }



    public class ControlCollection<T> where T : Control
    {
        public List<T> Controls{get; private set;} = new List<T>();
        public Action OnControlsChanged;

        public void Add(T control)
        {
            Controls.Add(control);
            OnControlsChanged?.Invoke();
        }
        public void Remove(T control)
        {
            Controls.Remove(control);
            OnControlsChanged?.Invoke();
        }
        public T Find(string name)
        {
            return Controls.FirstOrDefault(x => x.Name == name);
        } 
                      
    }
 
}