using UnityEngine;
using System.Collections;

public class WaterIlluminationRadius : MonoBehaviour {

    float illumRadius = 3f;

    public void Init(float radius) {
        illumRadius = radius;
    }

    void Update() {
        var gos = Physics2D.OverlapCircleAll(transform.position, illumRadius);
        foreach (var go in gos) {
            if (go.gameObject.tag == "Water") {
                go.GetComponent<WaterTile>().Illuminate();
            }
        }
    }
}
