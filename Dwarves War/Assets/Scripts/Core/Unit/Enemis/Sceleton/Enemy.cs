using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : Unit
{
    private Vector3 scale;

    protected override void Awake()
    {
        base.Awake();
        scale = transform.localScale;

    }
   
     protected override void FaceTarget(Vector3 target)
    {
        Vector3 scale = transform.localScale;

        if (target.x > transform.position.x)
            scale.x = -Mathf.Abs(scale.x);
        else
            scale.x = Mathf.Abs(scale.x);

        transform.localScale = scale;
    }
    private void FixedUpdate()
    {
    }
   


}
