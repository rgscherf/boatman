using UnityEngine;
using System.Collections;

public class Entities : MonoBehaviour {

    public Sprite[] rocks;
    public GameObject rockObject;
    public GameObject paddleLine;

    public GameObject weaponDagger;
    public GameObject cargoSupplies;

    public Sprite cargoFoodSprite;
    public GameObject cargoFoodObject;

    public Sprite cargoDaggerSprite;
    public GameObject cargoDaggerObject;

    public GameObject coin;

    public IPalette palette;
    void Awake() {
        palette = new Stories();
    }
}

