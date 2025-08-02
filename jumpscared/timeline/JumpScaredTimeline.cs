using UnityEngine;
using UnityEngine.Playables;
using System.Collections;
using System.Linq;

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

            // Switch camera
            MainCamera(false);
            CameraScared(true);

            timelineScene.Play();
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

    IEnumerator SwitchBackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        CameraScared(false);
        MainCamera(true);
    }
}
