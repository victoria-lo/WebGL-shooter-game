using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour
{
    public Transform weaponHold;
    public Bow startingBow;
    Bow equippedBow;

    private void Start()
    {
        if (startingBow != null)
        {
            EquipBow(startingBow);
        }
    }

    public void EquipBow(Bow bowToEquip)
    {
        if (equippedBow != null)
        {
            Destroy(equippedBow.gameObject);
        }
        equippedBow = Instantiate(bowToEquip, weaponHold.position, weaponHold.rotation) as Bow;
        equippedBow.transform.parent = weaponHold;
    }

    public void Shoot()
    {
        if (equippedBow != null)
        {
            equippedBow.Shoot();
        }
    }

    public float BowHeight
    {
        get
        {
            return weaponHold.position.y;
        }
    }
}
