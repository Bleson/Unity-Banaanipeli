using UnityEngine;
using System.Collections;

public class TimerWarning : LerpSizeSin {

    public float triggerTime = 5f;
    bool triggered = false;

    internal override void Update()
    {
        if (triggered)
        {
            base.Update();
        }
    }

    internal void UpdateTime(ref float timeRemaining)
    {
        if (!triggered && timeRemaining < triggerTime)
        {
            TurnOn();
        }
    }

    void TurnOn()
    {
        text.enabled = true;
        triggered = true;
    }

    internal void TurnOff()
    {
        text.enabled = false;
        triggered = false;
    }
}
