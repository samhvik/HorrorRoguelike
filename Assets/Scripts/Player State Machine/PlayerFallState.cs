using UnityEngine;

public class PlayerFallState : PlayerBaseState {
   
    private readonly int fallHash = Animator.StringToHash("Fall");
    private const float crossFadeDuration = 0.1f;

    public PlayerFallState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter(){
        stateMachine.velocity.y = 0f;
        stateMachine.animator.CrossFadeInFixedTime(fallHash, crossFadeDuration);
    }

    public override void Tick(){
        ApplyGravity();
        Move();

        if(stateMachine.controller.isGrounded){
            stateMachine.SwitchState(new PlayerMoveState(stateMachine));
        }
    }

    public override void Exit() { }
}
