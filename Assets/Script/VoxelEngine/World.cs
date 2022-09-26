using System;
using System.Collections.Generic;
using GameStates;
using UnityEngine;
using Util;
using VoxelEngine.Generation;
using Random = UnityEngine.Random;

namespace VoxelEngine
{
    public class World : MonoBehaviour, ILoader
    {
        private const float WaterLevelStart = 1.025f;

        public static readonly Vector3Int ChunkSize = new(10, 32, 10);
        public static World Get { get; private set; }

        public bool LoadingComplete { get; set; }

        public Vector3Int WorldBounds => ChunkSize * _worldSize;

        public GameObject Chunks;
        public GameObject Environment;
        public GameObject Prefab_Chunk;

        [NonSerialized] public TextureLoader TextureLoader;
        public WaterMesh Water;

        public event Action OnGeneratedEvent;

        private int _worldSize = 10;
        private int _heightOffset = 60;
        private int _heightIntensity = 5;

        private Dictionary<Vector2Int, Chunk> _chunks;
        private List<Chunk> _chunkList;
        private DistinctList<Chunk> _chunksToUpdate;
        private List<IGenerator> _generators;

        public List<Chunk> GetChunkList()
        {
            return _chunkList;
        }

        private void Awake()
        {
            if(Get == null)
            {
                Get = this;
            }
            else
            {
                Destroy(this);
            }

            _generators = new List<IGenerator>();
            _chunksToUpdate = new DistinctList<Chunk>();
            _chunkList = new List<Chunk>();
            _chunks = new Dictionary<Vector2Int, Chunk>();

            Water.WaterLevel = WaterLevelStart;

            TextureLoader = gameObject.GetComponent<TextureLoader>();

            GridHelper.Init(ChunkSize);
        }

        private void Start()
        {
        }

        public void Load()
        {
            Generate();
            UpdateChunkMeshes();
        }

        private void Update()
        {
            UpdateChunkMeshes();
        }

        public void UpdateChunkMeshes()
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

            for(int wx = 0; wx < _worldSize; wx++)
            for(int wz = 0; wz < _worldSize; wz++)
            {
                var chunk = new Chunk(ChunkSize);
                int offX = wx * ChunkSize.x + globalOffset.x;
                int offZ = wz * ChunkSize.z + globalOffset.y;

                for(int x = 0; x < ChunkSize.x; x++)
                for(int z = 0; z < ChunkSize.z; z++)
                {
                    const float scale = 0.2f;

                    float perX = (offX + x) / (float)WorldBounds.x;
                    float perZ = (offZ + z) / (float)WorldBounds.z;

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

        private void AddChunk(Chunk chunk)
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
                return;
            }

            var ch = GetChunk(position);

            if(ch == null)
            {
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

            for(int x = sx; x <= ex; x++)
            for(int y = sy; y <= ey; y++)
            for(int z = sz; z <= ez; z++)
            {
                SetVoxel(x, y, z, voxelType);
            }
        }

        public void SetVoxelSphere(Vector3 worldPosition, int radius, int voxelType)
        {
            var sx = (int)(worldPosition.x - radius);
            var ex = (int)(worldPosition.x + radius);
            var sy = (int)(worldPosition.y - radius);
            var ey = (int)(worldPosition.y + radius);
            var sz = (int)(worldPosition.z - radius);
            var ez = (int)(worldPosition.z + radius);

            for(int x = sx; x <= ex; x++)
            for(int y = sy; y <= ey; y++)
            for(int z = sz; z <= ez; z++)
            {
                var dis = Vector3.Distance(new Vector3(x, y, z), worldPosition);
                if(dis < radius)
                {
                    SetVoxel(x, y, z, voxelType);
                }
            }
        }
    }
}