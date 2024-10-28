using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ShootEmUp
{
    public class GameListenerInstaller: IStartable
    {
        public GameListenerInstaller(GameManager gameManager, IEnumerable<IGameListener> gameListeners)
        {
            foreach (var listener in gameListeners)
            {
                gameManager.RegisterListener(listener);
            }
        }

        public void Start()
        {
            //NonLazy VContainer
        }
    }
}