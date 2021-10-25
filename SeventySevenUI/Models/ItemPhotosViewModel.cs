using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeventySevenUI.Models
{
    public class ItemPhotosViewModel
    {
        public int Id { get; set; }
        public Nullable<int> ItemId { get; set; }
        public int TypeId { get; set; }
        public string FileName { get; set; }
        public Nullable<int> Position { get; set; }
        public string Alt { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public Nullable<System.DateTime> ModifiedAt { get; set; }
        public bool IsActive { get; set; }

        public string Shape { get; set; }

        public string Metal { get; set; }

    }

}