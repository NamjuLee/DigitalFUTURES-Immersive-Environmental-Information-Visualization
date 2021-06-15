using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Component_03_TransformUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject cube;

    [HideInInspector] 
    public float intervalTime = 0.1f;

    float time = 0.0f;

    Transform transforms;
    void Start()
    {
        cube = GameObject.Find("MyCube");
        
        Debug.Log(cube.tag);
        Debug.Log(cube.transform.position);

        cube.transform.localScale = new Vector3(2, 1, 1);
       
    }
    void Update( ){
        this.time += this.intervalTime;

        cube.transform.position = new Vector3(
            cube.transform.position.x + 0.001f,
            cube.transform.position.y + Mathf.Cos(this.time * 0.1f) * 0.035f,
            cube.transform.position.z
            );


        cube.transform.Rotate(new Vector3(1, 0, 0), 1.5f);

        Debug.Log(this.time);
    }


}
