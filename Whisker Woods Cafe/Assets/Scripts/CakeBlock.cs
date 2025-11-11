using UnityEngine;

public class CakeBlock : MonoBehaviour
{
    private bool placed = false;
    private CakeTowerManager manager;

    private void Start()
    {
        manager = FindObjectOfType<CakeTowerManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (placed) return;

        // Detect successful placement
        if (collision.collider.CompareTag("Cake") || collision.collider.CompareTag("Ground") || collision.collider.CompareTag("Base"))
        {
            placed = true;
            if (manager != null)
                manager.CakePlaced();

            // Freeze in place
            var rb = GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.bodyType = RigidbodyType2D.Static;
        }
    }

    private void OnBecameInvisible()
    {
        // only lose a life if it fell off screen *and wasn’t placed*
        if (!placed)
        {
            if (manager != null)
            {
                manager.RemoveLife();
            }
            else
            {
                Debug.LogWarning("CakeTowerManager not found when trying to remove a life!");
            }
        }

        // destroy the block to clean up
        Destroy(gameObject);
    }
}
