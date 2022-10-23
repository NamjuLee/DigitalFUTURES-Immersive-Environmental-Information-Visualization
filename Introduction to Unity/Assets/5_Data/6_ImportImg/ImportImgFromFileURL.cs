using System.Collections;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class ImportImgFromFileURL : MonoBehaviour
{
	// https://en.wikipedia.org/wiki/Heightmap

	// https://www.google.com/search?q=height+map+image&sxsrf=ALeKk03EBdWwQHlVwVyMaERV3s1qtjL36A:1624143725676&source=lnms&tbm=isch&sa=X&ved=2ahUKEwjYyNTu5qTxAhWrct8KHQNjADQQ_AUoAXoECAEQAw&biw=1942&bih=1138
	// https://codepen.io/NJStudio/pen/WNreJdy
	

	int resolution = 2;

	void Start() {

		// string url = "https://raw.githubusercontent.com/NamjuLee/data/master/img/heightMap/Heightmap_01.png";
		// string url = "https://raw.githubusercontent.com/NamjuLee/data/master/img/heightMap/Heightmap_02.jpg";
		string url = "https://raw.githubusercontent.com/NamjuLee/data/master/img/heightMap/Heightmap_03.jpg";

		// https://docs.unity3d.com/ScriptReference/MonoBehaviour.StartCoroutine.html
		StartCoroutine(download(url));
 
    }
	private IEnumerator download(string path) {
        if(File.Exists(path)) Debug.LogFormat("Image exists! Path: {0}", path);
        
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(path)) {
            UnityWebRequestAsyncOperation asyncOperation = webRequest.SendWebRequest();

            while (!asyncOperation.isDone) yield return null;

			// when the request done, we will execute the scope of code below
            if (webRequest.isDone) {
				// Unity built-in feature to create texture2d by webRequest.
				Texture2D texture = DownloadHandlerTexture.GetContent(webRequest);

				print(texture);
				// the custom computation for building terrain happens in the function
				BuildTerrain(texture);

            } else {
                Debug.LogError("WebRequest left loop before it was considered done.");
            }
            webRequest.Dispose();
        }
    }

	void BuildTerrain(Texture2D texture) {
		// image is basically two dimensional array, we need to double-for-loop to visit individual pixel of the image 
		// to populate spheres along each axis with the color from data.
		for(int j = 0; j < texture.height; j += resolution ) {
			for(int i = 0; i < texture.width; i +=resolution ) {
				// get pixel value from the 2D array
				float value = texture.GetPixel(i, j).grayscale;
				Vector3 v = new Vector3(0, 0, 0);
				v.x = i;
				v.y = value * 100; // scale for the y factor
				v.z = j;

				GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				sphere.transform.position = v;
				sphere.transform.localScale = new Vector3(1.95f, 1.95f, 1.95f);
				sphere.GetComponent<Renderer>().material.color = new Color(value , 0 , 0 );
			}
		}
	}



}
