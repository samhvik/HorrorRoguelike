using UnityEngine;

public class PlayerSprintState : PlayerBaseState {

    private readonly int moveSpeedHash = Animator.StringToHash("MoveSpeed");
    private readonly int sprintHash = Animator.StringToHash("Sprint");
    private const float animationDampTime = 0.1f;
    private const float crossFadeDuration = 0.1f;

    public PlayerSprintState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter(){
        stateMachine.velocity.y = Physics.gravity.y;

        stateMachine.animator.CrossFadeInFixedTime(sprintHash, crossFadeDuration);
        stateMachine.movementSpeed = stateMachine.baseMovementSpeed * stateMachine.sprintModifier;

        stateMachine.inputReader.OnJumpPerformed += SwitchToJumpState;
    }

    public override void Tick(){
        if(!stateMachine.controller.isGrounded){
            stateMachine.SwitchState(new PlayerFallState(stateMachine));
        }

        if(stateMachine.inputReader.moveComposite.sqrMagnitude < 1f){
            stateMachine.SwitchState(new PlayerMoveState(stateMachine));
        }

        CalculateMoveDirection();
        FaceMoveDirection();
        Move();

        stateMachine.animator.SetFloat(moveSpeedHash, stateMachine.inputReader.moveComposite.sqrMagnitude > 0f ? 1f : 0f, animationDampTime, Time.deltaTime);
    }

    public override void Exit(){
        stateMachine.inputReader.OnJumpPerformed -= SwitchToJumpState;
        stateMachine.movementSpeed = stateMachine.baseMovementSpeed;
    }

    private void SwitchToJumpState(){
        stateMachine.SwitchState(new PlayerJumpState(stateMachine));
    }
}
