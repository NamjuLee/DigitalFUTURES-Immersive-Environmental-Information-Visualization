using System.Collections.Generic;
using UnityEngine;

public class SceneKeyboardAndCollision : MonoBehaviour
{
    public GameObject box;
    // Start is called before the first frame update
    void Start()
    {

     GameObject[] gs = GameObject.FindGameObjectsWithTag("cube");

     foreach(GameObject g in gs) {
         g.AddComponent<Rigidbody>();
         // ! we want to add this custom script(Component) to the gameobjects.
         g.AddComponent<CollisionEvent>();

     }

    // box.AddComponent<Collider>();
        
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
        } else if (Input.GetKey(KeyCode.Space)) {
            // jump when pressing space on your keyboard
            box.GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(0, 9f, 0), box.transform.position);
        }

        // by clicking a cube, we want to inspect the history data of collisions. 
        if (Input.GetMouseButtonDown(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.gameObject.tag == "cube") {
                    List<GameObject> others = hit.collider.gameObject.GetComponent<CollisionEvent>().others;
                     Debug.Log(others.Count);
                    foreach(GameObject g in others) {
                        print(hit.collider.gameObject.name + " and " + g.name + " were collided.");
                    }
                }
            }
        }
    }

    // This is a custom class that will be attached to other c 
    class CollisionEvent : MonoBehaviour {
        Renderer ren;

        // this is the array to save all the gameobject which collided with this gameobject.
        // so that we could track the history data of collisions later. 
        public List<GameObject> others = new List<GameObject>();
        void Start() {
            this.ren = this.gameObject.GetComponent<Renderer>();
        }
        void OnCollisionEnter(Collision collisionInfo) {
            // if the collided mesh is the ground bject, then we skip.
            if (collisionInfo.gameObject.name == "ground") return;
            print(gameObject.name + " and " + collisionInfo.collider.name + " are still colliding");
             // we want save the collided gameobject in the array.
            others.Add(collisionInfo.gameObject);
        }
        void OnCollisionStay(Collision collisionInfo) {
            if (collisionInfo.gameObject.name == "ground") return;

            // if this gameobject collided with other, then the color of this sphere becomes red!
            ren.material.color = Color.red;
        }
        void OnCollisionExit(Collision collisionInfo) {
            if (collisionInfo.gameObject.name == "ground") return;

            // if this object fall apart with other, then the color of this sphere becomes cyan!
            ren.material.color = Color.cyan;
        }
        void Update() {
           
            // if the object fall from the ground, it will be removed.
            if (this.transform.position.y < -10.0) {
                GameObject.Destroy(this.gameObject);
            }
        }
    }
}


