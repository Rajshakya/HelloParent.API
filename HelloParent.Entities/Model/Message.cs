using System;
using System.Collections.Generic;
using System.Text;

namespace HelloParent.Entities.Model
{
   public class Message
    {
    }
    public class Attachement
    {
        public string Url { get; set; }
        public string Name { get; set; }

        public Dimensions Dimensions { get; set; }

        public Attachement Clone()
        {
            return new Attachement
            {
                Url = Url,
                Name = Name,
                Dimensions = Dimensions
            };
        }
    }

    public class Dimensions
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
