using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    public int startY = 10;
    public Material material;
    public Rigidbody rb;

    Mesh mesh;
    GameObject obj;

    Vector3[] vertices;
    int[] triangles;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        obj = new GameObject();
        obj.name = "Mesh";

        obj.AddComponent<MeshFilter>();
        obj.GetComponent<MeshFilter>().mesh = mesh;
        //obj.AddComponent<Rigidbody>();
        obj.AddComponent<MeshCollider>();
        obj.AddComponent<MeshRenderer>();

        obj.GetComponent<MeshCollider>().sharedMesh = mesh;
        obj.GetComponent<MeshCollider>().convex = true;
        obj.GetComponent<MeshRenderer>().material = material;

        MakeMeshData();
        CreateMesh();

        //obj.transform.Translate(0, startY, 0);

        //RandomForces();
    }

    void MakeMeshData()
    {
        vertices = new Vector3[]{
            new Vector3(0,0,0),
            new Vector3(0,0,1),
            new Vector3(1,0,0),
            new Vector3(1,0,1)
        };

        triangles = new int[]{
            0,1,2,2,1,3
        };
    }

    void CreateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

    void RandomForces()
    {
        rb = obj.GetComponent<Rigidbody>();
        //Random.Range(0.0f, 10.0f)
        rb.AddTorque(transform.up * 1000.0f);
    }
}
