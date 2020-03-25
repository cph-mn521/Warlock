using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{

    public GameObject projectile;

    public GameObject spawnProjectile(Vector3 spawnPos, Quaternion playerAngle) 
    {
        Vector3 filteredSpawnPos = new Vector3(spawnPos.x, 1.0f, spawnPos.z);
        GameObject obj = Instantiate(projectile, filteredSpawnPos, playerAngle);

        return obj;
    }

    void Update()
    {
        
    }

}
