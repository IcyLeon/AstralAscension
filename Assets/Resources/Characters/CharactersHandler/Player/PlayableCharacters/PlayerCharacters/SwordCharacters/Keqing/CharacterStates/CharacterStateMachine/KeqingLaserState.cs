using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeqingLaserState : PlayerElementalBurstState
{
    private enum ElementalBurst
    {
        First_Phase,
        Last_Hit,
    }
    private ElementalBurst laserBurst;
    private int TotalHits = 12;
    private float TimeInBetweenHits;
    private float LastHitTimer;
    private float HitElapsed;
    private int CurrentHits;
    private Vector3 LastPosition;

    public KeqingLaserState(SkillStateMachine Skill) : base(Skill)
    {
        TimeInBetweenHits = 2.5f / (TotalHits * 2f);
    }

    public override void Enter()
    {
        base.Enter();
        CurrentHits = 0;
        laserBurst = ElementalBurst.First_Phase;
        LastPosition = playableCharacters.GetCenterBound();

        playableCharacters.Animator.gameObject.SetActive(false);

        HitElapsed = Time.time + TimeInBetweenHits;
    }

    private void UpdateBurstState()
    {
        switch (laserBurst)
        {
            case ElementalBurst.First_Phase:
                if (CurrentHits >= TotalHits - 1)
                {
                    laserBurst = ElementalBurst.Last_Hit;
                    LastHitTimer = Time.time;
                    return;
                }

                if (Time.time - HitElapsed > TimeInBetweenHits)
                {
                    BurstAreaDamage(LastPosition);
                    HitElapsed = Time.time;
                    CurrentHits++;
                }
                break;
            case ElementalBurst.Last_Hit:
                if (Time.time - LastHitTimer > 0.8f)
                {
                    BurstAreaDamage(LastPosition);
                    CurrentHits++;
                }
                break;
        }
    }

    private void BurstAreaDamage(Vector3 pos)
    {
        Collider[] colliders = Physics.OverlapSphere(pos, 10f);
        for (int i = 0; i < colliders.Length; i++)
        {
            IDamageable damage = colliders[i].GetComponent<IDamageable>();
            if (damage != null && damage is not PlayableCharacters)
            {
                if (!damage.IsDead())
                {
                    damage.TakeDamage(playableCharacters, playableCharacters.GetElementsSO(), 10f, colliders[i].ClosestPoint(LastPosition));
                }
            }
        }
    }

    protected override void InitBaseBurstAction()
    {
    }

    public override void Exit()
    {
        base.Exit();
        HitElapsed = 0;
        playableCharacters.Animator.gameObject.SetActive(true);
    }

    public override bool IsElementalStateEnded()
    {
        return CurrentHits >= TotalHits || base.IsElementalStateEnded();
    }

    public override void UpdateElementalState()
    {
        base.UpdateElementalState();
        UpdateBurstState();
    }

    public override void OnElementalStateExit()
    {
        base.OnElementalStateExit();
    }

    public override void Update()
    {
        if (CurrentHits >= TotalHits / 2f)
        {
            playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.EntityState);
        }
    }
}
