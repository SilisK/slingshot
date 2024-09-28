using UnityEngine;

public class TargetBehavior : MonoBehaviour
{
    public Rigidbody targetBody;

    private void OnCollisionEnter(Collision other)
    {
        if(other != null)
        {
            targetBody.isKinematic = false;
            targetBody.useGravity = true;

            float force = 150;

            if(TryGetComponent<Rigidbody>(out var otherBody))
            {
                force *= otherBody.linearVelocity.magnitude;
            }

            targetBody.AddExplosionForce(force, other.transform.position, 0.5f);
        }    
    }
}
