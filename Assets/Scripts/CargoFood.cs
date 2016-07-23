using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CargoFood : ICargo {

    #region ICargo implementation
    public void leftClick() {
        throw new System.NotImplementedException();
    }
    public void shopClick() {
        throw new System.NotImplementedException();
    }
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
