using UnityEngine;

namespace VoxelEngine
{
    public class FaceData
    {
        //  size of cube
        public static float s = 1f;
        
        public static readonly Vector3Int[] Directions = new Vector3Int[]
        {
            Vector3Int.right,
            Vector3Int.left,
            Vector3Int.up,
            Vector3Int.down,
            Vector3Int.forward,
            Vector3Int.back
        };
        
        private static int[] XUVOrder = new int[]
        {
            2, 3, 1, 0
        };

        private static int[] YUVOrder = new int[]
        {
            0, 1, 3, 2
        };

        private static int[] ZUVOrder = new int[]
        {
            3, 1, 0, 2
        };

        public static FaceData Right = new FaceData(new Vector3[]
            {
                new Vector3(1f, 0f, 0f) * s,
                new Vector3(1f, 0f, 1f) * s,
                new Vector3(1f, 1f, 1f) * s,
                new Vector3(1f, 1f, 0f) * s
            },
            new int[]
            {
                0, 2, 1, 0, 3, 2
            }, YUVOrder);

        public static FaceData Left = new FaceData(new Vector3[]
            {
                new Vector3(0f, 0f, 0f) * s,
                new Vector3(0f, 0f, 1f) * s,
                new Vector3(0f, 1f, 1f) * s,
                new Vector3(0f, 1f, 0f) * s
            },
            new int[]
            {
                0, 1, 2, 0, 2, 3
            }, YUVOrder);

        public static FaceData Up = new FaceData(new Vector3[]
            {
                new Vector3(0f, 1f, 0f) * s,
                new Vector3(0f, 1f, 1f) * s,
                new Vector3(1f, 1f, 1f) * s,
                new Vector3(1f, 1f, 0f) * s
            },
            new int[]
            {
                0, 1, 2, 0, 2, 3
            }, ZUVOrder);

        public static FaceData Down = new FaceData(new Vector3[]
            {
                new Vector3(0f, 0f, 0f) * s,
                new Vector3(0f, 0f, 1f) * s,
                new Vector3(1f, 0f, 1f) * s,
                new Vector3(1f, 0f, 0f) * s
            },
            new int[]
            {
                0, 2, 1, 0, 3, 2
            }, ZUVOrder);

        public static FaceData Forward = new FaceData(new Vector3[]
            {
                new Vector3(0f, 0f, 1f) * s,
                new Vector3(0f, 1f, 1f) * s,
                new Vector3(1f, 1f, 1f) * s,
                new Vector3(1f, 0f, 1f) * s
            },
            new int[]
            {
                0, 2, 1, 0, 3, 2
            }, XUVOrder);

        public static FaceData Back = new FaceData(new Vector3[]
            {
                new Vector3(0f, 0f, 0f) * s,
                new Vector3(0f, 1f, 0f) * s,
                new Vector3(1f, 1f, 0f) * s,
                new Vector3(1f, 0f, 0f) * s
            },
            new int[]
            {
                0, 1, 2, 0, 2, 3
            }, XUVOrder);

        public Vector3[] Verts;
        public int[] Ind;
        public int[] Uvs;

        public FaceData(Vector3[] verts, int[] ind, int[] uvs)
        {
            Verts = verts;
            Ind = ind;
            Uvs = uvs;
        }
    }
 }