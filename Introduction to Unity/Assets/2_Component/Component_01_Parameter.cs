using UnityEngine;

public class Component_01_Parameter : MonoBehaviour
{
    // Class diagram in unity
    // https://preview.redd.it/j94mc5wgctg21.png?width=950&format=png&auto=webp&s=6f04153026cac6d0062f543691ce46ca69341472

    void Start()
    {
        // Each components have their own params and functions we can execute or use the values of parameters.
        // `this` indicates the context, meaning that this points the class itself.
        Debug.Log("Tag: " + this.tag );
        // Also we can use `print(values)` rather than Debug.log(values).
        print("Name: " + this.name );

        Debug.Log("transform: " + this.transform.ToString());
        Debug.Log("position: " + this.transform.position.ToString());
        Debug.Log("localScale: " + this.transform.localScale.ToString());
        Debug.Log("localRotation: " + this.transform.localRotation.ToString());

        // get component 
        MeshFilter mf = this.GetComponent<MeshFilter>();
        
        // access vertices(a set of vectors) from MeshFilter which is one of important component to render the geometrical shapes we can see on the viewport.
        Vector3[] vertices = mf.mesh.vertices;

        // .Log function needs string(Data type) to print, thus we need to convert int type to string(.ToString()) type to print. 
        Debug.Log("Num of Vertices: " + vertices.Length.ToString());

       for (var i = 0; i < vertices.Length; i++) {
            Debug.Log(vertices[i]);
        }

        // get renderer component to shift the color value of the material.
        Renderer renderer = this.GetComponent<Renderer>();
        renderer.material.SetColor("_Color", Color.red); // https://docs.unity3d.com/ScriptReference/Material.SetColor.html
        Debug.Log("color: " + renderer.material.color);
        
    }

}
