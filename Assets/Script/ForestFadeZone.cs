using UnityEngine;

public class DoorForestTrigger : MonoBehaviour
{
    [Header("Forest Settings")]
    public AudioClip forestClip;       // clip à jouer pour la forêt
    public float fadeDuration = 1f;    // durée du fade
    public float insideVolume = 0.2f;  // volume quand le joueur est à l'intérieur
    public float outsideVolume = 1f;   // volume quand le joueur est dehors

    private AudioSource loopSource;
    private bool playerInside = false;

    private void Start()
    {
        // Récupère le loopSource du AudioManager
        loopSource = AudioManager.Instance.loopSource;
        if (loopSource == null)
        {
            Debug.LogWarning("AudioManager.loopSource non assigné !");
            return;
        }

        // Centralisation : joue le clip de forêt dès le départ si pas déjà en train de jouer
        if (loopSource.clip != forestClip)
        {
            AudioManager.Instance.PlayLoop(forestClip);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = !playerInside; // inverse l'état
            float targetVolume = playerInside ? insideVolume : outsideVolume;
            StopAllCoroutines();
            StartCoroutine(FadeVolume(targetVolume));
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

    // Fonction utilitaire pour jouer un SFX de porte
    public void PlayDoorSFX(AudioClip clip)
    {
        AudioManager.Instance.PlaySFX(clip);
    }
}