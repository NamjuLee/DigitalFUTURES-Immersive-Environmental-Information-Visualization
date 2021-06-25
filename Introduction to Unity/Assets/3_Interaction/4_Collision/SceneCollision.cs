using UnityEngine;

public class SceneCollision : MonoBehaviour
{
    public GameObject sphere;
    public GameObject box;
    float t = 0.0f;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)){

            // When hovering over the box emitter, it generates spheres.
            if (hit.collider.gameObject.name == "Cube") 
            {
                GameObject go = GameObject.Instantiate(sphere, this.box.transform.position, this.box.transform.rotation);
                float scale = Random.Range(0.3f, 1.2f);
                go.transform.localScale = new Vector3(scale, scale, scale);
                // go.transform.position = this.box.transform.position;
            }
        }

        // the oscillated box acts as emitter where other spheres will be generated.
        this.box.transform.position = new Vector3(this.box.transform.position.x + Mathf.Sin(t) * 0.01f, this.box.transform.position.y, this.box.transform.position.z);
        t+= 0.01f;
    }
}
