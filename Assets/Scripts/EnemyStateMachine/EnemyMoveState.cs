using UnityEngine;

public class EnemyMoveState : EnemyBaseState {

    private readonly int moveSpeedHash = Animator.StringToHash("MoveSpeed");
    private readonly int moveBlendTreeHash = Animator.StringToHash("MoveBlendTree");
    private const float animationDampTime = 0.1f;
    private const float crossFadeDuration = 0.1f;

    public EnemyMoveState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter(){
        stateMachine.velocity.y = Physics.gravity.y;

        stateMachine.animator.CrossFadeInFixedTime(moveBlendTreeHash, crossFadeDuration);

        //stateMachine.inputReader.OnJumpPerformed += SwitchToJumpState;
        //stateMachine.inputReader.OnSprintPerformed += SwitchToSprintState;
    }

    public override void Tick(){
        if(!stateMachine.controller.isGrounded){
            //stateMachine.SwitchState(new EnemyFallState(stateMachine));
        }

        CalculateMoveDirection();
        FaceMoveDirection();
        Move();

        //stateMachine.animator.SetFloat(moveSpeedHash, stateMachine.inputReader.moveComposite.sqrMagnitude > 0f ? 1f : 0f, animationDampTime, Time.deltaTime);
    }

    public override void Exit(){
        //stateMachine.inputReader.OnJumpPerformed -= SwitchToJumpState;
        //stateMachine.inputReader.OnSprintPerformed -= SwitchToSprintState;
    }

    private void SwitchToJumpState(){
        //stateMachine.SwitchState(new PlayerJumpState(stateMachine));
    }

    private void SwitchToSprintState(){
        //stateMachine.SwitchState(new PlayerSprintState(stateMachine));
    }
}
