using UnityEngine;
using System.Collections;

public class TestGeometry : MonoBehaviour {

    // Use this for initialization
    void Start () {
        var entities = GameObject.Find("Entities").GetComponent<Entities>();
        GetComponent<SpriteRenderer>().color = entities.palette.geometry;
    }

    // Update is called once per frame
    void Update () {

    }
}
