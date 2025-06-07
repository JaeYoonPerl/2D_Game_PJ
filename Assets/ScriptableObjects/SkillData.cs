using UnityEngine;

public enum SkillType
{
    MultiShot,
    AreaBlast,
    OrbitObject
}

[CreateAssetMenu(menuName = "Skill/SkillData")]
public class SkillData : ScriptableObject
{
    public string skillName;
    public string description;
    public Sprite icon;
    public int maxLevel = 5;

    public SkillType skillType;  
}
