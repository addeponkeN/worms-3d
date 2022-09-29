using UnityEngine;

namespace VoxelEngine
{
    public class FaceData
    {
        //  size of voxels
        public const float VoxelSize = 1f;

        public static readonly Vector3Int[] Directions =
        {
            Vector3Int.right,
            Vector3Int.left,
            Vector3Int.up,
            Vector3Int.down,
            Vector3Int.forward,
            Vector3Int.back
        };
        
        private static readonly int[] XUVOrder = {2, 3, 1, 0};
        private static readonly int[] YUVOrder = {0, 1, 3, 2};
        private static readonly int[] ZUVOrder = {3, 1, 0, 2};

        public static readonly FaceData Right = new(new[]
            {
                new Vector3(1f, 0f, 0f) * VoxelSize,
                new Vector3(1f, 0f, 1f) * VoxelSize,
                new Vector3(1f, 1f, 1f) * VoxelSize,
                new Vector3(1f, 1f, 0f) * VoxelSize
            },
            new[] {0, 2, 1, 0, 3, 2},
            YUVOrder);

        public static readonly FaceData Left = new(new[]
            {
                new Vector3(0f, 0f, 0f) * VoxelSize,
                new Vector3(0f, 0f, 1f) * VoxelSize,
                new Vector3(0f, 1f, 1f) * VoxelSize,
                new Vector3(0f, 1f, 0f) * VoxelSize
            },
            new[] {0, 1, 2, 0, 2, 3},
            YUVOrder);

        public static readonly FaceData Up = new(new[]
            {
                new Vector3(0f, 1f, 0f) * VoxelSize,
                new Vector3(0f, 1f, 1f) * VoxelSize,
                new Vector3(1f, 1f, 1f) * VoxelSize,
                new Vector3(1f, 1f, 0f) * VoxelSize
            },
            new[] {0, 1, 2, 0, 2, 3},
            ZUVOrder);

        public static readonly FaceData Down = new(new[]
            {
                new Vector3(0f, 0f, 0f) * VoxelSize,
                new Vector3(0f, 0f, 1f) * VoxelSize,
                new Vector3(1f, 0f, 1f) * VoxelSize,
                new Vector3(1f, 0f, 0f) * VoxelSize
            },
            new[] {0, 2, 1, 0, 3, 2},
            ZUVOrder);

        public static readonly FaceData Forward = new(new[]
            {
                new Vector3(0f, 0f, 1f) * VoxelSize,
                new Vector3(0f, 1f, 1f) * VoxelSize,
                new Vector3(1f, 1f, 1f) * VoxelSize,
                new Vector3(1f, 0f, 1f) * VoxelSize
            },
            new[] {0, 2, 1, 0, 3, 2},
            XUVOrder);

        public static readonly FaceData Back = new(new[]
            {
                new Vector3(0f, 0f, 0f) * VoxelSize,
                new Vector3(0f, 1f, 0f) * VoxelSize,
                new Vector3(1f, 1f, 0f) * VoxelSize,
                new Vector3(1f, 0f, 0f) * VoxelSize
            },
            new[] {0, 1, 2, 0, 2, 3},
            XUVOrder);

        public Vector3[] Vertices;
        public int[] Indices;
        public int[] Uvs;

        public FaceData(Vector3[] vertices, int[] indices, int[] uvs)
        {
            Vertices = vertices;
            Indices = indices;
            Uvs = uvs;
        }
    }
}