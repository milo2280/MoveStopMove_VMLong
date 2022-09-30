using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : GameUnit, IHit<Character>
{
    public Animator animator;
    public Collider m_Collider;
    public Transform m_Transform;

    public Hand hand;
    public HeadBar headBar;
    public Transform bulletPoint, rangeTranform, headTransform;
    public SkinnedMeshRenderer bodyMesh, pantMesh;

    protected int score;
    public int Score { get { return score; } private set { } }
    protected Vector3 scale;
    public Vector3 Scale { get { return scale; } private set { } }
    protected string charName;
    public string CharName { get { return charName; } private set { } }

    public float baseRange, baseAS, baseMS, baseBS;
    [HideInInspector]
    public float range, attackSpeed, moveSpeed, bulletSpeed;

    protected Dictionary<SkinType, Skin> dictUsedSkin = new Dictionary<SkinType, Skin>();
    protected Skin currentHair;
    protected Color color;
    protected string currentAnim;
    protected Weapon weapon;
    protected float[] addBuff = { 0f, 0f, 0f, 0f };
    protected float[] percentBuff = { 0f, 0f, 0f, 0f };
    protected Character target;
    protected List<Character> targets = new List<Character>();
    protected bool isDead, isAttacking, isThrew, isDelaying;
    protected float attackTimer, delayTimer;
    protected float fullAttackDuration, throwDuration, retractHandDuration, delayDuration;

    public virtual void OnInit() 
    {
        InitStat();
        isDead = false;
        m_Collider.enabled = true;
        UpdateScore(0);
        InitTarget();
    }

    #region Add buff & Setup stat

    public void InitStat()
    {
        InitBaseStat();
        AddBuff();
        SetupAS();
        SetupRange();
    }

    public void InitBaseStat()
    {
        range = baseRange;
        attackSpeed = baseAS;
        moveSpeed = baseMS;
        bulletSpeed = baseBS;
        scale = new Vector3(1f, 1f, 1f);
        m_Transform.localScale = scale;
    }

    public void AddBuff()
    {
        for (int i = 0; i < addBuff.Length; i++)
        {
            addBuff[i] = 0;
            percentBuff[i] = 0;
        }

        if (weapon != null)
        {
            bulletSpeed += weapon.bulletSpeed;
            ClassifyBuff(weapon.buff);
            CalculateNewStat();
        }
    }

    public void ClassifyBuff(Buff buff)
    {
        if (buff.buffClass == BuffClass.Add)
        {
            addBuff[(int)buff.buffType] = buff.buffAmount;
        }
        else
        {
            percentBuff[(int)buff.buffType] = buff.buffAmount;
        }
    }

    public virtual void CalculateNewStat()
    {
        range = (range + addBuff[(int)BuffType.Range] / 10) * (1 + percentBuff[(int)BuffType.Range] / 100);
        attackSpeed = (attackSpeed + addBuff[(int)BuffType.AttackSpeed]) * (1 + percentBuff[(int)BuffType.AttackSpeed] / 100);
        moveSpeed = (moveSpeed + addBuff[(int)BuffType.MoveSpeed]) * (1 + percentBuff[(int)BuffType.MoveSpeed] / 100);
    }

    protected void SetupRange()
    {
        rangeTranform.localScale = new Vector3(1f, 1f, 1f) * range / baseRange;
    }

    protected void SetupAS()
    {
        fullAttackDuration = attackSpeed / 100;
        throwDuration = fullAttackDuration * Constant.THROW_RATIO;
        retractHandDuration = fullAttackDuration * Constant.RETRACT_RATIO;
        delayDuration = fullAttackDuration * Constant.DELAY_RATIO;
        animator.SetFloat(Constant.ANIM_ATTACK_SPEED, fullAttackDuration);
    }

    #endregion

    public void SetColor(Color color)
    {
        this.color = color;
        bodyMesh.material.color = color;
        pantMesh.material.color = color;
        headBar.SetColor(color);
    }

    public virtual void OnHit(Character killer)
    {
        OnDeath();
    }

    public virtual void OnDeath()
    {
        isDead = true;
        m_Collider.enabled = false;
        SetColor(color / 3);
        ChangeAnim(Constant.ANIM_DEAD);
        if (isAttacking) StopAttack();
        SoundManager.Ins.PlayAudio(AudioType.Die);
    }

    public virtual void OnDespawn() { }

    public void ChangeAnim(string nextAnim)
    {
        if (currentAnim != nextAnim)
        {
            if (!string.IsNullOrEmpty(currentAnim))
            {
                animator.ResetTrigger(currentAnim);
            }
            animator.SetTrigger(nextAnim);
            currentAnim = nextAnim;
        }
    }

    #region Detect Target & Attack

    private void InitTarget()
    {
        target = null;
        targets.Clear();
    }

    public void AddTarget(Character target)
    {
        targets.Add(target);
    }

    public virtual void RemoveTarget(Character target)
    {
        targets.Remove(target);
    }

    public bool ScanTarget()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i].isDead) RemoveTarget(targets[i]);
        }

        if (targets.Count > 0)
        {
            if (!targets.Contains(target)) target = targets[0];
            return true;
        }
        else
        {
            target = null;
            return false;
        }
    }

    public void Attack()
    {
        if (!isAttacking && !isDelaying)
        {
            isAttacking = true;
            m_Transform.LookAt(target.m_Transform);
            ChangeAnim(Constant.ANIM_ATTACK);
        }
    }

    public void Attacking()
    {
        attackTimer += Time.deltaTime;

        if (!ScanTarget() && !isThrew)
        {
            ChangeAnim(Constant.ANIM_IDLE);
            StopAttack();
        }

        if (attackTimer > throwDuration && !isThrew)
        {
            weapon.Attack();
            hand.HideWeapon(retractHandDuration);
            isDelaying = true;
            isThrew = true;
        }

        if (attackTimer > fullAttackDuration)
        {
            attackTimer = 0;
            isAttacking = false;
            isThrew = false;
            ChangeAnim(Constant.ANIM_IDLE);
        }
    }

    public void Delaying()
    {
        delayTimer += Time.deltaTime;
        if (delayTimer > delayDuration)
        {
            delayTimer = 0;
            isDelaying = false;
        }
    }

    public void StopAttack()
    {
        isAttacking = false;
        isThrew = false;
        attackTimer = 0;
    }

    #endregion

    public virtual void OnKill()
    {
        IncreaseSize();
        UpdateScore(++score);
    }

    private void IncreaseSize()
    {
        scale += scale * Constant.TEN_PERCENT;
        m_Transform.localScale = scale;
        range += range * Constant.TEN_PERCENT;
    }

    public virtual void UpdateScore(int score)
    {
        this.score = score;
        headBar.UpdateScore(score);
    }

    public virtual void SetName(string name)
    {
        headBar.SetName(name);
        charName = name;
    }

    public void WearSkin(SkinType skinType)
    {
        Skin skin = SkinManager.Ins.dictSkin[skinType];

        switch (skin.skinClass)
        {
            case SkinClass.Hair:
                WearHair(skin);
                break;
        }
    }

    private void WearHair(Skin skin) 
    {
        if (currentHair != null)
        {
            currentHair.gameObject.SetActive(false);
        }

        if (dictUsedSkin.ContainsKey(skin.skinType))
        {
            currentHair = dictUsedSkin[skin.skinType];
            currentHair.gameObject.SetActive(true);
        }
        else
        {
            currentHair = Instantiate<Skin>(skin, headTransform);
            currentHair.m_Transform.localPosition = Vector3.zero;
            dictUsedSkin.Add(currentHair.skinType, currentHair);
        }
    }
}
