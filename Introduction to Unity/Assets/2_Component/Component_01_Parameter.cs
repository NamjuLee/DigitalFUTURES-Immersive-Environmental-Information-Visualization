using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Component_01_Parameter : MonoBehaviour
{
    // Class diagram in unity
    // https://preview.redd.it/j94mc5wgctg21.png?width=950&format=png&auto=webp&s=6f04153026cac6d0062f543691ce46ca69341472
    // Start is called before the first frame update
    void Start()
    {
        // Print values;

        Debug.Log("Tag: " + this.tag );
        Debug.Log("Name: " + this.name );

        Debug.Log("transform: " + this.transform.ToString());
        Debug.Log("position: " + this.transform.position.ToString());
        Debug.Log("localScale: " + this.transform.localScale.ToString());
        Debug.Log("localRotation: " + this.transform.localRotation.ToString());

        // get component
        MeshFilter mf = this.GetComponent<MeshFilter>();
        
        Vector3[] vertices = mf.mesh.vertices;
        Debug.Log("Num of Vertices: " + vertices.Length.ToString());

       for (var i = 0; i < vertices.Length; i++)
        {
            Debug.Log(vertices[i]);
        }

        // get component and assign value
        Renderer mr = this.GetComponent<Renderer>();
        mr.material.SetColor("_Color", Color.red); // https://docs.unity3d.com/ScriptReference/Material.SetColor.html
        Debug.Log("color: " + mr.material.color);
        
    }

}
