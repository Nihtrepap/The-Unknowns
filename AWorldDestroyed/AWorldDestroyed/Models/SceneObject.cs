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
using AWorldDestroyed.Models.Components;
using AWorldDestroyed.Utility;

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
        public bool HasCollider { get; private set; }
        public bool HasSpriteRenderer { get; private set; }

        private List<Component> components;
        private List<SceneObject> children;

        /// <summary>
        /// Initialize a new SceneObject.
        /// </summary>
        public SceneObject() : this(null)
        {
        }

        /// <summary>
        /// Initialize a new GameObject with a given Transform component.
        /// </summary>
        /// <param name="transform">A Transform component supplying transformation capabilities to this object.</param>
        public SceneObject(Transform transform) : base()
        {
            if (transform == null) Transform = new Transform();
            else Transform = transform;

            Parent = null;
            children = new List<SceneObject>();
            components = new List<Component>();
            
            AddComponent(Transform);
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
            foreach (Component component in components)
            {
                if (component is Script script) script.Update(deltaTime);
                if (component is Animator animator) animator.Update(deltaTime);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">The type of component to retrieve.</typeparam>
        /// <returns>.</returns>
        public List<T> GetComponents<T>() where T : Component
        {
            List<T> a = new List<T>();
            foreach (Component component in components)
                if (component is T comp) a.Add(comp);

            return a;
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
            
            component.AttachedTo = this;
            components.Add(component);
            
            if (component is Collider) HasCollider = true;
            if (component is SpriteRenderer) HasSpriteRenderer = true;
        }

        /// <summary>
        /// Check if this object has a component of a specific type.
        /// </summary>
        /// <typeparam name="T">The type of component to check for.</typeparam>
        /// <returns>true if this object has a component of the specific type, otherwise false.</returns>
        public bool HasComponent<T>() where T : Component
        {
            foreach (Component component in components)
                if (component is T) return true;
        
            return false;
        }

        /// <summary>
        /// Try getting an object that is a child of this object with the specified name.
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
        /// <param name="child">The child to add.</param>
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
        /// Mark this object and all its children for destruction.
        /// </summary>
        public void Destroy()
        {
            foreach (SceneObject child in children)
                child.Destroy();
            
            Destroyed = true;
        }
    }
}
