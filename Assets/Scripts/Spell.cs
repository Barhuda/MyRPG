using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spell")]
public class Spell : ScriptableObject
{

    public new string name;

    public int damage;

    public int manaCost;

    public Sprite icon;

    public float speed;

    public float castTime;

    public GameObject spellPrefab;

    public Color barcolor;

    public float cooldown;

}
