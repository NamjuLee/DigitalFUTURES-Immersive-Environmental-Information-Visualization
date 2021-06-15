using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneKeyboardInteraction : MonoBehaviour
{
    public GameObject box;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // https://docs.unity3d.com/ScriptReference/Input.GetKeyDown.html
    // Update is called once per frame
    void Update()
    {
         if (Input.GetKey(KeyCode.LeftArrow)) {
            box.transform.position = new Vector3(box.transform.position.x - 0.01f, box.transform.position.y, box.transform.position.z);
            print("LeftArrow key was pressed");
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            box.transform.position = new Vector3(box.transform.position.x + 0.01f, box.transform.position.y, box.transform.position.z);
            print("RightArrow key was pressed");
        } else if (Input.GetKey(KeyCode.UpArrow)) {
            box.transform.position = new Vector3(box.transform.position.x, box.transform.position.y, box.transform.position.z + 0.01f);
            print("UpArrow key was pressed");
        } else if (Input.GetKey(KeyCode.DownArrow)) {
            box.transform.position = new Vector3(box.transform.position.x, box.transform.position.y, box.transform.position.z - 0.01f);
            print("DownArrow key was pressed");
        }
    }
}
