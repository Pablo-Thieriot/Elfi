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
        animator.SetBool("In", true);

        if (audioSource != null && openSound != null)
        {
            audioSource.PlayOneShot(openSound);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("In", false);
    }
}