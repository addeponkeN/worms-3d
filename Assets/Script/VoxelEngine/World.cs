using System;
using System.Collections.Generic;
using UnityEngine;
using Util;
using VoxelEngine.Generation;
using Random = UnityEngine.Random;

namespace VoxelEngine
{
    public class World : MonoBehaviour
    {
        public static readonly Vector3Int ChunkSize = new(10, 32, 10);
        private const float WaterLevelStart = 1.025f;

        public Vector3Int WorldBounds => ChunkSize * worldSize;

        public static World Get { get; private set; }

        public GameObject Chunks;
        public GameObject Environment;

        public GameObject Prefab_Chunk;

        [SerializeField] private TextureLoader _textureLoader;
        private int worldSize = 16;
        private int heightOffset = 60;
        private int heightIntensity = 5;

        private Dictionary<Vector2Int, Chunk> _chunks;
        private List<Chunk> _chunkList;

        public ChunkMeshGenerator MeshGenerator;
        private DistinctList<Chunk> _chunksToUpdate;

        private List<IGenerator> _generators;

        public event Action OnGeneratedEvent;

        public float WaterLevel = WaterLevelStart;

        private void Awake()
        {
            if(Get == null)
                Get = this;

            _generators = new List<IGenerator>();
            _chunksToUpdate = new DistinctList<Chunk>();
            _chunkList = new List<Chunk>();
            _chunks = new Dictionary<Vector2Int, Chunk>();
            MeshGenerator = new ChunkMeshGenerator(_textureLoader);

            GridHelper.Init(ChunkSize);
        }

        private void Start()
        {
            Generate();
        }

        private void Update()
        {
            if(_chunksToUpdate.Count > 0)
            {
                for(int i = 0; i < _chunksToUpdate.Count; i++)
                    _chunksToUpdate[i].UpdateMesh();
                _chunksToUpdate.Clear();
                
                // _chunksToUpdate[_chunksToUpdate.Count - 1].UpdateMesh();
                // _chunksToUpdate.RemoveAt(_chunksToUpdate.Count - 1);
                
            }
        }

        public void Generate()
        {
            Vector2Int globalOffset = new Vector2Int(Random.Range(0, 1000), Random.Range(0, 1000));

            for(int wx = 0; wx < worldSize; wx++)
            for(int wz = 0; wz < worldSize; wz++)
            {
                var chunk = new Chunk(ChunkSize);
                int offX = wx * ChunkSize.x + globalOffset.x;
                int offZ = wz * ChunkSize.z + globalOffset.y;

                for(int x = 0; x < ChunkSize.x; x++)
                for(int z = 0; z < ChunkSize.z; z++)
                {
                    float scale = 0.2f;
                    float intens = 5f;

                    float perX = (offX + x) / (float)WorldBounds.x;
                    float perZ = (offZ + z) / (float)WorldBounds.z;

                    var noise = Mathf.PerlinNoise(perX / scale, perZ / scale);
                    int height = Mathf.RoundToInt(noise * ChunkSize.y - 10);

                    height = Mathf.Clamp(height, 0, ChunkSize.y - 1);

                    for(int y = height; y >= 0; y--)
                    {
                        int voxelType = 0;

                        if(y == height)
                            voxelType = 1;
                        else if(y > 0)
                            voxelType = 2;

                        // if(voxelType > 0)
                        chunk.SetVoxel(x, y, z, voxelType);
                    }
                }

                var ob = Instantiate(Prefab_Chunk, Chunks.transform);
                chunk.Init(ob);
                chunk.PositionIndex = new Vector2Int(wx, wz);

                var position = new Vector3(wx * ChunkSize.x * FaceData.s, 0, wz * ChunkSize.z * FaceData.s);
                ob.transform.position = position;

                _chunksToUpdate.Add(chunk);

                AddChunk(chunk);
            }

            OnGeneratedEvent?.Invoke();
        }

        void AddChunk(Chunk chunk)
        {
            _chunks.Add(chunk.PositionIndex, chunk);
            _chunkList.Add(chunk);
        }

        public Chunk GetChunk(Vector3 worldPos)
        {
            var cp = GridHelper.WorldToChunk(worldPos);
            if(_chunks.TryGetValue(cp, out var ch))
                return ch;

            return null;
        }

        public bool InWorldBounds(int x, int y, int z)
        {
            if((x >= 0 && x < WorldBounds.x) &&
               (y >= 0 && y < WorldBounds.y) &&
               (z >= 0 && y < WorldBounds.y))
                return true;
            return false;
        }

        public bool InChunkBounds(int x, int y, int z)
        {
            if(y < 0 || y >= ChunkSize.y)
                return false;

            int chunkVoxelX = x;
            int chunkVoxelZ = z;

            if((chunkVoxelX >= 0 && chunkVoxelX < ChunkSize.x) &&
               chunkVoxelZ >= 0 && chunkVoxelZ < ChunkSize.z)
                return true;

            return false;
        }

        public void SetVoxel(int x, int y, int z, int type)
        {
            var position = new Vector3(x, y, z);
            if(!InWorldBounds(x, y, z))
            {
                Debug.Log($"out of bounds: {position}");
                return;
            }

            var ch = GetChunk(position);

            if(ch == null)
            {
                Debug.Log($"chunk null: {position}");
                return;
            }

            var vxp = GridHelper.WorldToChunkVoxel(position, ch.PositionIndex);
            ch.SetVoxel(vxp.x, vxp.y, vxp.z, type);

            _chunksToUpdate.Add(ch);
        }

        public void SetVoxelCube(Vector3 worldPosition, int radius, int voxelType)
        {
            var sx = (int)(worldPosition.x - radius);
            var ex = (int)(worldPosition.x + radius);
            var sy = (int)(worldPosition.y - radius);
            var ey = (int)(worldPosition.y + radius);
            var sz = (int)(worldPosition.z - radius);
            var ez = (int)(worldPosition.z + radius);

            for(int x = sx; x < ex; x++)
            for(int y = sy; y < ey; y++)
            for(int z = sz; z < ez; z++)
            {
                SetVoxel(x, y, z, voxelType);
            }
        }
    }

    public struct Voxel
    {
        public byte Type;

        public Voxel(byte type)
        {
            Type = type;
        }
    }

    public class Chunk
    {
        private static ChunkMeshGenerator MeshGenerator => World.Get.MeshGenerator;

        public GameObject GameObject;
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
            GameObject = ob;
            _meshFilter = GameObject.GetComponent<MeshFilter>();
            _meshCollider = GameObject.GetComponent<MeshCollider>();
        }

        public void SetVoxel(int x, int y, int z, int type)
        {
            Voxels[x, y, z] = new Voxel((byte)type);
        }

        public void UpdateMesh()
        {
            var mesh = MeshGenerator.CreateMesh(Voxels);
            _meshFilter.mesh = mesh;
            _meshCollider.sharedMesh = mesh;
        }
    }
}