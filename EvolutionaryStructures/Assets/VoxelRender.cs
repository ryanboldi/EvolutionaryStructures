using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelRender : MonoBehaviour {
    public Material mat;

    List<Vector3> vertices;
    List<int> triangles;

    public float scale = 1f;

    float adjScale;

    void Awake() {
        adjScale = scale * 0.5f; //adj so that cubes are 1x1x1
    }

    void Start() {
        GenerateVoxelMesh(new VoxelData());
    }

    void GenerateVoxelMesh(VoxelData data) {
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
        //mesh.Clear();
        //mesh.vertices = vertices.ToArray();
        //mesh.triangles = triangles.ToArray();

        //mesh.RecalculateNormals();
    }

    void MakeCube(float cubeScale, Vector3 cubePos, int x, int y, int z, VoxelData data) {
        Mesh mesh = new Mesh();
        vertices = new List<Vector3>();
        triangles = new List<int>();
        GameObject cube = new GameObject();


        for (int i = 0; i < 6; i++) {
            //only make face if neighbour is open space
            if (data.GetNeighbour(x, y, z, (Direction)i) == 0) {
                MakeFace((Direction)i, cubeScale, cubePos);
            }
        }

        //cube.AddComponent<Rigidbody>();
        cube.AddComponent<MeshFilter>();
        cube.AddComponent<MeshRenderer>();
        cube.AddComponent<MeshCollider>();

        cube.transform.parent = GetComponent<Transform>();

        //mesh = cube.GetComponent<MeshFilter>().mesh;
        MeshCollider meshCol = cube.GetComponent<MeshCollider>();
        meshCol.sharedMesh = mesh;
        meshCol.convex = true;

        cube.GetComponent<MeshRenderer>().material = mat;
        cube.GetComponent<MeshFilter>().mesh = mesh;

        Rigidbody rb = cube.GetComponent<Rigidbody>();
        //rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;

        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();

        mesh.RecalculateNormals();
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
