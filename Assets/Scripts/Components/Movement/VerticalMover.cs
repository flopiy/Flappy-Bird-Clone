using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    // static дозволяє іншим скриптам звертатися до швидкості через MoveLeft.speed
    public static float speed = 5f; 

    void Update()
    {
        // Рух вліво з урахуванням поточної швидкості
        transform.position += Vector3.left * speed * Time.deltaTime;

        // Видалення об'єкта, якщо він вилетів за екран (опціонально)
        if (transform.position.x < -15) 
        {
            Destroy(gameObject);
        }
    }
}