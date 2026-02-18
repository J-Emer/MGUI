using System;
using MGUI.Controls;
using MGUI.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MGUI;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    public static SpriteFont Font;
    private UIManager _uiManager;

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
    

        _uiManager = new UIManager(_graphics.GraphicsDevice, this.Window);


        _uiManager.Add(new Window("Test Window")
        {
            Position = new Point(100, 100),
            Size = new Point(300, 400),
            BorderColor = Theme.BorderLight,
            BorderThickness = 1
        });

        Window _otherWindow = new Window("Other Window")
        {
            Position = new Point(500, 100),
            Size = new Point(300, 400),
            BorderColor = Theme.BorderLight,
            BorderThickness = 1            
        };

        _uiManager.Add(_otherWindow);

    }

    private void ButtonClick(Button button, MouseEvent @event)
    {
        Logger.Log(button.Name);
    }


    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        _uiManager.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        _uiManager.Draw();

        base.Draw(gameTime);
    }
}
