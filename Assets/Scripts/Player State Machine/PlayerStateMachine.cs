using UnityEngine;

[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]

public class PlayerStateMachine : StateMachine {

    public string currentState = "";
    public Vector3 velocity;
    public float baseMovementSpeed { get; private set; } = 5f;
    public float movementSpeed = 5f;
    public float sprintModifier {get; private set; } = 1.5f;
    public float jumpForce { get; private set; } = 5f;
    public float lookRotationDampFactor { get; private set; } = 10f;
    public Transform mainCamera { get; private set; }
    public InputReader inputReader { get; private set; }
    public Animator animator { get; private set; }
    public CharacterController controller { get; private set; }
    public PlayerHealthComponent healthComponent;

    // Start is called before the first frame update
    void Start() {
        mainCamera = Camera.main.transform;

        inputReader = GetComponent<InputReader>();
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        healthComponent = GetComponent<PlayerHealthComponent>();
        healthComponent.OnDeath += SwitchToDeathState;

        SwitchState(new PlayerMoveState(this));
    }

    private void SwitchToDeathState(){
        //Debug.Log("Player died!");
        SwitchState(new PlayerDeathState(this));
    }
}
