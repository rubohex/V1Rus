using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Orangedkeys.SPHERICFX;
using System.IO;

// Set the scale of all the imported models to  "globalScaleModifier"
// and dont generate materials for the imported objects

public class AssetPostImportProcess_SphericalFX : AssetPostprocessor
{
    static private bool WelcomeWin = false;
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {

        foreach (string item in importedAssets)
        {
            if (Path.GetFileName(item) == "AssetPostImportProcess_SphericalFX.cs") WelcomeWin = true;
        }

        foreach (string itemdel in deletedAssets)
        {
            if (Path.GetFileName(itemdel) == "AssetPostImportProcess_SphericalFX.cs") WelcomeWin = false;
            break;
        }

        if (WelcomeWin)
        {
            Debug.Log("SPHERICAL FX PACK IMPORTED !!");
            Welcome.ShowWindow();
        }


    }

}