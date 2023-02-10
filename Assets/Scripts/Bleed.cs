using UnityEngine;
using System;
using System.Collections;

public class Bleed : StatusEffect {

    private int stacks;
    private bool immune;
    public Bleed(){
        stacks = 5;
        immune = false;
    }
    public Bleed(HealthComponent target, int stacks){
        this.target = target;
        this.stacks = stacks;
        this.immune = false;
    }

    void Start(){
        stacks = 5;
        Apply();
    }

    public override void Apply(){
        StartCoroutine(Begin());
    }

    public override void Remove(){
        Destroy(this);
    }

    public void Update(){
        if(target.IsDead())
            Remove();
    }

    public void SetStacks(int amount){
        stacks = amount;
    }

    private IEnumerator Begin(){
        if(!immune && stacks > 0){
            target.MortalDamage(5f);
            immune = true;
            yield return new WaitForSeconds(1f);
            immune = false;
            stacks--;
        }else{
            Remove();
        }
    }
}