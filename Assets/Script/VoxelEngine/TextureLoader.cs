using System;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelEngine
{
    public class TextureLoader : MonoBehaviour
    {
        [SerializeField]
        private CubeTexture[] _cubeTextures;
        public Dictionary<int, CubeTexture> Textures;

        private void Awake()
        {
            Textures = new Dictionary<int, CubeTexture>();
            for(int i = 0; i < _cubeTextures.Length; i++)
            {
                Textures.Add(i + 1, _cubeTextures[i]);
            }
        }
    }

    [Serializable]
    public class CubeTexture
    {
        public Sprite FaceTexture;
        public Sprite XTexture, YTexture, ZTexture;

        public Vector2[] GetUVAtDirection(Vector3Int dir)
        {
            if(dir == Vector3Int.forward || dir == Vector3Int.back)
                return ZTexture != null ? ZTexture.uv : FaceTexture.uv;
            if(dir == Vector3Int.right || dir == Vector3Int.left)
                return XTexture != null ? XTexture.uv : FaceTexture.uv;
            if(dir == Vector3Int.up || dir == Vector3Int.down)
                return YTexture != null ? YTexture.uv : FaceTexture.uv;

            return null;
        }
    }
}