using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Controller : MonoBehaviour
{
    private Monster_Model model;  //动画层，类似前端显示
    private CharacterController characterController;  //角色控制器
    public int hp = 100; //血量

    private bool isHurt; //是否击退中
    private Vector3 hurtVelocity; //被击退的力量，
    private float hurtTime;    //击退时间
    private float currHurtTime;  //当前击退时间

    private float gravity = 9f;
    private GameObject hurtFrom;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        model = this.GetComponentInChildren<Monster_Model>();
        model.Init();
    }

    private void Update()
    {
        if(isHurt)
        {
            currHurtTime += Time.deltaTime;
            characterController.Move(hurtVelocity * Time.deltaTime / hurtTime);
            if(currHurtTime>hurtTime)
                isHurt = false;
        }
        else
        {
            characterController.Move(new Vector3(0, -gravity*Time.deltaTime, 0));
        }
    }

    /// <summary>
    /// 对怪物造成伤害
    /// </summary>
    /// <param name="from">伤害来自谁</param>
    public void Hurt(GameObject from,Skill_HitModel hitModel)
    {
        hurtFrom = from;
        //播放受伤动画
        model.PlayHurtAnim();
        //并硬直
        CancelInvoke("HurtOver");
        Invoke("HurtOver",hitModel.hardTime);
        //model.StopHurtAnim();
        //击飞，击退
        isHurt = true;
        hurtVelocity = hurtFrom.transform.TransformDirection(hitModel.repelVelocity); //后退一米，飞上去1米
        hurtTime = hitModel.repelTime;
        currHurtTime = 0;
        //扣血
        hp -= hitModel.damageValue;
    }
    private void HurtOver()
    {
        model.StopHurtAnim();
    }
}
