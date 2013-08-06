using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.Adventure.Worlds.Events;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Sprites;

namespace Xemio.Adventure.Worlds.Entities.Components
{
    public class AnimationComponent : EntityComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AnimationComponent"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="animations">The animations.</param>
        public AnimationComponent(Entity entity, IEnumerable<Animation> animations) : base(entity)
        {
            this.Animations = new List<AnimationInstance>();
            this.Animations.AddRange(animations.Select(a => a.CreateInstance()));
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the current.
        /// </summary>
        public AnimationInstance Current { get; private set; }
        /// <summary>
        /// Gets the animation.
        /// </summary>
        public List<AnimationInstance> Animations { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Changes the animation.
        /// </summary>
        /// <param name="animationName">Name of the animation.</param>
        public void PlayAnimation(string animationName)
        {
            var animation = this.Animations.FirstOrDefault(
                a => a.Animation.Name == animationName);

            if (animation == null)
            {
                var eventManager = XGL.Components.Get<EventManager>();
                eventManager.Publish(new AnimationNotFoundLoggingEvent(animationName));

                return;
            }

            if (this.Current == null || this.Current.Animation.Name != animationName)
            {
                this.Current = animation;
            }
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            if (this.Current != null)
                this.Current.Tick(elapsed);
        }
        #endregion
    }
}
