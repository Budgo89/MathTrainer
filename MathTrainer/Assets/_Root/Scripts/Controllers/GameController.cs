﻿using System;
using System.Collections;
using System.Collections.Generic;
using _Root.Scripts.Enums;
using _Root.Scripts.Interfaces;
using _Root.Scripts.Models;
using _Root.Scripts.View;
using Controllers;
using MB;
using Profile;
using Tool;
using Vector2 = UnityEngine.Vector2;
using UnityEngine;

namespace _Root.Scripts.Controllers
{
    public class GameController : BaseController
    {
        private readonly ProfilePlayers _profilePlayer;
        private IWorldGenerator _worldGenerator;
        private readonly SwipeDetection _swipeDetection;
        private readonly GameSettings _gameSettings;
        private readonly AudioModel _audioModel;

        private List<PointModel> _pointModels;
        private System.Random _random;

        private bool _isUp = false;
        private bool _isDown = false;
        private bool _isLeft = false;
        private bool _isRight = false;
        private bool _isHard = false;

        private PointModel _pointModel;
        
        private float time = 0.2f;

        private Coroutine _delay;

        public float _victoryCondition;
        private Coroutine _delayUItemView1;
        private Coroutine _delayUItemView2;
        private Coroutine _delayUItemView3;
        private Coroutine _delayUItemView4;

        private FieldEnum _fieldEnum;
        private Coroutine _swipeLeft;
        private Coroutine _cek;

        public event Action<float> VictoryAction;
        public event Action<float> DefeatAction;

        public GameController(ProfilePlayers profilePlayer, IWorldGenerator worldGenerator,
            SwipeDetection swipeDetection,
            GameSettings gameSettings, AudioModel audioModel)
        {
            _profilePlayer = profilePlayer;
            _pointModels = worldGenerator.GetPointModels();
            _swipeDetection = swipeDetection;
            _gameSettings = gameSettings;
            _audioModel = audioModel;
            worldGenerator.RestartAction += Restart;
            _random = new System.Random();
            _victoryCondition = _pointModels[GetVictoryCondition()].ItemModels.Сomposition;
            Debug.Log(_victoryCondition);
            _swipeDetection.SwipeEvevt += OnSwipe;
        }

