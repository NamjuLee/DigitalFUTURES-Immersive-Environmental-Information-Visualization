using UnityEngine;

public class Mesh_01_ProcedualCubeTriMesh : MonoBehaviour
{
    // declare a list to store the color of vertices
    Vector3[] vertices;
    // declare a list to store the connections of vertices for mesh
    int[] triangles;
    Mesh mesh;
    void Start() {

        // the size of rectangle
        float offset =1f;
        
        // vertices in order
        // 4 - 3
        // |   |
        // 0 - 1
        Vector3[] vertices = {
            new Vector3 (-offset, -offset, 0),
            new Vector3 (+offset, -offset, 0),
            new Vector3 (+offset, +offset, 0),
            new Vector3 (-offset, +offset, 0),
        };
        // connectivities for triangles
        // the numbers belows indicate the index of vertices
        int[] triangles = {
            0, 3, 2,
            0, 2, 1,
        };

        // MeshRenderer Component is for rendering a mesh.
        MeshRenderer renderer =  this.gameObject.AddComponent<MeshRenderer>();
        // Apply a material
        renderer.material = new Material(Shader.Find("Specular"));

        // Add MeshFilter Component, which an essential component for mesh, to this gameobject
        this.gameObject.AddComponent<MeshFilter>();

        this.transform.position = new Vector3(0, 0, 0);

        // get mesh from MeshFilter component
        Mesh mesh = GetComponent<MeshFilter>().mesh;

        // update the vertices and connections to the mesh object
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.Optimize();
        mesh.RecalculateNormals();

        // also we want to update the vertices and connections data in the scope of this class, so that we are able to update the position while looping(Update()).
        this.vertices = vertices;
        this.triangles = triangles;
        this.mesh = mesh;

        this.mesh.MarkDynamic();
    }

    // Update is called once per frame
    void Update()
    {
        // update the position of vertices
        this.vertices[2].x = 1 + Mathf.Cos(Time.time * 3.5f) * .8f;
        this.vertices[2].y = 1 + Mathf.Sin(Time.time * 3.5f) * .8f;

        // update the data to mesh object
        this.mesh.vertices = this.vertices;
        this.mesh.triangles = this.triangles;
    }
}