using UnityEngine;
using System.Collections;

public class WaterTile : MonoBehaviour {

    SpriteRenderer spr;
    bool lit;
    float fadeSpeed = 0.02f;

    // Use this for initialization
    void Start () {
        spr = GetComponent<SpriteRenderer>();

        var newCol = spr.color;
        newCol.a = 0f;
        spr.color = newCol;
    }

    public void Illuminate() {
        lit = true;
        var newCol = spr.color;
        newCol.a = 0.5f;
        spr.color = newCol;
    }

    // Update is called once per frame
    void Update () {
        if (lit) {
            var newCol = spr.color;
            newCol.a -= fadeSpeed;
            spr.color = newCol;
        }

        lit = spr.color.a >= 0f;
    }
}
