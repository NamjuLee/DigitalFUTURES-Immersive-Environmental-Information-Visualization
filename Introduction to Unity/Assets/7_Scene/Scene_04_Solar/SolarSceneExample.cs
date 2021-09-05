using UnityEngine;
using NJSUnityUtility;
using UnityEngine.UI;
public class SolarSceneExample : MonoBehaviour {
    public GameObject LatSlider;
    public GameObject timeSlider;
    public GameObject daySlider;
    Solar solar;
    GameObject lt;
    void Start() {
        // get Light in the scene and store it as a global value in the scope of this class, allowing us to update it while looping.
        lt = GameObject.Find("Light");
        // Create a solar instance
        this.solar = new Solar();
    }
    void Update() {
        
        // the original values is a sort of normalized value between 0 and 1
        // thus, we need to rescale the factor for the each input value
        // lat : from -90 to 90 - Math: (value(0 or 1) * 180 ) - 90
        // day : 1 to 365 - Math: value(0 or 1) * 360
        // time : 0.0 to 24.00 - Math: value(0 or 1) * 24
        //
        float lat = (LatSlider.GetComponent<Slider>().value * 180f) - 90f;
        int day = (int)(daySlider.GetComponent<Slider>().value * 365f);
        float time = timeSlider.GetComponent<Slider>().value * 24f;
        Debug.Log(this.solar.ToString());

        // update the solar position, and then reflect the result of the light's rotation in scene of Unity
        // reference: ASHARE Handbook of fundamentals, Solar Declination, Altitud,e Azimuth
        // https://youtu.be/W2OQxCaVvEo?list=PLIyZNoxG7nmnP7-zipkajjIibqTblDS88&t=4
        this.solar.UpdateSun(lat, day, time);
        this.lt.transform.rotation = this.solar.GetQuaternion();

        // update the values on UI text while interacting with UI slider
        GameObject.Find("TextTime").GetComponent<Text>().text = "Time: " + time.ToString("F");
        GameObject.Find("TextLatitude").GetComponent<Text>().text = "Latitude: " + lat.ToString("F");
        GameObject.Find("TextDay").GetComponent<Text>().text = "Day: " + day.ToString("F");
    }
}
