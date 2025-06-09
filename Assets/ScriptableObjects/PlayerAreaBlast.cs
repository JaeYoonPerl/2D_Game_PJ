using UnityEngine;

public class PlayerAreaBlast : MonoBehaviour
{
    [SerializeField] private GameObject zonePrefab;
    [SerializeField] private float baseRadius = 3f;
    [SerializeField] private int baseDamage = 1;
    [SerializeField] private LayerMask enemyLayer;

    private GameObject currentZone;
    private AreaDamageZone zoneScript;

    private int level = 0;

    public void EnableSkill()
    {
        if (level == 0)
        {
            level = 1;
            CreateZone();
        }
    }

    public void UpdateSkillLevel(int newLevel)
    {
        level = newLevel;
        UpdateZoneStats();
    }

    private void CreateZone()
    {
        if (zonePrefab == null)
        {
            Debug.LogError("Zone 프리팹이 연결되어 있지 않습니다.");
            return;
        }

        currentZone = Instantiate(zonePrefab, transform.position, Quaternion.identity, transform);
        zoneScript = currentZone.GetComponent<AreaDamageZone>();
        if (zoneScript != null)
        {
            zoneScript.Init(GetRadius(), GetDamage(), Mathf.Infinity, enemyLayer);
        }
    }

    private void UpdateZoneStats()
    {
        if (zoneScript != null)
        {
            zoneScript.UpdateStats(GetRadius(), GetDamage());
        }
    }

    float GetRadius() => baseRadius + (level - 1) * 0.5f;
    int GetDamage() => baseDamage + (level - 1) * 3;
}
