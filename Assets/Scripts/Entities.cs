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

    public Sprite cargoCeramicSprite;
    public Sprite cargoCeramicBrokenSprite;


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
    // ECONOMY
    //////////

    public static int Price(CargoType t) {
        switch (t) {
            case CargoType.Food:
                return 2;
            case CargoType.None:
                return 0;
            case CargoType.Empty:
                return 0;
            case CargoType.Remains:
                return 0;
            case CargoType.Healing:
                return 6;
            case CargoType.Dagger:
                return 1;
            case CargoType.Rock:
                return 0;
            case CargoType.Sword:
                return 15;
            case CargoType.Cannoner:
                return 5;
            case CargoType.Ceramic:
                return 5;
            default:
                return 99;
        }
    }

    public static int SellPrice(CargoType t) {
        switch (t) {
            case CargoType.Food:
                return 6;
            case CargoType.None:
                return 0;
            case CargoType.Empty:
                return 0;
            case CargoType.Remains:
                return 2;
            case CargoType.Healing:
                return 1;
            case CargoType.Dagger:
                return 2;
            case CargoType.Rock:
                return 3;
            case CargoType.Sword:
                return 20;
            case CargoType.Cannoner:
                return 0;
            case CargoType.Ceramic:
                return 12;
            case CargoType.CeramicBroken:
                return 1;
            default:
                return 99;
        }
    }


    //////////
    // PALETTE
    //////////

    public IPalette palette;
    void Awake() {
        palette = new Stories();
    }
}

