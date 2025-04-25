using Unity.VisualScripting;
using UnityEngine;

public class EnemyStateAttack : State<States>
{
    PlayerController player;
     private int damage = 2;
    public EnemyStateAttack(bool CanAttack)
    {
      

}
public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void Execute()
    {
    
        player.Health -= damage;
    }

    public override void OnExit()
    {

    }

    
}