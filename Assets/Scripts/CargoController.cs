using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CargoController : MonoBehaviour {
    public Sprite empty;
    Dictionary<int, GameObject> cargoSlots;
    Entities entities;

    public int currentSelection;
    Timer selectionBlinkTimer;
    bool originalColor;
    const float selectionBlinkSpeed = 0.25f;

    void Update() {
        UpdateSelectionColor();
    }

    void Awake() {
        entities = GameObject.Find("Entities").GetComponent<Entities>();

        cargoSlots = new Dictionary<int, GameObject>();
        cargoSlots[1] = GameObject.Find("CargoSlot1");
        cargoSlots[2] = GameObject.Find("CargoSlot2");
        cargoSlots[3] = GameObject.Find("CargoSlot3");
        cargoSlots[4] = GameObject.Find("CargoSlot4");

        selectionBlinkTimer = new Timer(0f);
    }

    public void Repaint(Dictionary<int, ICargo> dict) {
        for (var i = 1; i < 5; i++) {
            if (dict.ContainsKey(i)) {
                cargoSlots[i].transform.FindChild("Image").GetComponent<Image>().sprite = dict[i].cargoImage;
                cargoSlots[i].transform.FindChild("Title").GetComponent<Text>().text = dict[i].cargoTitle;
                cargoSlots[i].transform.FindChild("Body").GetComponent<Text>().text = dict[i].cargoBody;
            } else {
                cargoSlots[i].transform.FindChild("Image").GetComponent<Image>().sprite = empty;
                cargoSlots[i].transform.FindChild("Title").GetComponent<Text>().text = "Empty";
                cargoSlots[i].transform.FindChild("Body").GetComponent<Text>().text = "";
            }
        }
    }

    void UpdateSelectionColor() {
        if (currentSelection != 0) {
            selectionBlinkTimer.Tick(Time.deltaTime);
            if (selectionBlinkTimer.Check()) {
                cargoSlots[currentSelection].GetComponent<Image>().color = originalColor ?
                        entities.palette.player :
                        entities.palette.danger;
                originalColor = !originalColor;
                selectionBlinkTimer.Reset();
            }
        }
    }

    public void ChangeSelection(int c) {
        if (currentSelection != 0) {
            // cleanup previous selection
            cargoSlots[currentSelection].GetComponent<Image>().color = entities.palette.UI;
        }
        originalColor = false;
        selectionBlinkTimer = new Timer(selectionBlinkSpeed);
        currentSelection = c;
        cargoSlots[currentSelection].GetComponent<Image>().color = entities.palette.player;
    }

}
