using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : ScriptableObject
{
  public string skillName;
  public string skillDescription;
  public int skillLevel;
  public float skillCooldown;
  public float skillDuration;

  public AnimationClip skillAnimation;
  public SkillType skillType;
}

public enum SkillType
{
  TebasanKilat,
  SemprotanRacun,
  SeranganGuts
}