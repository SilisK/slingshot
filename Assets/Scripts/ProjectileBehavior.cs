using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public GameObject impactEffect;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<TargetBehavior>(out TargetBehavior targetBehavior))
        {
            GameObject _impactEffect = Instantiate(impactEffect, transform.position, Quaternion.identity);
            Destroy(_impactEffect, 2);
            Destroy(gameObject, 0.1f);
        }
    }
}
