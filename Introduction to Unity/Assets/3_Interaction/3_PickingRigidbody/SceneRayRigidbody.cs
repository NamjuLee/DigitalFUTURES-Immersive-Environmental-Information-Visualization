using UnityEngine;

// https://docs.unity3d.com/ScriptReference/RaycastHit-point.html
public class SceneRayRigidbody : MonoBehaviour
{
    float pokeForce = 120.0f;
    void Update(){

        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)){
            if (hit.rigidbody != null){
                if (Input.GetMouseButtonDown(0)){
                    hit.rigidbody.AddForceAtPosition(ray.direction * pokeForce, hit.point);
                } else if (Input.GetMouseButtonDown(1)){
                    hit.rigidbody.AddForceAtPosition(ray.direction * -pokeForce, hit.point);
                }
            }


            Debug.DrawRay(hit.point, new Vector3(10, 10, 0),  Color.green);
        }

    }
}
