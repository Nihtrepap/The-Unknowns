namespace AWorldDestroyed.Utility
{
    /// <summary>
    /// Defines a set of properties and metods to allow an object to take damage.
    /// </summary>
    public interface IDamageable
    {
        float Health { get; set; }
        float MaxHealth { get; set; }
        bool IsDead { get; }

        /// <summary>
        /// Defines what happens when an object takes damage.
        /// Should modify the Health property and call OnDeath if Health is zero.
        /// </summary>
        /// <param name="amount"></param>
        void TakeDamage(float amount);

        /// <summary>
        /// Defines what happens when the object hits zero Health and dies.
        /// </summary>
        void OnDeath();
    }
}
