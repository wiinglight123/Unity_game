using System.Collections;
using UnityEngine;
//Test
public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed of the enemy movement
    private float direction = 1f; // Current direction (-1 for left, 1 for right)
    private float timer = 0f; // Timer to track direction change
    public float duration = 10f; // Duration for moving in one direction

    private void Update()
    {
        // Move the enemy
        transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);

        // Update the timer
        timer += Time.deltaTime;

        // Check if it's time to switch direction
        if (timer >= duration)
        {
            timer = 0f; // Reset the timer
            direction *= -1; // Switch direction
            Flip(); // Flip the enemy to face the correct direction
        }
    }

    private void Flip()
    {
        // Change the local scale to flip the sprite
        Vector3 localScale = transform.localScale;
        localScale.x *= -1; // Set x scale based on direction
        transform.localScale = localScale;
    }
}
