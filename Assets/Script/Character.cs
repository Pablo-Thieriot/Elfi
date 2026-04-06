using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    Camera cam;
    CharacterController characterController;
    float maxSpeed = 10, acceleration = 10, jumpForce = 5;
    float speed, verticalMovement;
    Vector3 direction, directionForward, directionRight, nextDir;
    Animator animator;
    AudioSource audioSource;

    [Header("Sons de pas par surface")]
    [SerializeField] AudioClip stoneSound;
    [SerializeField] AudioClip defaultSound;

    [Header("Réglages Aléatoires")]
    [Range(0.1f, 0.5f)][SerializeField] float pitchRange = 0.5f; // Variation 

    void Awake()
    {
        cam = Camera.main;
        direction = transform.forward;
        nextDir = transform.forward;
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        gravity();
        Move();
        characterController.Move((direction * speed + verticalMovement * Vector3.up) * Time.deltaTime);
        animator.SetFloat("Speed", speed / maxSpeed);
    }

    private void Move()
    {
        if ((Input.GetAxisRaw("Vertical")) != 0 || (Input.GetAxisRaw("Horizontal")) != 0)
        {
            directionForward = cam.transform.forward;
            directionForward.y = 0;
            directionForward *= Input.GetAxisRaw("Vertical");

            directionRight = cam.transform.right;
            directionRight.y = 0;
            directionRight *= Input.GetAxisRaw("Horizontal");

            nextDir = Vector3.Normalize(directionForward + directionRight);
            direction = Vector3.Lerp(direction, nextDir, Time.deltaTime * 2);

            if (speed < maxSpeed)
                speed += acceleration * Time.deltaTime;
            else
                speed = maxSpeed;
        }
        else
        {
            if (speed != 0)
            {
                if (speed <= 2 * acceleration * Time.deltaTime)
                    speed = 0;
                else
                    speed -= 2 * acceleration * Time.deltaTime;
            }
        }

        transform.rotation = Quaternion.LookRotation(direction, transform.up);
    }

    private void gravity()
    {
        if (verticalMovement <= 0 && characterController.isGrounded)
            verticalMovement = -5;
        else
            verticalMovement -= jumpForce * 2 * Time.deltaTime;
    }

    public void StepSound()
    {
        RaycastHit hit;

        Debug.DrawRay(transform.position, Vector3.down * 1.5f, Color.red, 1.0f);

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f))
        {
            Debug.Log("Objet : " + hit.collider.name + " | Tag : " + hit.collider.tag);

            AudioClip soundToPlay = null;


            if (hit.collider.CompareTag("Stone"))
            {
                soundToPlay = stoneSound;
            }
            else
            {
                soundToPlay = defaultSound;
            }

            if (soundToPlay != null && audioSource != null)
            {
                audioSource.pitch = Random.Range(1f - pitchRange, 1f + pitchRange);
                audioSource.PlayOneShot(soundToPlay);
            }
        }
        else
        {
            Debug.Log("Le Raycast ne touche aucun collider sous le personnage.");
        }
    }
}