using UnityEngine;

public class TargetBehavior : MonoBehaviour
{
    public Rigidbody targetBody;
    public bool isHit = false;

    private void OnCollisionEnter(Collision other)
    {
        if (isHit) return;

        if (other != null)
        {
            targetBody.isKinematic = false;
            targetBody.useGravity = true;

            float force = 150;

            if (other.gameObject.TryGetComponent<Rigidbody>(out var otherBody))
            {
                force *= otherBody.linearVelocity.magnitude;
            }

            targetBody.AddExplosionForce(force, other.transform.position, 0.5f);

            if (ValidHit(other.gameObject))
            {
                isHit = true;
                GameManager.Instance.UpdateUI();
            }
        }
    }

    /// <summary>
    /// Projectiles are valid hits, as well as other targets that have already been hit (collateral).
    /// </summary>
    /// <returns>True when a valid Collision GameObject collides with this Target.</returns>
    bool ValidHit(GameObject m_gameObject)
    {
        return m_gameObject.GetComponent<ProjectileBehavior>() != null ||
            m_gameObject.TryGetComponent<TargetBehavior>(out var otherTargetBehavior) && otherTargetBehavior.isHit;
    }
}
