using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralGrid : MonoBehaviour
{

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    //grid settings
    public float cellSize = 1;
    public Vector3 gridOffset;
    int gridsize;

    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;

    }

    void Start()
    {
        MakeDiscreteProceduralGrid();
        UpdateMesh();
    }

    void MakeDiscreteProceduralGrid()
    {
        //set array sizes
        vertices = new Vector3[4];
        triangles = new int[6];

        //set vertex offset -> we want quad in centered on x,y
        float vertexOffset = cellSize * 0.5f;

        //populate the vertices and triangles arrrays
        vertices[0] = new Vector3(-vertexOffset, 0, -vertexOffset) + gridOffset;
        vertices[1] = new Vector3(-vertexOffset, 0,  vertexOffset) + gridOffset;
        vertices[2] = new Vector3( vertexOffset, 0, -vertexOffset) + gridOffset;
        vertices[3] = new Vector3( vertexOffset, 0,  vertexOffset) + gridOffset;

        triangles[0] = 0;
        triangles[1] = triangles[4] = 1;
        triangles[2] = triangles[3] = 2;
        triangles[5] = 3;
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
}
