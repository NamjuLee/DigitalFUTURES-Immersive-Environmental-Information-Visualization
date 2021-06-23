using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mesh_01_ProcedualCubeTriMesh : MonoBehaviour
{
    Vector3[] vertices;
    int[] triangles;
    Mesh mesh;
    void Start()
    {
        
        float offset =1f;
        Vector3[] vertices = {
            new Vector3 (-offset, -offset, 0),
            new Vector3 (+offset, -offset, 0),
            new Vector3 (+offset, +offset, 0),
            new Vector3 (-offset, +offset, 0),
        };

        int[] triangles = {
            0, 3, 2,
            0, 2, 1,
        };

        MeshRenderer renderer =  this.gameObject.AddComponent<MeshRenderer>();
        renderer.material = new Material(Shader.Find("Specular"));

        this.gameObject.AddComponent<MeshFilter>();

        this.transform.position = new Vector3(0, 0, 0);

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.Optimize();
        mesh.RecalculateNormals();

        this.vertices = vertices;
        this.triangles = triangles;
        this.mesh = mesh;

        this.mesh.MarkDynamic();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Time.time);
        this.vertices[2].x = 1 + Mathf.Cos(Time.time * 3.5f) * .8f;
        this.vertices[2].y = 1 + Mathf.Sin(Time.time * 3.5f) * .8f;

        this.mesh.vertices = this.vertices;
        this.mesh.triangles = this.triangles;
    }
}