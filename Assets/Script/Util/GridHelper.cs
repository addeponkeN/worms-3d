using UnityEngine;

namespace Util
{
    public static class GridHelper
    {
        private static Vector3Int _cs;
        private static int _vs;

        public static void Init(Vector3Int chunkSize)
        {
            _cs = chunkSize;
            _vs = 1;
        }

        public static Vector2Int WorldToChunk(int x, int z)
        {
            return new Vector2Int(
                x / _cs.x,
                z / _cs.z);
        }

        public static Vector2Int WorldToChunk(Vector3 pos)
        {
            return new Vector2Int(
                (int)(pos.x / _cs.x),
                (int)(pos.z / _cs.z));
        }

        public static Vector3Int WorldToVoxel(Vector3 pos)
        {
            return new Vector3Int(
                (int)(pos.x / _vs),
                (int)(pos.y / _vs),
                (int)(pos.z / _vs));
        }

        public static Vector3Int WorldToChunkVoxel(Vector3 pos, Vector2Int chPosition)
        {
            return new Vector3Int(
                (int)(pos.x / _vs - chPosition.x * _cs.x),
                (int)(pos.y / _vs),
                (int)(pos.z / _vs - chPosition.y * _cs.z));
        }
    }
}