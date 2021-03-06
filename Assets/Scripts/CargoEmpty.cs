﻿using UnityEngine;
using System.Collections;

public class CargoEmpty : ICargo {
    #region ICargo implementation
    public UnityEngine.Sprite cargoImage {
        get {
            return GameObject.Find("Entities").GetComponent<Entities>().cargoEmptySprite;
        }
    }
    public string cargoTitle {
        get {
            return "Empty";
        }
    }
    public string cargoBody {
        get {
            return "";
        }
    }
    public global::CargoType cargoType {
        get {
            return CargoType.Empty;
        }
    }
    public int price {
        get {
            return Entities.Price(CargoType.Empty);
        }
    }
    public int sellprice {
        get {
            return Entities.SellPrice(CargoType.Empty);
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
