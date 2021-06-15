using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCollision : MonoBehaviour
{
    public GameObject sphere;
    public GameObject box;
    float t = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)){
            if (hit.collider.gameObject.name == "Cube") 
            {
                GameObject go = GameObject.Instantiate(sphere, this.box.transform.position, this.box.transform.rotation);
                float scale = Random.Range(0.3f, 1.2f);
                go.transform.localScale = new Vector3(scale, scale, scale);
                // go.transform.position = this.box.transform.position;
            }


        }


        this.box.transform.position = new Vector3(this.box.transform.position.x + Mathf.Sin(t) * 0.01f, this.box.transform.position.y, this.box.transform.position.z);
        t+= 0.01f;
    }
}
