using UnityEditor;
using UnityEngine;

public static class AssetHelper
{
    public static Sprite LoadSprite(string texturePath)
    {
        var tex = AssetDatabase.LoadAssetAtPath<Texture2D>(texturePath);
        return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one * .5f, 1);
    }


    public static GameObject CreateDestructibleEnv(GameObject pref)
    {
        var go = Object.Instantiate(pref);
        go.AddComponent<ExplosionTargetObject>();
        return go;
    }
    
}