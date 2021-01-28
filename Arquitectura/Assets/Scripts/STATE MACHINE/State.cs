using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StateBehaviour
{
    //State Class from which other states derive from
    public abstract class State
    {
        //State behaviour functions
        public virtual void EnterState()
        {
            throw new System.NotImplementedException();
        }

        public virtual void UpdateState()
        {
            throw new System.NotImplementedException();
        }

        public virtual void ExitState()
        {
            throw new System.NotImplementedException();
        }
    }
}


