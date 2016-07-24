using UnityEngine;
using System.Collections;

public class CargoRock : ICargo {
    #region ICargo implementation
    public UnityEngine.Sprite cargoImage {
        get {
            return GameObject.Find("Entities").GetComponent<Entities>().cargoRockSprite;
        }
    }
    public string cargoTitle {
        get {
            return "Dumb Rock";
        }
    }
    public string cargoBody {
        get {
            return "Useless. Sell in Old Town for 2.";
        }
    }
    public global::CargoType cargoType {
        get {
            return CargoType.Rock;
        }
    }
    public int price {
        get {
            return 0;
        }
    }
    public int sellprice {
        get {
            return 2;
        }
    }
    public UnityEngine.GameObject cargoFireObject {
        get {
            return GameObject.Find("Entities").GetComponent<Entities>().emptyObject;
        }
    }
    public float cargoFireTimer {
        get {
            return 9999;
        }
    }
    #endregion
}
