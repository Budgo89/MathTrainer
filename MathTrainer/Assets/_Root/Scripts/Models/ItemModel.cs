using System;
using _Root.Scripts.Enums;
using _Root.Scripts.View;

namespace _Root.Scripts.Models
{
    public class ItemModel
    {
        public ItemView ItemView1;
        public ItemView ItemView2;
        public ItemView ItemView3;
        public ItemView ItemView4;
        public float Сomposition;

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
        public ItemModel(ItemView itemView1, ItemView itemView2, ItemView itemView3, ItemView itemView4, TypeGameEnum typeGameEnum, bool flag = false)
        {
            ItemView1 = itemView1;
            ItemView2 = itemView2;
            ItemView3 = itemView3;
            ItemView4 = itemView4;

            Random random = new Random();
            
            if (typeGameEnum == TypeGameEnum.Multiplication)
            {
                if (RandomPosition(random) == 0)
                {
                    var result1 = ItemView1.GetValue() * ItemView2.GetValue();
                    var result2 = ItemView3.GetValue() * ItemView4.GetValue();
                    Сomposition = result1 * result2;
                }
                else
                {
                    var result1 = ItemView1.GetValue() * ItemView3.GetValue();
                    var result2 = ItemView2.GetValue() * ItemView4.GetValue();
                    Сomposition = result1 * result2;
                }
            }
            
            if (typeGameEnum == TypeGameEnum.Addition)
                if (RandomPosition(random) == 0)
                {
                    var result1 = ItemView1.GetValue() + ItemView2.GetValue();
                    var result2 = ItemView3.GetValue() + ItemView4.GetValue();
                    Сomposition = result1 + result2;
                }
                else
                {
                    var result1 = ItemView1.GetValue() + ItemView3.GetValue();
                    var result2 = ItemView2.GetValue() + ItemView4.GetValue();
                    Сomposition = result1 + result2;
                }
            
            if (typeGameEnum == TypeGameEnum.Division)
            {
                float result1 = 0;
                float result2 = 0;

                if (flag == false)
                {
                    if (ItemView1.GetValue() >= ItemView2.GetValue())
                    {
                        result1 = ItemView1.GetValue() / ItemView2.GetValue();
                    }
                    else
                    {
                        result1 = ItemView2.GetValue() / ItemView1.GetValue();
                    }
                    if (ItemView3.GetValue() >= ItemView4.GetValue())
                    {
                        result2 = ItemView3.GetValue() / ItemView4.GetValue();
                    }
                    else
                    {
                        result2 = ItemView4.GetValue() / ItemView3.GetValue();
                    }
                }
                else
                {
                    if (ItemView1.GetValue() >= ItemView3.GetValue())
                    {
                        result1 = ItemView1.GetValue() / ItemView3.GetValue();
                    }
                    else
                    {
                        result1 = ItemView3.GetValue() / ItemView1.GetValue();
                    }

                    if (ItemView2.GetValue() >= ItemView4.GetValue())
                    {
                        result2 = ItemView2.GetValue() / ItemView4.GetValue();
                    }
                    else
                    {
                        result2 = ItemView4.GetValue() / ItemView2.GetValue();
                    }
                }
                if (result1 >= result2)
                    Сomposition = result1 / result2;
                else
                    Сomposition = result2 / result1;
            }

            if (typeGameEnum == TypeGameEnum.Subtraction)
            {
                float result1 = 0;
                float result2 = 0;
                
                if (RandomPosition(random) == 0)
                {
                    if (ItemView1.GetValue() >= ItemView2.GetValue())
                    {
                        result1 = ItemView1.GetValue() - ItemView2.GetValue();
                    }
                    else
                    {
                        result1 = ItemView2.GetValue() - ItemView1.GetValue();
                    }                    
                    if (ItemView3.GetValue() >= ItemView4.GetValue())
                    {
                        result2 = ItemView3.GetValue() - ItemView4.GetValue();
                    }
                    else
                    {
                        result2 = ItemView4.GetValue() - ItemView3.GetValue();
                    }

                    if (result1 >= result2)
                        Сomposition = result1 - result2;
                    else
                        Сomposition = result2 - result1;
                }
                else
                {
                    if (ItemView1.GetValue() >= ItemView3.GetValue())
                    {
                        result1 = ItemView1.GetValue() - ItemView3.GetValue();
                    }
                    else
                    {
                        result1 = ItemView3.GetValue() - ItemView1.GetValue();
                    }                    
                    if (ItemView2.GetValue() >= ItemView4.GetValue())
                    {
                        result2 = ItemView2.GetValue() - ItemView4.GetValue();
                    }
                    else
                    {
                        result2 = ItemView4.GetValue() - ItemView2.GetValue();
                    }

                    if (result1 >= result2)
                        Сomposition = result1 - result2;
                    else
                        Сomposition = result2 - result1;
                }
                
            }
        }

        private int RandomPosition(Random random)
        {
            var item = random.Next(0, 10);
            if (item % 2 == 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}