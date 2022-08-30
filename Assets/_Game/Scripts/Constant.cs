using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constant
{
    // Character animation
    public const string ANIM_IDLE = "idle";
    public const string ANIM_RUN = "run";
    public const string ANIM_ATTACK = "attack";
    public const string ANIM_DEAD = "dead";

    // Tag
    public const string TAG_CHARACTER = "Character";
    public const string TAG_BULLET = "Bullet";

    // Number
    public static readonly float SCALE_FLOAT = 1.1f;
    public static readonly Vector3 SCALE_VECTOR3 = new Vector3(1.1f, 1.1f, 1.1f);
}
