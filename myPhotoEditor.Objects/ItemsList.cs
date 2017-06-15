using System.Collections.Generic;

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
