using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;
using System.IO;

public class RestAPIWeatherController  : MonoBehaviour {
  // api.openweathermap.org/data/2.5/weather?q={city name}&appid={e2e94ef6b8780e2e4500b1db42c49fde}
  private string API_KEY = "e2e94ef6b8780e2e4500b1db42c49fde"; // YOUR API
  string CityName = "Seoul";
  // string CityName = "New York";
  // string CityName = "Manhattan";
  public float timer = 0.0f;

  // we can apply the height of cylinder by the temperature of the designated place.
  GameObject cylinder;
  void Start() {
    // get data by the name of city;
    WeatherInfo info = GetWeather(DataByCityName(this.CityName));

    // https://openweathermap.org/current
    // print valuses
    Debug.Log(info.coord.lat);
    Debug.Log(info.coord.lon);
    Debug.Log("id: " + info.id + ", Name: " + info.name );
    Debug.Log("temp: " + info.main.temp);
    Debug.Log("temp: " + info.main.temp);
    Debug.Log("main: " + info.weather[0].main);

    GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
    cylinder.transform.position = new Vector3(0, 0, 0);
    cylinder.name = this.CityName;
    cylinder.GetComponent<Renderer>().material.color = Color.blue;

    // update the loation of the sylinder by the latitude and longitude searched by the keyword
    // cylinder.transform.Rotate(new Vector3(0, 0, 1), 45f);
    cylinder.transform.position = new Vector3(info.coord.lon, cylinder.transform.position.y , info.coord.lat);
    this.cylinder = cylinder;

  }
  void Update() {
    
    // every 100 frame, we want to update 
    if(this.timer++ > 100) {
      this.timer = 0;
      Debug.Log("updating...");

      WeatherInfo info = GetWeather(DataByCityName(this.CityName));
      Debug.Log(info.coord.lat);
      Debug.Log(info.coord.lon);
      Debug.Log("id: " + info.id + ", Name: " + info.name );
      Debug.Log("feels like: " + info.main.feels_like);
      Debug.Log("main: " + info.weather[0].main);
      // update the scale factor by the temperature searched by the keyword
      this.cylinder.transform.localScale = new Vector3(1, (float)info.main.temp * 0.1f, 1);
      
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