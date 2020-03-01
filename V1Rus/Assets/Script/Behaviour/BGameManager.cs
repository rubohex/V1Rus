using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGameManager : MonoBehaviour
{
    #region ATRIBUTES

    private List<BEnemy> enemies = new List<BEnemy>();

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        enemies.AddRange(FindObjectsOfType<BEnemy>());
    }

    public void EnemyTurn()
    {
        enemies.ForEach(enemy => enemy.NextMovement());
    }
}
