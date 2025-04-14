using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public InputScript inputScript;

    private void OnCollisionEnter(Collision collision)
    {
        inputScript.OnPlayerCollision();
    }
}
