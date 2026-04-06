using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    Animator animator;
    private AudioSource audioSource;
    public AudioClip openSound;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // Animation de la porte
        if (animator != null)
            animator.SetBool("In", true);

        // Son de la porte via AudioManager
        if (openSound != null)
            AudioManager.Instance.PlaySFX(openSound);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // Animation de la porte
        if (animator != null)
            animator.SetBool("In", false);
    }
}