using UnityEngine;
using System.Collections;

public class UIColor : MonoBehaviour {

    void Start () {
        GetComponent<SpriteRenderer>().color = GameObject.Find("Entities")
                                               .GetComponent<Entities>()
                                               .palette.UI;
    }
}
