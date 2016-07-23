using UnityEngine;
using System.Collections;

public class TestEnemy : MonoBehaviour {

    // Use this for initialization
    void Start () {
        var entities = GameObject.Find("Entities").GetComponent<Entities>();
        GetComponent<SpriteRenderer>().color = entities.palette.danger;
    }

    // Update is called once per frame
    void Update () {

    }
}
