using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BootyController : MonoBehaviour {

    Text moneyText;

    public void Repaint(int money) {
        moneyText.text = money.ToString();
    }

    void Awake () {
        moneyText = GameObject.Find("BootyNumber").GetComponent<Text>();
    }
}
