using UnityEngine;

public class Component_03_TransformUpdate : MonoBehaviour
{
    GameObject cube;

    // when you use the `public` keyword, you can make a variable visible on the Unity inspector, So that you can assign default value of it
    // However, `[HideInInspector]` this keyword prevent from access the parameters on the Unity inspector. but you can still access the param from other contexts(out of this class).
    [HideInInspector]
    public float intervalTime = 0.1f;

    float time = 0.0f;
    bool isIntersect = false;
    void Start()
    {
        cube = GameObject.Find("MyCube!!");
        
        Debug.Log(cube.tag);
        Debug.Log(cube.transform.position);

        // https://docs.unity3d.com/ScriptReference/Transform.html
        // Transform component is a super important component to update the position, scale, and rotation of the gameobject!
        cube.transform.localScale = new Vector3(2, 1, 1);
       
    }
    void Update( ){
        this.time += this.intervalTime;

        cube.transform.position = new Vector3(
            cube.transform.position.x + 0.001f,
            cube.transform.position.y + Mathf.Cos(this.time * 0.1f) * 0.035f,
            cube.transform.position.z
            );


        cube.transform.Rotate(new Vector3(1, 0, 0), 1.5f);

        Debug.Log(this.time);

        if (isIntersect) {
            // cube.transform.position
        }
    }


}
