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
    public class WorldGeneratorDivisionHardController: BaseController, IWorldGenerator
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
        
        public WorldGeneratorDivisionHardController(Transform placeFor)
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
                var composition1 = 0;
                var composition2 = 0;
                var composition = 0;
                
                var item1 = RandomItem(_random,10);
                var item2 = RandomItem(_random,10);
                var item3 = RandomItem(_random,10);
                var item4 = RandomItem(_random,10);

                bool flag = false;

                /// итем 1 и 2
                if (GerRandomPoint(_random) == 0)
                {
                    composition1 = item1 * item2;
                    composition2 = item3 * item4;
                    flag = false;
                }
                else
                {
                    composition1 = item1 * item3;
                    composition2 = item2 * item4;
                    flag = true;
                }
                
                composition = composition1 * composition2;


                if (GerRandomPoint(_random) == 0)
                {
                    (composition, composition1) = (composition1, composition);
                }
                else
                {
                    (composition, composition2) = (composition2, composition);
                }

                if (flag == false)
                {
                    if (GerRandomPoint(_random) == 0)
                    {
                        (composition1, item1) = (item1, composition1);
                        (composition2, item3) = (item3, composition2);
                    }
                    else
                    {
                        (composition1, item2) = (item2, composition1);
                        (composition2, item4) = (item4, composition2);
                    }
                }
                else
                {
                    if (GerRandomPoint(_random) == 0)
                    {
                        (composition1, item1) = (item1, composition1);
                        (composition2, item2) = (item2, composition2);
                    }
                    else
                    {
                        (composition1, item3) = (item3, composition1);
                        (composition2, item4) = (item4, composition2);
                    }
                }
                
                PointModel pointModel = new PointModel();
                pointModel.PointView = LoadPointView(point, _resourcePathPointView);
                pointModel.ItemModels =
                    new ItemModel(LoadItemView(pointModel.PointView.Point1, item1), 
                        LoadItemView(pointModel.PointView.Point2, item2), 
                        LoadItemView(pointModel.PointView.Point3, item3), 
                        LoadItemView(pointModel.PointView.Point4, item4), 
                        TypeGameEnum.Division, flag);
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
        
        private ItemView LoadItemView(Transform placeFor, int itemValue)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePathItemView);
            GameObject objectView = Object.Instantiate(prefab, placeFor, false);
            AddGameObject(objectView);

            var item = objectView.GetComponent<ItemView>();
            item.SetValue(itemValue);

            return item;
        }
    }
}