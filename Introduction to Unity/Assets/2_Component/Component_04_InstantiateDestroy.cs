using System.Collections.Generic;
using UnityEngine;

public class Component_04_InstantiateDestroy : MonoBehaviour
{
    // Global variable in this class
    public GameObject refer;

    // we want to track time. Also we could use Unity built-in `Time` class.
    int t = 0;
    // we want to store gameobject later
    List<GameObject> gs = new List<GameObject>();

    bool needInit = true;
    void Start()
    {

    }

    // Update is called once per frame
    void Update() {

        // increase time, which is identical to `t = t + 1`
        t++;

        if(needInit) {

            // Instantiate gameobjects
            if ( t  > 60) {
                GameObject go = GameObject.Instantiate(refer);
                go.transform.position = new Vector3(gs.Count * 1.5f, 0, 0);
                gs.Add(go);
                 t = 0;
            }

            // condition for shifting the mode to destory gameobjects
            if (this.gs.Count == 10) needInit = false;

        } else {

            // Destroy gameobjects in order
            if ( t  > 60 && this.gs.Count != 0) {
                print("remove");
                GameObject g = this.gs[0];
                this.gs.RemoveAt(0);
                GameObject.Destroy(g);
                t = 0;
            }
        }



    }
}
