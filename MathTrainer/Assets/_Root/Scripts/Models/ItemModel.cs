using _Root.Scripts.Enums;
using _Root.Scripts.View;

namespace _Root.Scripts.Models
{
    public class ItemModel
    {
        public ItemView ItemView1;
        public ItemView ItemView2;
        public int Сomposition;

        public ItemModel(ItemView itemView1, ItemView itemView2, TypeGameEnum typeGameEnum)
        {
            ItemView1 = itemView1;
            ItemView2 = itemView2;
            if (typeGameEnum == TypeGameEnum.Multiplication)
                Сomposition = ItemView1.GetValue() * ItemView2.GetValue();
            
            if (typeGameEnum == TypeGameEnum.Addition)
                Сomposition = ItemView1.GetValue() + ItemView2.GetValue();
            
            if (typeGameEnum == TypeGameEnum.Division)
            {
                if (ItemView1.GetValue() >= ItemView2.GetValue())
                    Сomposition = ItemView1.GetValue() / ItemView2.GetValue();
                else
                    Сomposition = ItemView2.GetValue() / ItemView1.GetValue();
            }

            if (typeGameEnum == TypeGameEnum.Subtraction)
            {
                if (ItemView1.GetValue() >= ItemView2.GetValue())
                    Сomposition = ItemView1.GetValue() - ItemView2.GetValue();
                else
                    Сomposition = ItemView2.GetValue() - ItemView1.GetValue();
            }
        }
    }
}