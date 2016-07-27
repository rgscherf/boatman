using UnityEngine;
using System.Collections.Generic;

public class TitleBackgroundController : MonoBehaviour {

    const float slideTime = .25f;
    Vector3 startpos;
    Vector3 endpos;

    void Awake() {
        startpos = new Vector3(66f, 9.73f, -8f);
        endpos = new Vector3(20f, 9.73f, -8f);
        gameObject.transform.position = startpos;
    }

    public void TweenInRestart() {
        gameObject.transform.position = startpos;
        LeanTween.move(gameObject, endpos, slideTime);
    }

    public void ResetPosition() {
        gameObject.transform.position = startpos;
    }


    void TweenHelper(GameObject obj, Vector3 vec) {
        LeanTween.move(obj, vec, slideTime).setEase(LeanTweenType.easeInOutQuint);
    }


}
