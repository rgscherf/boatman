using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CargoFood : ICargo {
    public int sellprice {get {return 1;}}
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
            return "Sell for 5 in Rat Town. Can use as cannon fodder.";
        }
    }
    public CargoType cargoType {
        get {
            return CargoType.Food;
        }
    }
    #endregion



}
