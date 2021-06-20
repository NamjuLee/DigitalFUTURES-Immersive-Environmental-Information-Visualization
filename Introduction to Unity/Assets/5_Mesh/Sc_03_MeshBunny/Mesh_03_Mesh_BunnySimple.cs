using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Mesh_03_Mesh_BunnySimple : MonoBehaviour
{
    // Start is called before the first frame update

    List<Vector3> vs = new List<Vector3>();
    List<int> fs = new List<int>();
    
    // https://en.wikipedia.org/wiki/Wavefront_.obj_file

    void Start()
    {
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
                GetMesh(req.downloadHandler.text);
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
    void GetMesh(string text) {
        string[] txts =  text.Split('\n');

        Debug.Log("txts : " + txts.Length.ToString());

        // int count = 0;
        for (int i = 0 ; i < txts.Length; i++) {
            string[] subText = (txts[i].Split(' '));

            if(subText[0] == "v") {
                vs.Add(new Vector3(  float.Parse(subText[1]), float.Parse(subText[2]), float.Parse(subText[3])));
            } 

            if(subText[0] == "f") {
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

        MeshRenderer renderer =  this.gameObject.AddComponent<MeshRenderer>();
        renderer.material = new Material(Shader.Find("Specular"));

        this.gameObject.AddComponent<MeshFilter>();
        this.transform.position = new Vector3(0, 0, 0);
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        mesh.vertices = this.vs.ToArray();
        mesh.triangles = this.fs.ToArray();
        mesh.Optimize();
        mesh.RecalculateNormals();



    }
    void Update()
    {
        
    }

    void OnDrawGizmos() {

        // Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        // for(int i = 0 ; i < this.vs.Count; ++i) {
        //     Gizmos.DrawSphere(this.vs[i], 0.001f);
        // }

    }

}
