using UnityEngine;

public class CameraPositionScripter : MonoBehaviour {

    public Vector3 mainPosition;
    public Vector3 titlePosition;
    public Vector3 tutorialPosition;

    // Use this for initialization
    void Start () {
        mainPosition = new Vector3(19.5f, 4.8f, -10f);
        titlePosition = new Vector3(-100f, -100f, -10f);
        tutorialPosition = new Vector3(-100f, -145f, -10f);
        transform.position = titlePosition;
    }

    public void AdvanceTutorial() {
        TweenHelper(gameObject, tutorialPosition, 1.5f);
        GetComponent<CameraShaker>().ResetPosition(tutorialPosition);
    }

    public void TweenHelper(GameObject obj, Vector3 vec, float slide) {
        LeanTween.move(obj, vec, slide).setEase(LeanTweenType.easeInQuad);
    }

    public void CompleteTutorial() {
        transform.position = mainPosition;
        GetComponent<CameraShaker>().ResetPosition(mainPosition);
    }


}
