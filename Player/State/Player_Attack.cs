using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : StateBase<PlayerState>
{
    public Player_Controller player;  //脚本
    public override void Init(PlayerState stateType, FSMController<PlayerState> controller)
    {
        base.Init(stateType, controller);
        player = controller as Player_Controller;
    }

    public override void OnEnter()
    {
        //这里就是攻击
        player.StandardAttack();
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        //二段击功能
        if(player.CheckAttack())
        {
            player.StandardAttack();
        }
        //边打边转向的功能
        if(player.CurrSkillData.ReleaseModel.CanRotate)
        {
            float rotateSpeed = 60f;
            Vector3 rot = new Vector3(0, player.input.Horizontal, 0);
            player.transform.Rotate(rot * Time.deltaTime * rotateSpeed);
        }
    }

    
}
