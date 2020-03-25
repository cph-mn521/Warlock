using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int maxHealth;
    private int health;
    public void setHealth(int value){
        this.maxHealth=value;
        this.health = value;
    }
    public void takeDmg(int value){
        this.health-=value;
    }
    public void gainHealth(int value){
        this.health += value;
    }

    public int getMax(){
        return this.maxHealth;
    }
    public int getHealth(){
        return this.health;
    }
}
