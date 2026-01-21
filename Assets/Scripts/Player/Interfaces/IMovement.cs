using UnityEngine;

public interface IMovement
{
    void Move(Vector3 direction);
   // _rb.AddForce(direction* moveForce * Time.fixedDeltaTime, ForceMode.Acceleration);
}
