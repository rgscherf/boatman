using UnityEngine;

public class CameraShaker : MonoBehaviour {
    Vector3 originalPosition;
    bool shaking;

    float magnitude;

    Timer shakeTimer;

    // Use this for initialization
    void Start () {
        originalPosition = transform.position;
        shakeTimer = new Timer(0f);
    }

    void Update() {
        shakeTimer.Tick(Time.deltaTime);
        if (shakeTimer.Check()) {
            if (shaking) {
                shaking = false;
                transform.position = originalPosition;
            }
        } else {
            Vector3 rand = Random.insideUnitCircle * magnitude;
            transform.position = originalPosition + new Vector3(rand.x, rand.y, 0f);
        }
    }

    public void ResetPosition(Vector3 pos) {
        originalPosition = pos;
    }

    public void Shake(float time, float mag = 0.1f) {
        magnitude = mag;
        shakeTimer = new Timer(time);
        shaking = true;
    }
}
