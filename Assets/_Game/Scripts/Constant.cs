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
    public const string ANIM_WIN = "win";
    public const string ANIM_ATTACK_SPEED = "attackSpeed";

    // Tag
    public const string TAG_CHARACTER = "Character";
    public const string TAG_BULLET = "Bullet";
    public const string TAG_BUTTON = "Button";

    // Other
    public const float ZERO = 0.001f;
    public const float TEN_PERCENT = 0.1f;
    public const float DELAY_BUTTON = 2f;

    public const float THROW_RATIO = 2f / 5f;
    public const float RETRACT_RATIO = 3f / 5f;
    public const float DELAY_RATIO = 1f;
}
