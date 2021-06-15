using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGizmosVisualization : MonoBehaviour
{
    public GameObject refer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // Vector3 v0 = new Vector3(1, 0, 1);
        // Vector3 v1 = new Vector3(3, 3, 3);

        // // DrawRay draw a ray with a position and a direction.
        // Debug.DrawRay(v0, v1);

        // // DrawLine draws a line between two points.
        // Debug.DrawLine(v0, v1);

    }

    // https://docs.unity3d.com/ScriptReference/Gizmos.html
    void  OnDrawGizmos(){

        Vector3 v0 = new Vector3(1, 0, 1);
        Vector3 v1 = new Vector3(3, 3, 3);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(v0, v1);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(v0, .23f);

        Gizmos.DrawCube(v1, new Vector3(1, 0.5f, 1));
        Gizmos.DrawWireMesh(refer.GetComponent<MeshFilter>().sharedMesh , new Vector3(0, 0, 5));

    }
}
