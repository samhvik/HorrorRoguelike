using UnityEngine;

public class PlayerDeathState : PlayerBaseState {
    
    private readonly int deathHash = Animator.StringToHash("Dead");
    private const float crossFadeDuration = 0.1f;

    public PlayerDeathState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter(){
        stateMachine.currentState = "Dead";

        stateMachine.velocity = new Vector3(0, 0, 0);

        stateMachine.animator.CrossFadeInFixedTime(deathHash, crossFadeDuration);
    }

    public override void Tick(){
        ApplyGravity();
    }

    public override void Exit() { }
}
