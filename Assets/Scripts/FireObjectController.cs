using UnityEngine;

public class FireObjectController : MonoBehaviour, IFirable {
    public void Init(CargoType objectType, Transform playerTransform, Vector2 mouseClickPos) {
        if (objectType == CargoType.Dagger) {
            GetComponent<DaggerController>().Init(playerTransform, mouseClickPos);
        } else if (objectType == CargoType.Food) {
            GetComponent<FoodController>().Init(playerTransform, mouseClickPos);
        }
    }
}
