using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebCamExample : MonoBehaviour
{
    // we want to keep the reasonable performance, the lower resolution will be decrease the performance
    int resolution = 20;
    // globally use the WebCamTexture variable
    WebCamTexture webcamTexture;

    // store the color and transform in order.
    // This practice allows us to reduce other extra operations like GetComponent.
    // So that we can maintain the performance.
    List<Transform> pixelsTransform = new List<Transform>();
    List<Renderer> pixelsColor = new List<Renderer>();

    // This below is the scale of pixels
    float pixelScale = 0.25f;
    void Start()
    {
        // prepare for the plane to show the video from web camera on the surface of the plane as a texture
        GameObject plane  = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.transform.position = new Vector3(0, 3, 10);
        plane.transform.Rotate(new Vector3(0, 0, 1), 180f);
        plane.transform.Rotate(new Vector3(1, 0, 0), -90f);

        // create an instance of WebCamTexture and apply it to the renderer of the Plane above as a texture
        webcamTexture = new WebCamTexture();
        Renderer renderer = plane.GetComponent<Renderer>();
        renderer.material.mainTexture = webcamTexture;
        webcamTexture.Play();

        // offset value for the each pixel along x and z axis
        float offset = 0.01f;

        for(int j = 0; j < webcamTexture.height; j += resolution ) {
			for(int i = 0; i < webcamTexture.width; i +=resolution ) {

                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

				// get pixel value from the video
                // apply the pixel color data to the color of the sphere
                // save the sphere's renderer component for the later use in looping
                Color col = webcamTexture.GetPixel(i, j);
				Renderer theRenderer = sphere.GetComponent<Renderer>();
                theRenderer.material.color = col;
                pixelsColor.Add(theRenderer);
			
                // get the Transform component
                // update scale and position in the grid
                // save the sphere's transform component for the later use in looping
                Transform theTransform = sphere.GetComponent<Transform>();
				theTransform.localScale = new Vector3(pixelScale, pixelScale, pixelScale);
				theTransform.position =  new Vector3(
                    (i * offset) - 6,
                        -1,
                    (j * offset) - 2
                );
                pixelsTransform.Add(theTransform);
			}
		}
    }

    // Update is called once per frame
    void Update()
    {
        // this helps us to track the idex of pixel in order 
        int index = 0;
        for(int j = 0; j < webcamTexture.height; j += resolution ) {
			for(int i = 0; i < webcamTexture.width; i +=resolution ) {

				// get pixel value from the real-time video
                // apply the pixel color data to the color of the sphere
                // also, update the position of the pixel sphere based on the grayscale value, along y axis
				Color col = webcamTexture.GetPixel(i, j);
				pixelsColor[index].material.color = col;
				pixelsTransform[index].position = new Vector3(
                    pixelsTransform[index].position.x,
                    1 - col.grayscale * 1.75f,
                    pixelsTransform[index].position.z
                );

                // condition for the scale of the sphere based on the grayscale value of the pixel
                if (col.grayscale > 0.5 ) {
                    pixelsTransform[index].localScale = new Vector3(0.1f, 0.1f, 0.1f);
                } else {
                    pixelsTransform[index].localScale = new Vector3(pixelScale, pixelScale, pixelScale);
                }

                // once we done for all necessary operations then,
                // increase one to select next item in order
                index++;
            }
		}
    }
}
