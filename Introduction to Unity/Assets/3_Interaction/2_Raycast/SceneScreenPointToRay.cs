using UnityEngine;

public class SceneScreenPointToRay : MonoBehaviour
{
    int num = 0;
    public GameObject gameObj;

    // Update is called once per frame
    void Update () {

        // https://codersdesiderata.com/2016/09/10/screen-view-to-world-coordinates/
        // https://www.google.com/search?q=frustum+ray+cast&tbm=isch&ved=2ahUKEwjRh77bwJjxAhVRh-AKHXxsCKcQ2-cCegQIABAA&oq=frustum+ray+cast&gs_lcp=CgNpbWcQAzoECCMQJ1CbXli1ZGC0ZWgBcAB4AIAB5wGIAcYFkgEFMS4xLjKYAQCgAQGqAQtnd3Mtd2l6LWltZ8ABAQ&sclient=img&ei=0ATIYNHfAtGOggf82KG4Cg&bih=872&biw=1796
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.Log(num++.ToString());
        if (Physics.Raycast(ray, out hit)){
            
            // only test geometry which name is `ground`
            // which is important for performance optimization
            // alos we can use tag or layer rather than name. 
            if (hit.collider.gameObject.name == "ground") 
            {
                Debug.Log(num.ToString() + hit.point.ToString());
                gameObj.transform.position = hit.point;
                // this LookAt function helps us to rotate geometry. In this case, using normal direction, we are able to maintain the object's orientation based on the surface.
                gameObj.transform.LookAt(hit.point + hit.normal);
            }

            Debug.DrawLine(Camera.main.transform.position, hit.transform.position);
        }
    }
}
