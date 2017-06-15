using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myPhotoEditor.Objects
{
    public class ItemsList : List<Item>
    {
        public new void Add(Item item)
        {
            base.Add(item);
        }
    }
}
