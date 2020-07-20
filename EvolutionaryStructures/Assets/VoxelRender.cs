using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class VoxelRender : MonoBehaviour {
    Mesh mesh;


    List<Vector3> vertices;
    List<int> triangles;

    public float scale = 1f;

    float adjScale;

    void Awake() {
        mesh = GetComponent<MeshFilter>().mesh;
        adjScale = scale * 0.5f; //adj so that cubes are 1x1x1
    }

    void Start() {
        GenerateVoxelMesh(new VoxelData());
        UpdateMesh();
        MeshCollider meshCol = GetComponent<MeshCollider>();
        meshCol.sharedMesh = mesh;
    }

    void GenerateVoxelMesh(VoxelData data) {
        vertices = new List<Vector3>();
        triangles = new List<int>();

        for (int z = 0; z < data.Depth; z++) {
            for (int y = 0; y < data.Height; y++) {
                for (int x = 0; x < data.Width; x++) {
                    if (data.GetCell(x, y, z) == 0) {
                        continue;
                    }
                    MakeCube(adjScale, new Vector3((float)x * scale, (float)y * scale, (float)z * scale), x, y, z, data);
                }
            }
        }
    }

    void UpdateMesh() {
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();

        mesh.RecalculateNormals();
    }

    void MakeCube(float cubeScale, Vector3 cubePos, int x, int y, int z, VoxelData data) {
        for (int i = 0; i < 6; i++) {
            //only make face if neighbour is open space
            if (data.GetNeighbour(x, y, z, (Direction)i) == 0) {
                MakeFace((Direction)i, cubeScale, cubePos);
            }
        }
    }

    void MakeFace(Direction dir, float faceScale, Vector3 facePos) {
        vertices.AddRange(CubeMeshData.faceVertices(dir, faceScale, facePos));
        int vCount = vertices.Count;

        triangles.Add(vCount - 4);
        triangles.Add(vCount - 3);
        triangles.Add(vCount - 2);
        triangles.Add(vCount - 4);
        triangles.Add(vCount - 2);
        triangles.Add(vCount - 1);
    }
}
