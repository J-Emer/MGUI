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
    

        new UIManager(_graphics.GraphicsDevice, this.Window);


        UIManager.Instance.Add(new Window("Test Window")
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
        UIManager.Instance.Add(_otherWindow);

        DropDown _dropDown = new DropDown();

        for (int i = 0; i < 5; i++)
        {
            _dropDown.Add(new Button
            {
                Name = $"Button:{i}",
                Text = $"Button:{i}",
                Size = new Point(150, 30),
                OnClick = ButtonClick
            });
        }

        _otherWindow.Add(_dropDown);




    }

    private void ButtonClick(Button button, MouseEvent @event)
    {
        Logger.Log(this, button.Text);
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
