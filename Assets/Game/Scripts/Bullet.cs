using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  private ShipWeapon shipWeapon;

  public void LoadWeapon(ShipWeapon weapon)
  {
    shipWeapon = weapon;
  }

  private void OnTriggerEnter2D(Collider2D col)
  {
    if (col.TryGetComponent(out TopBarrier topBarrier))
    {
      shipWeapon.ReturnBullet(this);
    }
  }
}
