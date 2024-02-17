using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;

namespace Controllers
{
    public class BaseController : IDisposable
    {
        private List<IDisposable> _disposableObjects;
        private List<GameObject> _gameObjects;
        private bool _isDisposed;


        public void Dispose()
        {
            if (_isDisposed)
                return;

            _isDisposed = true;

            DisposeDisposableObjects();
            DisposeGameObjects();

            OnDispose();
        }

        private void DisposeDisposableObjects()
        {
            if (_disposableObjects == null)
                return;

            foreach (IDisposable disposableObject in _disposableObjects)
                disposableObject.Dispose();

            _disposableObjects.Clear();
        }

        private void DisposeGameObjects()
        {
            if (_gameObjects == null)
                return;

            foreach (GameObject gameObject in _gameObjects)
                Object.Destroy(gameObject);

            _gameObjects.Clear();
        }

        protected virtual void OnDispose() { }

        protected void AddController(BaseController baseController) =>
            AddDisposableObject(baseController);

        protected void AddGameObject(GameObject gameObject)
        {
            _gameObjects ??= new List<GameObject>();
            _gameObjects.Add(gameObject);
        }
        
        protected int RandomItem(Random random, int max = 100)
        {
            return random.Next(1, max);
        }
        
        protected int GerRandomPoint(Random random)
        {
            var item = random.Next(0, 10);
            if (item % 2 == 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        private void AddDisposableObject(IDisposable disposable)
        {
            _disposableObjects ??= new List<IDisposable>();
            _disposableObjects.Add(disposable);
        }

        protected void Log(string message) =>
            Debug.Log(WrapMessage(message));

        protected void Error(string message) =>
            Debug.LogError(WrapMessage(message));

        private string WrapMessage(string message) =>
            $"[{GetType().Name}] {message}";
    }
}
