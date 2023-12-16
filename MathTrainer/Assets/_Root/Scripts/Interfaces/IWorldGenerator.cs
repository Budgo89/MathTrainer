using System;
using System.Collections.Generic;
using _Root.Scripts.Models;

namespace _Root.Scripts.Interfaces
{
    public interface IWorldGenerator
    {
        public void StartGenerator();
        public void RestartGenerator();
        public List<PointModel> GetPointModels();
        public event Action<List<PointModel>> RestartAction;
        public void Dispose();
    }
}