using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public static PrefabManager Get { get; private set; }

    private Dictionary<string, GameObject> _prefabs;

    public GameObject GetPrefab(string name)
    {
        return _prefabs[name];
    }

    private void Awake()
    {
        if(Get == null)
            Get = this;

        _prefabs = new Dictionary<string, GameObject>();

        var prefabGuids = AssetDatabase.FindAssets("t:prefab", new string[] {"Assets/Prefabs/"});

        for(int i = 0; i < prefabGuids.Length; i++)
        {
            var path = AssetDatabase.GUIDToAssetPath(prefabGuids[i]);
            var go = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            _prefabs.Add(go.name, go);
        }
    }
    
}