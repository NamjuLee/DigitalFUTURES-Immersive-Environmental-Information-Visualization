using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;
using System.IO;

public class ImportJSONURL : MonoBehaviour
{
    // declare a list to store vectors which represent data visually
    List<Vector3> vs = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        // prepare for the random function to generate random colors for data
        System.Random rnd = new System.Random();

        // http://www.njstudio.co.kr/main/project/2016_MobilityEnergyConsumptionMITMediaLab/index.html
        string path = "https://raw.githubusercontent.com/NamjuLee/data/master/Boston/thrid-place/ThridPlaceBoston.json";
        // https://en.wikipedia.org/wiki/JSON
        // https://jsonlint.com/

        // a key feature to download the data by URL
        DataThirdPlace info = GetDataFromJSON(path);
        print(info);

        for(int i = 0 ; i < info.dataset.Count; ++i) {

            // create a random color for representing a group of data.
            Color col = new Color( (float)(rnd.NextDouble() * 1) ,(float)(rnd.NextDouble() * 1), (float)(rnd.NextDouble() * 1));

            for(int j = 0 ;  j < info.dataset[i].data.Count; ++j ) {
                Pos pos =  info.dataset[i].data[j];
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = new Vector3(pos.lon * 100 ,0 , pos.lat * 100);
                sphere.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                sphere.name =  info.dataset[i].id;
                // apply the color we prepared for
                sphere.GetComponent<Renderer>().material.color = col;
            }
            
        }

    }
    private DataThirdPlace GetDataFromJSON(string url) {
        // these are a routine to download and parse data as string.
        HttpWebRequest request =  (HttpWebRequest)WebRequest.Create(url);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        // JsonUtility is a built-in feature to parse JSON data.
        DataThirdPlace info = JsonUtility.FromJson<DataThirdPlace>(jsonResponse);
        return info;
    }

}

// these classes are pre-cooked classes representing the structure of JSON
// it could be complicated for those who are not enough familiar with data structure.
// but, you could have ideas and senses the structure by comparing it with the raw json data below.
[Serializable]
class Pos {
    public float lat;
    public float lon;
}
[Serializable]
class DataSet {
    public string id;
    public List<Pos> data;
}

[Serializable]
class DataThirdPlace {
    [SerializeField]
    public List<DataSet> dataset;
}

/*

{ "dataset" : [
	{
		"id": "restaurant",
		"data": [
			{
			"lat": 42.29722379999999,
			"lon": -71.060441299999994
		}, {
			"lat": 42.335621000000003,
			"lon": -71.070272799999998
		}, {
			"lat": 42.323405999999999,
			"lon": -71.103567599999991
		}, {
			...
		}, {
			"lat": 42.336646700000003,
			"lon": -71.089406699999998
		}]
	} ,
	{
        "id": "subway_station",
    "data": 
	    [
	        {
	            "lat": 42.3733705, 
	            "lon": -71.1189594
	        }, 
	        {
	            "lat": 42.39542730000001, 
				"lon": -71.08290740000001
			}, 
				....
			{
				"lat": 42.3698255, 
				"lon": -71.0688265
			}
		]
	}
]}

*/