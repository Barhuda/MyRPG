using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave", menuName = "Wave")]
public class Wave : ScriptableObject
{
    public Enemy[] arrayOfEnemies;

    public float timeBetweenSpawns;

    public int amountOfEnemies;




}
