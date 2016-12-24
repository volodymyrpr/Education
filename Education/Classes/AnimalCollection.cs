using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Classes
{
    public class AnimalCollection : KeyedCollection<string, Animal>
    {
        Zoo zoo;

        public AnimalCollection(Zoo zoo)
        {
            this.zoo = zoo;
        }

        internal void NotifyNameChange (Animal animal, string newName)
        {
            ChangeItemKey(animal, newName);
        }

        protected override string GetKeyForItem(Animal item)
        {
            return item.Name;
        }

        protected override void ClearItems()
        {
            foreach (var item in this)
            {
                item.Zoo = null;
            }

            base.ClearItems();
        }

        protected override void InsertItem(int index, Animal item)
        {
            item.Zoo = this.zoo;
            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            this[index].Zoo = null;
            base.RemoveItem(index);
        }

        protected override void SetItem(int index, Animal item)
        {
            item.Zoo = this.zoo;
            base.SetItem(index, item);
        }
    }
}
