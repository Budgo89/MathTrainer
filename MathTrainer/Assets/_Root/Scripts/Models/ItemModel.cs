using _Root.Scripts.View;

namespace _Root.Scripts.Models
{
    public class ItemModel
    {
        public ItemView ItemView1;
        public ItemView ItemView2;
        public int Сomposition;

        public ItemModel(ItemView itemView1, ItemView itemView2)
        {
            ItemView1 = itemView1;
            ItemView2 = itemView2;
            Сomposition = ItemView1.GetValue() * ItemView2.GetValue();
        }
    }
}