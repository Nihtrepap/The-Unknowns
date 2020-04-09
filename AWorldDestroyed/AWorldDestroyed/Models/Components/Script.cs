// =============================================
//         Editor:     Daniel Abdulahad
//         Last edit:  2020-03-30 
// _-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_
//
//       (\                 >+{{{o)> - kvaouk
//    >+{{{{{0)> - kraouk      LL  
//       /_\_
//
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//                <333333><                     
//         <3333333><           <33333>< 

using AWorldDestroyed.Utility;

namespace AWorldDestroyed.Models.Components
{
    public abstract class Script : Component, IUpdateable
    {
        public Script() : base()
        {
        }

        public abstract void Update(double deltaTime);

        public abstract override Component Copy();

        //public virtual void OnCollision(GameObject other, Side side) { }

        //public virtual void OnTrigger(GameObject other, Side side) { }

        //public virtual void OnTriggerEnter(GameObject other, Side side) { }

        //public virtual void OnTriggerExit(GameObject other, Side side) { }
    }
}
