using Assets.Source.Abilities.Pick;
using Assets.Source.Entity;
using Assets.Source.Restart;
using Assets.Source.Step.Agent;
using UnityEngine;
using VContainer;
using VContainer.Unity;

internal class MainScope : LifetimeScope
{
    [SerializeField] private AEntity[] entities;

    protected override void Configure(IContainerBuilder builder)
    {
        ConfigureAbilities(builder);
        ConfigureEntities(builder);
        ConfigureStep(builder);
        ConfigureRestart(builder);
    }

    private void ConfigureAbilities(IContainerBuilder builder)
    {
        builder.Register<AbilityPicker>(Lifetime.Singleton).AsImplementedInterfaces();
    }

    private void ConfigureEntities(IContainerBuilder builder)
    {
        foreach (var entity in entities)
            builder.RegisterInstance(entity).AsImplementedInterfaces();
    } 

    private void ConfigureStep(IContainerBuilder builder)
    {
        builder.Register<TurnManager>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
    }

    private void ConfigureRestart(IContainerBuilder builder)
    {
        builder.Register<GlobalRestarter>(Lifetime.Singleton).As<IGlobalRestarter>();

        builder.RegisterEntryPoint<OnDeathRestarter>(Lifetime.Singleton);
    }
}
