using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Mesh_03_Mesh_BunnySimple : MonoBehaviour
{
    // declare a list to store the vertices
    List<Vector3> vs = new List<Vector3>();
    // declare a list to store the connections of vertices
    List<int> fs = new List<int>();
    
    void Start()
    {
        // https://en.wikipedia.org/wiki/Wavefront_.obj_file
        // https://codepen.io/NJStudio/pen/zYrYGOG
        // https://codepen.io/NJStudio/pen/QWyLraB
        DownloadFile("https://raw.githubusercontent.com/NamjuLee/data/master/geometry/bunny/bunnySimple.obj");
    }
    // https://theslidefactory.com/loading-3d-models-from-the-web-at-runtime-in-unity/
    public void DownloadFile(string url) {
        StartCoroutine(GetFileRequest(url, (UnityWebRequest req) => {
            if (req.result == UnityWebRequest.Result.ConnectionError || req.result == UnityWebRequest.Result.ProtocolError){
                // Log any errors that may happen
                Debug.Log($"{req.error} : {req.downloadHandler.text}");
            } else
            {
                // Success!
                // Debug.Log(req.downloadHandler.text);
                BuildMesh(req.downloadHandler.text);
            }
        }));
    }
    IEnumerator GetFileRequest(string url, Action<UnityWebRequest> callback) {
        using(UnityWebRequest req = UnityWebRequest.Get(url))
        {
            yield return req.SendWebRequest();
            callback(req);
        }
    }
    void BuildMesh(string text) {
        // split a text into multiple line based on linebreaks, the return value will be the array of strings
        string[] txts =  text.Split('\n');

        Debug.Log("txts : " + txts.Length.ToString());

        // int count = 0;
        for (int i = 0 ; i < txts.Length; i++) {
            // split the string by ' '(empty space) into array of string.
            string[] subText = (txts[i].Split(' '));
             // "v" means Geometric vertex.
            if(subText[0] == "v") {
                vs.Add(new Vector3(  float.Parse(subText[1]), float.Parse(subText[2]), float.Parse(subText[3])));
            } 

            // "f" means the index of vertices for triangles.
            if(subText[0] == "f") {
                // convert data type from string to int
                int indexA = int.Parse(subText[1]) -1;
                int indexB = int.Parse(subText[2]) -1;
                int indexC = int.Parse(subText[3]) -1;

                fs.Add(indexA);
                fs.Add(indexB);
                fs.Add(indexC);
            }

 
        }

        Debug.Log(this.vs.Count);
        Debug.Log(this.fs.Count / 3);

        // add MeshRenderer for visualize mesh on the camera view
        MeshRenderer renderer =  this.gameObject.AddComponent<MeshRenderer>();
        renderer.material = new Material(Shader.Find("Specular"));

        // add MeshFilter to update vertices, and triangles to the mesh in MeshFilter component
        MeshFilter mf = this.gameObject.AddComponent<MeshFilter>();
        this.transform.position = new Vector3(0, 0, 0);
        Mesh mesh = mf.mesh;
        mesh.Clear();
        mesh.vertices = this.vs.ToArray();
        mesh.triangles = this.fs.ToArray();
        mesh.Optimize();
        mesh.RecalculateNormals();

    }
     void OnDrawGizmos() {
        // Visualze vertices on the scene
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        for(int i = 0 ; i < this.vs.Count; ++i) {
            Gizmos.DrawSphere(this.vs[i], 0.001f);
        }

    }

}
