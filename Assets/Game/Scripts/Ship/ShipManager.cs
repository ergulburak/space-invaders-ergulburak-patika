using System;
using UnityEngine;

[RequireComponent(typeof(ShipMovement))]
public class ShipManager : MonoBehaviour
{
  private ShipMovement shipMovement => GetComponent<ShipMovement>();

  private void Start()
  {
    InputManager.Instance.OnSwerve += OnSwerve;
  }

  private void OnSwerve(object sender, Vector2 e)
  {
    shipMovement.MoveHorizontal(e);
  }

  private void Update()
  {
  }
}
