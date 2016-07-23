using UnityEngine;
using System.Collections;

public class Entities : MonoBehaviour {

    public Sprite[] rocks;
    public GameObject rockObject;
    public GameObject paddleLine;

    public IPalette palette;

    void Awake() {
        palette = new Stories();
    }
}

