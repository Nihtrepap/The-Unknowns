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

using System;
using System.Collections.Generic;
using AWorldDestroyed.Models;

namespace AWorldDestroyed.Utility
{
    /// <summary>
    /// Represents a collection of objects that share a purpose in a game. 
    /// Supplies modularity to scene design and a unified way to handle scene objects.
    /// </summary>
    public abstract class ISceneLayer<T> where T : SceneObject
    {
        //void Initialize();
        //void Update(double deltaTime);
        public List<T> sceneObjects;
        public Type Type => typeof(T);

        public ISceneLayer()
        {
            sceneObjects = new List<T>();
        }

        public void Initialize()
        {
            if (sceneObjects == null) return;

            foreach (T obj in sceneObjects)
                obj?.Initialize();
        }

        public void Update(double deltaTime)
        {
            if (sceneObjects == null) return;

            foreach (T obj in sceneObjects)
                obj?.Update(deltaTime);
        }

        protected void AddObject(T obj)
        {
            sceneObjects?.Add(obj);
        }
    }

    public interface IObjectHandlerManageable
    {
        GameObject[] GetGameObjects();
    }


    public class ANiceLayer : ISceneLayer<GameObject>, IObjectHandlerManageable
    {
        public ANiceLayer()
        {
            AddObject(new GameObject(this));
        }

        public GameObject[] GetGameObjects() => sceneObjects.ToArray();
    }
}
