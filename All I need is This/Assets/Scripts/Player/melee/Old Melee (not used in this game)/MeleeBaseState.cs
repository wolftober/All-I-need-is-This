using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class MeleeBaseState : State
{
    // How long this state should be active for before moving on
    public float duration;
    // Cached animator component
    protected Animator animator;
    // bool to check whether or not the next attack in the sequence should be played or not
    protected bool shouldCombo;
    // The attack index in the sequence of attacks
    protected int attackIndex;

    // cached hit collider component of this attack
    protected Collider2D hitCollider;
    // cached already struck objects of said attack to avoid overlapping attacks on the same target
    private List<Collider2D> collidersDamaged;
    // The Hit Effect to spawn on the afflicted Enemy
    private GameObject HitEffectPrefab;
    // Input buffer timer
    private float AttackPressedTimer = 0;

    public override void OnEnter(StateMachine _stateMachine){
        base.OnEnter(_stateMachine);
        animator = GetComponent<Animator>();
        collidersDamaged = new List<Collider2D>();
        hitCollider = GetComponent<ComboCharacter>().hitbox;
        HitEffectPrefab = GetComponent<ComboCharacter>().Hiteffect;
    }

    public override void OnUpdate(){
        base.OnUpdate();
        AttackPressedTimer -= Time.deltaTime;
        
        if(animator.GetFloat("Weapon.Active") > 0f){
            Attack();
        }

        if(Input.GetMouseButtonDown(0)){
            AttackPressedTimer = 2;
        }

        if(animator.GetFloat("AttackWindow.Open") > 0f && AttackPressedTimer > 0){
            shouldCombo = true;
        }
    }
    public override void OnExit(){
        base.OnExit();
    }

    protected void Attack(){
        Collider2D[] collidersToDamage = new Collider2D[10];
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = true;
        int colliderCount = Physics2D.OverlapCollider(hitCollider, filter, collidersToDamage);
        for(int i = 0; i < colliderCount; i++){
            if(!collidersDamaged.Contains(collidersToDamage[i])){
                TeamComponent hitTeamComponent = collidersToDamage[i].GetComponentInChildren<TeamComponent>();

                // only check colliders with a valid team componnent attached
                if(hitTeamComponent && hitTeamComponent.teamIndex == TeamIndex.Enemy){
                    GameObject.Instantiate(HitEffectPrefab, collidersToDamage[1].transform);
                    Debug.Log("Enemy Has Taken:" + attackIndex + "Damage");
                    collidersDamaged.Add(collidersToDamage[i]);
                }
            }
        }
    }
}
