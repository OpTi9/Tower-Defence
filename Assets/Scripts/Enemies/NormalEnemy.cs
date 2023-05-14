using UnityEngine;

public class NormalEnemy : Enemy
{
    protected override void Start()
    {
        base.Start();
        Debug.Log("NormalEnemy");
    }
}
