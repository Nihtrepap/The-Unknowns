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
    /// Holds a refenrence to the GameObject a component subclass should act upon.
    /// </summary>
    abstract class Component : BaseObject
    {
        public GameObject GameObject { get; protected set; }

        /// <summary>
        /// Instantiates the Component bas class, with the specified GameObject reference. 
        /// </summary>
        /// <param name="gameObject">The GameObject a component subclass should act upon.</param>
        public Component(GameObject gameObject)
        {
            GameObject = gameObject;
        }

        /// <summary>
        /// All subclasses must define a method that creates a copy of an instance and its attribute values.
        /// </summary>
        /// <returns>Retruns a new instance of the Component subclass, with the same attribute values as this instance.</returns>
        public abstract Component Copy();
    }
}
