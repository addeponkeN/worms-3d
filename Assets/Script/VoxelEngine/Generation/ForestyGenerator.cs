using System.Collections.Generic;
using UnityEngine;
using Util;

namespace VoxelEngine.Generation
{
    public class ForestyGenerator : MapGeneratorComponent
    {
        public override void Init()
        {
        }

        public override void Generate()
        {
            int treeCount = Random.Range(300, 500);

            var w = World.Get;
            var naturePrefabs = GetPrefabs("branch", "bBush", "flowers", "grass", "mushroom", "stump");

            const float fullTurn = 360f;

            for(int i = 0; i < treeCount; i++)
            {
                var randomPosition = w.GetRandomSafePosition();
                var randomRotation = new Vector3(0, Random.Range(0, fullTurn), 0);
                var prefab = naturePrefabs.Random();
                w.InstantiateEnvironment(prefab, randomPosition, Quaternion.Euler(randomRotation));
            }
        }

        List<GameObject> GetPrefabs(params string[] names)
        {
            var retList = new List<GameObject>();
            for(int i = 0; i < names.Length; i++)
            {
                retList.AddRange(PrefabManager.GetPrefabs("Environment/", $"{names[i]}"));
            }

            return retList;
        }
    }
}