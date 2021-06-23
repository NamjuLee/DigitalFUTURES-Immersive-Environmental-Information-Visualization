using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIEvent : MonoBehaviour
{
    float t = 0.0f;
    GameObject go;

    List<GameObject> sphereList = new List<GameObject>();
    // Start is called before the first frame update
    void Start() {
        Button btn1 = GameObject.Find("BtnReset1").GetComponent<Button>();
        btn1.onClick.AddListener(BtnReset1);
  
        Button btn2 = GameObject.Find("BtnReset2").GetComponent<Button>();
        btn2.onClick.AddListener(BtnReset2);

        this.go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        this.go.transform.position = new Vector3(0, 1.5f, 0);
        this.go.transform.localScale = new Vector3(7, 7, 7);
    }
    void Update(){
        this.go.transform.position = new Vector3(this.go.transform.position.x + Mathf.Sin(t) * 0.1f, this.go.transform.position.y, this.go.transform.position.z);
        t+= 0.01f;
    }
    void BtnReset1() {
        GameObject sphere =  GameObject.CreatePrimitive(PrimitiveType.Sphere);
        float scale = Random.Range(2.3f, 5.2f);
        sphere.transform.position = new Vector3(go.transform.position.x, go.transform.position.y + 10, go.transform.position.z );
        sphere.transform.localScale = new Vector3(scale, scale, scale);
        sphere.AddComponent<Rigidbody>();
        Rigidbody rigidbody = sphere.GetComponent<Rigidbody>();
        rigidbody.mass = .9f;
        rigidbody.AddForceAtPosition(Vector3.up * 1000f, sphere.transform.position);
        sphereList.Add(sphere);
    }
    void BtnReset2(){
        foreach(GameObject g in this.sphereList) GameObject.Destroy(g);
        sphereList = new List<GameObject>();
    }
}
