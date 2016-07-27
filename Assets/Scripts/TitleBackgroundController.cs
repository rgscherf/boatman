using UnityEngine;
using System.Collections.Generic;

public class TitleBackgroundController : MonoBehaviour {

    Slider[] sliders;
    const float slideTime = 2f;

    bool active = false;

    struct Slider {
        public GameObject obj;
        public Vector3 inPos;
        public Vector3 outPos;
        public Slider(GameObject obj, Vector3 inPos, Vector3 outPos) {
            this.obj = obj;
            this.inPos = inPos;
            this.outPos = outPos;
        }
    }

    void SlideIn() {
        foreach (var s in sliders) {
            s.obj.transform.position = s.outPos;
            TweenHelper(s.obj, s.inPos);
        }
    }

    void SlideOut() {
        foreach (var s in sliders) {
            s.obj.transform.position = s.inPos;
            TweenHelper(s.obj, s.outPos);
        }
    }

    void TweenHelper(GameObject obj, Vector3 vec) {
        LeanTween.move(obj, vec, slideTime).setEase(LeanTweenType.easeInCubic);
    }


    // Use this for initialization
    void Start () {
        if (active) {
            sliders = new[] {
                new Slider(GameObject.Find("titlescreen-wipe"), new Vector3(20f, 0f, -9f), new Vector3(71, 0f, -9f))
            };
            SlideOut();
        }
    }
}
