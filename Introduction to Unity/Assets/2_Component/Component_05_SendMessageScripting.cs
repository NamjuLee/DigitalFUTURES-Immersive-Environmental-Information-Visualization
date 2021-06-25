using UnityEngine;

public class Component_05_SendMessageScripting : MonoBehaviour
{
    // This is a function to be executed remotely.
    public void PrintName(int a){
        string namePrint = "";
        for(int i = 0 ; i < a; ++i) {
            namePrint += this.gameObject.name + " ";
        } 

        print(namePrint);
    }
}
