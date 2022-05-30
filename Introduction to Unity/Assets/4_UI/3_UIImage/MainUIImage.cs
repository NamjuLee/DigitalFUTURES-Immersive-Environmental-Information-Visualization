using UnityEngine;
using UnityEngine.UI;
using System.IO; // needed for File.ReadAllBytes
using UnityEngine.EventSystems; // needed for IPointerClickHandler
public class MainUIImage : MonoBehaviour
{
    // we want to use an image as UI.
    void Start(){

        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.name = "myCube";
        cube.transform.localScale = new Vector3(3, 1, 1);
        cube.AddComponent<CubeAnimation>();

        // get the canvas gameobject
        GameObject canvas =  GameObject.Find("Canvas");

        // create the gameobject for image UI
        GameObject imgObject = new GameObject("image");
        imgObject.AddComponent<ClickAction>();
        imgObject.GetComponent<Transform>().position = new Vector3(0, 0, 0);
        // put the image on a child of the canvas
        imgObject.transform.SetParent(canvas.transform);

        // add RectTransform component to image, we could use the relative transform on the context of the canvas
        RectTransform trans = imgObject.AddComponent<RectTransform>();
        trans.anchoredPosition = new Vector2(0, 0);
        trans.localPosition = new Vector3(0, 0, 0);
        trans.position = new Vector3(0, 0, 0);
        trans.localScale = new Vector3(1, 1, 1);
        trans.sizeDelta = new Vector2(25, 25); // the width and height of Rect

        // read image into byte by using C# built-in feature
        byte[] rawData= File.ReadAllBytes (@"./Assets/4_UI/3_UIImage/image.png");
        // create Texture2d Class to put image as a texture
        Texture2D texture = new Texture2D(1, 1);
        texture.LoadImage(rawData);
        // create sprite from the texture
        Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

        // add the Image Component and update the sprite
        Image image = imgObject.AddComponent<Image>();
        image.sprite = sp;
            
    }
}
public class ClickAction : MonoBehaviour, IPointerClickHandler
{ 
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("clicked");
        CubeAnimation aniComp = GameObject.Find("myCube").GetComponent<CubeAnimation>();
        aniComp.toggle = !aniComp.toggle;
    }
}
public class CubeAnimation : MonoBehaviour {
    public Vector3 t0 = new Vector3(0, -8, 5);
    public Vector3 t1 = new Vector3(0, 8, 5);
    public float speed = 0.0175f;
    public bool toggle = true;
	void Update () {
        if (toggle) {
            this.transform.position = easingMotion(this.transform.position , t0);
        } else {
            this.transform.position = easingMotion(this.transform.position , t1);
        }
	}
    Vector3 easingMotion(Vector3 v1 , Vector3 v2){
       Vector3 v = v2 - v1;
       return  v1 + v * Mathf.Log(v.magnitude) * this.speed;
    }
}
