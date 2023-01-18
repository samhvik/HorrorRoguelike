using UnityEngine;

public class EnemyIdleState : EnemyBaseState {

    private readonly int idleHash = Animator.StringToHash("Idle");
    //private readonly int moveBlendTreeHash = Animator.StringToHash("MoveBlendTree");
    private const float animationDampTime = 0.1f;
    private const float crossFadeDuration = 0.1f;

    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter(){
        stateMachine.velocity.y = Physics.gravity.y;

        stateMachine.animator.CrossFadeInFixedTime(idleHash, crossFadeDuration);

        //stateMachine.inputReader.OnJumpPerformed += SwitchToJumpState;
        //stateMachine.inputReader.OnSprintPerformed += SwitchToSprintState;
    }

    public override void Tick(){
        if(!stateMachine.controller.isGrounded){
            stateMachine.SwitchState(new EnemyFallState(stateMachine));
        }
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
