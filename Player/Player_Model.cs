using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//动画，武器，刀光效果
//这个层有点像前端，视觉层
public class Player_Model : MonoBehaviour
{
    private Player_Controller player;
    private Animator animator;
    public WeaponCollider weaponCollider;
    //当前技能数据
    private Conf_SkillData skillData;
    //是否可以切换
    public bool canSwitch { get; private set; }
    public void Init(Player_Controller player)
    {
        canSwitch = true;
        this.player = player;
        animator = GetComponent<Animator>();
        weaponCollider = GetComponentInChildren<WeaponCollider>();
        weaponCollider.Init();
    }


    public void PlayAudio(AudioClip audioClip)
    {
        player.PlayAudio(audioClip);
    }

    //提供参数给动画机
    public void UpdateMovePar(float x,float z)
    {
        animator.SetFloat("LeftRight", x);
        animator.SetFloat("Forward", z);
    }

    public void StartAttack(Conf_SkillData skillData)
    {
        this.skillData = skillData;
        animator.SetTrigger(skillData.ReleaseTrigger);
        canSwitch = false;
    }


    #region 动画事件
    public void EndAttack(string skillname)
    {
        
        if (skillname == this.skillData.ConfigName)
        {//结束生成
           
            SpawnObj(skillData.EndModel.SpawnObj);
            canSwitch = true;
            animator.SetTrigger(skillData.EndTrigger);
            player.CurrAttackIndex = 0;
            player.ChangeState<Player_Move>(PlayerState.Player_Move);
        }
        

    }
    public void SkillCanSwitch()
    {
        canSwitch = true;
    }
    public void StartSkillHit()
    {
        //开启刀光
        //开启伤害
        weaponCollider.AttackInit(this.skillData.HitModel);
        //释放需要释放的粒子效果
        
        SpawnObj(skillData.ReleaseModel.SpawnObj);
        if (skillData.ReleaseModel.AudioClip != null)
            PlayAudio(skillData.ReleaseModel.AudioClip);

    }
    //这里是结束伤害，不是结束攻击，有区别，结束攻击在上面
    public void EndSkillHit()
    {
        //关闭刀光
        //关闭伤害
        weaponCollider.AttackOver();
    }

    #endregion
    //放招时候产生物体
    private void SpawnObj(Skill_SpawnObj spawnObj)
    {
        if (spawnObj != null && spawnObj.Prefab != null)
        {
            GameObject temp = GameObject.Instantiate(spawnObj.Prefab, null);
            temp.transform.position = transform.position + spawnObj.Position;
            temp.transform.eulerAngles = player.transform.eulerAngles+spawnObj.Rotation;
            //播放生成物体音效
            if (spawnObj.AudioClip != null)
                PlayAudio(spawnObj.AudioClip);

        }

    }

}
