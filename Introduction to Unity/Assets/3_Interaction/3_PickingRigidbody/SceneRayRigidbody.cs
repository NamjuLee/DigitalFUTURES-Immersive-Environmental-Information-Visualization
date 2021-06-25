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
                // Input class is the Unity's built-in class which allows us to access the mouse or keyboard event data
                // https://docs.unity3d.com/ScriptReference/Input.GetMouseButtonDown.html
                // 0 = Pressed primary button
                // 1 = Pressed secondary  button
                // 2 = Pressed middle click
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
