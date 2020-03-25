using UnityEngine;

public class CollisionDetector : MonoBehaviour
{

    public float pushMultiplier = 4; 
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 diff = gameObject.transform.position - other.transform.position;
        diff.Normalize();
        diff.y = 0.0f;

        other.GetComponent<Rigidbody>().AddForce(-diff * 1000.0f * pushMultiplier);
        Destroy(gameObject);
    }

}