        public void Update()
        {
            if (_pointModel == null)
            {
                return;
            }
            
            if (_isDown)
            {
                if (_isHard)
                {
                    if (_pointModel.MoveOption == MoveOption.Non)
                    {
                        _pointModel.PointView.Point1.position = Vector2.Lerp(_pointModel.PointView.Point1.position,
                            _pointModel.PointView.Point3.position, time);
                    
                        _pointModel.PointView.Point2.position = Vector2.Lerp(_pointModel.PointView.Point2.position,
                            _pointModel.PointView.Point4.position, time); 
                    }
                    if (_pointModel.MoveOption == MoveOption.VerticalRight)
                    {
                        _pointModel.PointView.Point2.position = Vector2.Lerp(_pointModel.PointView.Point2.position,
                            _pointModel.PointView.Point4.position, time); 
                    }
                    if (_pointModel.MoveOption == MoveOption.VerticalLeft)
                    {
                        _pointModel.PointView.Point1.position = Vector2.Lerp(_pointModel.PointView.Point1.position,
                            _pointModel.PointView.Point3.position, time); 
                    }
                }
                else
                {
                    _pointModel.PointView.Point1.position = Vector2.Lerp(_pointModel.PointView.Point1.position,
                        _pointModel.PointView.Point2.position, time);
                }
            }

            if (_isUp)
            {
                if (_isHard)
                {
                    if (_pointModel.MoveOption == MoveOption.Non)
                    {
                        _pointModel.PointView.Point3.position = Vector2.Lerp(_pointModel.PointView.Point3.position,
                            _pointModel.PointView.Point1.position, time);

                        _pointModel.PointView.Point4.position = Vector2.Lerp(_pointModel.PointView.Point4.position,
                            _pointModel.PointView.Point2.position, time);
                    }

                    if (_pointModel.MoveOption == MoveOption.VerticalRight)
                    {
                        _pointModel.PointView.Point4.position = Vector2.Lerp(_pointModel.PointView.Point4.position,
                            _pointModel.PointView.Point1.position, time); 
                    }
                    if (_pointModel.MoveOption == MoveOption.VerticalLeft)
                    {
                        _pointModel.PointView.Point3.position = Vector2.Lerp(_pointModel.PointView.Point3.position,
                            _pointModel.PointView.Point1.position, time); 
                    }
                }
                else
                {
                    _pointModel.PointView.Point2.position = Vector2.Lerp(_pointModel.PointView.Point2.position,
                        _pointModel.PointView.Point1.position, time);
                }

            }

            if (_isLeft)
            {
                if (_isHard)
                {
                    if (_pointModel.MoveOption == MoveOption.Non)
                    {
                        _pointModel.PointView.Point2.position = Vector2.Lerp(_pointModel.PointView.Point2.position,
                            _pointModel.PointView.Point1.position, time);

                        _pointModel.PointView.Point4.position = Vector2.Lerp(_pointModel.PointView.Point4.position,
                            _pointModel.PointView.Point3.position, time);
                    }
                    if (_pointModel.MoveOption == MoveOption.HorizontalUp)
                    {
                        _pointModel.PointView.Point2.position = Vector2.Lerp(_pointModel.PointView.Point2.position,
                            _pointModel.PointView.Point1.position, time); 
                    }
                    if (_pointModel.MoveOption == MoveOption.HorizontalDown)
                    {
                        _pointModel.PointView.Point4.position = Vector2.Lerp(_pointModel.PointView.Point4.position,
                            _pointModel.PointView.Point3.position, time); 
                    }
                }
                else
                {
                    _pointModel.PointView.Point2.position = Vector2.Lerp(_pointModel.PointView.Point2.position,
                        _pointModel.PointView.Point1.position, time);
                }

            }

            if (_isRight)
            {
                if (_isHard)
                {
                    if (_pointModel.MoveOption == MoveOption.Non)
                    {
                        _pointModel.PointView.Point1.position = Vector2.Lerp(_pointModel.PointView.Point1.position,
                            _pointModel.PointView.Point2.position, time);
                    
                        _pointModel.PointView.Point3.position = Vector2.Lerp(_pointModel.PointView.Point3.position,
                            _pointModel.PointView.Point4.position, time);
                    }
                    if (_pointModel.MoveOption == MoveOption.HorizontalUp)
                    {
                        _pointModel.PointView.Point1.position = Vector2.Lerp(_pointModel.PointView.Point1.position,
                            _pointModel.PointView.Point2.position, time); 
                    }
                    if (_pointModel.MoveOption == MoveOption.HorizontalDown)
                    {
                        _pointModel.PointView.Point3.position = Vector2.Lerp(_pointModel.PointView.Point3.position,
                            _pointModel.PointView.Point4.position, time); 
                    }
                }
                else
                {
                    _pointModel.PointView.Point1.position = Vector2.Lerp(_pointModel.PointView.Point1.position,
                        _pointModel.PointView.Point2.position, time);
                }
            }
        }
        
