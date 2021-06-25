using UnityEngine;

public class Component_02_AttachComponent : MonoBehaviour
{

    // This cube below could be used as a global scope inside of this class.
    GameObject cube;
    void Start()
    {
        // https://docs.unity3d.com/ScriptReference/GameObject.CreatePrimitive.html
        // GameObject is a one of the key classes provided by Unity, and it has a static function to create Unity primitive shapes.
        GameObject plane  = GameObject.CreatePrimitive(PrimitiveType.Plane);

        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(0, 0.5f, 0);

        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = new Vector3(0, 1.5f, 0);

        GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        capsule.transform.position = new Vector3(2, 1, 0);

        GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cylinder.transform.position = new Vector3(-2, 1, 0);
        cylinder.name = "myCylinder";
        cylinder.GetComponent<Renderer>().material.color = Color.blue;
        cylinder.transform.Rotate(new Vector3(0, 0, 1), 45f);
        cylinder.transform.position = new Vector3(cylinder.transform.position.x, cylinder.transform.position.y + 3, cylinder.transform.position.z);

        // AddComponent function helps us to append components to this gameobject.
        // rigidbody component allows us to apply Unity physics engine on this gameobject.
        Rigidbody rigidbody = cylinder.AddComponent<Rigidbody>();
        rigidbody.mass = 10f;
        
        this.cube = cube;
        this.cube.AddComponent<Rigidbody>();
    }
    // Update is called every frame, if the MonoBehaviour is enabled.
    void Update( ){
        Renderer render = this.cube.GetComponent<Renderer>();
        render.material.color = Color.yellow;;

        // In the every frame, the position of this gameobject will be updated like below.
        render.gameObject.transform.position = new Vector3(
            render.gameObject.transform.position.x - 0.005f, 
            render.gameObject.transform.position.y, 
            render.gameObject.transform.position.z
            );
    }


}
