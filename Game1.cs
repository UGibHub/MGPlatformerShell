using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PlatformerShell;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    public static int ScreenWidth;
    public static int ScreenHeight;
    private List<Component> _components;
    private LevelBuilder _levelBuilder;
    private Player _player;
    private Camera _camera;
    

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    { // 1/4 = 480/270    1/2 = 960/540  Resolutions.

        /* 1/4 size
        _graphics.PreferredBackBufferWidth = 480;
        _graphics.PreferredBackBufferHeight = 270;
        */
        ///* 1/2 size
        _graphics.PreferredBackBufferWidth = 960;
        _graphics.PreferredBackBufferHeight = 540;
        //*/
        _graphics.IsFullScreen = true;
        _graphics.ApplyChanges();
       
        ScreenHeight = _graphics.PreferredBackBufferHeight;
        ScreenWidth = _graphics.PreferredBackBufferWidth;
        _camera = new();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _player = new (Content.Load<Texture2D>("Textures/Sprites/GreenDude25x25"), true) {
                Position = new Vector2( 100, 100),
                Physics = new(),
                };

        _components = new () {
            new Sprite(Content.Load<Texture2D>("Textures/Backgrounds/4ShadeDarkBG800x480"), false) {
                Position = new Vector2(0, 0), 
                },
            _player,
        };
        //File Path for the loaded Json level.
        var LoadJson = File.ReadAllText("World/Levels/TestLevel.json");
        var tiledict = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(LoadJson);
        _levelBuilder = new();
        _levelBuilder.BuildLevel(tiledict, _components, Content);
        
    }

    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape)) {
            Exit();
        }
        foreach (var component in _components) {
            if (component is Tile) {
                continue;
            }
            component.Update(gameTime, _components);
        }

        _camera.CameraFollow(_player);

        

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(transformMatrix: _camera.Transformation);

        foreach (var component in _components) {
            component.Draw(gameTime, _spriteBatch);
        }
        _spriteBatch.End();
        


        base.Draw(gameTime);
    }
}
