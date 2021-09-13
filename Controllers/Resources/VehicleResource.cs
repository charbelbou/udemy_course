using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using udemy.Controllers.Resources;

namespace udemy_course.Controllers.Resources
{
    public class VehicleResource
    {
        // Vehicle Id
        public int Id { get; set; }

        // ModelResource object
        public KeyValuePairResource Model { get; set; }
        // MakeResource object
        public KeyValuePairResource Make { get; set; }
        // Whether it's registered or not
        public bool isRegistered { get; set; }

        // ContactResource
        public ContactResource Contact { get; set; }

        // DateTime for when it was last updated
        public DateTime LastUpdate { get; set; }

        // Collection of features
        public ICollection<KeyValuePairResource> Features { get; set; }
        
        // Constructor instantializing the Features collection
        public VehicleResource()
        {
            Features = new Collection<KeyValuePairResource>();
        }
    }
}