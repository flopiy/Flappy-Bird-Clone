using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    [Header("Налаштування префабів")]
    [SerializeField] private GameObject[] wallVariants;
    [SerializeField] private Transform wallParent;

    [Header("Пара стовпів (верх+низ)")]
    [SerializeField] private float gapSize = 2.5f;
    [SerializeField] private float minGapY = -1.5f;
    [SerializeField] private float maxGapY = 2.5f;
    [SerializeField] private bool flipTopPipe = true;

    [Header("Таймінги")]
    [SerializeField] private float spawnRate = 3.5f;

    private float timer = 0f;

    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            SpawnWall();
            timer = 0;
        }
    }

    void SpawnWall()
    {
        if (wallVariants.Length == 0) return;

        int index = Random.Range(0, wallVariants.Length);

        GameObject prefab = wallVariants[index];
        if (prefab == null) return;

        float gapCenterY = Random.Range(minGapY, maxGapY);

        float pipeHalfHeight = GetPrefabHalfHeight(prefab);
        float bottomY = gapCenterY - (gapSize * 0.5f) - pipeHalfHeight;
        float topY = gapCenterY + (gapSize * 0.5f) + pipeHalfHeight;

        Vector3 basePos = transform.position;

        Instantiate(
            prefab,
            new Vector3(basePos.x, bottomY, basePos.z),
            Quaternion.identity,
            wallParent
        );

        Quaternion topRotation = flipTopPipe ? Quaternion.Euler(0f, 0f, 180f) : Quaternion.identity;
        Instantiate(
            prefab,
            new Vector3(basePos.x, topY, basePos.z),
            topRotation,
            wallParent
        );
    }

    private static float GetPrefabHalfHeight(GameObject prefab)
    {
        // Спроба №1: Collider2D (найчастіше саме він відповідає “реальній” висоті перешкоди)
        Collider2D col = prefab.GetComponentInChildren<Collider2D>();
        if (col != null)
        {
            return col.bounds.extents.y;
        }

        // Спроба №2: Renderer (якщо колайдера немає)
        Renderer rend = prefab.GetComponentInChildren<Renderer>();
        if (rend != null)
        {
            return rend.bounds.extents.y;
        }

        // Фолбек, щоб не зламати спавн навіть без компонентів
        return 1f;
    }
}