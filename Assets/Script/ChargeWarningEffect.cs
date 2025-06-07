using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeWarningEffect : MonoBehaviour
{
    [SerializeField] private SpriteRenderer crackSprite;
    [SerializeField] private SpriteRenderer glowRing;
    [SerializeField] private float duration = 1.2f;

    private void Start()
    {
        StartCoroutine(AnimateEffect());
    }

    IEnumerator AnimateEffect()
    {
        float elapsed = 0f;
        Color crackColor = crackSprite.color;
        Color glowColor = glowRing.color;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            // Scale Èçµé¸²
            crackSprite.transform.localScale = Vector3.one * (1 + Mathf.Sin(t * 20f) * 0.1f);

            // Fade out
            crackColor.a = Mathf.Lerp(1f, 0f, t);
            glowColor.a = Mathf.Lerp(0.7f, 0f, t);

            crackSprite.color = crackColor;
            glowRing.color = glowColor;

            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
