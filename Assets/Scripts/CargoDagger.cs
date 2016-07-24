using UnityEngine;
using System;
using System.Collections;

public class CargoDagger : ICargo {
    public int sellprice {get {return 1;}}
    public int price {
        get { return 1;}
    }
    public float cargoFireTimer {
        get {
            return 0.35f;
        }
    }
    public GameObject cargoFireObject {
        get {
            return GameObject.Find("Entities").GetComponent<Entities>().cargoDaggerObject;
        }
    }
    #region ICargo implementation
    public UnityEngine.Sprite cargoImage {
        get {
            return GameObject.Find("Entities").GetComponent<Entities>().cargoDaggerSprite;
        }
    }
    public string cargoTitle {
        get {
            return "Dagger";
        }
    }
    public string cargoBody {
        get {
            return String.Format("Weak melee weapon with short range. Sell for {0}.", sellprice);
        }
    }
    public CargoType cargoType {
        get {
            return CargoType.Dagger;
        }
    }
    #endregion
}
