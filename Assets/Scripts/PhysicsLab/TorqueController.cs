using UnityEngine;

public class TorqueController : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    float MaxLinearVelocity = 5f;

    [SerializeField]
    float MaxAngularVelocity = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.maxLinearVelocity = MaxLinearVelocity;
        rb.maxAngularVelocity = MaxAngularVelocity;
    }


}
