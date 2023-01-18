using UnityEngine;

public class EnemyFallState : EnemyBaseState {
   
    private readonly int fallHash = Animator.StringToHash("Fall");
    private const float crossFadeDuration = 0.1f;

    public EnemyFallState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter(){
        stateMachine.currentState = "Falling";

        stateMachine.velocity.y = 0f;
        stateMachine.animator.CrossFadeInFixedTime(fallHash, crossFadeDuration);
    }

    public override void Tick(){
        ApplyGravity();
        Move();

        if(stateMachine.controller.isGrounded){
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
        }
    }

    public override void Exit() { }
}
