using UnityEngine;

namespace VoxelEngine.Generation
{
    public abstract class MapGeneratorComponent
    {
        protected Vector3Int ChunkSize => World.ChunkSize;
        protected int WorldSize => World.Get.WorldSize;
        public abstract void Init();
        public abstract void Generate();
    }
}