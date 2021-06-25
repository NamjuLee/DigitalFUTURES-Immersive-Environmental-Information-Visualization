using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class ImportCSVFile : MonoBehaviour
{
    // declare a list to store vectors which represent data visually
    List<Vector3> vs = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        // prepare for the local path of data(CSV)
        string fileName = "Broadway_predictedPM.csv";
        string path = Path.Combine(Environment.CurrentDirectory, @"Assets\5_Data\1_ImportCSVFile\Data\", fileName);

        // actual processing happens in the function
        VisData(path);
    }

    void VisData(string path) {

        string data = GetDataFromCSVSecond(path);
        // split a text into multiple line based on linebreaks, the return value will be the array of strings
        string[] datalist = data.Split('\n');

        int numData = 0;

        // the cv vector below is the dummy for computing the center of data.
        // the min vector below is the dummy for computing the min value in space of data.
        // the max vector below is the dummy for computing the max value in space of data.
        Vector3 cv = new Vector3(0, 0, 0);
        Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);

        // the loop starts from line 1, because line 0 is the index of data. we can see the file in the data folder!
        for (int i = 1 ; i < datalist.Length; i++) {
            Debug.Log(datalist[i]);

            // data is always unpredictable, but we do not want to break this process even if the data is wrong.
            // thus, we can use `try catch` statement to loop through till the end of data.
            try{
                // https://en.wikipedia.org/wiki/Comma-separated_values
                // CSV(Comma-separated values) could be break down by "," .

                string[] row = datalist[i].Split(',');
                // the data type of All number from CSV is basically string, not float, int, or double which are number to compute.
                // thus, we need to cast string type to number type to compute.
                float xPos = (Convert.ToSingle(row[0]) * 100 ) + 7399;
                float yPos = (Convert.ToSingle(row[1]) * 100 ) - 4072;

                // to compute the center vector, we need to add all value along each axis. later we will divide the total number of vectors. 
                cv.x += xPos;
                cv.z += yPos;

                // if the value along the each axis meet the conditions below, we need to update to compute min and max values in the space of data.
                if (min.x > xPos) min.x = xPos;
                if (min.y > yPos) min.y = yPos;

                if (max.x < xPos) max.x = xPos;
                if (max.y < yPos) max.y = yPos;

                // now that, we create a sphere to visualize the each data.
                Vector3 v = new Vector3(xPos, 0, yPos);
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = v;
                float scale = 0.1f;
                sphere.transform.localScale = new Vector3(scale, scale, scale);
                Debug.Log(i);
                
                // add the sphere to the list
                vs.Add(v);

                // we can track the number how many spheres we successfully generate.
                numData++;
            } catch (System.Exception) {    
                // if an error happens, print it, and keep looping!!     
                Debug.Log(i);
            }

        }

        // to compute the center vector
        cv.x /= numData;
        cv.y /= numData;
        cv.z /= numData;
        float offset = (max.x - min.x);

        // update the position of camera, which is looking at the center on the data set
        Camera.main.transform.position = new Vector3(cv.x + offset , cv.y + offset ,cv.z);
        Camera.main.transform.LookAt(cv, Vector3.up);
        
    }

    private string GetDataFromCSVSecond(string path) {
        // import and read texts from path by the built-in feature
        StreamReader reader = new StreamReader(path);
        return reader.ReadToEnd();
    }
    private string GetDataFromCSVFirst(string path) {
        // another way to import and read texts from path by the built-in feature
        return System.IO.File.ReadAllText(path);
    }
}
