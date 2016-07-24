using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShopController : MonoBehaviour {
    public Sprite empty;

    Dictionary<int, ICargo> inventory;
    Dictionary<int, GameObject> inventorySlots;

    void Awake() {
        inventory = new Dictionary<int, ICargo>();
        MakeSlots();
    }

    void MakeSlots() {
        inventorySlots = new Dictionary<int, GameObject>();
        inventorySlots[1] = GameObject.Find("BuySlot1");
        inventorySlots[2] = GameObject.Find("BuySlot2");
        inventorySlots[3] = GameObject.Find("BuySlot3");
    }

    public void Repaint() {
        if (inventorySlots == null) {
            MakeSlots();
        }

        for (var i = 1; i <= 3; i++) {
            if (inventory.ContainsKey(i)) {
                inventorySlots[i].transform.FindChild("Image").GetComponent<Image>().sprite = inventory[i].cargoImage;
                inventorySlots[i].transform.FindChild("Title").GetComponent<Text>().text = inventory[i].cargoTitle;
                inventorySlots[i].transform.FindChild("Body").GetComponent<Text>().text = inventory[i].cargoBody;
                inventorySlots[i].transform.FindChild("Price").GetComponent<Text>().text = inventory[i].price.ToString();
            } else {
                inventorySlots[i].transform.FindChild("Image").GetComponent<Image>().sprite = empty;
                inventorySlots[i].transform.FindChild("Title").GetComponent<Text>().text = "Empty";
                inventorySlots[i].transform.FindChild("Body").GetComponent<Text>().text = "";
                inventorySlots[i].transform.FindChild("Price").GetComponent<Text>().text = "--";
            }
        }
    }

    public void Restock(ICargo[] newstock) {
        if (inventory == null) {
            inventory = new Dictionary<int, ICargo>();
        }
        inventory.Clear();
        for (var i = 0; i <= 2; i++) {
            // remember that UI dicts are 1-indexed.
            inventory[i + 1] = newstock[i];
        }
        Repaint();
    }

    public int GetPrice(int index) {
        return inventory[index].price;
    }
    public bool ContainsKey(int index) {
        return inventory.ContainsKey(index);
    }
    public CargoType GetCargoType(int index) {
        return inventory.ContainsKey(index)
               ? inventory[index].cargoType
               : CargoType.None;
    }
    public ICargo GetItem(int index) {
        var ret = inventory[index];
        inventory.Remove(index);
        Repaint();
        return ret;
    }
}
