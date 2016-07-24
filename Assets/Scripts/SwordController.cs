using UnityEngine;
using System.Collections;

public class SwordController : MonoBehaviour {
    Transform player;

    float swingSpeed = -900f;

    const float alive = 0.53f;
    Timer aliveTimer;

    public void Init(Transform playerTransform, Vector2 mouseClickPos) {
        player = playerTransform;
        transform.position = player.position;
        Vector3 mousePos = new Vector3(mouseClickPos.x, mouseClickPos.y, -1);
        Quaternion rot = Quaternion.LookRotation (mousePos - transform.position, transform.TransformDirection(Vector3.back));
        transform.rotation = new Quaternion(0, 0, rot.z, rot.w);
    }

    // Use this for initialization
    void Start () {
        aliveTimer = new Timer(alive);
    }

    // Update is called once per frame
    void Update () {
        transform.position = player.position;
        transform.Rotate(Vector3.forward, swingSpeed * Time.deltaTime);
        aliveTimer.Tick(Time.deltaTime);
        if (aliveTimer.Check()) {
            Object.Destroy(gameObject);
        }
    }
}
