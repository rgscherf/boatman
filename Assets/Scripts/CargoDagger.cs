using UnityEngine;
using System.Collections;

public class CargoDagger : ICargo {
    #region ICargo implementation
    public void leftClick() {
        throw new System.NotImplementedException();
    }
    public void shopClick() {
        throw new System.NotImplementedException();
    }
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
            return "Weak melee weapon with short range.";
        }
    }
    public global::CargoType cargoType {
        get {
            return CargoType.Dagger;
        }
    }
    #endregion
}
