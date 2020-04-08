namespace AWorldDestroyed.Utility
{
    public interface IDamageable
    {
        float Health { get; set; }
        float MaxHealth { get; set; }
        bool IsDead { get; }

        void TakeDamage(float amount);
        void OnDeath();
    }
}
