using UnityEngine;

public class SwordController : MonoBehaviour {

    Transform player;

    public void Init(Transform playerTransform, Vector2 mouseClickPos) {
        player = playerTransform;
        transform.parent = player;
    }
}
