using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Component_05_SendMessageScripting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PrintName(int a){
        string namePrint = "";
        for(int i = 0 ; i < a; ++i) {
            namePrint += this.gameObject.name + " ";
        } 

        print(namePrint);
    }
}
