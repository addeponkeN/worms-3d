using System;
using System.Collections.Generic;
using GameStates;
using UnityEngine;
using Util;
using VoxelEngine.Generation;

namespace VoxelEngine
{
    public class World : MonoBehaviour, ILoader
    {
        private const float WaterLevelStart = 1.025f;

        public static readonly Vector3Int ChunkSize = new(10, 32, 10);
        public static World Get { get; private set; }

        public Vector3Int WorldBounds => ChunkSize * WorldSize;
        public bool LoadingComplete { get; set; }

        public event Action OnGeneratedEvent;
        
        [NonSerialized] public TextureLoader TextureLoader;
        public GameObject Chunks;
        public GameObject Environment;
        public GameObject Prefab_Chunk;
        public WaterMesh Water;

        [SerializeField] public int WorldSize = 14;
        [SerializeField] private int _heightOffset = 60;
        [SerializeField] private int _heightIntensity = 5;
        private Dictionary<Vector2Int, Chunk> _chunks;
        private List<Chunk> _chunkList;
        private DistinctList<Chunk> _chunksToUpdate;
        private List<MapGeneratorComponent> _generators;

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

            _generators = new List<MapGeneratorComponent>();
            _chunksToUpdate = new DistinctList<Chunk>();
            _chunkList = new List<Chunk>();
            _chunks = new Dictionary<Vector2Int, Chunk>();

            Water.WaterLevel = WaterLevelStart;

            TextureLoader = gameObject.GetComponent<TextureLoader>();

            GridHelper.Init(ChunkSize);
        }

        public void Load()
        {
            _generators.Add(new WorldGenerator());
            _generators.Add(new TreeGenerator());
            _generators.Add(new RockGenerator());
            _generators.Add(new ForestyGenerator());
            
            Generate();
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
            }
        }

        public List<Chunk> GetChunkList()
        {
            return _chunkList;
        }
        
        public void Generate()
        {
            for(int i = 0; i < _generators.Count; i++)
                _generators[i].Init();
            for(int i = 0; i < _generators.Count; i++)
                _generators[i].Generate();
            OnGeneratedEvent?.Invoke();
        }

        public GameObject InstantiateEnvironment(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            var go = Instantiate(prefab, position, rotation, Environment.transform);
            go.AddComponent<EnvironmentObject>();
            return go;
        }
        
        public GameObject InstantiateEnvironment(GameObject prefab, Vector3 position, Vector3 rotationEuler)
        {
            return InstantiateEnvironment(prefab, position, Quaternion.Euler(rotationEuler));
        }

        public void AddChunk(Chunk chunk)
        {
            _chunksToUpdate.Add(chunk);
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

        public void SetVoxelsCube(Vector3 worldPosition, int radius, int voxelType)
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

        public void SetVoxelsSphere(Vector3 worldPosition, int radius, int voxelType)
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