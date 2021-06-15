using System.Collections.Generic;
using UnityEngine;

public class ScriptCollision : MonoBehaviour
{
    Renderer ren;

    List<GameObject> gs = new List<GameObject>();
    void Start() {
        this.ren = this.gameObject.GetComponent<Renderer>();
    }

    void OnCollisionEnter(Collision collisionInfo) {
        if (collisionInfo.gameObject.name == "ground") return;
        print(gameObject.name + " and " + collisionInfo.collider.name + " are still colliding");
        gs.Add(collisionInfo.gameObject);
    }
    void OnCollisionStay(Collision collisionInfo) {
        if (collisionInfo.gameObject.name == "ground") return;
        ren.material.color = Color.red;
        
    }
    void OnCollisionExit(Collision collisionInfo) {
        if (collisionInfo.gameObject.name == "ground") return;
        ren.material.color = Color.cyan;
        
    }

    // void OnDrawGizmos(){
    //     foreach(GameObject g in gs) Gizmos.DrawLine(this.transform.position, g.transform.position );
    // }
}
