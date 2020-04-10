namespace AWorldDestroyed.Utility
{
    /// <summary>
    /// States that an object has an Update method and can be updated.
    /// </summary>
    public interface IUpdateable
    {
        /// <summary>
        /// Defines what happens when the object is updated.
        /// </summary>
        /// <param name="deltaTime"></param>
        void Update(double deltaTime);
    }
}
