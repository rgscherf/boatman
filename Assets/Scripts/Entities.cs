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

    public Sprite cargoRockSprite;
    public GameObject emptyObject;

    public Sprite cargoSwordSprite;
    public GameObject cargoSwordObject;

    public GameObject coin;

    public GameObject mapRockSmall;
    public GameObject mapRockLarge;
    public GameObject basicEnemy;


    public IPalette palette;
    void Awake() {
        palette = new Stories();
    }
}

