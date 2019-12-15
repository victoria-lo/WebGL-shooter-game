using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public Transform muzzle;
    public Arrows arrow;
    public float msBtwShots = 100000;
    //The speed at which the arrow will leave the bow.
    public float muzzleVelocity = 35;

    float nextShotTime;
    public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + msBtwShots / 1000;
            Arrows newArrows = Instantiate(arrow, muzzle.position, muzzle.rotation) as Arrows;
            newArrows.SetSpeed(muzzleVelocity);
            GetComponent<AudioSource>().Play();
        }
        
    }
}
