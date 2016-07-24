using UnityEngine;
using UnityEngine.UI;

public enum CargoType {Food,
                       None,
                       Dagger,
                       Rock,
                       Sword,
                      }

public interface ICargo {
    Sprite cargoImage {get;}
    string cargoTitle {get;}
    string cargoBody {get;}
    CargoType cargoType {get;}
    int price {get;}
    int sellprice {get;}

    GameObject cargoFireObject {get;}
    float cargoFireTimer {get;}
}
