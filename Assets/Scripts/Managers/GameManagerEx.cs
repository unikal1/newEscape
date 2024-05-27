using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerEx
{

    [SerializeField]
    Define.GameState _state;
    public Define.GameState State { get { return _state; } set { _state = value; } }

}
