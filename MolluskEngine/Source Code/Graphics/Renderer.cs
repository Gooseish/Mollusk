using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MolluskEngine.Graphics;

public static class Renderer
{
    private static GraphicsDevice graphicsDevice;
    private static Letterbox letterbox;
    #region Render Targeting
    /// <summary>
    /// Stable render targets are used to store the cumulative image built
    /// during the drawing process.
    /// </summary>
    private static RenderTarget2D[] stableRenderTargets = new RenderTarget2D[2];
    /// <summary>
    /// Volatile render targets are used to store temporary image data.
    /// </summary>
    private static RenderTarget2D[] volatileRenderTargets = new RenderTarget2D[2];
    private static int stableIndex = 0;
    private static int volatileIndex = 0;
    private static RenderTarget2D currentVolatile {
        get {return volatileRenderTargets[volatileIndex];}
        set {volatileRenderTargets[volatileIndex] = value;}}
    private static RenderTarget2D previousVolatile {
        get {return volatileRenderTargets[(volatileIndex + 1) % 2];}
        set {volatileRenderTargets[(volatileIndex + 1) % 2] = value;}}
    private static RenderTarget2D currentStable {
        get {return stableRenderTargets[stableIndex];}
        set {stableRenderTargets[stableIndex] = value;}}
    private static RenderTarget2D previousStable {
        get {return stableRenderTargets[(stableIndex + 1) % 2];}
        set {stableRenderTargets[(stableIndex + 1) % 2] = value;}}
    public static void NextVolatile()
    {
        volatileIndex = (volatileIndex + 1) % 2;
        graphicsDevice.SetRenderTarget(currentVolatile);
        graphicsDevice.Clear(Color.Transparent);
    }
    public static void NextStable()
    {
        stableIndex = (stableIndex + 1) % 2;
        graphicsDevice.SetRenderTarget(currentStable);
        graphicsDevice.Clear(Color.Transparent);
    }
    private static void InitializeRenderTargets()
    {
        for (int n = 0; n < stableRenderTargets.Length; n++)
            stableRenderTargets[n] = new RenderTarget2D(
                graphicsDevice, Config.sourceResolutionWidth, Config.sourceResolutionHeight);

        for (int n = 0; n < volatileRenderTargets.Length; n++)
            volatileRenderTargets[n] = new RenderTarget2D(
                graphicsDevice, Config.sourceResolutionWidth, Config.sourceResolutionHeight);
    }
    #endregion
    public static void Draw()
    {
        SpriteBatch spriteBatch = new SpriteBatch(graphicsDevice);
        Global.Draw(spriteBatch); 
        DrawToTargetResolution(spriteBatch, currentStable);
    }
    private static void DrawToTargetResolution(SpriteBatch spriteBatch, RenderTarget2D renderTarget)
    {
        graphicsDevice.SetRenderTarget(null);
        spriteBatch.Begin();
        spriteBatch.Draw(renderTarget, letterbox.LetterboxPicture, Color.White);
        spriteBatch.End();
    }
    public static void Initialize(GraphicsDevice _graphicsDevice)
    {
        graphicsDevice = _graphicsDevice;
        letterbox = new Letterbox();
        InitializeRenderTargets();
    }
    public static void ResizeGameWindow(int width, int height)
    {
        Core.Graphics.PreferredBackBufferHeight = width;
        Core.Graphics.PreferredBackBufferWidth = height;
        Core.Graphics.ApplyChanges();

        // Black bar positions for letterboxing
        letterbox.RecalculateLetterbox();
    }    
}
