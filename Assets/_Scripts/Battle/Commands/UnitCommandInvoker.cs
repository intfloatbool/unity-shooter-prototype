using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts.Battle.Commands
{
    public class UnitCommandInvoker : MonoBehaviour
    {
        public event Action<UnitCommandBase> OnCommandExecuted; 

        public void RunCommand<TCommand>(TCommand cmd) where TCommand : UnitCommandBase
        {
            Assert.IsNotNull(cmd);
            cmd.Execute();
            
            OnCommandExecuted?.Invoke(cmd);
        }
    }
}
