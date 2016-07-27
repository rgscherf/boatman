using UnityEngine;
using System;

public class CargoCeramicBroken : ICargo {
    #region ICargo implementation
    public UnityEngine.Sprite cargoImage {
        get {
            return GameObject.Find("Entities").GetComponent<Entities>().cargoCeramicBrokenSprite;
        }
    }
    public string cargoTitle {
        get {
            return "Broken Ceramics";
        }
    }
    public string cargoBody {
        get {
            return String.Format("They were fragile. Sell for {0}.", sellprice);
        }
    }
    public global::CargoType cargoType {
        get {
            return CargoType.CeramicBroken;
        }
    }
    public int price {
        get {
            return Entities.Price(CargoType.CeramicBroken);
        }
    }
    public int sellprice {
        get {
            return Entities.SellPrice(CargoType.CeramicBroken);
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
