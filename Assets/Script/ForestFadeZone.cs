using UnityEngine;

public class ForestFadeZone : MonoBehaviour
{
    public float fadeDuration = 1f; // temps pour réduire / remonter
    private AudioSource loopSource;

    private void Start()
    {
        // récupère la loopSource de l'AudioManager
        loopSource = AudioManager.Instance.loopSource;
        if (loopSource == null)
            Debug.LogWarning("AudioManager.loopSource non assigné !");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(FadeVolume(0f)); // réduit à 20% du volume
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(FadeVolume(1f)); // revient à 100%
        }
    }

    private System.Collections.IEnumerator FadeVolume(float targetVolume)
    {
        float startVolume = loopSource.volume;
        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            loopSource.volume = Mathf.Lerp(startVolume, targetVolume, time / fadeDuration);
            yield return null;
        }
        loopSource.volume = targetVolume;
    }
}