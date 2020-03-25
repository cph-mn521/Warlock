using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile", menuName = "Projectile")]
public class BasicAttack : ScriptableObject
{
    public float speed;
    public float size;
    public float lifetime;
    public float cooldown;
}
