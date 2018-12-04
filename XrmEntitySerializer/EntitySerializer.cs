﻿using Microsoft.Xrm.Sdk;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace XrmEntitySerializer
{
    /// <summary>
    /// This class inherits from <see cref="JsonSerializer"/> and provides constructors with the necessary converters to convert an <see cref="Entity"/>
    /// </summary>
    public class EntitySerializer : JsonSerializer
    {
        /// <summary>
        /// <see cref="JsonSerializer"/> is initialized with the necessary converters
        /// </summary>
        public EntitySerializer() : base()
        {
            TypeNameHandling = TypeNameHandling.Objects;
            Converters.Add(new GuidConverter());
            Converters.Add(new AttributeCollectionConverter());
            Converters.Add(new FormattedValueCollectionConverter());
            Converters.Add(new RelatedEntityCollectionConverter());
#if !XRM_7 && !XRM_6 && !XRM_5
            Converters.Add(new KeyAttributeCollectionConverter());
#endif
        }

        /// <summary>
        /// <see cref="JsonSerializer"/> is initialized with the specified converters only adding the default necessary converters if not specified in the list. 
        /// </summary>
        /// <param name="converters">List of converters to initialize the JsonSerializer with</param>
        public EntitySerializer(IEnumerable<JsonConverter> converters) : base()
        {
            if (converters == null) throw new ArgumentNullException("converters");
            TypeNameHandling = TypeNameHandling.Objects;
            foreach (JsonConverter converter in converters)
            {
                Converters.Add(converter);
            }

            if (!converters.Any(x => x.CanConvert(typeof(Guid))))
            {
                Converters.Add(new GuidConverter());
            }
            if (!converters.Any(x => x.CanConvert(typeof(AttributeCollection))))
            {
                Converters.Add(new AttributeCollectionConverter());
            }
            if (!converters.Any(x => x.CanConvert(typeof(FormattedValueCollection))))
            {
                Converters.Add(new FormattedValueCollectionConverter());
            }
            if (!converters.Any(x => x.CanConvert(typeof(RelatedEntityCollection))))
            {
                Converters.Add(new RelatedEntityCollectionConverter());
            }
#if !XRM_7 && !XRM_6 && !XRM_5
            if (!converters.Any(x => x.CanConvert(typeof(KeyAttributeCollection))))
            {
                Converters.Add(new KeyAttributeCollectionConverter());
            }
#endif        
        }
    }
}