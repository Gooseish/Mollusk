using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using MolluskEngine.Input;

namespace MolluskEngine.Settings;

/// <summary>
/// Manages user-defined settings
/// </summary>
public class _Settings
{
    public InputSettings InputSettings = new InputSettings();
    public _Settings()
    {
        RestoreDefaultSettings();
        RestoreSavedSettings();
    }

    public void RestoreDefaultSettings()
    {
        InputSettings.RestoreDefaultSettings();
    }
    public void RestoreSavedSettings()
    {
        InputSettings.RestoreSavedSettings();
    }
}
