using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveForce;

    private IMovement movement;
    private  PlayerOnPut playerOnPut;

    private void Start()
    {
        var rb = GetComponent<Rigidbody>();

     playerOnPut = new PlayerInput();
        movement = new PlayerMove(rb, moveForce);
    }
    private void FixedUpdate()
    {
        Vector3 direction = playerOnPut.GetDirection();

        movement.Move(direction);
    }
}

