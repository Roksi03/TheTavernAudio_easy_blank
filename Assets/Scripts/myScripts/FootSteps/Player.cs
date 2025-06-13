using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : StateMachine.StateMachine
{

    private void Start()
    {
        Debug.Log("chodzenie");
        Begin(new PlayerFoot(this));
    }
}
