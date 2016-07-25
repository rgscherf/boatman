using UnityEngine;
using System.Collections;
using System;

public class CargoHealing : ICargo {
    #region ICargo implementation
    public UnityEngine.Sprite cargoImage {
        get {
            return GameObject.Find("Entities").GetComponent<Entities>().cargoHealingSprite;
        }
    }
    public string cargoTitle {
        get {
            return "Repair Kit";
        }
    }
    public string cargoBody {
        get {
            return String.Format("Repair hull for 3, one use. Sell for {0}.", sellprice);
        }
    }
    public global::CargoType cargoType {
        get {
            return CargoType.Healing;
        }
    }
    public int price {
        get {
            return Entities.Price(CargoType.Healing);
        }
    }
    public int sellprice {
        get {
            return Entities.SellPrice(CargoType.Healing);
        }
    }
    public UnityEngine.GameObject cargoFireObject {
        get {
            return GameObject.Find("Entities").GetComponent<Entities>().cargoHealingObject;
        }
    }
    public float cargoFireTimer {
        get {
            return 1f;
        }
    }
    #endregion

}
