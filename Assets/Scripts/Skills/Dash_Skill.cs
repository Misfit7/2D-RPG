using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash_Skill : Skill
{
    [Header("Dash info")]
    [SerializeField] public float dashDuration = 0.25f;
    [SerializeField] public float dashForce = 2.0f;
    public override void UseSkill()
    {
        base.UseSkill();
    }
}
