using System;
using System.Collections.Generic;
using _Root.Scripts.Enums;
using _Root.Scripts.Interfaces;
using _Root.Scripts.Models;
using _Root.Scripts.View;
using Controllers;
using MB;
using Profile;
using Vector2 = UnityEngine.Vector2;
using UnityEngine;

namespace _Root.Scripts.Controllers
{
    public class GameController : BaseController
    {
        private readonly ProfilePlayers _profilePlayer;
        private IWorldGenerator _worldGenerator;
        private readonly SwipeDetection _swipeDetection;

        private List<PointModel> _pointModels;
        private System.Random _random;

        public int _victoryCondition;

        public event Action<int> VictoryAction;
        public event Action<int> DefeatAction;

        public GameController(ProfilePlayers profilePlayer, IWorldGenerator worldGenerator, SwipeDetection swipeDetection)
        {
            _profilePlayer = profilePlayer;
            _pointModels = worldGenerator.GetPointModels();
            _swipeDetection = swipeDetection;
            worldGenerator.RestartAction += Restart;
            _random = new System.Random();
            _victoryCondition = _pointModels[GetVictoryCondition()]._itemModels.Сomposition;
            Debug.Log(_victoryCondition);
            _swipeDetection.SwipeEvevt += OnSwipe;
        }

        private void Restart(List<PointModel> pointModels)
        {
            _pointModels = pointModels;
            _victoryCondition = _pointModels[GetVictoryCondition()]._itemModels.Сomposition;
        }

        private int GetVictoryCondition()
        {
            var victoryCondition = _random.Next(0, 100);
            if (victoryCondition < 25)
                return 0;
            if (victoryCondition >= 25 && victoryCondition < 50)
                return 1;
            if (victoryCondition >= 50 && victoryCondition < 75)
                return 2;
            return 3;
        }
        
        private void OnSwipe(Vector2 direction)
        {
            Vector2 clickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(clickPoint, Vector2.zero, LayerMask.GetMask("Field"));
            if (hit.collider == null) return;
            var pointView = hit.collider.gameObject.GetComponent<PointView>();
            if (pointView == null) return;
            var pointModel = _pointModels.Find(x => x._pointView == pointView);

            foreach (var point in _pointModels)
            {
                if (point._itemModels.ItemView2.gameObject.activeSelf == false ||
                    point._itemModels.ItemView1.gameObject.activeSelf == false)
                {
                    return;
                }
            }
            
            if (pointModel._pointView.FieldEnum == FieldEnum.Vertical)
            {
                if (direction == Vector2.up)
                {
                    pointModel._pointView.Point2.position = Vector2.Lerp(pointModel._pointView.Point2.position,
                        new Vector2(pointModel._pointView.Point2.position.x,
                            pointModel._pointView.Point2.position.y + 1), Time.time);
                    
                    pointModel._itemModels.ItemView2.SetValue(pointModel._itemModels.Сomposition);
                    pointModel._itemModels.ItemView1.gameObject.SetActive(false);
                    Check(pointModel._itemModels.Сomposition);
                }                
                if (direction == Vector2.down)
                {
                    pointModel._pointView.Point1.position = Vector2.Lerp(pointModel._pointView.Point1.position,
                        new Vector2(pointModel._pointView.Point1.position.x,
                            pointModel._pointView.Point1.position.y - 1), Time.time);
                    
                    pointModel._itemModels.ItemView1.SetValue(pointModel._itemModels.Сomposition);
                    pointModel._itemModels.ItemView2.gameObject.SetActive(false);
                    Check(pointModel._itemModels.Сomposition);
                }
            }
            if (pointModel._pointView.FieldEnum == FieldEnum.Horizontal)
            {

                if (direction == Vector2.left)
                {
                    pointModel._pointView.Point2.position = Vector2.Lerp(pointModel._pointView.Point2.position,
                        new Vector2(pointModel._pointView.Point2.position.x - 1,
                            pointModel._pointView.Point2.position.y), Time.time);
                    
                    pointModel._itemModels.ItemView2.SetValue(pointModel._itemModels.Сomposition);
                    pointModel._itemModels.ItemView1.gameObject.SetActive(false);
                    Check(pointModel._itemModels.Сomposition);
                }
                if (direction == Vector2.right)
                {
                    pointModel._pointView.Point1.position = Vector2.Lerp(pointModel._pointView.Point1.position,
                        new Vector2(pointModel._pointView.Point1.position.x +1 ,
                            pointModel._pointView.Point1.position.y), Time.time);
                    
                    pointModel._itemModels.ItemView1.SetValue(pointModel._itemModels.Сomposition);
                    pointModel._itemModels.ItemView2.gameObject.SetActive(false);
                    Check(pointModel._itemModels.Сomposition);
                }
            }
        }

        private void Check(int response)
        {
            if (response == _victoryCondition)
                VictoryAction?.Invoke(_victoryCondition);
            else
                DefeatAction?.Invoke(_victoryCondition);
        }
    }
}