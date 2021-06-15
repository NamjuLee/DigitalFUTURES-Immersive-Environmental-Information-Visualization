using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Component_04_InstantiateDestroy : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject refer;
    int t = 0;
    List<GameObject> gs = new List<GameObject>();

    bool needInit = true;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update() {

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

            // Destroy gameobjects
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
