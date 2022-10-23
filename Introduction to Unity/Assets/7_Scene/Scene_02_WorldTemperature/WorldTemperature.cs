using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;
using System.IO;

public class WorldTemperature  : MonoBehaviour {
  // api.openweathermap.org/data/2.5/weather?q={city name}&appid={e2e94ef6b8780e2e4500b1db42c49fde}
  private string API_KEY = "e2e94ef6b8780e2e4500b1db42c49fde"; // YOUR API
  // public string CityName = "Seoul";
  List<string> cityNames = new List<String>();
  // string CityName = "Manhattan";
  public float timer = 0.0f;
  public float minRemap = 1f;
  public float maxRemap = 40.0f;

  List<WeatherInfo> weatherList = new List<WeatherInfo>();
  List<GameObject> gameObjectList = new List<GameObject>();
  void Start() {

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

        GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cylinder.transform.position = new Vector3(0, 0, 0);
        cylinder.name = this.cityNames[i] + " : " + info.main.temp.ToString() + " Celsius.";
        
        int[] colorData =  NJSUnityUtility.ColorUtility.GetFalseColor(Remap (info.main.temp, minRemap, maxRemap, 0, 1));
        cylinder.GetComponent<Renderer>().material.color = new Color(colorData[0]/255f, colorData[1]/255f, colorData[2]/255f);

        // update the location of the cylinder by the latitude and longitude searched by the keyword
        // cylinder.transform.Rotate(new Vector3(0, 0, 1), 45f);
        cylinder.transform.localScale = new Vector3(2, info.main.temp , 2);
        cylinder.transform.position = new Vector3(info.coord.lon, info.main.temp , info.coord.lat);
        // this.cylinder = cylinder;
        gameObjectList.Add(cylinder);

    }

  }
  public static float Remap(float CValue, float OldMin, float OldMax, float NewMin, float NewMax)
  {
    float Nvalue = (((CValue - OldMin) * (NewMax - NewMin)) / (OldMax - OldMin)) + NewMin;
    return Nvalue;
  }
  void Update() {
    
    // every 100 frame, we want to update 
    if(this.timer++ > 100) {

      for(int i = 0 ; i < this.gameObjectList.Count; ++i) {
        GameObject cylinder = gameObjectList[i];
        WeatherInfo data = weatherList[i];

        int[] colorData =  NJSUnityUtility.ColorUtility.GetFalseColor(Remap (data.main.temp, minRemap, maxRemap, 0, 1));
        cylinder.GetComponent<Renderer>().material.color = new Color(colorData[0]/255f, colorData[1]/255f, colorData[2]/255f);

      }
      
    }

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
      Debug.Log(jsonResponse);
      // JsonUtility is a built-in feature to parse JSON data.
      WeatherInfo info = JsonUtility.FromJson<WeatherInfo>(jsonResponse);
      return info;
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