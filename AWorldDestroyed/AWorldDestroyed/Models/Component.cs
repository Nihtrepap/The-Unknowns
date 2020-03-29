// =============================================
//         Editor:     Lone Maaherra
//         Last edit:  2020-03-19 
// _-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_
//
//       (\                 >+{{{o)> - kvaouk
//    >+{{{{{0)> - kraouk      LL  
//       /_\_
//
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//                <333333><                     
//         <3333333><           <33333>< 

namespace AWorldDestroyed.Models
{
    /// <summary>
    /// Base class for all components.
    /// Holds a reference to the SceneObject a component subclass should act upon.
    /// </summary>
    public abstract class Component : BaseObject
    {
        private SceneObject _attachedTo;

        /// <summary>
        /// Instantiates the Component base class. 
        /// </summary>
        public Component() : base()
        {
        }

        /// <summary>
        /// The SceneObject this Component is attached to.
        /// </summary>
        public SceneObject AttachedTo
        {
            get => _attachedTo;

            set
            {
                if (value != null && _attachedTo == null) _attachedTo = value;
            }
        }

        /// <summary>
        /// All subclasses must define a method that creates a copy of an instance and its attribute values.
        /// </summary>
        /// <returns>Returns a new instance of the Component subclass, with the same attribute values as this instance.</returns>
        public abstract Component Copy();
    }
}
