using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;
using UnityEngine;

public class ShipWeapon : MonoBehaviour
{
  [SerializeField] private Bullet bullet;
  [SerializeField] private Transform nuzzle;
  [SerializeField] private Transform magazine;
  [SerializeField] private int poolSize;
  [SerializeField] private float bulletSpeed;
  [SerializeField] private float fireRate;

  private List<Bullet> firedBullets = new List<Bullet>();
  private List<Bullet> pooledBullets = new List<Bullet>();
  private bool isTriggered;
  private float cooldownTime;

  private void Awake()
  {
    InputManager.Instance.OnClick += OnHold;

    cooldownTime = fireRate;
    for (int i = 0; i < poolSize; i++)
    {
      SpawnBullet();
    }
  }

  private void SpawnBullet()
  {
    var instance = Instantiate(bullet, magazine);
    instance.LoadWeapon(this);
    instance.transform.position = nuzzle.position;
    instance.gameObject.SetActive(false);
    pooledBullets.Add(instance);
  }

  private void OnHold(object sender, bool e)
  {
    isTriggered = e;
  }

  private void Update()
  {
    cooldownTime += Time.deltaTime;
    MoveFiredBullets();
    if (isTriggered)
    {
      FireBullet();
    }
  }

  private void FireBullet()
  {
    if (cooldownTime < fireRate) return;
    cooldownTime = 0f;
    if (!(pooledBullets.Count > 0)) SpawnBullet();
    var poolFirstBullet = pooledBullets.First();
    pooledBullets.Remove(poolFirstBullet);
    firedBullets.Add(poolFirstBullet);
    poolFirstBullet.transform.SetParent(null);
    poolFirstBullet.gameObject.SetActive(true);
  }

  private void MoveFiredBullets()
  {
    if (firedBullets.Count.Equals(0)) return;

    foreach (var firedBullet in firedBullets)
    {
      firedBullet.transform.position += Vector3.up * bulletSpeed * Time.deltaTime;
    }
  }

  public void ReturnBullet(Bullet returnedBullet)
  {
    firedBullets.Remove(returnedBullet);
    pooledBullets.Add(returnedBullet);
    returnedBullet.transform.position = nuzzle.position;
    returnedBullet.transform.SetParent(magazine);
    returnedBullet.gameObject.SetActive(false);
  }
}
