using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName ="技能配置",menuName ="配置/技能配置/技能数据")]
public class Conf_SkillData : ScriptableObject
{
    //技能名称
    public string ConfigName;
    //技能触发的trigger
    public string ReleaseTrigger;
    //技能结束的trigger
    public string EndTrigger;
    //放大招配置
    public Skill_ReleaseModel ReleaseModel;
    //击中配置
    public Skill_HitModel HitModel;
    //完成配置
    public Skill_EndModel EndModel;
    

}
[Serializable]
public class Skill_ReleaseModel
{
    //因为是配置，所以变量大写会比较好，个人习惯，不影响。
    //发招时候产生的粒子，物体，比如说放光，放箭等等
    //需要生成很多粒子的话，这里可以变成数组
    public Skill_SpawnObj SpawnObj;
    //发招时候发出的声音，游戏音效很重要
    public AudioClip AudioClip;
    //发招时候能不能旋转，有的招是一边旋转一边发的
    public bool CanRotate;
}
[Serializable]
public class Skill_HitModel
{
    //攻击伤害
    public int damageValue;
    //硬直时间
    public float hardTime;
    //击退效果
    public Vector3 repelVelocity;
    //击退时间
    public float repelTime;
    //特效配置
    public Conf_SkillHitEF SkillHitEF;
}
[Serializable]
public class Skill_EndModel
{
    //发招结束时候产生的粒子，物体，比如说放光，放箭等等
    public Skill_SpawnObj SpawnObj;
}
/// <summary>
/// 技能产生的物体，比如粒子
/// </summary>
[Serializable]
public class Skill_SpawnObj
{
    //生成的预制体
    public GameObject Prefab;
    //生成的音效
    public AudioClip AudioClip;
    //位置
    public Vector3 Position;
    //旋转
    public Vector3 Rotation;
}