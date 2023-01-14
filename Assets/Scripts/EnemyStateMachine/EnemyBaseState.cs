using UnityEngine;

public abstract class EnemyBaseState : State {

    protected readonly EnemyStateMachine stateMachine;

    protected EnemyBaseState(EnemyStateMachine stateMachine){
        this.stateMachine = stateMachine;
    }
    
    protected void CalculateMoveDirection(){
        Vector3 moveDirection = Vector3.forward * 1f + Vector3.right * 1f;

        stateMachine.velocity.x = moveDirection.x * stateMachine.movementSpeed;
        stateMachine.velocity.z = moveDirection.z * stateMachine.movementSpeed;
    }

    protected void FaceMoveDirection(){
        Vector3 faceDirection = new(stateMachine.velocity.x, 0f, stateMachine.velocity.z);

        if (faceDirection == Vector3.zero)
            return;

        stateMachine.transform.rotation = Quaternion.Slerp(stateMachine.transform.rotation, Quaternion.LookRotation(faceDirection), stateMachine.lookRotationDampFactor * Time.deltaTime);
    }

    protected void ApplyGravity(){
        if (stateMachine.velocity.y > Physics.gravity.y)
        {
            stateMachine.velocity.y += Physics.gravity.y * Time.deltaTime;
        }
    }

    protected void Move(){
        stateMachine.controller.Move(stateMachine.velocity * Time.deltaTime);
    }
}
