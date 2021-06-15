using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;
using System.IO;

public class ImportCSVURL : MonoBehaviour
{
    List<Vector3> vs = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        // https://www.youtube.com/watch?v=HDqZGzKdiA4
        
        string url = "https://raw.githubusercontent.com/NamjuLee/data/master/NYC/NY_street_lat_long.csv";
        string data = this.GetDataFromCSV(url);
        string[] datalist = data.Split('\n');

        string index = datalist[0];
        int numData = 0;
        Debug.Log(index);

        Vector3 cv = new Vector3(0, 0, 0);
        Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);

        List<Vector3> vs = new List<Vector3>();
        for (int i = 1 ; i < datalist.Length; i++) {

            try{
                string[] row = datalist[i].Split(',');
                float xPos = (Convert.ToSingle(row[3]) * 100 ) - 4000;
                float yPos = (Convert.ToSingle(row[4]) * 100 ) + 7000;

                cv.x += xPos;
                cv.z += yPos;

                if (min.x > xPos) min.x = xPos;
                if (min.y > yPos) min.y = yPos;

                if (max.x < xPos) max.x = xPos;
                if (max.y < yPos) max.y = yPos;


                Vector3 v = new Vector3(xPos, 0, yPos);
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = v;
                float scale = 0.1f;
                sphere.transform.localScale = new Vector3(scale, scale, scale);
                Debug.Log(i);
                
                vs.Add(v);

                numData++;
            } catch (System.Exception) {         
                Debug.Log(i);
            }

        }
        this.vs = vs;

        cv.x /= numData;
        cv.y /= numData;
        cv.z /= numData;

        float offset = (max.x - min.x);
        Camera.main.transform.position = new Vector3(cv.x + offset , cv.y + offset ,cv.z);
        Camera.main.transform.LookAt(cv, Vector3.up);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnDrawGizmos() {
        // Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        // for(int i = 0 ; i < this.vs.Count; ++i) Gizmos.DrawSphere(this.vs[i], 0.1f);
    }

    private string GetDataFromCSV(string url) {
            HttpWebRequest request =  (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            return reader.ReadToEnd(); // .ReadToEnd();
    }

}
