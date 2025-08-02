using UnityEngine;

public class JumpscareZone : MonoBehaviour
{
    public GameObject jumpscarePrefab;
    private bool triggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        if (other.CompareTag("Player"))
        {
            triggered = true;
            TriggerJumpscare();
        }
    }

    void TriggerJumpscare()
    {
        GameObject js = Instantiate(jumpscarePrefab);
        GameObject camScared = GameObject.FindWithTag("JumpScaredCam");

        if (camScared == null) return;

        //js.transform.SetParent(Camera.main.transform); // ini default Camera.main == tag "MainCamera"
        js.transform.SetParent(camScared.transform); // ini custom tag dari tag "JumpScaredCam"
        js.transform.localPosition = new Vector3(0, 0, 3);
        js.transform.localRotation = Quaternion.identity;
        js.transform.localScale = Vector3.one * 2f;
        Destroy(js, 2f);
    }
}
