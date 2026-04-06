using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sources")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource loopSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;

        sfxSource.pitch = Random.Range(0.9f, 1.1f);
        sfxSource.PlayOneShot(clip);
    }

    public void PlaySFX(AudioClip[] clips)
    {
        if (clips == null || clips.Length == 0) return;

        AudioClip clip = clips[Random.Range(0, clips.Length)];

        sfxSource.pitch = Random.Range(0.9f, 1.1f);
        sfxSource.PlayOneShot(clip);
    }

    public void PlaySFXAtPosition(AudioClip clip, Vector3 position)
    {
        if (clip == null) return;

        AudioSource.PlayClipAtPoint(clip, position);
    }

    public void PlayLoop(AudioClip clip)
    {
        if (clip == null) return;

        loopSource.clip = clip;
        loopSource.loop = true;
        loopSource.Play();
    }

    public void StopLoop()
    {
        loopSource.Stop();
    }
}