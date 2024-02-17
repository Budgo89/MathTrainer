using System;
using System.Collections;
using System.Collections.Generic;
using _Root.Scripts.Enums;
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
    public class WorldGeneratorMultiplicationHardController: BaseController, IWorldGenerator
    {
        private readonly ResourcePath _resourcePathFieldView = new ResourcePath("Prefabs/Field");
        private readonly ResourcePath _resourcePathPointView = new ResourcePath("Prefabs/FieldHard");
        private readonly ResourcePath _resourcePathItemView = new ResourcePath("Prefabs/Item");
        
        private readonly Transform _placeFor;

        private FieldView _fieldView;
        private Random _random;
        private Coroutine _pauseCoroutine;
        
        public List<PointModel> _pointModels;
        
        public event Action<List<PointModel>> RestartAction;
        
        public WorldGeneratorMultiplicationHardController(Transform placeFor)
        {
            _placeFor = placeFor;
            _pointModels = new List<PointModel>();
            _random = new Random();
            _fieldView = LoadFieldView(_placeFor);
        }
        
        public void StopGenerator()
        {
            foreach (var pointModel in _pointModels)
            {
                pointModel.PointView.OnDestroy();
            }
        }
        
        public List<PointModel> GetPointModels()
        {
            return _pointModels;
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
                pointModel.PointView.OnDestroy();
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
                var item =  GerRandomPoint(_random);
                
                PointModel pointModel = new PointModel();
                pointModel.PointView = LoadPointView(point, _resourcePathPointView);
                pointModel.ItemModels =
                    new ItemModel(LoadItemView(pointModel.PointView.Point1), 
                        LoadItemView(pointModel.PointView.Point2), 
                        LoadItemView(pointModel.PointView.Point3), 
                        LoadItemView(pointModel.PointView.Point4), 
                        TypeGameEnum.Multiplication);
                _pointModels.Add(pointModel);
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
            item.SetValue(_random.Next(2,10));

            return item;
        }
    }
}