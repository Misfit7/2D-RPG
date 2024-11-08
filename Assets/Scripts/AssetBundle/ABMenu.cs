using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class ABMenu
{

    [MenuItem("AssetBundle/Export/Windows")]
    public static void ForWindows()
    {
        Export(BuildTarget.StandaloneWindows);
    }

    [MenuItem("AssetBundle/Export/Android")]
    public static void ForAndroid()
    {
        Export(BuildTarget.Android);
    }

    [MenuItem("AssetBundle/Export/IOS")]
    public static void ForIOS()
    {
        Export(BuildTarget.iOS);
    }

    [MenuItem("AssetBundle/Export/Mac")]
    public static void ForMac()
    {
        Export(BuildTarget.StandaloneOSX);
    }

    private static void Export(BuildTarget targetPlatform)
    {
        string path = Application.dataPath + "AssetBundles/StandaloneWindows";
        if (!Directory.Exists(path))    //Â·¾¶²»´æÔÚ
        {
            Directory.CreateDirectory(path);
        }

        BuildPipeline.BuildAssetBundles(
            path,
            BuildAssetBundleOptions.ChunkBasedCompression | BuildAssetBundleOptions.ForceRebuildAssetBundle | BuildAssetBundleOptions.StrictMode,
            targetPlatform
        );
    }
}
