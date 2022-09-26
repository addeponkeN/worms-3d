using UnityEngine;

namespace VoxelEngine
{
    public class Chunk
    {
        public GameObject ChunkGo;
        public Vector2Int PositionIndex;
        public Voxel[,,] Voxels;

        private MeshCollider _meshCollider;
        private MeshFilter _meshFilter;

        public Chunk(Vector3Int size)
        {
            Voxels = new Voxel[size.x, size.y, size.z];
        }

        public void Init(GameObject ob)
        {
            ChunkGo = ob;
            _meshFilter = ChunkGo.GetComponent<MeshFilter>();
            _meshCollider = ChunkGo.GetComponent<MeshCollider>();
        }

        public void SetVoxel(int x, int y, int z, int type)
        {
            Voxels[x, y, z] = new Voxel((byte)type);
        }

        public void UpdateMesh()
        {
            var mesh = ChunkMeshGenerator.CreateMesh(Voxels, World.Get.TextureLoader, World.Get.InChunkBounds);
            _meshFilter.mesh = mesh;
            _meshCollider.sharedMesh = mesh;
        }
        
    }
}