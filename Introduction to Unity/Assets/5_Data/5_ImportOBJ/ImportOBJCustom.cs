using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;
using System.IO;

public class ImportOBJCustom : MonoBehaviour
{
    List<Vector3> vs = new List<Vector3>();
    List<Color> vc = new List<Color>();
    // Start is called before the first frame update
    void Start()
    {
        // https://github.com/NamjuLee/data/blob/master/geometry/CFDSimulationOBJ/CFDSimulation.obj
        // https://github.com/NamjuLee/data/blob/master/geometry/CFDSimulationOBJ/CFDSimulationCustom.gif

        string url = "https://raw.githubusercontent.com/NamjuLee/data/master/geometry/CFDSimulationOBJ/CFDSimulationCustom.obj";
        string dataStrings = GetDataFromOBJ(url);
        print(dataStrings);
        VisData(dataStrings);
    }

    void VisData(string dataStrings) {
        string[] datalist = dataStrings.Split('\n');

        for(int i = 0 ; i < datalist.Length; ++i) {

            string[] row = datalist[i].Split(' ');
            if(row[0] == "v") {
                float x = (float)System.Convert.ToSingle(row[1]);
                float y = (float)System.Convert.ToSingle(row[2]);
                float z = (float)System.Convert.ToSingle(row[3]);
                Vector3 v = new Vector3(x, z, y);
                vs.Add(v);
            }

            if(row[0] == "vc") {
                int r = (int)System.Convert.ToInt16(row[1]);
                int g = (int)System.Convert.ToInt16(row[2]);
                int b = (int)System.Convert.ToInt16(row[3]);
  
                Color c = new Color( r / 255f, g / 255f, b / 255f);
                vc.Add(c);
            }
        }

        for(int i = 0 ; i < vs.Count; ++i){
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            go.transform.position = vs[i];
            go.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            go.GetComponent<Renderer>().material.color = vc[i];
        }
        
    }
    private string GetDataFromOBJ(string url) {
        HttpWebRequest request =  (HttpWebRequest)WebRequest.Create(url);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        return reader.ReadToEnd();
    }

}
