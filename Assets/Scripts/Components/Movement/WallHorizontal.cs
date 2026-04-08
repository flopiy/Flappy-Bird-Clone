using UnityEngine;

public class PipeMover : MonoBehaviour
{
    [Header("Налаштування руху")]
    [SerializeField] private float speed = 3.5f; // Швидкість руху вліво
    [SerializeField] private float leftEdge = -15f; // Координата X, де колона видаляється

    void Update()
    {
        // Рухаємо об'єкт вліво по осі X (у world-space, щоб поворот не міняв напрям)
        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);

        // Якщо колона вилетіла за межі екрана (зліва)
        if (transform.position.x < leftEdge)
        {
            // Видаляємо об'єкт, щоб не засмічувати пам'ять
            Destroy(gameObject);
        }
    }
}