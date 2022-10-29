using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerState {
    //默认
    Player_None,
    //移动
    Player_Move,
    //攻击
    Player_Attack,
}

public class Player_Controller : FSMController<PlayerState>
{
    //private PlayerState playerState;
    
    public Player_Input input { get; private set; }
    public Player_Model model { get; private set; }
    public new Player_Audio audio { get; private set; }

    //因为有很多技能配置文件
    public Conf_SkillData[] standSkillDatas;
    //当前是第几段攻击
    private int currAttackIndex=0;
    //当前的技能配置文件
    public Conf_SkillData CurrSkillData { get; private set; }
    public int CurrAttackIndex
    {
        get => currAttackIndex; 
        set
        {
            //防止越界，循环回0的标准写法，很多地方要用
            if (value >= standSkillDatas.Length) currAttackIndex = 0;
            else currAttackIndex = value;
        }
    }
    public CharacterController characterController { get; private set; }
    

    private void Start()
    {
        input = new Player_Input();
        audio = new Player_Audio(GetComponent<AudioSource>());
        CurrentState = PlayerState.Player_None;
        characterController = GetComponent<CharacterController>();
        model = GetComponentInChildren<Player_Model>();
        model.Init(this);
        //默认状态是移动
        ChangeState<Player_Move>(PlayerState.Player_Move);
    }

    public bool CheckAttack()
    {
        return input.GetAttackKeyDown() && model.canSwitch;
    }
    //普通攻击
    public void StandardAttack()
    {
        CurrSkillData = standSkillDatas[CurrAttackIndex];
        model.StartAttack(CurrSkillData);
        
        CurrAttackIndex++;
    }

    public void PlayAudio(AudioClip audioClip)
    {
        if(audioClip!=null)
            audio.PlayAudio(audioClip);
    }
}
