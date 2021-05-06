public interface IHittable
{
    void Hit(int damage = 1);
}

public interface IHealth
{
    void Heal(int heal = 1);
}
