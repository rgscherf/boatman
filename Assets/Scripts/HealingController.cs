using UnityEngine;
using System.Collections;

public class HealingController : MonoBehaviour {

    Transform player;
    const float rotateSpeed = -800f;

    const float alive = 0.5f;
    Timer aliveTimer;

    const int healing = 3;

    public void Init(Transform playerTransform, Vector2 mouseClickPos) {
        player = playerTransform;
        transform.position = player.position + new Vector3(0f, 1f, 0f);
        player.gameObject.GetComponent<HealthController>().HealDamage(healing);
    }

    void Start () {
        aliveTimer = new Timer(alive);
    }

    void Update () {
        if (aliveTimer.TickCheck(Time.deltaTime)) {
            Object.Destroy(gameObject);
        }
        transform.position = player.position + new Vector3(0f, 1f, 0f);
        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
    }
}
