using UnityEngine;
using System.IO;

public class ImportImgFromFile : MonoBehaviour
{
	// https://www.google.com/search?q=height+map+image&sxsrf=ALeKk03EBdWwQHlVwVyMaERV3s1qtjL36A:1624143725676&source=lnms&tbm=isch&sa=X&ved=2ahUKEwjYyNTu5qTxAhWrct8KHQNjADQQ_AUoAXoECAEQAw&biw=1942&bih=1138

	int resolution = 2;

	void Start () {
		Texture2D texture = GetImage(@"./Assets/5_Data/6_ImportImg/data/Heightmap.png");
		BuildTerrain(texture);
    }
	Texture2D GetImage(string path) {
		byte[] rawData= File.ReadAllBytes (path);
		Texture2D heightmap = new Texture2D(1, 1);
        heightmap.LoadImage(rawData);
		return heightmap;
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
