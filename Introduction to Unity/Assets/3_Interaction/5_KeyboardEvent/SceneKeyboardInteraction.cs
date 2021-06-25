using UnityEngine;

public class SceneKeyboardInteraction : MonoBehaviour
{
    public GameObject box;

    void Update()
    {
        // just like playing a game, we use the arrow key on keyboard to control the box above.
        // https://docs.unity3d.com/ScriptReference/Input.GetKeyDown.html

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
        } else if (Input.GetKey(KeyCode.Space)) {
            // jump when pressing space on your keyboard
            box.GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(0, 9f, 0), box.transform.position);
        }
    }
}
