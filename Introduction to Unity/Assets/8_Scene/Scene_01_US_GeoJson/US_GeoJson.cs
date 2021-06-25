using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;

public class US_GeoJson : MonoBehaviour
{
    // declare a list of list(2D array) to store the vertices
    List<List<Vector3>> vs = new List<List<Vector3>>();
    // declare a list to store the county names
    List<string> names = new List<string>();
    void Start() {
        
        // https://en.wikipedia.org/wiki/GeoJSON
        string url = "https://raw.githubusercontent.com/python-visualization/folium/master/tests/us-counties.json";
        string data = this.GetDataFromJSON(url);
        Debug.Log(data);

        // a custom function to parse GeoJSON data.
		NJSUnityUtility.GeoJSON.FeatureCollection collection = NJSUnityUtility.GeoJSON.GeoJSONObject.Deserialize(data);
		Debug.Log(collection);


		for(int i = 0; i < collection.features.Count; ++i) {
            // get vector data based on features
            List<NJSUnityUtility.GeoJSON.PositionObject> pos = collection.features[i].geometry.AllPositions();
            List<Vector2> vertices2 = new List<Vector2>();
            
            // construct vector out of longitude(x) and latitude(y) 
            for(int j = 0 ; j < pos.Count -1; ++j) {
                NJSUnityUtility.GeoJSON.PositionObject p = pos[j];

                if (j < pos.Count - 1) {
                    List<Vector3> vvv = new List<Vector3>(){
                        new Vector3(p.longitude , p.latitude, 0),
                        new Vector3(pos[j+ 1].longitude , pos[j+1].latitude, 0)
                    };
                    vs.Add(vvv);
                }
             
                Vector3 v = new Vector3(pos[j].longitude, pos[j].latitude, 0);
                Vector2 v2 = new Vector2(p.longitude , p.latitude );
                vertices2.Add(v2);
            }

            Vector3[] vertices3D = new Vector3[vertices2.Count];
            for (int j = 0; j<vertices3D.Length; j++) {
                vertices3D[j] = new Vector3(vertices2[j].x, vertices2[j].y, 0);
            }
    
            // Use the triangulator to get indices for creating triangles
            // because we have no idea about correct triangle from arbitrary boundary condition
            var triangulator = new NJSUnityUtility.Triangulator(vertices2.ToArray());
            int[] indices =  triangulator.Triangulate();

            // get a random color by the Unity built-in function
            Color col = UnityEngine.Random.ColorHSV();
            Color[] colors = Enumerable.Range(0, vertices3D.Length).Select(i => col).ToArray();

            // create the mesh 
            Mesh mesh = new Mesh {
                vertices = vertices3D,
                triangles = indices,
                colors = colors
            };
            
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
    
            string name = collection.features[i].properties["name"];
            names.Add(name);

            // create gameobject with the name string
            GameObject gobj = new GameObject(name);
            MeshRenderer meshRenderer = gobj.AddComponent<MeshRenderer>();
            meshRenderer.material = new Material(Shader.Find("Sprites/Default"));
            
            MeshFilter filter = gobj.AddComponent<MeshFilter>();
            // update MeshFilter mesh by the mesh be generated before.
            filter.mesh = mesh; 
        }

        Debug.Log(names.Count);
        Debug.Log(names);
    }
    void OnDrawGizmos() {
        // visualize the boundary of each counties in US
        // Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        // Color color = new Color(1f, 0, 1.0f);
        // for(int y = 0 ; y < this.vs.Count; ++y) {
        //     Gizmos.DrawLine(this.vs[y][0], this.vs[y][1]);
        //     Debug.DrawLine(this.vs[y][0], this.vs[y][1], color);
        // }
    }
    private string GetDataFromJSON(string url) {
        // these are a routine to download and parse data as string.
        HttpWebRequest request =  (HttpWebRequest)WebRequest.Create(url);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        // return the string data which will be converted by the pre-cooked GeoJson data structure
        return jsonResponse;
    }
}