using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Projectile;
    public GameObject Slingshot;

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Debug.Log("Mouse Button Getted...");
        }
    }
}
