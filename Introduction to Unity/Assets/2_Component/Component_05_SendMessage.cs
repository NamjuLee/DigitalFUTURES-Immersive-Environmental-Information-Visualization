using UnityEngine;
public class Component_05_SendMessage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
         // SendMessage allows us to execute the functions with params in other gameobjects remotely.
         gameObject.SendMessage("PrintName", 2.0);
    }

    // Update is called once per frame
    void Update()
    {
          // GameObject.FindGameObjectsWithTag helps us to select all geometries which contain the tag in the scope of the Scene. 
          GameObject[] gs = GameObject.FindGameObjectsWithTag("geometry");

          // Debug.Log(gs.Length.ToString());
          
          // loop through the selected geometries and emit the command remotely
          for(int i = 0 ; i < gs.Length; ++i) {
               // gs[i].GetComponent<Component_05_SendMessageScripting>().PrintName( i + 3 );
               gs[i].SendMessage("PrintName", i + 3);
          }

     }
     void PrintName(int i) {
          print("from main :" + i.ToString() );
     }
}
