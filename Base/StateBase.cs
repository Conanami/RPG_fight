using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// 所有状态都继承这个基类
/// Idle ,Walk , Attack之类的
/// </summary>
public abstract class StateBase<T> 
{
    //所有状态的枚举
    public T StateType;
    //public FSMController Controller;
    //首次实例化时候的初始化
    public virtual void Init(T stateType, FSMController<T> controller)
    {
        //
        this.StateType = stateType;
    }

    //进入
    public abstract void OnEnter();
    //更新
    public abstract void OnUpdate();
    //退出
    public abstract void OnExit();
}
