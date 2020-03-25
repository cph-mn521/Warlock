using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerScript : MonoBehaviour
{
    public Transform transform;
    public Transform prefab;
    public Camera cam;

    public Health hp;
    public NavMeshAgent agent;
    public Rigidbody rigidbody;
    // Start is called before the first frame update
    private bool controllable = true;
    private bool punt = false;
    public float cooldown = 2.0f;
    private float nxtFire = 0f;


    void Start()
    {
        this.hp.setHealth(100);
        Debug.Log("hello");
    }

    // Update is called once per frame
    void Update()
    {


        //Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        //Get the angle between the points
        float angle = -AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        //Ta Daaa
        transform.rotation = Quaternion.Euler(new Vector3(0f, angle - 90f, 0f));

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
                Debug.Log(hit.point);
            }

        }
        if (Time.time > nxtFire && Input.GetMouseButton(1))
        {
            Instantiate(prefab, transform.position, transform.rotation);
            nxtFire = Time.time + cooldown;
        }
        if (Input.GetKey("n"))
        {
            Debug.Log(transform.position.x);
            Debug.Log(transform.position.z);
        }
    }
    void FixedUpdate()
    {
        if (punt)
        {
            Vector3 dir = transform.position.normalized;
            dir.y = 0;
            rigidbody.AddForce(transform.position.normalized * 20);
            punt = false;
            controllable = true;
            agent.ResetPath();
        }
        if ((Math.Abs(transform.position.z) > 8.5 || Math.Abs(transform.position.x) > 8.5) && controllable)
        {
            agent.updatePosition = false;
            controllable = false;
            punt = true;

        }
        else
        {
            rigidbody.velocity = Vector3.zero;
        }

    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

}
