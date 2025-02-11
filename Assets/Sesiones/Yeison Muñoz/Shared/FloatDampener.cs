using System;
using UnityEngine;

[Serializable]
public struct FloatDampener
{
    [SerializeField] private float smoothTime;
    public float CurrentValue {get; private set;}
    private float currentVelocity;

        public float TargetValue {get; set;}
    

    public void Update()
    {
        CurrentValue = Mathf.SmoothDamp(CurrentValue, TargetValue, ref currentVelocity ,smoothTime);
    }

    
}
