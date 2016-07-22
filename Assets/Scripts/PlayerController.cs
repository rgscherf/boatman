using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    Rigidbody2D rigid;
    SpriteRenderer spr;
    public GameObject paddle;

    const float cooldown = 1f;
    float current = 2f;

    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        paddle.SetActive(false);
    }

    void KeyboardMove() {
        const float moveSpeed = 30f;
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");

        var mv = new Vector2(x, y) * moveSpeed;
        rigid.AddForce(mv);
        current = 0f;
    }

    void MouseMove() {
        // press RMB to make paddle appear at bow of boat.
        // hold RMB and it draws toward stern.
        // release RMB to row in direction of mouse.
        // closeness to end of row range determines row power.
        if (Input.GetAxisRaw("Fire2") == 1) {
            paddle.SetActive(true);
        } else {
            paddle.SetActive(false);
        }
    }

    void FixedUpdate() {
        if (current >= cooldown) {
            KeyboardMove();
        }
        MouseMove();

    }
    // Update is called once per frame
    void Update () {
        current += Time.deltaTime;
        spr.flipX = rigid.velocity.x < 0;
    }
}
