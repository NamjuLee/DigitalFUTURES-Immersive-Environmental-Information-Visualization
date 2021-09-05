using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;
using System;

public class US_GeoJsonWether : MonoBehaviour
{
    // you need to make your own API key, https://openweathermap.org/
    private string API_KEY = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"; // YOUR API
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

        List<string> namesCounties = new List<string>(){ // https://www.california-demographics.com/counties_by_population
            "Los Angeles",
            "San Diego",
            "Orange",
            "Riverside",
            "San Bernardino",
            "Santa Clara",
            "Alameda",
            "Sacramento",
            "Contra Costa",
            "Fresno",
            "Kern",
            "San Francisco",
            "Ventura",
            "San Mateo",
            "San Joaquin",
            "Stanislaus",
            "Sonoma",
            "Tulare",
            "Santa Barbara",
            "Solano",
            "Monterey",
            "Placer",
            "San Luis Obispo",
            "Santa Cruz",
            "Merced",
            "Marin",
            "Butte",
            "Yolo",
            "El Dorado",
            "Imperial",
            "Shasta",
            "Madera",
            "Kings",
            "Napa",
            "Humboldt",
            "Nevada",
            "Sutter",
            "Mendocino",
            "Yuba",
            "Lake",
            "Tehama",
            "San Benito",
            "Tuolumne",
            "Calaveras",
            "Siskiyou",
            "Amador",
            "Lassen",
            "Glenn",
            "Del Norte",
            "Colusa",
            "Plumas",
            "Inyo",
            "Mariposa",
            "Mono",
            "Trinity",
            "Modoc",
            "Sierra",
            "Alpine",
        };

		for(int i = 0; i < collection.features.Count; ++i) {
            string name = collection.features[i].properties["name"];

            // if the feature is one of counties in CA, then do the follwing executions
            bool has = namesCounties.Contains(name); 
            if(has){
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

                // Based on the name of counties from GeoJson, we will gat the temperature data to fill the color of the individual mesh
                // However, we need to know the fact that the query somethimes fails to get the data
                // Thus, we use the `try catch statement` to keep processing with the error from the query.   
                
                Color[] colors;
                Color col = new Color(0, 0, 0);
                try {
                    WeatherInfo info = GetWeather(DataByCityName(name));
                    col.r = info.main.temp * 0.01f;
                    name += " : " + info.main.temp.ToString() + " Celsius.";
                    colors = Enumerable.Range(0, vertices3D.Length).Select(i => col).ToArray();
                    print(info.main.temp);
                } catch (System.Exception) {    
                    name += " : NA";
                    colors = Enumerable.Range(0, vertices3D.Length).Select(i => col).ToArray();
                }


                // create the mesh 
                Mesh mesh = new Mesh {
                    vertices = vertices3D,
                    triangles = indices,
                    colors = colors
                };
                
                mesh.RecalculateNormals();
                mesh.RecalculateBounds();
                names.Add(name);

                // create gameobject with the name string
                GameObject gobj = new GameObject(name);
                MeshRenderer meshRenderer = gobj.AddComponent<MeshRenderer>();
                meshRenderer.material = new Material(Shader.Find("Sprites/Default"));
                
                MeshFilter filter = gobj.AddComponent<MeshFilter>();
                // update MeshFilter mesh by the mesh be generated before.
                filter.mesh = mesh;
            }
        }

        Debug.Log(names.Count);
        Debug.Log(names);
    }
    private HttpWebRequest DataByCityName(string cityName) {
    // http://api.openweathermap.org/data/2.5/weather?q=Clarke&appid=ea3a82254235cb5e21e70ff21ef6fe97", cityName, this.API_KEY
      return WebRequest.Create(String.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}&units=metric", cityName, this.API_KEY)) as HttpWebRequest;
    }
    private WeatherInfo GetWeather(HttpWebRequest request) {
    // this is a routine to download and parse data as string.
      HttpWebResponse response = (HttpWebResponse)request.GetResponse();
      StreamReader reader = new StreamReader(response.GetResponseStream());
      string jsonResponse = reader.ReadToEnd();
      Debug.Log(jsonResponse);
      // JsonUtility is a built-in feature to parse JSON data.
      WeatherInfo info = JsonUtility.FromJson<WeatherInfo>(jsonResponse);
      return info;
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
    
  [Serializable]
  public class Weather{
      public int id;
      public string main;
  }
  [Serializable]
  public class Main {
		public float temp;
    public float feels_like;
    public float temp_min;
    public float temp_max;
    public float pressure;
    public float humidity;
    public float sea_level;
    public float grnd_level;
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
      public Main main;
      public int timezone;
      public string name;
      public List<Weather> weather;
  }

}
