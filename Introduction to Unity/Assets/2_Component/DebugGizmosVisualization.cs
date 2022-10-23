using UnityEngine;

public class DebugGizmosVisualization : MonoBehaviour
{
    public GameObject refer;

    // debug and gizmo are very useful built-in static functions from the Unity Class
    // This is because, debug and gizmo make it easy to visualize. 
    // Normally, in order to visualize we need to build Gameobject correctly(with Renderer, MeshFilter, Transform, and so on)
    // However, the built-in debug and gizmo allows us render data on screen without such complex gameobject's pipeline.  
    void Update()
    {
        // https://docs.unity3d.com/ScriptReference/Debug.html

        // Vector3 v0 = new Vector3(1, 0, 1);
        // Vector3 v1 = new Vector3(3, 3, 3);

        // // DrawRay draw a ray with a position and a direction, which means that the line will start from the V0 along V1 direction.
        // Debug.DrawRay(v0, v1);

        // // DrawLine draws a line between two points.
        // Debug.DrawLine(v0, v1);

    }

    // https://docs.unity3d.com/ScriptReference/Gizmos.html
    void  OnDrawGizmos(){

        // Vector3 v0 = new Vector3(1, 0, 1);
        // Vector3 v1 = new Vector3(3, 3, 3);
        // Gizmos.color = Color.red;
        // Gizmos.DrawLine(v0, v1);
        // Gizmos.color = Color.blue;
        // Gizmos.DrawSphere(v0, .23f);

        // Gizmos.DrawCube(v1, new Vector3(1, 0.5f, 1));
        // Gizmos.DrawWireMesh(refer.GetComponent<MeshFilter>().sharedMesh , new Vector3(0, 0, 5));

    }
}
