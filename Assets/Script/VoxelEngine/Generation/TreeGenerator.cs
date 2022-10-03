using UnityEngine;
using Util;

namespace VoxelEngine.Generation
{
    public class TreeGenerator : MapGeneratorComponent
    {
        public override void Init()
        {
        }

        public override void Generate()
        {
            int treeCount = Random.Range(10, 20);

            var w = World.Get;
            var treePrefabs = PrefabManager.GetPrefabs("Environment/", "tree");
            
            for(int i = 0; i < treeCount; i++)
            {
                var randomPosition = w.GetRandomSafePosition();
                var randomRotation = new Vector3(0, Random.Range(0, 360f), 0);
                var prefab = treePrefabs.Random();
                w.InstantiateEnvironment(prefab, randomPosition, randomRotation);
            }
        }
    }
}