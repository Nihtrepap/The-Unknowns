// =============================================
//         Editor:     Daniel Abdulahad
//         Last edit:  2020-03-21 
// _-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_
//
//       (\                 >+{{{o)> - kvaouk
//    >+{{{{{0)> - kraouk      LL  
//       /_\_
//
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//                <333333><                     
//         <3333333><           <33333>< 

using System.Collections.Generic;
using System.Linq;

namespace AWorldDestroyed.Models
{
    /// <summary>
    /// Base class for all entities in Scenes.
    /// </summary>
    public class SceneObject : BaseObject
    {
        public Transform Position { get; private set; }
        public SceneObject Parent { get; private set; }
        public bool Destroyed { get; private set; }

        private ISceneLayer sceneLayer;
        private List<Component> components;
        private List<SceneObject> children;

        /// <summary>
        /// Initialize a new SceneObject within the context of a given SceneLayer.
        /// </summary>
        /// <param name="sceneLayer">The SceneLayer related to this object.</param>
        public SceneObject(ISceneLayer sceneLayer) : this(sceneLayer, null)
        {
        }

        /// <summary>
        /// Initialize a new GameObject within the context of a given SceneLayer, with a given Transform component.
        /// </summary>
        /// <param name="sceneLayer">The SceneLayer related to this object.</param>
        /// <param name="position">A Transform component supplying transformation capabilities to this object.</param>
        public SceneObject(ISceneLayer sceneLayer, Transform position) : base()
        {
            this.sceneLayer = sceneLayer;

            if (position == null) Position = new Transform(this);
            else Position = position;

            Parent = null;
            children = new List<SceneObject>();

            components = new List<Component>()
            {
                Position
            };
        }

        /// <summary>
        /// Returns all components attached to this SceneObject.
        /// </summary>
        public Component[] Components => components.ToArray();

        /// <summary>
        /// Returns all children of this SceneObject.
        /// </summary>
        public SceneObject[] Children => children.ToArray();

        /// <summary>
        /// Returns all children of the parent, excluding this object.
        /// </summary>
        public SceneObject[] Siblings
        {
            get
            {
                if (Parent == null) return new SceneObject[0];

                List<SceneObject> siblings = Parent.Children.ToList();
                siblings.Remove(this);

                return siblings.ToArray();
            }
        }

        /// <summary>
        /// Used to update this object.
        /// </summary>
        /// <param name="deltaTime">Time in milliseconds since last update.</param>
        public virtual void Update(double deltaTime)
        {
        }

        /// <summary>
        /// Try getting a component that is attached to this SceneObject of the specified Type.
        /// </summary>
        /// <typeparam name="T">The type of component to retrieve.</typeparam>
        /// <returns>The first component of the specified Type found; null if it failed.</returns>
        public T GetComponent<T>() where T : Component
        {
            foreach (Component component in components)
                if (component is T comp) return comp;

            return null;
        }

        /// <summary>
        /// Try getting a component that is attached to this SceneObject with the specified name.
        /// </summary>
        /// <param name="name">The name of the component to find.</param>
        /// <returns>The first component named name; null if no one was found.</returns>
        public Component GetComponent(string name)
        {
            return components.Find((component) => component.Name == name);
        }

        /// <summary>
        /// Adds a component of type T to this object.
        /// </summary>
        /// <typeparam name="T">The type of component to add.</typeparam>
        public void AddComponent<T>() where T : Component, new()
        {
            T component = new T();

            AddComponent(component);
        }

        public void AddComponent(Component component)
        {
            if (component == null) return;

            components.Add(component);
        }

        public SceneObject GetChild(string name)
        {
            return children.Find((child) => child.Name == name);
        }

        public void AddChild(SceneObject child)
        {
            if (child == null) return;

            if (!children.Contains(child))
            {
                children.Add(child);
                child.Parent = this;
            }
        }

        public void Destroy() => Destroyed = true;
    }
    public class Component : BaseObject
    {
        public Component()
        {

        }
        public Component(SceneObject p)
        {

        }
    }
    public interface ISceneLayer { }
}
