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

namespace AWorldDestroyed.Models.Components
{
    public abstract class Script : Component
    {
        public Script() : base()
        {
        }

        public abstract void Update(double deltaTime);

        public abstract override Component Copy();
    }
}
