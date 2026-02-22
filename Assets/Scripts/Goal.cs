using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public ParticleSystem Goal_Effect;
    public void OnBecameVisible()
    {
        if(Goal_Effect != null)
        { 
            Goal_Effect.Play(); 
        }
        
    }
    public void OnBecameInvisible()
    {
        if (Goal_Effect != null)
        {
            Goal_Effect.Stop();
        }
    }
}
