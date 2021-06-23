using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO; // needed for File.ReadAllBytes
public class MainUIImage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject canvas =  GameObject.Find("Canvas");

        GameObject imgObject = new GameObject("image");
        imgObject.GetComponent<Transform>().position = new Vector3(0, 0, 0);
        imgObject.transform.SetParent(canvas.transform);
        

        RectTransform trans = imgObject.AddComponent<RectTransform>();
        trans.anchoredPosition = new Vector2(0, 0);
        trans.localPosition = new Vector3(0, 0, 0);
        trans.position = new Vector3(0, 0, 0);
        trans.localScale = new Vector3(1, 1, 1);
        trans.sizeDelta = new Vector2(25, 25); // the width and height of Rect

        byte[] rawData= File.ReadAllBytes (@"./Assets/4_UI/UIImage/image.png");
        Texture2D texture = new Texture2D(1, 1);
        texture.LoadImage(rawData);
        Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);


        Image image = imgObject.AddComponent<Image>();
        image.sprite = sp;
            
    }
    // Update is called once per frame
    void Update()
    {




    }
}
