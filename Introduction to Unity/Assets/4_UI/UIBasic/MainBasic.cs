using UnityEngine;
using UnityEngine.UI;

public class MainBasic : MonoBehaviour
{
    void Start() {
        Button btn1 = GameObject.Find("BtnReset1").GetComponent<Button>();
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
