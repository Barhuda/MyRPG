﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBook : MonoBehaviour
{
    [SerializeField] Spell[] spells;

    public Spell CastSpell(int index)
    {
        return spells[index];
    }
  }
