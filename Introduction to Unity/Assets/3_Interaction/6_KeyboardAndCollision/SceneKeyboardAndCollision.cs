using System.Collections;
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
        }

    
        if (Input.GetMouseButtonDown(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)){
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

    class CollisionEvent : MonoBehaviour {
        Renderer ren;
        public List<GameObject> others = new List<GameObject>();
        void Start() {
            this.ren = this.gameObject.GetComponent<Renderer>();
        }
        void OnCollisionEnter(Collision collisionInfo) {
            if (collisionInfo.gameObject.name == "ground") return;
            print(gameObject.name + " and " + collisionInfo.collider.name + " are still colliding");
            others.Add(collisionInfo.gameObject);
        }
        void OnCollisionStay(Collision collisionInfo) {
             if (collisionInfo.gameObject.name == "ground") return;
            ren.material.color = Color.red;
           
        }
        void OnCollisionExit(Collision collisionInfo) {
            if (collisionInfo.gameObject.name == "ground") return;
            ren.material.color = Color.cyan;
            
        }
    }
}


