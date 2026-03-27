using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[CreateAssetMenu(menuName = "Attack/Normal Attack")]
public class AttackSO : ScriptableObject
{
    public AnimatorOverrideController animatorOV;
    public float Damage;
}
