using Code.Abstractions.Command;
using UnityEngine;

namespace Code.ControlSystem.Command
{
    public class StopCommand:IStopCommand
    {
        public void Stop()
        {
            Debug.Log("Stop");
        }
    }
}