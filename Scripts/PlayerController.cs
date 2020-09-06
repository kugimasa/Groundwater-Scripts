using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 0.01f;
    [SerializeField] float jumpStep = 2.0f;
    [SerializeField] bool onGround;
    [SerializeField] bool onWater;
    [SerializeField] float drownDuration = 2.0f;
    public bool isMoving;
    public bool drowned;
    public bool cleared;
    public AudioClip inWaterSE;
    public AudioClip drowningSE;
    public AudioClip clearSE;

    Rigidbody rb;
    Animator animator;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        isMoving = true;
        onGround = true;
        drowned = false;
        cleared = false;
    }

    private void Update() {
        if (isMoving && !cleared) 
        {
            Move();
            Jump();
        }
    }
    public void Move()
    {
        animator.SetBool("isWalking", false);
        audioSource.Pause();
        if (InputController.UP_KEY)
        {
            if (onWater)
            {
                animator.SetBool("isWalking", true);
                audioSource.clip = inWaterSE;
                audioSource.Play();
            }
            else if (onGround)
            {
                animator.SetBool("isWalking", true);
            }
            gameObject.transform.Translate(Vector3.right * speed, Space.Self);   
        }
        if (InputController.DOWN_KEY)
        {
            if (onWater)
            {
                animator.SetBool("isWalking", true);
                audioSource.clip = inWaterSE;
                audioSource.Play();
            }
            else if (onGround)
            {
                animator.SetBool("isWalking", true);
            }
            gameObject.transform.Translate(Vector3.left * speed, Space.Self);
        }
        if (InputController.RIGHT_KEY_UP)
        {
            animator.SetBool("isWalking", false);
            audioSource.Pause();
            Vector3 currentRot = gameObject.transform.rotation.eulerAngles;
            gameObject.transform.rotation = Quaternion.Euler(currentRot.x, currentRot.y  + 90.0f, currentRot.z);
        }
        if (InputController.LEFT_KEY_UP)
        {
            animator.SetBool("isWalking", false);
            audioSource.Pause();
            Vector3 currentRot = gameObject.transform.rotation.eulerAngles;
            gameObject.transform.rotation = Quaternion.Euler(currentRot.x, currentRot.y - 90.0f, currentRot.z);
        }
    }

    public void Jump()
    {
        if (onGround && InputController.JUMP_KEY)
        {
            audioSource.Pause();
            rb.AddForce(Vector3.up * jumpStep, ForceMode.Impulse);
        }
    }

    public void PlayClearedSE()
    {
        audioSource.PlayOneShot(clearSE);
    }
    private void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.tag == "Stage") 
        {
            onGround = true;
        }
        if (other.gameObject.tag == "WaterWave")
        {
            onWater = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Stage") 
        {
            onGround = false;
        }
        if (other.gameObject.tag == "WaterWave")
        {
            onWater = false;
        }
    }
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Water")
        {
            StartCoroutine(Drown());
        }
        if (other.gameObject.tag == "Goal")
        {
            cleared = true;
        }
    }

    private IEnumerator Drown()
    {
        isMoving = false;
        audioSource.PlayOneShot(drowningSE);
        yield return new WaitForSeconds(drownDuration);
        drowned = true;
    }
}
