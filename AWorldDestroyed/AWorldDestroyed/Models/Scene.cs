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
using AWorldDestroyed.GUI;
using AWorldDestroyed.Models.Components;
using AWorldDestroyed.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AWorldDestroyed.Models
{
    /// <summary>
    /// Holds information about a scene and connects a Camera and an ObjectHandler with the objects of this scene.
    /// </summary>
    public abstract class Scene
    {
        public bool Debug { get; set; }
        public SpriteBatch SpriteBatch { get; set; }

        protected Camera Camera;
        protected SceneObject CameraFollow;

        private ObjectHandler objectHandler;
        //private UIObjectHandler uiObjectHandler;
        private bool _initialized;

        /// <summary>
        /// Creates a new instance of the Scene class, with the specified spriteBatch and a collection of GameObjects. 
        /// </summary>
        /// <param name="spriteBatch">A MonoGame SpriteBatch.</param>
        public Scene(SpriteBatch spriteBatch) : this(spriteBatch, new Vector2(800, 480))
        {
        }

        /// <summary>
        /// Creates a new instance of the Scene class, with the specified spriteBatch, camera view size and a collection of GameObjects. 
        /// </summary>
        /// <param name="spriteBatch">A MonoGame SpriteBatch.</param>
        /// <param name="cameraViewSize">The view size of the camera in this Scene.</param>
        public Scene(SpriteBatch spriteBatch, Vector2 cameraViewSize)
        {
            SpriteBatch = spriteBatch;

            Camera = new Camera(cameraViewSize);
            objectHandler = new ObjectHandler();
        }

        public bool IsInitialized => _initialized;

        /// <summary>
        /// Loads all GameObjects and UIElements in this Scene.
        /// All subscenes must define a Load method, it should contain all subscene specific content.
        /// </summary>
        public abstract void Load();

        /// <summary>
        /// Perfom a first time setup of the Scene.
        /// </summary>
        public void Initialize()
        {
            Load();
            _initialized = true;
        }

        /// <summary>
        /// Removes all objects in this scene and then reinstantiates them.
        /// </summary>
        public void ReLoad()
        {
            objectHandler.GameObjects.Clear();
            //uiObjectHandler.UIObjects.Clear();

            Load();
        }

        /// <summary>
        /// Update the logic of this Scene.
        /// </summary>
        /// <param name="deltaTime">Time in milliseconds since last update.</param>
        public void Update(double deltaTime)
        {
            if (CameraFollow != null) Camera.Transform.Position = CameraFollow.Transform.Position - Camera.ViewSize * 0.5f;
            objectHandler.Update(deltaTime, Camera.View);
        }

        /// <summary>
        /// Draws all GameObjects within the Camera and displays any UIElements in this Scene.
        /// </summary>
        public void Draw()
        {
            GameObject[] gameObjects = objectHandler.Query(Camera.View);

            SpriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, 
                null, null, Camera.GetTranslationMatrix());

            for (int i = 0; i < 50; i++)
            {
                SpriteBatch.Draw(ContentManager.Pixel, new Rectangle(-1000 + (2000/50) * i, -1000, (2000/50), 2000), (i%2==0 ? Color.Gray : Color.White) * 0.5f);
                SpriteBatch.Draw(ContentManager.Pixel, new Rectangle(-1000, -1000 + (2000 / 50) * i, 2000, (2000 / 50)), (i%2==0 ? Color.Gray : Color.White) * 0.5f);
            }

            foreach (GameObject obj in gameObjects)
            {
                if (obj.Enabled && obj.HasSpriteRenderer)
                {
                    SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
                    float sortingOrder = ((float)renderer.SortingLayer * 1000f + renderer.SortingOrder + 1000f)
                        / (Enum.GetValues(typeof(SortingLayer)).Length * 1000f + 1000f);

                    if (renderer.Enabled)
                    {
                        SpriteBatch.Draw(
                            renderer.Sprite.Texture,
                            obj.Transform.WorldPosition,
                            renderer.Sprite.SourceRectangle,
                            renderer.Color,
                            MathHelper.ToRadians(obj.Transform.WorldRotation),
                            renderer.Sprite.Origin,
                            obj.Transform.Scale,
                            renderer.SpriteEffect,
                            sortingOrder);
                    }
                    if (Debug)
                    {
                        foreach (Collider collider in obj.GetComponents<Collider>())
                        {
                            if (!collider.Enabled) continue;

                            SpriteBatch.Draw(
                                Game1.Pixel,
                                (Rectangle)collider.GetRectangle(),
                                null,
                                (collider.IsTrigger ? Color.Orange : Color.Red) * 0.3f,
                                0f,
                                Vector2.Zero,
                                SpriteEffects.None,
                                sortingOrder + 0.0001f);
                        }
                    }

                    OnObjectDraw(obj, sortingOrder);
                }
            }

            SpriteBatch.End();

            SpriteBatch.Begin();
            OnGUIDraw();
            SpriteBatch.End();
        }

        protected virtual void OnGUIDraw()
        {
        }

        protected virtual void OnObjectDraw(GameObject gameObject, float sortingOrder)
        {
        }

        /// <summary>
        /// Add a GameObject to this Scene.
        /// </summary>
        /// <param name="gameObject">The GameObject to add.</param>
        public void AddObject(GameObject gameObject)
        {
            if (gameObject == null || objectHandler.GameObjects.Contains(gameObject)) return;

            //gameObjects.Add(gameObject);
            objectHandler.AddObject(gameObject);

            foreach (GameObject obj in gameObject.Children)
            {
                AddObject(obj);
            }
        }

        /// <summary>
        /// Add a UIElement to this Scene.
        /// </summary>
        /// <param name="uIElement">The UIElement to add.</param>
        public void AddUIObject(UIElement uIElement)
        {
            // Need UIHandler.
            //if (uIElement == null || uIElements.Contains(uIElement))
            //    return;

            //uIElements.Add(uIElement);
        }
    }
}
