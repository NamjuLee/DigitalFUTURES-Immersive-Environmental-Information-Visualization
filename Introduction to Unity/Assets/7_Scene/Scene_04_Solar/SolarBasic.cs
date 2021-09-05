using UnityEngine;
using NJSUnityUtility;
public class SolarBasic : MonoBehaviour {
    public float lat = 38f; // between -90.0 to 90.0
    public float time = 13f; // between 0.0 to 24.0
    public int day = 150; // between 1 to 265
    Solar solar;
    GameObject lt;
    void Start()
    {
        // get Light in the scene and store it as a global value in the scope of this class, allowing us to update it while looping.
        lt = GameObject.Find("Light");
        // Create a solar instance
        this.solar = new Solar();
    }
    void Update()
    {
        // update the solar position, and then reflect the result of the light's rotation in scene of Unity
        // reference: ASHARE Handbook of fundamentals, Solar Declination, Altitud,e Azimuth
        // https://youtu.be/W2OQxCaVvEo?list=PLIyZNoxG7nmnP7-zipkajjIibqTblDS88&t=4
        this.solar.UpdateSun(lat, day, time);
        this.lt.transform.rotation = this.solar.GetQuaternion();
    }
}
