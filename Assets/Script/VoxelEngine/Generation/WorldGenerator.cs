using UnityEngine;

namespace VoxelEngine.Generation
{
    public class WorldGenerator : MapGeneratorComponent
    {
        public override void Init()
        {
        }

        public override void Generate()
        {
            Vector2Int globalOffset = new Vector2Int(Random.Range(0, 1000), Random.Range(0, 1000));

            var w = World.Get;
            
            for(int wx = 0; wx < WorldSize; wx++)
            for(int wz = 0; wz < WorldSize; wz++)
            {
                var chunk = new Chunk(ChunkSize);
                int offX = wx * ChunkSize.x + globalOffset.x;
                int offZ = wz * ChunkSize.z + globalOffset.y;

                for(int x = 0; x < ChunkSize.x; x++)
                for(int z = 0; z < ChunkSize.z; z++)
                {
                    const float scale = 0.2f;

                    float perX = (offX + x) / 150f;
                    float perZ = (offZ + z) / 150f;

                    var noise = Mathf.PerlinNoise(perX / scale, perZ / scale);
                    int height = Mathf.RoundToInt(noise * ChunkSize.y - 10);

                    height = Mathf.Clamp(height, 0, ChunkSize.y - 1);

                    for(int y = height; y >= 0; y--)
                    {
                        if(y < 1)
                            continue;

                        int voxelType = 0;

                        if(y == height)
                            voxelType = 1;
                        else if(y > 0)
                            voxelType = 2;

                        chunk.SetVoxel(x, y, z, voxelType);
                    }
                }

                var ob = Object.Instantiate(w.Prefab_Chunk, w.Chunks.transform);
                chunk.Init(ob);
                chunk.PositionIndex = new Vector2Int(wx, wz);

                var position = new Vector3(wx * ChunkSize.x * FaceData.VoxelSize, 0, wz * ChunkSize.z * FaceData.VoxelSize);
                ob.transform.position = position;

                w.AddChunk(chunk);
            }
            
            w.UpdateChunkMeshes();
        }
    }
}