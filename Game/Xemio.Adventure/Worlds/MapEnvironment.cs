using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.Adventure.Worlds.Entities;
using Xemio.Adventure.Worlds.Entities.Components;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Math.Collision.Entities;

namespace Xemio.Adventure.Worlds
{
    public class MapEnvironment : CollisionEnvironment
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MapEnvironment"/> class.
        /// </summary>
        /// <param name="map">The map.</param>
        public MapEnvironment(Map map)
            : base(
                map.Bounds.Width * map.Bounds.TileHeight / 4,
                map.Bounds.Height * map.Bounds.TileHeight / 4,
                4)
        {
            this.Map = map;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the map.
        /// </summary>
        public Map Map { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void Add(Entity entity)
        {
            var component = entity.GetComponent<PropertyComponent>();

            if (component.Properties.Retrieve<bool>("Obsolete"))
            {
                this.Factory.CreateId();
                return;
            }

            base.Add(entity);
        }
        /// <summary>
        /// Sorts the entity collection.
        /// </summary>
        protected override IEnumerable<Entity> SortedEntityCollection()
        {
            return this.Entities.OrderBy(
                entity => entity.Position.Y + (entity.GetComponent<AnimationComponent>() != null
                              ? entity.GetComponent<AnimationComponent>().Current.Frame.Height
                              : 0));
        }
        #endregion
    }
}
