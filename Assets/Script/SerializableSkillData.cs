using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableSkillData
{
    public SkillType skillType;
    public int level;

    public SerializableSkillData(SkillType type, int level)
    {
        this.skillType = type;
        this.level = level;
    }
}
