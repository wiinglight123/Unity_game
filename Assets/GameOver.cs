using UnityEngine;

public class GameOver : MonoBehaviour
{
    public Transform player;
    public Vector3 respawnPosition;
    public float fallThreshold = -10f;
    public bool Finish = false;
    private Animator animator;
    public string winningTag = "WinningSprite";
    public bool Dies = false; 
    public string enemyTag = "Enemy";

    void Start()
    {
        animator = GetComponent<Animator>();

        if (player == null)
        {
            Debug.LogError("Player transform is not assigned.");
            return;
        }

        respawnPosition = player.position;
        RespawnPlayer();
    }

    void Update()
    {
        if (player.position.y < fallThreshold)
        {
            RespawnPlayer();
        }
    }

    void RespawnPlayer()
    {
        player.position = respawnPosition;
        Debug.Log("Player has been respawned.");
        Dies = false;
        animator.SetBool("Dies", Dies);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {   
            Dies = true;
            animator.SetBool("Dies", Dies);
            IEnumerator RespawnAfterDelay(float delay)
            {
                yield return new WaitForSeconds(delay);
                RespawnPlayer();
            }

            // Inside OnCollisionEnter2D
            StartCoroutine(RespawnAfterDelay(2.0f)); // Adjust the delay as needed
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(winningTag))
        {
            Debug.Log("Player has won the game!");

            Finish = true;
            animator.SetBool("Finish", Finish);
            // Implement your win game logic here
        }
    }
}