using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{

    public class SkillSlot
    {
        public SkillData data;
        public int level;

        public SkillSlot(SkillData data)
        {
            this.data = data;
            this.level = 1;
        }

        public void LevelUp() => level++;
    }

    public List<SkillSlot> acquiredSkills = new List<SkillSlot>();

    public void AddOrLevelUpSkill(SkillData skill)
    {
        var found = acquiredSkills.Find(s => s.data.skillType == skill.skillType);
        if (found != null)
        {
            found.LevelUp();
            ApplySkillEffect(found);
        }
        else
        {
            var slot = new SkillSlot(skill);
            acquiredSkills.Add(new SkillSlot(skill));
            ActivateSkill(skill);
            acquiredSkills.Add(slot);
            ApplySkillEffect(slot);
        }
    }

    void ActivateSkill(SkillData skill)
    {
        switch (skill.skillType)
        {
            case SkillType.MultiShot:
                GetComponent<PlayerShooting>()?.EnableMultiShot();
                break;
           case SkillType.AreaBlast:
                GetComponent<PlayerAreaBlast>()?.EnableSkill();
                break;
                /* case SkillType.OrbitObject:
                    GetComponent<PlayerOrbitSkill>()?.EnableSkill();
                    break;*/
        }
    }
    void ApplySkillEffect(SkillSlot slot)
    {
        switch (slot.data.skillType)
        {
            case SkillType.MultiShot:
                GetComponent<PlayerShooting>()?.UpdateMultiShotLevel(slot.level);
                break;

           case SkillType.AreaBlast:
                GetComponent<PlayerAreaBlast>()?.UpdateSkillLevel(slot.level);
                break;

                // case SkillType.OrbitObject:
                //     GetComponent<PlayerOrbitSkill>()?.UpdateSkillLevel(slot.level);
                //     break;
        }
    }


    public int GetSkillLevel(SkillType type)
    {
        var found = acquiredSkills.Find(s => s.data.skillType == type);
        return found?.level ?? 0;
    }
}
