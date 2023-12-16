using System;
using System.Collections;
using System.Collections.Generic;
using _Root.Scripts.Interfaces;
using _Root.Scripts.Models;
using _Root.Scripts.View;
using Controllers;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;

namespace _Root.Scripts.Controllers
{
    public class WorldGeneratorController : BaseController, IWorldGenerator
    {
        private readonly ResourcePath _resourcePathFieldView = new ResourcePath("Prefabs/Field");
        private readonly ResourcePath _resourcePathPointHorizontalView = new ResourcePath("Prefabs/FieldHorizontal");
        private readonly ResourcePath _resourcePathPointVerticalView = new ResourcePath("Prefabs/FieldVertical");
        private readonly ResourcePath _resourcePathItemView = new ResourcePath("Prefabs/Item");
        
        private readonly Transform _placeFor;

        private FieldView _fieldView;
        private List<ResourcePath> _resourcePathsPoint;
        private Random _random;
        private Coroutine _pauseCoroutine;
        
        public List<PointModel> _pointModels;

        public List<PointModel> GetPointModels()
        {
            return _pointModels;
        }

        public event Action<List<PointModel>> RestartAction;

        public WorldGeneratorController(Transform placeFor)
        {
            _placeFor = placeFor;
            _resourcePathsPoint = new List<ResourcePath>();
            _pointModels = new List<PointModel>();
            _resourcePathsPoint.Add(_resourcePathPointHorizontalView);
            _resourcePathsPoint.Add(_resourcePathPointVerticalView);
            _random = new Random();
            _fieldView = LoadFieldView(_placeFor);
            
        }

        public void StartGenerator()
        {
            WorldGenerator();
            RestartAction?.Invoke(_pointModels);
        }

        public void RestartGenerator()
        {
            foreach (var pointModel in _pointModels)
            {
                pointModel._pointView.OnDestroy();
            }
            
            _pauseCoroutine = CoroutineController.StartRoutine(Pause());
        }

        private IEnumerator Pause()
        {
            yield return new WaitForSeconds(1f);
            _pointModels.Clear();
            WorldGenerator();
            RestartAction?.Invoke(_pointModels);
            CoroutineController.StopRoutine(_pauseCoroutine);
        }

        private void WorldGenerator()
        {
            
            foreach (var point in _fieldView.Points)
            {
                var item =  GerRandomPoint();
                
                PointModel pointModel = new PointModel();
                pointModel._pointView = LoadPointView(point, _resourcePathsPoint[item]);
                pointModel._itemModels =
                    new ItemModel(LoadItemView(pointModel._pointView.Point1), LoadItemView(pointModel._pointView.Point2));
                _pointModels.Add(pointModel);
            }
        }

        private int GerRandomPoint()
        {
            var item = _random.Next(0, 10);
            if (item % 2 == 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        
        private FieldView LoadFieldView(Transform placeFor)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePathFieldView);
            GameObject objectView = Object.Instantiate(prefab, placeFor, false);
            AddGameObject(objectView);

            return objectView.GetComponent<FieldView>();
        }
        
        private PointView LoadPointView(Transform placeFor, ResourcePath resourcePath)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeFor, false);
            AddGameObject(objectView);

            return objectView.GetComponent<PointView>();
        }
        
        private ItemView LoadItemView(Transform placeFor)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePathItemView);
            GameObject objectView = Object.Instantiate(prefab, placeFor, false);
            AddGameObject(objectView);

            var item = objectView.GetComponent<ItemView>();
            item.SetValue(_random.Next(2,9));

            return item;
        }
    }
}