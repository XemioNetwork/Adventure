﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Entities;

namespace Xemio.Adventure.Worlds.Entities.Components
{
    public class PropertyComponent : EntityComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyComponent"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="properties">The properties.</param>
        public PropertyComponent(Entity entity, ObjectStorage properties) : base(entity)
        {
            this.Properties = properties;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the properties.
        /// </summary>
        public ObjectStorage Properties { get; private set; }
        #endregion
    }
}
