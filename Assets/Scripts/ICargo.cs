using UnityEngine;
using UnityEngine.UI;

public enum CargoType {Food,
                       Dagger,
                      }

public interface ICargo {
    Sprite cargoImage {get;}
    string cargoTitle {get;}
    string cargoBody {get;}
    CargoType cargoType {get;}
    int price {get;}

    GameObject cargoFireObject {get;}
    float cargoFireTimer {get;}
}
