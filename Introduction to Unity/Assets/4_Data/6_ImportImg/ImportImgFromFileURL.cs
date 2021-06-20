using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;
using System.IO;
using  UnityEngine.Networking;
// using UnityEngine.Networking

public class ImportImgFromFileURL : MonoBehaviour
{
	// https://www.google.com/search?q=height+map+image&sxsrf=ALeKk03EBdWwQHlVwVyMaERV3s1qtjL36A:1624143725676&source=lnms&tbm=isch&sa=X&ved=2ahUKEwjYyNTu5qTxAhWrct8KHQNjADQQ_AUoAXoECAEQAw&biw=1942&bih=1138
	// https://codepen.io/NJStudio/pen/WNreJdy


	int resolution = 2;

	void Start() {

		// string url = "https://raw.githubusercontent.com/NamjuLee/data/master/img/heightMap/Heightmap_01.png";
		// string url = "https://raw.githubusercontent.com/NamjuLee/data/master/img/heightMap/Heightmap_02.jpg";
		string url = "https://raw.githubusercontent.com/NamjuLee/data/master/img/heightMap/Heightmap_03.jpg";

		StartCoroutine(download(url));
 
    }
	private IEnumerator download(string path) {
        if(File.Exists(path))
            Debug.LogFormat("Image exists! Path: {0}", path);
        
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(path))
        {
            UnityWebRequestAsyncOperation asyncOperation = webRequest.SendWebRequest();

            while (!asyncOperation.isDone)
                yield return null;

            if (webRequest.isDone) {
				Texture2D texture = DownloadHandlerTexture.GetContent(webRequest);

				print(texture);
				BuildTerrain(texture);

            }
            else
            {
                Debug.LogError("WebRequest left loop before it was considered done.");
            }
            
            webRequest.Dispose();
        }
    }

	void BuildTerrain(Texture2D texture) {
		for(int j = 0; j < texture.height; j += resolution ) {
			for(int i = 0; i < texture.width; i +=resolution ) {
				float value = texture.GetPixel(i, j).grayscale;
				Vector3 v = new Vector3(0, 0, 0);
				v.x = i;
				v.y = value * 100;
				v.z = j;

				GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				sphere.transform.position = v;
				sphere.transform.localScale = new Vector3(1.95f, 1.95f, 1.95f);
				sphere.GetComponent<Renderer>().material.color = new Color(value , 0 , 0 );
			}
		}
	}



}
