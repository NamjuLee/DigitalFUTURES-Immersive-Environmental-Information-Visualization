using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;

public class ImportOBJCustom : MonoBehaviour
{
    // declare a list to store vectors of the geometry
    List<Vector3> vs = new List<Vector3>();
    // declare a list to store the color of vertices
    List<Color> vc = new List<Color>();

    // Start is called before the first frame update
    void Start()
    {
        // https://en.wikipedia.org/wiki/Wavefront_.obj_file
        // https://github.com/NamjuLee/data/blob/master/geometry/CFDSimulationOBJ/CFDSimulation.obj
        // https://github.com/NamjuLee/data/blob/master/geometry/CFDSimulationOBJ/CFDSimulationCustom.gif

        string url = "https://raw.githubusercontent.com/NamjuLee/data/master/geometry/CFDSimulationOBJ/CFDSimulationCustom.obj";
        string dataStrings = GetDataFromOBJ(url);
        print(dataStrings);
        VisData(dataStrings);
    }

    void VisData(string dataStrings) {
        // split a text into multiple line based on linebreaks, the return value will be the array of strings
        string[] datalist = dataStrings.Split('\n');

        for(int i = 0 ; i < datalist.Length; ++i) {

            // split the string by ' '(empty space) into array of string.
            string[] row = datalist[i].Split(' ');
            // "v" means Geometric vertex.
            if(row[0] == "v") {
                // ex:
                // data -  v -18.04885 -15.9842 0
                // index - [0]: v  [1]: -18.04885 [2]: -15.9842 [3]: 0  
                float x = (float)System.Convert.ToSingle(row[1]);
                float y = (float)System.Convert.ToSingle(row[2]);
                float z = (float)System.Convert.ToSingle(row[3]);
                Vector3 v = new Vector3(x, z, y);
                vs.Add(v);
            }
            // "vc" means vertex color.
            if(row[0] == "vc") {
                // ex:
                // data -  vc 0 183 36
                // index - [0]: vc  [1]: 0 [2]: 183 [3]: 36  
                int r = (int)System.Convert.ToInt16(row[1]);
                int g = (int)System.Convert.ToInt16(row[2]);
                int b = (int)System.Convert.ToInt16(row[3]);
  
                // each rgb value is normalized one, that is why we need to divide each data by 255.
                Color c = new Color( r / 255f, g / 255f, b / 255f);
                vc.Add(c);
            }
        }

        for(int i = 0 ; i < vs.Count; ++i){
            // populate spheres in each vertex, and visualze the color of the spheres
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            go.transform.position = vs[i];
            go.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            go.GetComponent<Renderer>().material.color = vc[i];
        }
        
    }
    private string GetDataFromOBJ(string url) {
        // this is a routine to download and parse data as string.
        HttpWebRequest request =  (HttpWebRequest)WebRequest.Create(url);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        return reader.ReadToEnd();
    }

}

/*
v -18.04885 -15.9842 0
v -17.4263 -15.9842 0
v -16.80375 -15.9842 0
v -16.18121 -15.9842 0
v -15.55866 -15.9842 0
...

vc 0 183 36
vc 0 183 36
vc 0 194 30
vc 0 203 25
vc 0 210 22
vc 0 217 18
vc 0 223 15
...

f 0 1 61 60
f 1 2 62 61
f 2 3 63 62
f 3 4 64 63
f 4 5 65 64
f 5 6 66 65
f 6 7 67 66
f 7 8 68 67
...
*/