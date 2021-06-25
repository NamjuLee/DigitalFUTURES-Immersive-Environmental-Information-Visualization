using UnityEngine;
using UnityEngine.UI;

public class MainBasic : MonoBehaviour
{
    // Unity has its own built-in class in the UI namespace.
    // https://docs.unity3d.com/Packages/com.unity.ugui@1.0/api/UnityEngine.UI.html

    void Start() {
        // UI could be considered as a gameobject, and we could get Button component by .GetComponent function.
        // select the existing Button 
        Button btn1 = GameObject.Find("BtnReset1").GetComponent<Button>();
        // bind the callback function
        btn1.onClick.AddListener(BtnReset1);
  
        Button btn2 = GameObject.Find("BtnReset2").GetComponent<Button>();
        btn2.onClick.AddListener(BtnReset2);
    }
    void BtnReset1() {
        print("click by BtnReset 1");
    }
    void BtnReset2() {
        print("click by BtnReset 2");
    }
}
