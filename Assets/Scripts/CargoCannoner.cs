using UnityEngine;
using System.Collections;

public class CargoCannoner : ICargo {
    #region ICargo implementation
    public UnityEngine.Sprite cargoImage {
        get {
            return GameObject.Find("Entities").GetComponent<Entities>().cargoCannonerSprite;
        }
    }
    public string cargoTitle {
        get {
            return "Cannoner";
        }
    }
    public string cargoBody {
        get {
            return "Fire cannon for 2 booty. Debark at any port.";
        }
    }
    public global::CargoType cargoType {
        get {
            return CargoType.Cannoner;
        }
    }
    public int price {
        get {
            return Entities.Price(CargoType.Cannoner);
        }
    }
    public int sellprice {
        get {
            return Entities.SellPrice(CargoType.Cannoner);
        }
    }
    public UnityEngine.GameObject cargoFireObject {
        get {
            return GameObject.Find("Entities").GetComponent<Entities>().cargoCannonerObject;
        }
    }
    public float cargoFireTimer {
        get {
            return 1;
        }
    }
    #endregion

}
