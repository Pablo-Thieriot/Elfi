using UnityEngine;

public class FireSoundEmitter : MonoBehaviour
{
    public AudioClip fireLoop;        // le son du feu
    public float minDistance = 1f;    // distance à laquelle le son est à 100%
    public float maxDistance = 10f;   // distance à laquelle le son disparaît
    private AudioSource loopSource;

    void Start()
    {
        if (fireLoop == null)
        {
            Debug.LogWarning("Aucun clip assigné au feu !");
            return;
        }

        // Crée ou récupère un AudioSource sur ce GameObject
        loopSource = gameObject.AddComponent<AudioSource>();
        loopSource.clip = fireLoop;
        loopSource.loop = true;
        loopSource.spatialBlend = 1f;  // 3D
        loopSource.minDistance = minDistance;
        loopSource.maxDistance = maxDistance;
        loopSource.rolloffMode = AudioRolloffMode.Linear; // attenuation linéaire
        loopSource.Play();
    }

    // Optionnel : arrêter le feu
    public void StopFire()
    {
        if (loopSource != null)
        {
            loopSource.Stop();
        }
    }
}