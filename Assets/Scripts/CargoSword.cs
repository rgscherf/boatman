using UnityEngine;
using System;

public class CargoSword : ICargo {
    #region ICargo implementation
    public UnityEngine.Sprite cargoImage {
        get {
            return GameObject.Find("Entities").GetComponent<Entities>().cargoSwordSprite;
        }
    }
    public string cargoTitle {
        get {
            return "Grtswrd";
        }
    }
    public string cargoBody {
        get {
            return String.Format("Ultimate melee weapon. Sell for {0} in Rat Town.", sellprice);
        }
    }
    public global::CargoType cargoType {
        get {
            return CargoType.Sword;
        }
    }
    public int price {
        get {
            return Entities.Price(CargoType.Sword);
        }
    }
    public int sellprice {
        get {
            return Entities.SellPrice(CargoType.Sword);
        }
    }
    public UnityEngine.GameObject cargoFireObject {
        get {
            return GameObject.Find("Entities").GetComponent<Entities>().cargoSwordObject;
        }
    }
    public float cargoFireTimer {
        get {
            return 1f;
        }
    }
    #endregion

}
