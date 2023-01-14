using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]

public class EnemyStateMachine : StateMachine {

    public Vector3 velocity;
    public float baseMovementSpeed { get; private set; } = 5f;
    public float movementSpeed = 5f;
    public float sprintModifier {get; private set; } = 1.5f;
    public float jumpForce { get; private set; } = 5f;
    public float lookRotationDampFactor { get; private set; } = 10f;
    public Animator animator { get; private set; }
    public CharacterController controller { get; private set; }

    void Start() {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

        SwitchState(new EnemyMoveState(this));
    }
}
