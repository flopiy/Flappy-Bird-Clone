using UnityEngine;
using TMPro; // Для роботи з TextMeshPro

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private int score = 0;
    private int highScore = 0;

    private void Awake()
    {
        // Робимо синглтон, щоб до скрипту було легко звертатися
        instance = this;
        
        // Завантажуємо рекорд з пам'яті
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateUI();
    }

    public void AddScore()
    {
        score++;
    
        // Прискорення кожні 10 очок
        if (score > 0 && score % 10 == 0)
        {
            MoveLeft.speed += 0.2f; // Звертаємося до статичної змінної швидкості
            Debug.Log("Швидкість зросла до: " + MoveLeft.speed);
        }

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
    if (scoreText == null || highScoreText == null)
    {
        Debug.LogError("Помилка: Текстові поля не прив'язані в Inspector на об'єкті ScoreManager!");
        return;
    }

    scoreText.text = "Score: " + score;
    highScoreText.text = "Best: " + highScore;
    }
}