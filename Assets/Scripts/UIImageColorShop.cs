using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIImageColorShop : MonoBehaviour {

    // Use this for initialization
    void Start () {
        GetComponent<Image>().color = GameObject.Find("Entities")
                                      .GetComponent<Entities>()
                                      .palette
                                      .danger;

    }

    // Update is called once per frame
    void Update () {

    }
}
