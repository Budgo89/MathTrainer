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
    public class WorldGeneratorDivisionNormalController: BaseController, IWorldGenerator
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
        
        public event Action<List<PointModel>> RestartAction;
        
        public WorldGeneratorDivisionNormalController(Transform placeFor)
        {
            _placeFor = placeFor;
            _resourcePathsPoint = new List<ResourcePath>();
            _pointModels = new List<PointModel>();
            _resourcePathsPoint.Add(_resourcePathPointHorizontalView);
            _resourcePathsPoint.Add(_resourcePathPointVerticalView);
            _random = new Random();
            _fieldView = LoadFieldView(_placeFor);
        }
        
        
        public void StopGenerator()
        {
            foreach (var pointModel in _pointModels)
            {
                pointModel.PointView.DestroyOn();
            }
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
                pointModel.PointView.DestroyOn();
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

        public List<PointModel> GetPointModels()
        {
            return _pointModels;
        }
        
        private FieldView LoadFieldView(Transform placeFor)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePathFieldView);
            GameObject objectView = Object.Instantiate(prefab, placeFor, false);
            AddGameObject(objectView);

            return objectView.GetComponent<FieldView>();
        }
        
        private void WorldGenerator()
        {
            
            foreach (var point in _fieldView.Points)
            {
                var item =  GerRandomPoint(_random);
                
                PointModel pointModel = new PointModel();
                pointModel.PointView = LoadPointView(point, _resourcePathsPoint[item]);
                var item1 = RandomItem(_random,100);
                var item2 = RandomItem(_random,10);
                var composition = item1 * item2;

                if (GerRandomPoint(_random) == 0)
                {
                    (composition, item1) = (item1, composition);
                }
                else
                {
                    (composition, item2) = (item2, composition);
                }
                
                pointModel.ItemModels =
                    new ItemModel(LoadItemView(pointModel.PointView.Point1, item1), 
                        LoadItemView(pointModel.PointView.Point2, item2), 
                        TypeGameEnum.Division);
                _pointModels.Add(pointModel);
            }
        }
        
        private PointView LoadPointView(Transform placeFor, ResourcePath resourcePath)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeFor, false);
            AddGameObject(objectView);

            return objectView.GetComponent<PointView>();
        }
        
        private ItemView LoadItemView(Transform placeFor, int point)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePathItemView);
            GameObject objectView = Object.Instantiate(prefab, placeFor, false);
            AddGameObject(objectView);

            var item = objectView.GetComponent<ItemView>();
            item.SetValue(point);

            return item;
        }
    }
}