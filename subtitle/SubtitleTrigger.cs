using UnityEngine;
using TMPro;

public class SubtitleTrigger : MonoBehaviour
{
    public TextMeshProUGUI subtitleText;
    public float delaySecondSubtitle = 3f;

    private bool hasTriggered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            StartCoroutine(ShowSubtitles());
        }
    }

    private System.Collections.IEnumerator ShowSubtitles()
    {
        subtitleText.text = "<color=#5172a8>Author:</color> it's beginning to look a lot like christmas";
        yield return new WaitForSeconds(delaySecondSubtitle);
        subtitleText.text = "omke gas omke gas";
        yield return new WaitForSeconds(2f);
        subtitleText.text = "";
    }
}
