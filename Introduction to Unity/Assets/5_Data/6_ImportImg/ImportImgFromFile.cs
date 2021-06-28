using UnityEngine;
using System.IO;

public class ImportImgFromFile : MonoBehaviour
{
	// resolution for the 2D grid
	int resolution = 2;

	void Start () {

		// https://www.google.com/search?q=height+map+image&sxsrf=ALeKk03EBdWwQHlVwVyMaERV3s1qtjL36A:1624143725676&source=lnms&tbm=isch&sa=X&ved=2ahUKEwjYyNTu5qTxAhWrct8KHQNjADQQ_AUoAXoECAEQAw&biw=1942&bih=1138
		// https://codepen.io/NJStudio/pen/WNreJdy
		// import an image by the local path
		Texture2D texture = GetImage(@"./Assets/5_Data/6_ImportImg/data/Heightmap.png");
		BuildTerrain(texture);
    }
	Texture2D GetImage(string path) {
		// read image into byte by using C# built-in feature
		byte[] rawData= File.ReadAllBytes (path);
		 // create Texture2d Class to put image as a texture
		Texture2D heightmap = new Texture2D(1, 1);
        heightmap.LoadImage(rawData);
		return heightmap;
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
				sphere.GetComponent<Renderer>().material.color = new Color(value * 1.75f , 0 , 0 );
			}
		}
	}
}
