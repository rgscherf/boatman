using UnityEngine;

public class PickupColor : MonoBehaviour {

    // Use this for initialization
    void Start () {
        GetComponent<SpriteRenderer>().color = GameObject.Find("Entities")
                                               .GetComponent<Entities>()
                                               .palette.player;
    }
}
