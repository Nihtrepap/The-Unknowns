// =============================================
//         Editor:     Daniel Abdulahad
//         Last edit:  2020-03-22 
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
        public Transform Transform { get; private set; }
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
        /// <param name="transform">A Transform component supplying transformation capabilities to this object.</param>
        public SceneObject(ISceneLayer sceneLayer, Transform transform) : base()
        {
            this.sceneLayer = sceneLayer;

            if (transform == null) Transform = new Transform(this);
            else Transform = transform;

            Parent = null;
            children = new List<SceneObject>();

            components = new List<Component>()
            {
                Transform
            };
        }

        /// <summary>
        /// Returns all components attached to this object.
        /// </summary>
        public Component[] Components => components.ToArray();

        /// <summary>
        /// Returns all children of this object.
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
        /// Try getting a component that is attached to this object of the specified Type.
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
        /// Try getting a component that is attached to this object with the specified name.
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

        /// <summary>
        /// Adds a component to this object.
        /// </summary>
        /// <param name="component">The component to add.</param>
        public void AddComponent(Component component)
        {
            if (component == null) return;

            components.Add(component);
        }

        /// <summary>
        /// Try getting a object that is a child of this object with the specified name.
        /// </summary>
        /// <param name="name">The name of the child to find.</param>
        /// <returns>The first child named name; null if no one was found.</returns>
        public SceneObject GetChild(string name)
        {
            return children.Find((child) => child.Name == name);
        }

        /// <summary>
        /// Adds a child to this object.
        /// </summary>
        /// <param name="child">THe child to add.</param>
        public void AddChild(SceneObject child)
        {
            if (child == null) return;

            if (!children.Contains(child))
            {
                children.Add(child);
                child.Parent = this;
            }
        }

        /// <summary>
        /// Mark this object for destruction.
        /// </summary>
        public void Destroy() => Destroyed = true;
    }
}
