using UnityEngine;

public abstract class EnemyState : IState
{
    protected EnemyBase enemy;
    protected StateMachine stateMachine;

    public EnemyState(EnemyBase enemyBase,StateMachine stateMachine)
    {
        enemy = enemyBase;
        this.stateMachine = stateMachine;
    }
    public virtual void Enter()
    {
       
    }

    public virtual void Exit()
    {
        
    }



    public virtual void Update()
    {
        
    }
}
