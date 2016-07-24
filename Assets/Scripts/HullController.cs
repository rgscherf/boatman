using UnityEngine;
using System.Collections.Generic;

public class HullController : MonoBehaviour {

    Dictionary<int, GameObject> hullIcons;

    void Awake() {
        hullIcons = new Dictionary<int, GameObject>();
        hullIcons[1] = GameObject.Find("HullDmg1");
        hullIcons[2] = GameObject.Find("HullDmg2");
        hullIcons[3] = GameObject.Find("HullDmg3");
        hullIcons[4] = GameObject.Find("HullDmg4");
        hullIcons[5] = GameObject.Find("HullDmg5");
        hullIcons[6] = GameObject.Find("HullDmg6");
    }

    public void Repaint(int newHealth) {
        foreach (var val in hullIcons) {
            val.Value.SetActive(false);
        }
        for (var i = 1; i <= newHealth; i++) {
            hullIcons[i].SetActive(true);
        }

    }
}
