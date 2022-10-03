using System.Collections.Generic;
using UnityEngine;

public static partial class PrefabManager
{
    private static Dictionary<string, GameObject> _prefabs;
    private static List<GameObject> _prefabList;

    public static GameObject GetPrefab(string name)
    {
        return _prefabs[name];
    }

    public static void Load()
    {
        _prefabs = new Dictionary<string, GameObject>();
        _prefabList = new List<GameObject>();
        
        var prefabs = Resources.LoadAll<GameObject>("Prefabs/");
        for (int i = 0; i < prefabs.Length; i++)
        {
            var pf = prefabs[i];
            _prefabs.Add(pf.name, pf);
            _prefabList.Add(pf);
        }
    }

    public static GameObject[] GetPrefabs(string prefabFolder)
    {
        return Resources.LoadAll<GameObject>($"Prefabs/{prefabFolder}");
    }
    
    public static GameObject[] GetPrefabs(string prefabFolder, string containsName)
    {
        List<GameObject> retList = new List<GameObject>();
        
        for (int i = 0; i < _prefabList.Count; i++)
        {
            if (_prefabList[i].name.Contains(containsName))
                retList.Add(_prefabList[i]);
        }

        return retList.ToArray();
    }
    
}