        private void Restart(List<PointModel> pointModels)
        {
            _pointModels = pointModels;
            _victoryCondition = _pointModels[GetVictoryCondition()].ItemModels.Сomposition;
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
        
        private void AudioPlay()
        {
            _audioModel.AudioEffects.clip = _audioModel.AudioEffectsManager.SwaipClip;
            _audioModel.AudioEffects.Play();
        }
        
        private void OnSwipe(Vector2 direction)
        {
            AudioPlay();
            
            Vector2 clickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(clickPoint, Vector2.zero, LayerMask.GetMask("Field"));
            if (hit.collider == null) return;
            var pointView = hit.collider.gameObject.GetComponent<PointView>();
            if (pointView == null) return;
            _pointModel = _pointModels.Find(x => x.PointView == pointView);

            
            if (!_pointModel.PointView.IsWrite)
            {
                _fieldEnum = _pointModel.PointView.FieldEnum;
                _pointModel.PointView.SetIsWrite();
            }

            if (_pointModel.IsHard == false)
            {
                if (_pointModel.ItemModels.ItemView2.gameObject.activeSelf == false ||
                    _pointModel.ItemModels.ItemView1.gameObject.activeSelf == false)
                {
                    return;
                }
            }

            if (_pointModel.PointView.FieldEnum == FieldEnum.Vertical)
            {
                if (direction == Vector2.up)
                {
                    _isUp = true;
                    _delayUItemView1 = CoroutineController.StartRoutine(DelayItemView1());
                    
                    var composition = AssignmentResult(_pointModel.ItemModels.ItemView2, _pointModel.ItemModels.ItemView1);
                    
                    Check(composition);
                }

                if (direction == Vector2.down)
                {
                    _isDown = true;
                    
                    _delayUItemView2 = CoroutineController.StartRoutine(DelayItemView2());
                    
                    var composition = AssignmentResult(_pointModel.ItemModels.ItemView1, _pointModel.ItemModels.ItemView2);
                    
                    Check(composition);
                }

                _fieldEnum = FieldEnum.Vertical;
            }

            if (_pointModel.PointView.FieldEnum == FieldEnum.Horizontal)
            {

                if (direction == Vector2.left)
                {
                    _isLeft = true;
                    _delayUItemView1 = CoroutineController.StartRoutine(DelayItemView1());
                    
                    var composition = AssignmentResult(_pointModel.ItemModels.ItemView2, _pointModel.ItemModels.ItemView1);
                    
                    Check(composition);
                }

                if (direction == Vector2.right)
                {
                    _isRight = true;
                    _delayUItemView2 = CoroutineController.StartRoutine(DelayItemView2());
                    
                    var composition = AssignmentResult(_pointModel.ItemModels.ItemView1, _pointModel.ItemModels.ItemView2);
                    
                    Check(composition);
                }
                _fieldEnum = FieldEnum.Horizontal;
            }

            if (_pointModel.PointView.FieldEnum == FieldEnum.Hard && _pointModel.IsHard)
            {
                int count = 0;

                if (_pointModel.ItemModels.ItemView1.gameObject.activeSelf == false)
                    count++;
                if (_pointModel.ItemModels.ItemView2.gameObject.activeSelf == false)
                    count++;
                if (_pointModel.ItemModels.ItemView3.gameObject.activeSelf == false)
                    count++;
                if (_pointModel.ItemModels.ItemView4.gameObject.activeSelf == false)
                    count++;

                if (count >= 3)
                {
                    return;
                }

            }
            
            if (_pointModel.PointView.FieldEnum == FieldEnum.Hard)
            {
                if (!_pointModel.IsHard)
                {
                    SwipeHardFirstMove(direction);
                    _pointModel.IsHard = true;
                }
                else
                {
                    SwipeHardSecondMove(direction);
                }

            }

            _delay = CoroutineController.StartRoutine(Delay());

        }

        private void SwipeHardSecondMove(Vector2 direction)
        {
            if (direction == Vector2.left)
            {
                if (_pointModel.MoveOption == MoveOption.HorizontalUp)
                {
                    _isLeft = true;
                    _isHard = true;
                    
                    _delayUItemView1 = CoroutineController.StartRoutine(DelayItemView1());
                    
                    var composition = AssignmentResult(_pointModel.ItemModels.ItemView2, _pointModel.ItemModels.ItemView1);
                    
                    Check(composition);
                }

                if (_pointModel.MoveOption == MoveOption.HorizontalDown)
                {
                    _isLeft = true;
                    _isHard = true;
                    _delayUItemView3 = CoroutineController.StartRoutine(DelayItemView3());
                    var composition = AssignmentResult(_pointModel.ItemModels.ItemView4, _pointModel.ItemModels.ItemView3);
                    
                    Check(composition);
                }
            }

            if (direction == Vector2.right)
            {
                if (_pointModel.MoveOption == MoveOption.HorizontalUp)
                {
                    _isRight = true;
                    _isHard = true;
                    
                    _delayUItemView2 = CoroutineController.StartRoutine(DelayItemView2());
                    
                    var composition = AssignmentResult(_pointModel.ItemModels.ItemView1, _pointModel.ItemModels.ItemView2);
                    
                    Check(composition);
                }
                if (_pointModel.MoveOption == MoveOption.HorizontalDown)
                {
                    _isRight = true;
                    _isHard = true;
                    _delayUItemView4 = CoroutineController.StartRoutine(DelayItemView4());
                    var composition = AssignmentResult(_pointModel.ItemModels.ItemView3, _pointModel.ItemModels.ItemView4);
                    
                    Check(composition);
                }
            }

            if (direction == Vector2.up)
            {
                if (_pointModel.MoveOption == MoveOption.VerticalLeft)
                {
                    _isUp = true;
                    _isHard = true;
                    
                    _delayUItemView3 = CoroutineController.StartRoutine(DelayItemView3());
                    
                    var composition = AssignmentResult(_pointModel.ItemModels.ItemView1, _pointModel.ItemModels.ItemView3);
                    
                    Check(composition);
                }
                if (_pointModel.MoveOption == MoveOption.VerticalRight)
                {
                    _isUp = true;
                    _isHard = true;
                    
                    _delayUItemView4 = CoroutineController.StartRoutine(DelayItemView4());
                    
                    var composition = AssignmentResult(_pointModel.ItemModels.ItemView2, _pointModel.ItemModels.ItemView4);
                    
                    Check(composition);
                }
            }
            
            if (direction == Vector2.down)
            {
                if (_pointModel.MoveOption == MoveOption.VerticalLeft)
                {
                    _isDown = true;
                    _isHard = true;
                    
                    _delayUItemView3 = CoroutineController.StartRoutine(DelayItemView3());
                    
                    var composition = AssignmentResult(_pointModel.ItemModels.ItemView1, _pointModel.ItemModels.ItemView3);
                    
                    Check(composition);
                }
                if (_pointModel.MoveOption == MoveOption.VerticalRight)
                {
                    _isDown = true;
                    _isHard = true;
                    
                    _delayUItemView4 = CoroutineController.StartRoutine(DelayItemView4());
                    
                    var composition = AssignmentResult(_pointModel.ItemModels.ItemView2, _pointModel.ItemModels.ItemView4);
                    
                    Check(composition);
                }
            }
        }

        private void SwipeHardFirstMove(Vector2 direction)
        {
            if (direction == Vector2.left)
            {
                _isLeft = true;
                _isHard = true;
                
                _delayUItemView2 = CoroutineController.StartRoutine(DelayItemView2());
                _delayUItemView4 = CoroutineController.StartRoutine(DelayItemView4());

                AssignmentResult(_pointModel.ItemModels.ItemView1, _pointModel.ItemModels.ItemView2);
                AssignmentResult(_pointModel.ItemModels.ItemView3, _pointModel.ItemModels.ItemView4);
                _cek = CoroutineController.StartRoutine(Cek(_pointModel, MoveOption.VerticalLeft));
            }

            if (direction == Vector2.right)
            {
                _isRight = true;
                _isHard = true;

                _delayUItemView1 = CoroutineController.StartRoutine(DelayItemView1());
                _delayUItemView3 = CoroutineController.StartRoutine(DelayItemView3());

                AssignmentResult(_pointModel.ItemModels.ItemView2, _pointModel.ItemModels.ItemView1);
                AssignmentResult(_pointModel.ItemModels.ItemView4, _pointModel.ItemModels.ItemView3);
                _cek = CoroutineController.StartRoutine(Cek(_pointModel, MoveOption.VerticalRight));
            }

            if (direction == Vector2.up)
            {
                _isUp = true;
                _isHard = true;

                _delayUItemView3 = CoroutineController.StartRoutine(DelayItemView3());
                _delayUItemView4 = CoroutineController.StartRoutine(DelayItemView4());

                AssignmentResult(_pointModel.ItemModels.ItemView1, _pointModel.ItemModels.ItemView3);
                AssignmentResult(_pointModel.ItemModels.ItemView2, _pointModel.ItemModels.ItemView4);
                _cek = CoroutineController.StartRoutine(Cek(_pointModel, MoveOption.HorizontalUp));
            }

            if (direction == Vector2.down)
            {
                _isDown = true;
                _isHard = true;

                _delayUItemView1 = CoroutineController.StartRoutine(DelayItemView1());
                _delayUItemView2 = CoroutineController.StartRoutine(DelayItemView2());

                AssignmentResult(_pointModel.ItemModels.ItemView3, _pointModel.ItemModels.ItemView1);
                AssignmentResult(_pointModel.ItemModels.ItemView4, _pointModel.ItemModels.ItemView2);
                _cek = CoroutineController.StartRoutine(Cek(_pointModel, MoveOption.HorizontalDown));
            }

        }

        private float AssignmentResult(ItemView pointModel1, ItemView pointModel2)
        {
            var item1 = pointModel1.GetValue();
            var item2 = pointModel2.GetValue();
            var result1 = GetComposition(item1, item2);

            pointModel1.SetValue(result1);
            pointModel2.SetValue(0);
            return GetComposition(item2, item1);
        }

        private float GetComposition(float item1, float item2)
        {
            if (_gameSettings.TypeGameEnum == TypeGameEnum.Multiplication)
                return item1 * item2;
            if (_gameSettings.TypeGameEnum == TypeGameEnum.Addition)
                return item1 + item2;
            if (_gameSettings.TypeGameEnum == TypeGameEnum.Division)
                return item2 / item1;
            return (item2 - item1);
        }

        private void Check(float response)
        {
            if (response == _victoryCondition)
                VictoryAction?.Invoke(_victoryCondition);
            else
                DefeatAction?.Invoke(_victoryCondition);
        }

        protected override void OnDispose()
        {
            _swipeDetection.SwipeEvevt -= OnSwipe;
        }
        
        private IEnumerator Delay()
        {
            yield return new WaitForSeconds(0.9f);
            _isUp = false;
            _isDown = false;
            _isLeft = false;
            _isRight = false;
            _isHard = false;
            CoroutineController.StopRoutine(_delay);
        }

        private IEnumerator DelayItemView1()
        {
            yield return new WaitForSeconds(0.2f);

            _pointModel.ItemModels.ItemView1.gameObject.SetActive(false);
            CoroutineController.StopRoutine(_delayUItemView1);
        }

        private IEnumerator DelayItemView2()
        {
            yield return new WaitForSeconds(0.2f);
            _pointModel.ItemModels.ItemView2.gameObject.SetActive(false);
            CoroutineController.StopRoutine(_delayUItemView2);
        }        
        
        private IEnumerator DelayItemView3()
        {
            yield return new WaitForSeconds(0.2f);
            _pointModel.ItemModels.ItemView3.gameObject.SetActive(false);
            CoroutineController.StopRoutine(_delayUItemView3);
        }       
        
        private IEnumerator DelayItemView4()
        {
            yield return new WaitForSeconds(0.2f);
            _pointModel.ItemModels.ItemView4.gameObject.SetActive(false);
            CoroutineController.StopRoutine(_delayUItemView4);
        }

        private IEnumerator Cek(PointModel pointModel, MoveOption moveOption)
        {
            yield return new WaitForSeconds(0.9f);
            pointModel.MoveOption = moveOption;
            //CoroutineController.StopRoutine(_cek);
        }
    }
}