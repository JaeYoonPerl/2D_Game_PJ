using UnityEngine;

public class ChargeWarningEffect : MonoBehaviour
{
    [SerializeField] private float lifetime = 1f; // ���� �ð�
    [SerializeField] private float fadeDuration = 0.5f; // ���̵� �ƿ� �ð�

    private SpriteRenderer spriteRenderer;
    private float timer;

    void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
        timer = lifetime;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= fadeDuration)
        {
            float alpha = Mathf.Clamp01(timer / fadeDuration);
            if (spriteRenderer != null)
            {
                Color color = spriteRenderer.color;
                color.a = alpha;
                spriteRenderer.color = color;
            }
        }

        if (timer <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
