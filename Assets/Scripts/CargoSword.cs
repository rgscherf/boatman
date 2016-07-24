using UnityEngine;
using System.Collections;

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
            return "Slow but inflicts heavy damage. Sell for 5.";
        }
    }
    public global::CargoType cargoType {
        get {
            return CargoType.Sword;
        }
    }
    public int price {
        get {
            return 13;
        }
    }
    public int sellprice {
        get {
            return 5;
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
