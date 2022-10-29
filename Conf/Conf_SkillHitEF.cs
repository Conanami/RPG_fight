using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "特效配置", menuName = "配置/特效配置")]
public class Conf_SkillHitEF : ScriptableObject
{
    //产生的粒子物体
    public Skill_SpawnObj Skill_Spawn;
    //产生的音效
    public AudioClip AudioClip;
}


