using UnityEngine;

public class PlayerJumpState : PlayerBaseState {
    
    private readonly int jumpHash = Animator.StringToHash("Jump");
    private const float crossFadeDuration = 0.1f;

    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter(){
        stateMachine.currentState = "Jumping";

        stateMachine.velocity = new Vector3(stateMachine.velocity.x, stateMachine.jumpForce, stateMachine.velocity.z);

        stateMachine.animator.CrossFadeInFixedTime(jumpHash, crossFadeDuration);
    }

    public override void Tick(){
        ApplyGravity();

        if(stateMachine.velocity.y <= 0f){
            stateMachine.SwitchState(new PlayerFallState(stateMachine));
        }

        FaceMoveDirection();
        Move();
    }

    public override void Exit() { }
}
