using UnityEngine;

public class PlayerMove : IMovement

{
    
    private readonly Rigidbody rb;
    private readonly float moveForce;

    public PlayerMove(Rigidbody rigidbody, float moveForce) 
    {
        rb = rigidbody;
        this.moveForce = moveForce;
    }
    public void Move(Vector3 direction)
    {
        rb.AddForce(direction*moveForce*Time.fixedDeltaTime, ForceMode.Acceleration);
    }

}
