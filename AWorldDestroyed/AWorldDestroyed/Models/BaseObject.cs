namespace AWorldDestroyed.Models
{
    /// <summary>
    /// Base class for all objects.
    /// </summary>
    public class BaseObject
    {
        private static int id;

        public bool Enabled { get; set; }
        public string Name { get; set; }
        public Tag Tag { get; set; }

        /// <summary>
        /// Initialize a new BaseObject with a default name.
        /// </summary>
        public BaseObject()
            : this($"Object{id}")
        {
        }

        /// <summary>
        /// Initialize a new BaseObject with a given name.
        /// </summary>
        /// <param name="name">Name of the this object.</param>
        public BaseObject(string name)
        {
            Enabled = true;
            Name = name;
            Tag = 0;

            id++;
        }

        /// <summary>
        /// Get the name of this object.
        /// </summary>
        /// <returns>Returns the name of the object.</returns>
        public override string ToString() => Name;
    }
}
