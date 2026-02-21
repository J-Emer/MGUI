using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public static class ScissorStack
{
    private static Stack<Rectangle> _stack = new Stack<Rectangle>();
    private static GraphicsDevice _graphicsDevice;

    public static void Initialize(GraphicsDevice graphicsDevice)
    {
        _graphicsDevice = graphicsDevice;
    }

    public static void Push(Rectangle newRect)
    {
        if (_graphicsDevice == null)
            return;

        if (_stack.Count == 0)
        {
            _stack.Push(newRect);
            _graphicsDevice.ScissorRectangle = newRect;
        }
        else
        {
            Rectangle current = _stack.Peek();
            Rectangle intersected = Rectangle.Intersect(current, newRect);

            _stack.Push(intersected);
            _graphicsDevice.ScissorRectangle = intersected;
        }
    }

    public static void Pop()
    {
        if (_graphicsDevice == null || _stack.Count == 0)
            return;

        _stack.Pop();

        if (_stack.Count > 0)
        {
            _graphicsDevice.ScissorRectangle = _stack.Peek();
        }
        else
        {
            // Restore full screen
            _graphicsDevice.ScissorRectangle = _graphicsDevice.Viewport.Bounds;
        }
    }
}