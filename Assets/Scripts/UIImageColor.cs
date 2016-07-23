using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIImageColor : MonoBehaviour {

    // Use this for initialization
    void Start () {
        GetComponent<Image>().color = GameObject.Find("Entities")
                                      .GetComponent<Entities>()
                                      .palette
                                      .UI;

    }

    // Update is called once per frame
    void Update () {

    }
}
