using Assets.Source.Effects.Continous.Runtime;
using Assets.Source.Entity;
using Assets.Source.Restart;
using System;
using System.Collections.Generic;

namespace Assets.Source.Effects.Continous.Properties.ContinousTarget
{
    internal interface IContinousEffectsCounter : IRestartable
    {
        IEntity Entity { get; }

        IReadOnlyDictionary<IContinousEffect, uint> Effects { get; }

        event Action<IContinousEffect, uint> OnNewEffect;
        event Action<IContinousEffect, uint> OnCountEffect;
        event Action<IContinousEffect> OnLostEffect;

        void Register(IContinousEffect effect);
        void Clear(IEnumerable<IContinousEffect> effect);
    }
}
