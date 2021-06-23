using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mesh_02_ProcedualCubeMesh : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        // Vector3[] vertices = {
        // new Vector3 (0, 0, 0),
        // new Vector3 (1, 0, 0),
        // new Vector3 (1, 1, 0),
        // new Vector3 (0, 1, 0),
        // new Vector3 (0, 1, 1),
        // new Vector3 (1, 1, 1),
        // new Vector3 (1, 0, 1),
        // new Vector3 (0, 0, 1),

        // };

        // int[] triangles = {
        //     0, 2, 1, //face front
        //     0, 3, 2,
        //     2, 3, 4, //face top
        //     2, 4, 5,
        //     1, 2, 5, //face right
        //     1, 5, 6,
        //     0, 7, 4, //face left
        //     0, 4, 3,
        //     5, 4, 7, //face back
        //     5, 7, 6,
        //     0, 6, 7, //face bottom
        //     0, 1, 6
        // };

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

        this.gameObject.AddComponent<MeshFilter>();

        this.transform.position = new Vector3(0, 0, 0);

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        /*
        // create new colors array where the colors will be created.
        Color[] colors = new Color[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
            colors[i] = Color.Lerp(Color.red, Color.green, vertices[i].y);
        mesh.colors = colors;
        */
         mesh.colors = colors;


        mesh.Optimize ();
        mesh.RecalculateNormals();

        }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(1, 0, 0), 0.15f );
        this.transform.Rotate(new Vector3(0, 1, 0), 0.25f );
        this.transform.Rotate(new Vector3(0, 0, 1), 0.35f );
    }
}
