using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// 有限状态机控制类，是个基类
/// 挂载到玩家和怪物身上
/// </summary>
public abstract class FSMController<T> : MonoBehaviour
{
    public T CurrentState;

    //当前状态对象
    protected StateBase<T> currStateObj;

    //对象池
    private Dictionary<T, StateBase<T>> stateDict = new Dictionary<T, StateBase<T>>();


    /// <summary>
    /// 更改状态
    /// </summary>
    /// <param name="newState">新的状态</param>
    /// <param name="reCurrState">如果状态不变是否需要刷新</param>
    public void ChangeState<K>(T newState, bool reCurrState = false) where K : StateBase<T>, new()
    {
        //Debug.Log(CurrentState);
        if (newState.Equals(CurrentState) && !reCurrState) return;
        if (currStateObj != null) currStateObj.OnExit();
        currStateObj = GetStateObj<K>(newState);
        currStateObj.OnEnter();   //逻辑进入，做动作
    }
    /// <summary>
    /// 返回一个跟stateType同名的状态对象
    /// 并保证不返回null
    /// </summary>
    /// <param name="stateType"></param>
    /// <returns></returns>
    private StateBase<T> GetStateObj<K>(T stateType) where K:StateBase<T>,new()
    {
        //如果List里面有，就拿，如果没有，就new，放进List，然后返回
        if (stateDict.ContainsKey(stateType)) return stateDict[stateType];
        //这句比较高级，是创建一个这个名字的对象
        StateBase<T> state = new K();
        state.Init(stateType, this);
        stateDict.Add(stateType,state);
        return state;
    }

    //等待被子类重写的虚拟函数
    protected virtual void Update()
    {
        if (currStateObj != null) currStateObj.OnUpdate();
    }
}
