using System.Collections.Generic;
using UnityEngine;
using VoxelEngine;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class WaterMesh : MonoBehaviour
{
    public float Size = 1f;
    public int GridSize = 16;
    public float WaveSpeed = 1f;
    public float WavePower = 1f;

    private MeshFilter _meshFilter;

    private Vector2 offset;

    private void Awake()
    {
        World.Get.OnGeneratedEvent += GetOnOnGeneratedEvent;
    }

    private void Start()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _meshFilter.mesh = GenerateMesh();
        Noise();
    }

    private void GetOnOnGeneratedEvent()
    {
        var worldBounds = World.Get.WorldBounds;
        transform.position = new Vector3(worldBounds.x * .5f, 1.01f, worldBounds.z * .5f);
    }

    private void Update()
    {
        Noise();
    }

    void Noise()
    {
        offset.x += WaveSpeed * Time.deltaTime;
        offset.y += WaveSpeed * Time.deltaTime;

        var vertices = _meshFilter.mesh.vertices;

        for(int i = 0; i < vertices.Length; i++)
        {
            var v = vertices[i];
            var noise = Mathf.PerlinNoise(v.x + offset.x, v.z + offset.y);
            vertices[i] = new Vector3(v.x, noise * WavePower, v.z);
        }

        _meshFilter.mesh.vertices = vertices;
    }

    private Mesh GenerateMesh()
    {
        var retMesh = new Mesh();

        var vertices = new List<Vector3>();
        var normals = new List<Vector3>();
        var uvs = new List<Vector2>();

        for(int x = 0; x <= GridSize; x++)
        for(int y = 0; y <= GridSize; y++)
        {
            var v = new Vector3(-Size * .5f + Size * (x / ((float)GridSize)), 0,
                -Size * .5f + Size * (y / ((float)GridSize)));
            vertices.Add(v);
            normals.Add(Vector3.up);
            uvs.Add(new Vector2(x / (float)GridSize, y / (float)GridSize));
        }

        var triangles = new List<int>();
        var vertCount = GridSize + 1;

        for(int i = 0; i < vertCount * vertCount - vertCount; i++)
        {
            if((i + 1) % vertCount == 0)
            {
                continue;
            }

            triangles.AddRange(new List<int>
            {
                i + 1 + vertCount, i + vertCount, i,
                i, i + 1, i + vertCount + 1
            });
        }

        retMesh.SetVertices(vertices);
        retMesh.SetNormals(normals);
        retMesh.SetUVs(0, uvs);
        retMesh.SetTriangles(triangles, 0);
        return retMesh;
    }
}