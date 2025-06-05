using UnityEngine;

public class LevelUpTextFlipFix : MonoBehaviour
{
    void LateUpdate()
    {
        // 부모로부터 독립적으로 scale 보정
        Vector3 lossyScale = transform.lossyScale;
        lossyScale.x = Mathf.Abs(lossyScale.x);
        lossyScale.y = Mathf.Abs(lossyScale.y);
        lossyScale.z = Mathf.Abs(lossyScale.z);

        // 다시 로컬 스케일로 맞추기
        transform.localScale = transform.parent != null
            ? new Vector3(lossyScale.x / transform.parent.lossyScale.x, 1f, 1f)
            : new Vector3(1f, 1f, 1f);
    }
}
