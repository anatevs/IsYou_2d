using System;
using UnityEngine;

namespace GameCore
{
    public class WinManager : MonoBehaviour
    {
        public event Action OnWinned;

        public void MakeOnWin()
        {
            Debug.Log("you are win!");

            OnWinned?.Invoke();
        }
    }
}