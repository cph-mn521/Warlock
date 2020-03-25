using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{

    public ProjectileHandler ph;
    private List<GameObject> basicAttacks = new List<GameObject>();

    public float basicAttackCooldown = 1.0f;
    private float lastShot;

    void Start()
    {
        lastShot = Time.time;
    }

    void Update()
    {

        if (Input.GetMouseButton(0)) {

            if ( ( Time.time - lastShot > basicAttackCooldown ) )
            {
                GameObject projectile = ph.spawnProjectile(transform.position + transform.forward * 2, transform.rotation);
                projectile.GetComponent<ProjectileProps>().spawned = Time.time;
                basicAttacks.Add(projectile);
                lastShot = Time.time;
            }
        }
        
    }

    void FixedUpdate()
    {

        for (int i = 0; i < basicAttacks.Count; i++)
        {
            if (basicAttacks[i] != null)
            {
                GameObject ba = basicAttacks[i];

                ba.transform.Translate(Vector3.forward * Time.deltaTime * ba.GetComponent<ProjectileProps>().basicAttack.speed);
                
                if ((Time.time - ba.GetComponent<ProjectileProps>().spawned) > ba.GetComponent<ProjectileProps>().basicAttack.lifetime)
                {
                    Destroy(ba);
                    basicAttacks.RemoveAt(i);
                }
            }
            else {
                basicAttacks.RemoveAt(i);
            }
        }
        
    }

}
