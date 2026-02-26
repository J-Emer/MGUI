using System;
using MGUI.Controls;
using MGUI.Util;
using MGUI.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MGUI;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    public static SpriteFont Font;


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        Window.AllowUserResizing = true;
        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.PreferredBackBufferHeight = 720;
        _graphics.ApplyChanges();

        // TODO: use this.Content to load your game content here

        AssetLoader.Init(_graphics.GraphicsDevice, Content.Load<SpriteFont>("font"));
    
        new UIManager(_graphics.GraphicsDevice, this.Window);

        Window _test = new Window("Test")
        {
            Position = new Point(100, 100)
        };
        UIManager.Instance.Add(_test);



        _test.Children.Add(new Label
        {
            Text = "abcdefghijklmnopqrstuvwxyz"
        });



    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        UIManager.Instance.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        UIManager.Instance.Draw();

        base.Draw(gameTime);
    }
}
