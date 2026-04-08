using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject root;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Image background;

    [Header("Text")]
    [SerializeField] private string gameOverMessage = "GAME OVER";
    [SerializeField] private string hintMessage = "Press any key to restart";

    private void Awake()
    {
        EnsureUI();
        Hide();
    }

    public void Show()
    {
        EnsureUI();
        if (root != null) root.SetActive(true);
        if (titleText != null)
        {
            titleText.text = string.IsNullOrWhiteSpace(hintMessage)
                ? gameOverMessage
                : $"{gameOverMessage}\n{hintMessage}";
        }
    }

    public void Hide()
    {
        if (root != null) root.SetActive(false);
    }

    private void EnsureUI()
    {
        if (root != null && titleText != null && background != null) return;

        // If nothing is wired in the Inspector, create a clean overlay UI at runtime.
        if (root == null)
        {
            root = new GameObject("GameOverCanvas");
            root.transform.SetParent(transform, false);

            var canvas = root.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            var scaler = root.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920f, 1080f);
            scaler.matchWidthOrHeight = 0.5f;

            root.AddComponent<GraphicRaycaster>();
        }

        if (background == null)
        {
            GameObject bgGO = new GameObject("Background");
            bgGO.transform.SetParent(root.transform, false);

            background = bgGO.AddComponent<Image>();
            background.color = new Color(0f, 0f, 0f, 0.65f);

            RectTransform bgRt = background.rectTransform;
            bgRt.anchorMin = Vector2.zero;
            bgRt.anchorMax = Vector2.one;
            bgRt.offsetMin = Vector2.zero;
            bgRt.offsetMax = Vector2.zero;
        }

        if (titleText == null)
        {
            GameObject textGO = new GameObject("GameOverText");
            textGO.transform.SetParent(background.transform, false);

            titleText = textGO.AddComponent<TextMeshProUGUI>();
            titleText.alignment = TextAlignmentOptions.Center;
            titleText.fontSize = 80;
            titleText.color = Color.white;

            RectTransform rt = titleText.rectTransform;
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.offsetMin = new Vector2(60f, 60f);
            rt.offsetMax = new Vector2(-60f, -60f);
        }
    }
}

