using UnityEngine;
using Util;

namespace VoxelEngine.Generation
{
    public class RockGenerator : MapGeneratorComponent
    {
        public override void Init()
        {
        }

        public override void Generate()
        {
            int treeCount = Random.Range(20, 40);

            var w = World.Get;
            var treePrefabs = PrefabManager.GetPrefabs("Environment/", "Rock");
            
            for(int i = 0; i < treeCount; i++)
            {
                var randomPosition = w.GetRandomSafePosition();
                var randomRotation = new Vector3(0, Random.Range(0, 360f), 0);
                var prefab = treePrefabs.Random();
                w.InstantiateEnvironment(prefab, randomPosition, Quaternion.Euler(randomRotation));
            }
        }
    }
}