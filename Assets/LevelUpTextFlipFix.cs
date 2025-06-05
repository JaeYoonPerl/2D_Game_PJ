using UnityEngine;

public class LevelUpTextFlipFix : MonoBehaviour
{
    void LateUpdate()
    {
        // �θ�κ��� ���������� scale ����
        Vector3 lossyScale = transform.lossyScale;
        lossyScale.x = Mathf.Abs(lossyScale.x);
        lossyScale.y = Mathf.Abs(lossyScale.y);
        lossyScale.z = Mathf.Abs(lossyScale.z);

        // �ٽ� ���� �����Ϸ� ���߱�
        transform.localScale = transform.parent != null
            ? new Vector3(lossyScale.x / transform.parent.lossyScale.x, 1f, 1f)
            : new Vector3(1f, 1f, 1f);
    }
}
