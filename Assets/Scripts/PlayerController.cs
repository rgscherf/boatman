using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    Entities entities;

    Rigidbody2D rigid;
    SpriteRenderer spr;
    CargoController cargoController;

    // for paddle-based movement
    bool moveMouseDownPreviousFrame;
    GameObject paddleLine;
    Vector3 paddleStart;
    const float maxDistance = 20f;
    const float paddleForceMod = 10f;

    // Use this for initialization
    void Start () {
        entities = GameObject.Find("Entities").GetComponent<Entities>();
        cargoController = GameObject.Find("Cargo").GetComponent<CargoController>();
        rigid = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        spr.color = entities.palette.player;
    }

    void CheckCargoSelection() {
        if (Input.GetAxisRaw("Select1") == 1) {
            cargoController.ChangeSelection(1);
        } else if (Input.GetAxisRaw("Select2") == 1) {
            cargoController.ChangeSelection(2);
        } else if (Input.GetAxisRaw("Select3") == 1) {
            cargoController.ChangeSelection(3);
        } else if (Input.GetAxisRaw("Select4") == 1) {
            cargoController.ChangeSelection(4);
        }
    }

    // Update is called once per frame
    void Update () {
        spr.flipX = rigid.velocity.x < 0;
        MouseMove();
        CheckCargoSelection();
    }

    /////////////////
    // MOVEMENT STUFF
    /////////////////

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
        var dist = Mathf.Max(maxDistance, Vector3.Distance(paddleStart, mousepos));
        var dir = Vector3.ClampMagnitude(paddleStart - mousepos, dist);
        rigid.AddForce(dir * paddleForceMod);

        Object.Destroy(paddleLine);
        moveMouseDownPreviousFrame = false;
    }

    void UpdatePaddle(Vector3 mousepos) {
        var lr = paddleLine.GetComponent<LineRenderer>();
        var dist = Vector3.Distance(paddleStart, mousepos);
        mousepos.z = -9;
        lr.SetPosition(1, mousepos);
        lr.SetWidth(0f, dist * 0.15f);
    }
}
