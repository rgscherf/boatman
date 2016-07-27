using UnityEngine;
using UnityEngine.UI;

public enum Paint {
    Player,
    Background,
    Danger,
    Geometry,
    UI
}

public class SettableColor : MonoBehaviour {

    public Paint paintedColor;
    public float alpha = 1f;

    void Start() {
        ChangeSprite();
        ChangeUIText();
        ChangeUIImage();
    }

    void ChangeUIImage() {
        if (GetComponent<Image>() == null) { return; }
        switch (paintedColor) {
            case Paint.Player:
                GetComponent<Image>().color = GameObject.Find("Entities").GetComponent<Entities>().palette.player * new Color(1f, 1f, 1f, alpha);
                break;
            case Paint.Background:
                GetComponent<Image>().color = GameObject.Find("Entities").GetComponent<Entities>().palette.background * new Color(1f, 1f, 1f, alpha);
                break;
            case Paint.Danger:
                GetComponent<Image>().color = GameObject.Find("Entities").GetComponent<Entities>().palette.danger * new Color(1f, 1f, 1f, alpha);
                break;
            case Paint.Geometry:
                GetComponent<Image>().color = GameObject.Find("Entities").GetComponent<Entities>().palette.geometry * new Color(1f, 1f, 1f, alpha);
                break;
            case Paint.UI:
                GetComponent<Image>().color = GameObject.Find("Entities").GetComponent<Entities>().palette.UI * new Color(1f, 1f, 1f, alpha);
                break;
        }
    }

    void ChangeUIText() {
        if (GetComponent<Text>() == null) { return; }
        switch (paintedColor) {
            case Paint.Player:
                GetComponent<Text>().color = GameObject.Find("Entities").GetComponent<Entities>().palette.player * new Color(1f, 1f, 1f, alpha);
                break;
            case Paint.Background:
                GetComponent<Text>().color = GameObject.Find("Entities").GetComponent<Entities>().palette.background * new Color(1f, 1f, 1f, alpha);
                break;
            case Paint.Danger:
                GetComponent<Text>().color = GameObject.Find("Entities").GetComponent<Entities>().palette.danger * new Color(1f, 1f, 1f, alpha);
                break;
            case Paint.Geometry:
                GetComponent<Text>().color = GameObject.Find("Entities").GetComponent<Entities>().palette.geometry * new Color(1f, 1f, 1f, alpha);
                break;
            case Paint.UI:
                GetComponent<Text>().color = GameObject.Find("Entities").GetComponent<Entities>().palette.UI * new Color(1f, 1f, 1f, alpha);
                break;
        }
    }

    void ChangeSprite() {
        if (GetComponent<SpriteRenderer>() == null) { return; }
        switch (paintedColor) {
            case Paint.Player:
                GetComponent<SpriteRenderer>().color = GameObject.Find("Entities").GetComponent<Entities>().palette.player * new Color(1f, 1f, 1f, alpha);
                break;
            case Paint.Background:
                GetComponent<SpriteRenderer>().color = GameObject.Find("Entities").GetComponent<Entities>().palette.background * new Color(1f, 1f, 1f, alpha);
                break;
            case Paint.Danger:
                GetComponent<SpriteRenderer>().color = GameObject.Find("Entities").GetComponent<Entities>().palette.danger * new Color(1f, 1f, 1f, alpha);
                break;
            case Paint.Geometry:
                GetComponent<SpriteRenderer>().color = GameObject.Find("Entities").GetComponent<Entities>().palette.geometry * new Color(1f, 1f, 1f, alpha);
                break;
            case Paint.UI:
                GetComponent<SpriteRenderer>().color = GameObject.Find("Entities").GetComponent<Entities>().palette.UI * new Color(1f, 1f, 1f, alpha);
                break;
        }
    }
}

