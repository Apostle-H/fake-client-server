using Assets.Source.Entity;

namespace Assets.Source.Effects.Instant.Runtime
{
    internal interface IEffect
    {
        void Apply(IEntity target);
    }
}
