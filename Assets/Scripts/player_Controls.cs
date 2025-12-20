using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player_Controls : MonoBehaviour
{
    public Rigidbody2D rb;

    public Transform destination;
    [SerializeField] private float player_Speed;
    [SerializeField] public float jump_Height;
    [SerializeField] private float air_Speed;
    private float air_Control;

    public bool isGrounded;

    [Header("Death Penalty")]
    [SerializeField] private float deathPenaltySeconds = 5f;
    private bool isDying = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isGrounded)
        {
            horizontalMotion();
        }
        if (!isGrounded && air_Control == 1)
        {
            horizontalMotion();
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jump_Height);
            air_Control = 1;
        }

        if (isGrounded)
            Debug.Log("On ground");
        if (!isGrounded)
            Debug.Log("In air");
    }

    void horizontalMotion()
    {
        if (Input.GetAxis("Horizontal") != 0 && isGrounded)
        {
            rb.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * player_Speed, rb.linearVelocity.y);
        }
        if (Input.GetAxis("Horizontal") != 0 && !isGrounded)
        {
            rb.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * air_Speed, rb.linearVelocity.y);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("GameObject1 collided with " + other.name);
            isGrounded = true;
            air_Control = 0;
        }

        if (other.gameObject.CompareTag("gameWin"))
        {
            SceneManager.LoadSceneAsync(3);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isDying) return;

        if (other.gameObject.CompareTag("gameOver"))
        {
            isDying = true;

            //  Add +5 seconds penalty
            var sw = Object.FindFirstObjectByType<stopwatchControl>();
            if (sw != null)
            {
                sw.AddPenalty(deathPenaltySeconds);
            }

            //  Respawn player without reloading scene
            var spawner = Object.FindFirstObjectByType<spawnPlayer>();
            if (spawner != null)
            {
                Destroy(gameObject);   // kill current player
                spawner.Respawn();     // spawn new player
            }
            else
            {
                Debug.LogWarning("No spawnPlayer found in the scene!");
            }
        }
    }
}