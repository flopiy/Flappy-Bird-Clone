using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class FlappyBirdController : MonoBehaviour
{
    [Header("Налаштування польоту")]
    [SerializeField] private float jumpForce = 5.5f;
    [SerializeField] private float tiltSpeed = 4f;

    [Header("Game Over UI")]
    [SerializeField] private GameOverUI gameOverUI;

    private Rigidbody2D rb;
    private bool isDead = false;

    private void Awake()
    {
        Time.timeScale = 1f;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1.8f;
        rb.freezeRotation = true;

        if (gameOverUI == null)
        {
            gameOverUI = FindFirstObjectByType<GameOverUI>();
        }

        gameOverUI?.Hide();
    }

    private void Update()
    {
        if (isDead) 
        {
            if (Keyboard.current?.anyKey.wasPressedThisFrame == true || 
                Pointer.current?.press.wasPressedThisFrame == true)
            {
                RestartGame();
            }
            return;
        }

        if (Keyboard.current?.spaceKey.wasPressedThisFrame == true || 
            Pointer.current?.press.wasPressedThisFrame == true)
        {
            Jump();
        }

        float targetRotation = Mathf.Clamp(rb.linearVelocity.y * tiltSpeed, -45, 25);
        transform.rotation = Quaternion.Euler(0, 0, targetRotation);
    }

    public void Jump()
    {
        rb.linearVelocity = Vector2.up * jumpForce;
    }

    // Нарахування очок
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ScoreDetector") && !isDead)
        {
            ScoreManager.instance.AddScore();
        }
    }

    // Зіткнення (Програш)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDead) GameOver();
    }

    private void GameOver()
    {
        isDead = true;
        Time.timeScale = 0f;
        GetComponent<SpriteRenderer>().color = Color.red;
        if (gameOverUI != null) gameOverUI.Show();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}