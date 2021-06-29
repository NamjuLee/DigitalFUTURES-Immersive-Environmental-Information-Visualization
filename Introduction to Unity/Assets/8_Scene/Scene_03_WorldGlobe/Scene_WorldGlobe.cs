using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;
using System.IO;

public class Scene_WorldGlobe  : MonoBehaviour {
  // api.openweathermap.org/data/2.5/weather?q={city name}&appid={e2e94ef6b8780e2e4500b1db42c49fde}
  private string API_KEY = "e2e94ef6b8780e2e4500b1db42c49fde"; // YOUR API
  // public string CityName = "Seoul";
  List<string> cityNames = new List<String>();
  // string CityName = "Manhattan";
  public float timer = 0.0f;
  public float minRemap = 1f;
  public float maxRemap = 40.0f;
  public float scale = 0.1f;

  GameObject globe;
  List<WeatherInfo> weatherList = new List<WeatherInfo>();
  List<GameObject> gameObjectList = new List<GameObject>();
  void Start() {

    this.globe = GameObject.Find("globe");

     cityNames.Add("Seoul");
     cityNames.Add("New York");
     cityNames.Add("London");
     cityNames.Add("Sydney");
     cityNames.Add("Shanghai");
     cityNames.Add("Mumbai");
     cityNames.Add("Paris");
     cityNames.Add("Houston");
     cityNames.Add("Cape Town");
     cityNames.Add("Tokyo");
     cityNames.Add("Rio Gallegos");
     cityNames.Add("Panama City");
     cityNames.Add("Los Angeles");
     cityNames.Add("Moscow");
     cityNames.Add("Christchurch");
     cityNames.Add("Santiago");
     cityNames.Add("Juazeiro");
     cityNames.Add("Lisbon");

    foreach(string name in this.cityNames) {
        // get data by the name of city;
        WeatherInfo info = GetWeather(DataByCityName(name));
        weatherList.Add(info);
    }

    for(int i = 0 ; i < weatherList.Count; ++i) {
        WeatherInfo info = weatherList[i];
        // https://openweathermap.org/current
        // print valuses
        Debug.Log(this.cityNames[i]);
        Debug.Log(info.coord.lat);
        Debug.Log(info.coord.lon);
        Debug.Log("id: " + info.id + ", Name: " + info.name );
        Debug.Log("temp: " + info.main.temp);
        Debug.Log("temp: " + info.main.temp);
        Debug.Log("main: " + info.weather[0].main);

        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = new Vector3(0, 0, 0);
        sphere.name = this.cityNames[i] + " : " + info.main.temp.ToString() + " Celsius.";
        
        int[] colorData =  NJSUnityUtility.ColorUtility.GetFalseColor(Remap (info.main.temp, minRemap, maxRemap, 0, 1));
        sphere.GetComponent<Renderer>().material.color = new Color(colorData[0]/255f, colorData[1]/255f, colorData[2]/255f);

        sphere.transform.localScale = new Vector3(info.main.temp * scale * 0.01f, info.main.temp * scale * 0.01f , info.main.temp * scale * 0.01f);

        Vector3 v = Projection(remapLong(info.coord.lon), remapLat(info.coord.lat * -1), 10f);
        sphere.transform.position = v;
        sphere.transform.SetParent(this.globe.transform);
        gameObjectList.Add(sphere);

    }

  }
  void Update() {
    
    for(int i = 0 ; i < this.gameObjectList.Count; ++i) {
        GameObject sphere = gameObjectList[i];
        WeatherInfo info = weatherList[i];
        sphere.transform.localScale = new Vector3(info.main.temp * scale * 0.01f, info.main.temp * scale * 0.01f , info.main.temp * scale * 0.01f);
    }
    this.globe.transform.Rotate(new Vector3(0, 1, 0) , 0.0631f);

  }
  // https://openweathermap.org/api
  //
  private HttpWebRequest DataByCityName(string cityName) {
    // http://api.openweathermap.org/data/2.5/weather?q=Clarke&appid=ea3a82254235cb5e21e70ff21ef6fe97", cityName, this.API_KEY
      return WebRequest.Create(String.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}&units=metric", cityName, this.API_KEY)) as HttpWebRequest;
  }
  private WeatherInfo GetWeather(HttpWebRequest request) {
    // this is a routine to download and parse data as string.
      HttpWebResponse response = (HttpWebResponse)request.GetResponse();
      StreamReader reader = new StreamReader(response.GetResponseStream());
      string jsonResponse = reader.ReadToEnd();
      // JsonUtility is a built-in feature to parse JSON data.
      WeatherInfo info = JsonUtility.FromJson<WeatherInfo>(jsonResponse);
      return info;
  }
  private float Remap(float CValue, float OldMin, float OldMax, float NewMin, float NewMax){
    float Nvalue = (((CValue - OldMin) * (NewMax - NewMin)) / (OldMax - OldMin)) + NewMin;
    return Nvalue;
  }
  private float remapLong(float CValue) { 
      return (CValue + 180f) * Mathf.PI * 2f / 360f; 
  }
  private float remapLat(float CValue)  { 
      return (CValue + 90f) * Mathf.PI / 180f; 
  }
  private Vector3 Projection(float lon, float lat, float scale) {
        float x = Mathf.Cos(lon) * Mathf.Sin(lat) * scale;
        float y = Mathf.Sin(lon) * Mathf.Sin(lat) * scale;
        float z = Mathf.Cos(lat) * scale;
      return new Vector3(x, z, y);
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

/*
{
	"coord": {
		"lon": 126.9778,
		"lat": 37.5683
	},
	"weather": [{
		"id": 804,
		"main": "Clouds",
		"description": "overcast clouds",
		"icon": "04n"
	}],
	"base": "stations",
	"main": {
		"temp": 288.01,
		"feels_like": 286.73,
		"temp_min": 286.38,
		"temp_max": 289.88,
		"pressure": 1010,
		"humidity": 45,
		"sea_level": 1010,
		"grnd_level": 1003
	},
	"visibility": 10000,
	"wind": {
		"speed": 1.5,
		"deg": 71,
		"gust": 1.58
	},
	"clouds": {
		"all": 100
	},
	"dt": 1621447837,
	"sys": {
		"type": 1,
		"id": 5509,
		"country": "KR",
		"sunrise": 1621455554,
		"sunset": 1621507095
	},
	"timezone": 32400,
	"id": 1835848,
	"name": "Seoul",
	"cod": 200
}
*/