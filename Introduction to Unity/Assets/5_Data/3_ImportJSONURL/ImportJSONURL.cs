using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;
using System.IO;

public class ImportJSONURL : MonoBehaviour
{
    List<Vector3> vs = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        System.Random rnd = new System.Random();

        // http://www.njstudio.co.kr/main/project/2016_MobilityEnergyConsumptionMITMediaLab/index.html
        string path = "https://raw.githubusercontent.com/NamjuLee/data/master/Boston/thrid-place/ThridPlaceBoston.json";

        DataThirdPlace info = GetDataFromJSON(path);
        print(info);
        for(int i = 0 ; i < info.dataset.Count; ++i) {

            Color col = new Color( (float)(rnd.NextDouble() * 1) ,(float)(rnd.NextDouble() * 1), (float)(rnd.NextDouble() * 1));

            for(int j = 0 ;  j < info.dataset[i].data.Count; ++j ) {
                Pos pos =  info.dataset[i].data[j];
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = new Vector3(pos.lon * 100 ,0 , pos.lat * 100);
                sphere.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                sphere.name =  info.dataset[i].id;
                sphere.GetComponent<Renderer>().material.color = col;
            }
            
        }

    }
    private DataThirdPlace GetDataFromJSON(string url) {
        HttpWebRequest request =  (HttpWebRequest)WebRequest.Create(url);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        DataThirdPlace info = JsonUtility.FromJson<DataThirdPlace>(jsonResponse);
        return info;
    }

}

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