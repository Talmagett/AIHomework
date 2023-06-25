using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[RequireComponent(typeof(MeshFilter))]
public class MeshSpawner : MonoBehaviour
{
	[SerializeField] private FunctionPoints _function;

	private MeshFilter _meshFilter;
	private Mesh _mesh;
	private Vector3[] _vertices;
	private Vector3[] _normals;
	private int[] _triangles;
	private Vector2[] _uvs;
	private int width => Mathf.CeilToInt((_function.MaxRange - _function.MinRange) / _function.Step);
	private void Awake()
	{
		_meshFilter = GetComponent<MeshFilter>();

    }

	private void Start()
	{
		_mesh = new Mesh();
		_meshFilter.mesh = _mesh;
		CreateMesh();
		UpdateMesh();
        var meshRenderer = GetComponent<MeshRenderer>();
		HashSet<float> heights = _vertices.Select(t=>t.z).ToHashSet();
		print(meshRenderer.material.GetFloat("_MinHeight"));
        meshRenderer.material.SetFloat("_MinHeight", heights.Min());
        meshRenderer.material.SetFloat("_MaxHeight", heights.Max());
		print(meshRenderer.material.GetFloat("_MinHeight"));
    }

    private void CreateMesh()
	{
		//_normals = Vector3();
		CreateVertices();
		CreateTriangles();
		CreateUVs();
	}
	private void CreateVertices()
	{
		_vertices = new Vector3[width*width];
		for (int y = 0; y < width; y++)
		{
			for (int x = 0; x < width; x++)
			{
				_vertices[x + y * width] = _function.GetPoint(x * _function.Step + _function.MinRange, y * _function.Step+_function.MinRange);
			}
		}
	}
	private void CreateTriangles()
	{
		int triangleVertexCount = 0;
		_triangles = new int[(width*width-width-width+1)*2*3*2];
		for (int vertex = 0; vertex < width * width - width; vertex++)
		{
			if (vertex % width != (width - 1))
			{
				//first triangle
				int A = vertex;
				int B = A + width;
				int C = B + 1;
				_triangles[triangleVertexCount] = A;
				_triangles[triangleVertexCount + 1] = B;
				_triangles[triangleVertexCount + 2] = C;

				_triangles[triangleVertexCount + 3] = A;
				_triangles[triangleVertexCount + 4] = C;
				_triangles[triangleVertexCount + 5] = B;
				//second triangle
				B += 1;
				C = A + 1;
				_triangles[triangleVertexCount + 6] = A;
				_triangles[triangleVertexCount + 7] = B;
				_triangles[triangleVertexCount + 8] = C;

				_triangles[triangleVertexCount + 9] = A;
				_triangles[triangleVertexCount + 10] = C;
				_triangles[triangleVertexCount + 11] = B;
				triangleVertexCount += 12;
            }
		}
	}
	private void CreateUVs()
	{
        _uvs = new Vector2[width*width];
		int uvIndexCounter = 0;
		foreach (var item in _vertices)
		{
			_uvs[uvIndexCounter] = new Vector2(item.x,item.z);
			uvIndexCounter++;
		}
    }
    private void UpdateMesh()
	{
		_mesh.Clear();
		_mesh.vertices = _vertices;
		_mesh.triangles = _triangles;
		_mesh.RecalculateBounds();
		_mesh.normals = _normals;
		_mesh.uv = _uvs;
	}
}