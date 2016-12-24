using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Classes
{
    public class AnimalCollection : Collection<Animal>
    {
        Zoo zoo;

        public AnimalCollection(Zoo zoo) { this.zoo = zoo; }

        protected override void InsertItem(int index, Animal item)
        {
            base.InsertItem(index, item);
            item.Zoo = zoo;
        }

        protected override void SetItem(int index, Animal item)
        {
            base.SetItem(index, item);
            item.Zoo = zoo;
        }

        protected override void RemoveItem(int index)
        {
            this[index].Zoo = null;
            base.RemoveItem(index);
        }

        protected override void ClearItems()
        {
            foreach(var animal in this)
            {
                animal.Zoo = null;
            }

            base.ClearItems();
        }
    }
}
