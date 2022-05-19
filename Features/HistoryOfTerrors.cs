﻿using System;
using System.Collections.Generic;
using System.Linq;

using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;

using Microsoftenator.Wotr.Common.Blueprints.Extensions;
using Microsoftenator.Wotr.Common.Util;

namespace AlternateHumanTraits.Features
{
    internal static class HistoryOfTerrors
    {
        private static void PatchHistoryOfTerrors()
        {
            Main.Log?.Debug($"{nameof(HistoryOfTerrors)}.{nameof(PatchHistoryOfTerrors)}");

            var historyOfTerrors = FeatureBlueprints.HistoryOfTerrorsFeat;

            historyOfTerrors.GetBlueprint()
                .AddPrerequisiteNoFeature(FeatureBlueprints.HistoryOfTerrorsTrait.GetBlueprint(), Functional.Ignore);
        }

        internal static void AddHistoryOfTerrorsTrait()
        {
            Main.Log?.Debug($"{nameof(HistoryOfTerrors)}.{nameof(AddHistoryOfTerrorsTrait)}");

            var original = FeatureBlueprints.HistoryOfTerrorsFeat.GetBlueprint();

            var info = FeatureBlueprints.HistoryOfTerrorsTrait;

            var copy = original.CreateCopy(info.Name, info.Guid, feat =>
            {
                feat.SetDescription($"{feat.Description} This racial trait replaces the skilled trait.");

                feat.Groups = new[] { FeatureGroup.Racial };

                feat.RemoveComponents(c => c is PrerequisiteFeature p && p.Feature == FeatureBlueprints.HumanRace.GetBlueprint());

                feat.AddPrerequisiteFeature(FeatureBlueprints.HumanSkilled.GetBlueprint(), Functional.Ignore, removeOnApply: true);
            });

            PatchHistoryOfTerrors();
        }
    }
}
