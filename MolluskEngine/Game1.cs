using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MolluskEngine.Graphics;

namespace MolluskEngine;

public class Game1 
: Core
{
    public Game1() : base("Mollusk Game", 1280, 720, false)
    {
        
    }

    protected override void Initialize()
    {
        base.Initialize();

        Renderer.Initialize(GraphicsDevice);
        Global.Initialize();
        GraphicalContent.Initialize();
    }

    protected override void LoadContent()
    {
        // TODO: use this.Content to load your game content here
        Global.LoadContent();
        GraphicalContent.LoadContent(Content);

        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        if (Global.ExitCalling)
            Exit();

        Global.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        Renderer.Draw();
        base.Draw(gameTime);
    }
}
