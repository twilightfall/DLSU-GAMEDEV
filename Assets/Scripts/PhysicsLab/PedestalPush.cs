using UnityEngine;

public class PedestalPush : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        
    }

    private void Start()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.up * 10f, Color.yellow);
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.rigidbody.AddForce(Vector3.up * 5f, ForceMode.Impulse);
    }

    private void OnCollisionExit(Collision collision)
    {
        

    }

    private void OnCollisionStay(Collision collision)
    {
        
    }
}
