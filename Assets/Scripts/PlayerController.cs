using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    Entities entities;

    Rigidbody2D rigid;
    SpriteRenderer spr;

    bool moveMouseDownPreviousFrame;
    Vector2 mouseMoveDownPos;
    Vector2 mouseMoveUpPos;
    GameObject paddleLine;
    Vector3 paddleStart;

    const float cooldown = 1f;
    float current = 2f;

    // Use this for initialization
    void Start () {
        entities = GameObject.Find("Entities").GetComponent<Entities>();
        rigid = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        spr.color = entities.palette.player;
    }

    void InitPaddle(Vector3 startpos) {
        moveMouseDownPreviousFrame = true;

        paddleLine = (GameObject) Instantiate(entities.paddleLine, transform.position, Quaternion.identity);
        var lr = paddleLine.GetComponent<LineRenderer>();
        lr.SetColors(entities.palette.UI, entities.palette.UI);
        lr.SetWidth(0.25f, 0.25f);

        startpos.z = -9;
        paddleStart = startpos;
        paddleLine.GetComponent<LineRenderer>().SetPositions(new[] {paddleStart, paddleStart});
    }

    void ReleasePaddle(Vector3 mousepos) {
        Object.Destroy(paddleLine);
        moveMouseDownPreviousFrame = false;

        // TODO: add force based on distance...
    }

    void UpdatePaddle(Vector3 mousepos) {
        var dist = Vector3.Distance(paddleStart, mousepos);
        var lr = paddleLine.GetComponent<LineRenderer>();
        mousepos.z = -9;
        lr.SetPosition(1, mousepos);
        lr.SetWidth(0f, dist * 0.25f);

        // need max distance/power!
    }

    void MouseMove() {
        var moveMouseDownThisFrame = Input.GetMouseButton(1);

        var mousePos = Input.mousePosition;
        var worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        worldMousePos.z = -1f;

        if (moveMouseDownThisFrame) {
            if (!moveMouseDownPreviousFrame) {
                // start paddle action
                InitPaddle(worldMousePos);
            } else {
                // continue paddle action
                UpdatePaddle(worldMousePos);
            }
        } else {
            // we are not paddling this frame
            if (moveMouseDownPreviousFrame) {
                // end paddle action
                ReleasePaddle(worldMousePos);
            }
        }
    }

    // Update is called once per frame
    void Update () {
        current += Time.deltaTime;
        spr.flipX = rigid.velocity.x < 0;
        MouseMove();
    }
}
