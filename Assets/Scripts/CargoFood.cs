using UnityEngine;
using System;

public class CargoFood : ICargo {
    public int sellprice {get {return 6;}}
    public int price {
        get { return 2; }
    }
    public float cargoFireTimer {
        get {
            return 1f;
        }
    }
    public UnityEngine.GameObject cargoFireObject {
        get {
            return GameObject.Find("Entities").GetComponent<Entities>().cargoFoodObject;
        }
    }

    #region ICargo implementation
    public Sprite cargoImage {
        get {
            return GameObject.Find("Entities").GetComponent<Entities>().cargoFoodSprite;
        }
    }
    public string cargoTitle {
        get {
            return "Food";
        }
    }
    public string cargoBody {
        get {
            return String.Format("Sell for {0} in Rat Town. Can use as cannon fodder.", sellprice);
        }
    }
    public CargoType cargoType {
        get {
            return CargoType.Food;
        }
    }
    #endregion



}
