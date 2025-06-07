using UnityEngine;
using System.Collections.Generic;



[CreateAssetMenu(menuName = "Skill/SkillDatabase")]
public class SkillDatabase : ScriptableObject
{
    public List<SkillData> allSkills;
}
