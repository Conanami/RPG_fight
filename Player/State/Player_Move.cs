using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : StateBase<PlayerState>
{
    private float moveSpeed = 1.2f;  //移动速度
    private float rotateSpeed = 90;  //旋转速度
    private float transTime = 0.5f;  //跑步和走路之间的切换速度
    private float deltaV = 0;         //跑步和走路之间的速度差切换，1是走路，2是跑步
    public Player_Controller player;  //脚本

    private bool isRun
    {
        get => player.input.GetRunKey() && player.input.Vertical > 0;

    }
    public override void Init(PlayerState stateType, FSMController<PlayerState> controller)
    {
        base.Init(stateType, controller);
        player = controller as Player_Controller;
    }


    public override void OnUpdate()
    {
        float h = player.input.Horizontal;
        float v = player.input.Vertical;
        if (isRun)
        {
            deltaV += Time.deltaTime / transTime;
            deltaV = Mathf.Min(2, deltaV);
        }
        else
        {
            deltaV -= Time.deltaTime / transTime;
            deltaV = Mathf.Max(0, deltaV);
        }  
        v = v + deltaV;
        Move(h, v);

        //如果检测到玩家按键攻击，就攻击
        if (player.CheckAttack()) player.ChangeState<Player_Attack>(PlayerState.Player_Attack);
    }

    private void Move(float h, float v)
    {
        //前进后退
        Vector3 dir = new Vector3(0, 0, v);
        dir = player.transform.TransformDirection(dir);
        player.characterController.SimpleMove(dir * moveSpeed); 
        //SimpleMove只要填一个秒速单位即可

        //旋转
        Vector3 rot = new Vector3(0, h, 0);
        player.transform.Rotate(rot * Time.deltaTime * rotateSpeed);

        //同步动画
        player.model.UpdateMovePar(h, v);
    }
    public override void OnEnter() { }
    

    public override void OnExit() { }


  
}
