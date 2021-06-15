using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Component_05_SendMessage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
         gameObject.SendMessage("PrintName", 2.0);
    }

    // Update is called once per frame
    void Update()
    {
       GameObject[] gs = GameObject.FindGameObjectsWithTag("geometry");
       
       for(int i = 0 ; i < gs.Length; ++i) {
            gs[i].SendMessage("PrintName", 2.0); 
       }

    }
}
