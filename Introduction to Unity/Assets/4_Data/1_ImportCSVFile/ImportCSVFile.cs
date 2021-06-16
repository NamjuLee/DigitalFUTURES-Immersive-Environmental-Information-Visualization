using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;
using System.IO;

public class ImportCSVFile : MonoBehaviour
{
    List<Vector3> vs = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {

        string fileName = "Broadway_predictedPM.csv";
        string path = Path.Combine(Environment.CurrentDirectory, @"Assets\7_Data\1_ImportCSVFile\", fileName);

        VisData(path);
    }

    void VisData(string path) {

        string data = GetDataFromCSVSecond(path);
        string[] datalist = data.Split('\n');

        int numData = 0;

        Vector3 cv = new Vector3(0, 0, 0);
        Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);

        for (int i = 1 ; i < datalist.Length; i++) {
                Debug.Log(datalist[i]);
            try{
                string[] row = datalist[i].Split(',');
                float xPos = (Convert.ToSingle(row[0]) * 100 ) + 7399;
                float yPos = (Convert.ToSingle(row[1]) * 100 ) - 4072;

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

        cv.x /= numData;
        cv.y /= numData;
        cv.z /= numData;
        float offset = (max.x - min.x);
        Camera.main.transform.position = new Vector3(cv.x + offset , cv.y + offset ,cv.z);
        Camera.main.transform.LookAt(cv, Vector3.up);
        
    }
    private string GetDataFromCSVFirst(string path) {
        return System.IO.File.ReadAllText(path);
    }
    private string GetDataFromCSVSecond(string path) {
        StreamReader reader = new StreamReader(path);
        return reader.ReadToEnd();
    }
}
