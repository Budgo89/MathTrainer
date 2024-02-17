using _Root.Scripts.Enums;
using _Root.Scripts.View;

namespace _Root.Scripts.Models
{
    public class PointModel
    {
        public PointView PointView;
        public ItemModel ItemModels;
        public bool IsHard = false;
        public MoveOption MoveOption = MoveOption.Non;
    }
}