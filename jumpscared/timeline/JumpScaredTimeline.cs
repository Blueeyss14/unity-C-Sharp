using UnityEngine;
using UnityEngine.Playables;
using System.Collections;

public class TimelineScared : MonoBehaviour
{
    public GameObject playerObject;
    public PlayableDirector timelineScene;
    public Camera mainCamera;
    public Camera scaredCamera;

    private bool triggered = false;

    void OnTriggerEnter(Collider other) {
        if (triggered) return;
        if (other.CompareTag("Player"))
        {
            triggered = true;
            Debug.Log("Playing on " + scaredCamera.name);

            timelineScene.Play();

            // Switch camera (delayed)
            StartCoroutine(DelayedCameraSwitch(0.12f));
            //from timeline duration (dynamic)
            StartCoroutine(SwitchBackAfterDelay((float)timelineScene.duration));
        }
    }

    void MainCamera(bool isActive) {
        mainCamera.enabled = isActive;
        mainCamera.GetComponent<AudioListener>().enabled = isActive;
    }

    void CameraScared(bool isActive) {
        scaredCamera.enabled = isActive;
        scaredCamera.GetComponent<AudioListener>().enabled = isActive;

        //player is freezing
        playerObject.GetComponent<Player>().enabled = !isActive;
        playerObject.GetComponent<Animator>().enabled = !isActive;

    }

    //because the audio delay, so I delayed switching camera
    IEnumerator DelayedCameraSwitch(float delay)
    {
        yield return new WaitForSeconds(delay);

        Debug.Log("Switch ke CameraScared");
        MainCamera(false);
        CameraScared(true);
    }

    IEnumerator SwitchBackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        CameraScared(false);
        MainCamera(true);
    }
}
