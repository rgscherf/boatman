using UnityEngine;

public class FireObjectController : MonoBehaviour, IFirable {
    public void Init(CargoType objectType, Transform playerTransform, Vector2 mouseClickPos) {
        switch (objectType) {
            case CargoType.Dagger:
                GetComponent<DaggerController>().Init(playerTransform, mouseClickPos);
                break;
            case CargoType.Sword:
                GetComponent<SwordController>().Init(playerTransform, mouseClickPos);
                break;
            case CargoType.Food:
                GetComponent<FoodController>().Init(playerTransform, mouseClickPos);
                break;
            case CargoType.Healing:
                GetComponent<HealingController>().Init(playerTransform, mouseClickPos);
                break;
            case CargoType.Cannoner:
                GetComponent<CannonerController>().Init(playerTransform, mouseClickPos);
                break;
            default:
                break;
        }
    }
}
