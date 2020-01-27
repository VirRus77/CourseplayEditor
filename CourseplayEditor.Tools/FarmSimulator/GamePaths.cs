using System;
using System.Collections.Generic;
using System.IO;

namespace CourseplayEditor.Tools.FarmSimulator
{
    public static class GamePaths
    {
        private static readonly string MyGamesPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games");

        private static readonly IDictionary<FarmSimulatorVersion, string> VersionToNameSaveDirectory =
            new Dictionary<FarmSimulatorVersion, string>
            {
                {FarmSimulatorVersion.FarmingSimulator2019, "FarmingSimulator2019"},
                {FarmSimulatorVersion.FarmingSimulator2017, "FarmingSimulator2017"},
            };

        public static string GetSavesPath(FarmSimulatorVersion version)
        {
            return Path.Combine(MyGamesPath, VersionToNameSaveDirectory[version]);
        }
    }
}