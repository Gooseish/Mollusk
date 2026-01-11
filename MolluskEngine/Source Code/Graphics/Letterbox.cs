using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MolluskEngine.Graphics;

public class Letterbox
{
    private int letterboxPictureLocationX;
    private int letterboxPictureLocationY;
    private int letterboxPictureHeight;
    private int letterboxPictureWidth;
    public Rectangle LetterboxPicture {get; private set;}
    public void RecalculateLetterbox()
    {
        // Find scaling factor
        double widthScalingFactor = 
            (double)Core.Graphics.PreferredBackBufferWidth 
            / Config.sourceResolutionWidth;
        
        double heightScalingFactor = 
            (double)Core.Graphics.PreferredBackBufferHeight
            / Config.sourceResolutionHeight;
        
        double trueScalingFactor = Math.Min(widthScalingFactor, heightScalingFactor);

        // Accordingly recalculate letterbox position
        letterboxPictureWidth = (int)
            (Config.sourceResolutionWidth * trueScalingFactor);

        letterboxPictureHeight = (int)
            (Config.sourceResolutionHeight * trueScalingFactor);

        letterboxPictureLocationX = 
            (Core.Graphics.PreferredBackBufferWidth - letterboxPictureWidth)/2;

        letterboxPictureLocationY = 
            (Core.Graphics.PreferredBackBufferHeight - letterboxPictureHeight)/2;
        
        LetterboxPicture = new Rectangle(
            letterboxPictureLocationX, 
            letterboxPictureLocationY, 
            letterboxPictureWidth, 
            letterboxPictureHeight);
    }
    public Letterbox()
    {
        RecalculateLetterbox();
    }
}
