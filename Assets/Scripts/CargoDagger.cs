using UnityEngine;
using System.Collections;

public class CargoDagger : ICargo {
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
            return "Weak melee weapon with short range.";
        }
    }
    public CargoType cargoType {
        get {
            return CargoType.Dagger;
        }
    }
    #endregion
}
