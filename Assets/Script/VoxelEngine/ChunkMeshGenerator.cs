using System.Collections.Generic;
using UnityEngine;

namespace VoxelEngine
{
    public static class ChunkMeshGenerator
    {
        public delegate bool InBounds(int x, int y, int z);
        
        private static Dictionary<Vector3Int, FaceData> _faces;
        public static Dictionary<Vector3Int, FaceData> Faces
        {
            get
            {
                if(_faces == null)
                {
                    _faces = new Dictionary<Vector3Int, FaceData>();
                    for(int i = 0; i < FaceData.Directions.Length; i++)
                    {
                        var d = FaceData.Directions[i];
                        if(d == Vector3Int.up)
                            _faces.Add(d, FaceData.Up);
                        else if(d == Vector3Int.down)
                            _faces.Add(d, FaceData.Down);
                        else if(d == Vector3Int.forward)
                            _faces.Add(d, FaceData.Forward);
                        else if(d == Vector3Int.back)
                            _faces.Add(d, FaceData.Back);
                        else if(d == Vector3Int.left)
                            _faces.Add(d, FaceData.Left);
                        else if(d == Vector3Int.right)
                            _faces.Add(d, FaceData.Right);
                    }
                }

                return _faces;
            }
        }

        public static Mesh CreateMesh(Voxel[,,] data, TextureLoader textureLoader, InBounds chunkBoundsCheck)
        {
            var vertices = new List<Vector3>();
            var indices = new List<int>();
            var uv = new List<Vector2>();

            var retMesh = new Mesh();

            for(int x = 0; x < World.ChunkSize.x; x++)
            for(int y = 0; y < World.ChunkSize.y; y++)
            for(int z = 0; z < World.ChunkSize.z; z++)
            {
                var pos = new Vector3Int(x, y, z);
                for(int i = 0; i < FaceData.Directions.Length; i++)
                {
                    var faceDirection = FaceData.Directions[i];
                    var nei = pos + faceDirection;

                    if(chunkBoundsCheck(nei.x, nei.y, nei.z))
                    {
                        if(data[nei.x, nei.y, nei.z].Type == 0 &&
                           data[pos.x, pos.y, pos.z].Type != 0)
                        {
                            var face = Faces[faceDirection];
                            var voxel = data[x, y, z].Type;
                            var texture = textureLoader.Textures[voxel];

                            for(var j = 0; j < face.Vertices.Length; j++)
                            {
                                var v = face.Vertices[j];
                                vertices.Add(new Vector3(x, y, z) * FaceData.VoxelSize + v);
                            }

                            for(var j = 0; j < face.Indices.Length; j++)
                            {
                                var t = face.Indices[j];
                                indices.Add(vertices.Count - 4 + t);
                            }

                            var uvs = texture.GetUVAtDirection(faceDirection);
                            for(int j = 0; j < face.Uvs.Length; j++)
                            {
                                uv.Add(uvs[j]);
                            }
                        }
                    }
                    else
                    {
                        if(data[pos.x, pos.y, pos.z].Type != 0)
                        {
                            var newFace = Faces[faceDirection];
                            var voxel = data[x, y, z];
                            var texture = textureLoader.Textures[voxel.Type];

                            for(var j = 0; j < newFace.Vertices.Length; j++)
                            {
                                var v = newFace.Vertices[j];
                                vertices.Add(new Vector3(x, y, z) * FaceData.VoxelSize + v);
                            }

                            for(var j = 0; j < newFace.Indices.Length; j++)
                            {
                                var t = newFace.Indices[j];
                                indices.Add(vertices.Count - 4 + t);
                            }

                            var uvs = texture.GetUVAtDirection(faceDirection);
                            for(int j = 0; j < newFace.Uvs.Length; j++)
                            {
                                uv.Add(uvs[j]);
                            }
                        }
                    }
                }
            }

            retMesh.SetVertices(vertices);
            retMesh.SetIndices(indices, MeshTopology.Triangles, 0);
            retMesh.SetUVs(0, uv);

            retMesh.RecalculateBounds();
            retMesh.RecalculateTangents();
            retMesh.RecalculateNormals();

            return retMesh;
        }
    }
}