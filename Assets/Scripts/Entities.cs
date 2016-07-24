using UnityEngine;
using System.Collections;

public class Entities : MonoBehaviour {

    //////////////////
    // CARGO HANDLING
    //////////////////

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

    public Sprite cargoEmptySprite;

    public Sprite cargoHealingSprite;
    public GameObject cargoHealingObject;

    public Sprite cargoRemainsSprite;

    public Sprite cargoCannonerSprite;
    public GameObject cargoCannonerObject;


    //////////////
    // MAP OBJECTS
    //////////////

    public GameObject coin;
    public GameObject remains;

    public GameObject mapRockSmall;
    public GameObject mapRockLarge;
    public GameObject basicEnemy;


    /////////////////////
    // STATIC MAP OBJECTS
    /////////////////////

    public Sprite[] rocks;
    public GameObject rockObject;
    public GameObject paddleLine;


    //////////
    // PALETTE
    //////////

    public IPalette palette;
    void Awake() {
        palette = new Stories();
    }
}

