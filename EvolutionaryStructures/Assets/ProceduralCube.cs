﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralCube : MonoBehaviour {

    Mesh mesh;
    List<Vector3> vertices;
    List<int> triangles;

    public float scale = 1f;
    public int posX, posY, posZ;


    float adjScale;


    void Awake() {
        mesh = GetComponent<MeshFilter>().mesh;
        adjScale = scale * 0.5f; //adj so that cubes are 1x1x1
    }

    void Start() {
        MakeCube(adjScale);
        UpdateMesh();
    }

    void UpdateMesh() {
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();

        mesh.RecalculateNormals();
    }

    void MakeCube(float cubeScale) {
        vertices = new List<Vector3>();
        triangles = new List<int>();

        for (int i = 0; i < 6; i++) {
            MakeFace(i, cubeScale);
        }
    }

    void MakeFace(int dir, float faceScale) {
        vertices.AddRange(CubeMeshData.faceVertices(dir, faceScale));
        int vCount = vertices.Count;

        triangles.Add(vCount - 4);
        triangles.Add(vCount - 3);
        triangles.Add(vCount - 2);
        triangles.Add(vCount - 4);
        triangles.Add(vCount - 2);
        triangles.Add(vCount - 1);
    }
}
