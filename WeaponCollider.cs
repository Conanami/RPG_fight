using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    public BoxCollider boxCollider;  //武器上的碰撞器
    public TrailRenderer trail;     // 刀光
    public GameObject weaponTaker;  //拿武器的人，因为击退方向要根据人的方向
    private List<GameObject> monsterList = new List<GameObject>();
    private Skill_HitModel hitModel;
    public void Init()
    {
        AttackOver();
        
    }

    public void AttackInit(Skill_HitModel hitModel)
    {
        this.hitModel = hitModel;
        boxCollider.enabled = true;
        trail.emitting = true;
        monsterList.Clear();
    }
    public void AttackOver()
    {
        boxCollider.enabled = false;
        trail.emitting = false;
        monsterList.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        //这个monsterList就是保证一段伤害只会伤害一个怪物一次
        //但可以伤害多个怪物
        if(other.gameObject.tag=="Monster" && !monsterList.Contains(other.gameObject))
        {
            //放入列表，避免二次伤害
            monsterList.Add(other.gameObject);
            //输出伤害
            other.gameObject.GetComponent<Monster_Controller>().Hurt(weaponTaker,this.hitModel);
            if(hitModel.SkillHitEF!=null)
            {
                //产生击中粒子
                SpawnObjByHit(this.hitModel.SkillHitEF.Skill_Spawn, other.ClosestPointOnBounds(GameObject.Find("Trail").transform.position+new Vector3(0,1f,0)));
                //播放击中音效
                weaponTaker.GetComponentInChildren<Player_Controller>().PlayAudio(hitModel.SkillHitEF.AudioClip);
                //看向砍他的人
                other.gameObject.transform.LookAt(weaponTaker.transform);
            }
        }
    }
    private void SpawnObjByHit(Skill_SpawnObj spawnObj,Vector3 spawnPosition)
    {
        if(spawnObj!=null && spawnObj.Prefab !=null)
        {
            GameObject temp = GameObject.Instantiate(spawnObj.Prefab, null);
            temp.transform.position = spawnPosition + spawnObj.Position;
            temp.transform.LookAt(Camera.main.transform);
            temp.transform.eulerAngles += spawnObj.Rotation;
            //播放生成
            if(spawnObj.AudioClip!=null)
                weaponTaker.GetComponentInChildren<Player_Controller>().PlayAudio(spawnObj.AudioClip);

        }

    }


}
