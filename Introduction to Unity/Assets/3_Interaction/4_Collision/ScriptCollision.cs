using System.Collections.Generic;
using UnityEngine;

public class ScriptCollision : MonoBehaviour
{
    Renderer ren;

    // this is the array to save all the gameobject which collided with this gameobject.
    // so that we could track the history data of collisions later. 
    List<GameObject> gs = new List<GameObject>();
    void Start() {
        // explicitly select and store the component in the scope of this class when it starts(which happen one time for the life cycle).
        // so that we can use it later
        this.ren = this.gameObject.GetComponent<Renderer>();
    }

    void OnCollisionEnter(Collision collisionInfo) {
        // if the collided mesh is the ground bject, then we skip.
        if (collisionInfo.gameObject.name == "ground") return;

        // we want to print the collided object on Console window.
        print(gameObject.name + " and " + collisionInfo.collider.name + " are still colliding");

        // we want save the collided gameobject in the array.
        gs.Add(collisionInfo.gameObject);
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

    // void OnDrawGizmos(){
    //     foreach(GameObject g in gs) Gizmos.DrawLine(this.transform.position, g.transform.position );
    // }
}
