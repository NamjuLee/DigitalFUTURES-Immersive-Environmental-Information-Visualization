using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;
using System.IO;

public class US_GeoJson : MonoBehaviour
{
    // Start is called before the first frame update
    private string API_KEY = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"; // YOUR API
    List<List<Vector3>> vs = new List<List<Vector3>>();
    List<string> names = new List<string>();
    void Start()
    {
        string url = "https://raw.githubusercontent.com/python-visualization/folium/master/tests/us-counties.json";
        string data = this.GetDataFromJSON(url);
        Debug.Log(data);

		NJSUnityUtility.GeoJSON.FeatureCollection collection = NJSUnityUtility.GeoJSON.GeoJSONObject.Deserialize(data);

		Debug.Log(collection);


		for(int i = 0; i < collection.features.Count; ++i) {
					//    if (i > 100) return;	
            if(i > 1) {
                // return;
            }

            List<NJSUnityUtility.GeoJSON.PositionObject> pos = collection.features[i].geometry.AllPositions();

            // List<Vector3> vertices = new List<Vector3>();
            List<Vector2> vertices2 = new List<Vector2>();
            // vertices2.Add(new Vector2(pos[0].longitude + 0.1f, pos[0].latitude + 0.1f));
            
            for( int j = 0 ; j < pos.Count -1; ++j) {
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

                // vertices.Add(v);
                vertices2.Add(v2);
            }

            Vector3[] vertices3D = new Vector3[vertices2.Count];
            for (int j = 0; j<vertices3D.Length; j++) {
                vertices3D[j] = new Vector3(vertices2[j].x, vertices2[j].y, 0);
            }


    
            // Use the triangulator to get indices for creating triangles
            var triangulator = new NJSUnityUtility.Triangulator(vertices2.ToArray());
            var indices =  triangulator.Triangulate();
            
            // // Generate a color for each vertex
            // var colors = Enumerable.Range(0, vertices3D.Length)
            //     .Select(i => UnityEngine.Random.ColorHSV())
            //     .ToArray();

            var col = UnityEngine.Random.ColorHSV();
            var colors = Enumerable.Range(0, vertices3D.Length)
                .Select(i => col)
                .ToArray();

            // Create the mesh
            var mesh = new Mesh {
                vertices = vertices3D,
                triangles = indices,
                colors = colors
            };
            
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
    
            string name = collection.features[i].properties["name"];
            names.Add(name);

            GameObject gobj = new GameObject(name);

            // Set up game object with mesh;
            var meshRenderer = gobj.AddComponent<MeshRenderer>();
            meshRenderer.material = new Material(Shader.Find("Sprites/Default"));
            
            var filter = gobj.AddComponent<MeshFilter>();
            filter.mesh = mesh;
        }


        Debug.Log(names.Count);
        Debug.Log(names);
    }

      private HttpWebRequest DataByCityName(string cityName) {
    // http://api.openweathermap.org/data/2.5/weather?q=Clarke&appid=ea3a82254235cb5e21e70ff21ef6fe97", cityName, this.API_KEY
      return WebRequest.Create(String.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}", cityName, this.API_KEY)) as HttpWebRequest;
  }
  private WeatherInfo GetWeather(HttpWebRequest request) {
      HttpWebResponse response = (HttpWebResponse)request.GetResponse();
      StreamReader reader = new StreamReader(response.GetResponseStream());
      string jsonResponse = reader.ReadToEnd();
    //   Debug.Log(jsonResponse);
      WeatherInfo info = JsonUtility.FromJson<WeatherInfo>(jsonResponse);
      return info;
  }
    void OnDrawGizmos() {
        // Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        //  Color color = new Color(1f, 0, 1.0f);
        // for(int y = 0 ; y < this.vs.Count; ++y) {
        //         Gizmos.DrawLine(this.vs[y][0], this.vs[y][1]);
        //         Debug.DrawLine(this.vs[y][0], this.vs[y][1], color);

        // }
    }
    private string GetDataFromJSON(string url) {
        HttpWebRequest request =  (HttpWebRequest)WebRequest.Create(url);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        // Debug.Log(jsonResponse);
        // JSONObject info = JsonUtility.FromJson<JSONObject>(jsonResponse);
        return jsonResponse;
    }

  [Serializable]
  public class Weather{
      public int id;
      public string main;
  }

  [Serializable]
  public class Coord{
      public float lon;
      public float lat;
  }

  [Serializable]
  public class WeatherInfo{
      public Coord coord;
      public int id;
      public string main;
      public int timezone;
      public List<Weather> weather;
  }
}