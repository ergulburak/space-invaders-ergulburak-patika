using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class EnemyManager : MonoBehaviour
{
  [SerializeField] private Enemy enemyPrefab;
  private List<Enemy> enemies = new List<Enemy>();
  private BoxFormation formation => GetComponent<BoxFormation>();
  private List<Vector3> enemyPoints = new List<Vector3>();

  private void Awake()
  {
    enemyPoints = formation.EvaluatePoints().ToList();
    for (int i = 0; i < enemyPoints.Count; i++)
    {
      var enemy = Instantiate(enemyPrefab);
      enemies.Add(enemy);
      enemy.transform.position = enemyPoints[i]+transform.position;
    }
  }
}
