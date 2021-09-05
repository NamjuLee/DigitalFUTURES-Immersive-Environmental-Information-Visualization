using UnityEngine;

public class Mesh_02_ProcedualCubeMesh : MonoBehaviour
{
    void Start()
    {
        
        float offset = 0.5f;

        Vector3[] vertices = {
            // front 
            new Vector3 (-offset, -offset, -offset),
            new Vector3 (+offset, -offset, -offset),
            new Vector3 (+offset, +offset, -offset),
            new Vector3 (-offset, +offset, -offset),

            // back
            new Vector3 (-offset, -offset, +offset),
            new Vector3 (+offset, -offset, +offset),
            new Vector3 (+offset, +offset, +offset),
            new Vector3 (-offset, +offset, +offset),

            // Left 
            new Vector3 (-offset, -offset, -offset),
            new Vector3 (-offset, -offset, +offset),
            new Vector3 (-offset, +offset, +offset),
            new Vector3 (-offset, +offset, -offset),

            // Right 
            new Vector3 (+offset, -offset, -offset),
            new Vector3 (+offset, -offset, +offset),
            new Vector3 (+offset, +offset, +offset),
            new Vector3 (+offset, +offset, -offset),

            // Bottom 
            new Vector3 (-offset, -offset, -offset),
            new Vector3 (-offset, -offset, +offset),
            new Vector3 (+offset, -offset, +offset),
            new Vector3 (+offset, -offset, -offset),

            // Top
            new Vector3 (-offset, +offset, -offset),
            new Vector3 (-offset, +offset, +offset),
            new Vector3 (+offset, +offset, +offset),
            new Vector3 (+offset, +offset, -offset),
        };

        // each colors in the list are the color of vertices in order.
        Color[] colors = {
            Color.red,
            Color.red,
            Color.red,
            Color.red,
            
            Color.blue,
            Color.blue,
            Color.blue,
            Color.blue,

            Color.green,
            Color.green,
            Color.green,
            Color.green,

            Color.yellow,
            Color.yellow,
            Color.yellow,
            Color.yellow,

            Color.magenta,
            Color.magenta,
            Color.magenta,
            Color.magenta,

            Color.cyan,
            Color.cyan,
            Color.cyan,
            Color.cyan,
        };           
        // // create new colors array where the colors will be created.
        // colors = new Color[vertices.Length];
        // for (int i = 0; i < vertices.Length; i++) colors[i] = Color.Lerp(Color.red, Color.green, vertices[i].y);

        // this numbers below indicate the index of vertices
        int[] triangles = {
            // front
            0, 3, 2,
            0, 2, 1,

            // back
            4, 6, 7,
            4, 5, 6,
            
            // left
            8, 10, 11,
            8, 9, 10,

            // right
            12, 15, 14,
            12, 14, 13,

            // bottom
            16, 19, 18,
            16, 18, 17,

            // top
            20, 22, 23,
            20, 21, 22,
        };

        
        MeshRenderer renderer =  this.gameObject.AddComponent<MeshRenderer>();
        // renderer.material = new Material(Shader.Find("Specular"));
        renderer.material = new Material(Shader.Find("Particles/Standard Surface")); // https://docs.unity3d.com/ScriptReference/Shader.Find.html
        
        // Add MeshFilter to this gameobject
        this.gameObject.AddComponent<MeshFilter>();
 
        // get the mesh from MeshFilter component, and apply the vertices, triangles, and colors values
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;
        mesh.Optimize ();
        mesh.RecalculateNormals();

        // reset the position
        this.transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update() {

        // update the rotation of this gameobject
        this.transform.Rotate(new Vector3(1, 0, 0), 0.15f );
        this.transform.Rotate(new Vector3(0, 1, 0), 0.25f );
        this.transform.Rotate(new Vector3(0, 0, 1), 0.35f );

    }
}
