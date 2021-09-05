using UnityEngine;
using UnityEngine.UI;
using System.IO; // needed for File.ReadAllBytes
public class MainUIImage : MonoBehaviour
{
    // we want to use an image as UI.
    void Start()
    {
        // get the canvas gameobject
        GameObject canvas =  GameObject.Find("Canvas");

        // create the gameobject for image UI
        GameObject imgObject = new GameObject("image");
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
        byte[] rawData= File.ReadAllBytes (@"./Assets/4_UI/UIImage/image.png");
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
