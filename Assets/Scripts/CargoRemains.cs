using UnityEngine;
using System;
using System.Collections;

public class CargoRemains : ICargo {
    #region ICargo implementation
    public UnityEngine.Sprite cargoImage {
        get {
            return GameObject.Find("Entities").GetComponent<Entities>().cargoRemainsSprite;
        }
    }
    public string cargoTitle {
        get {
            return "Remains";
        }
    }
    public string cargoBody {
        get {
            return String.Format("Valuable trophy. Sell anywhere for {0}", sellprice);
        }
    }
    public global::CargoType cargoType {
        get {
            return CargoType.Remains;
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
