using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FantasyLeague.Common.Helpers
{
    public static class ProjectDirectory
    {
        public static string GetPath()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var directoryName = Path.GetFileName(currentDirectory);
            var relativePath = directoryName.StartsWith("netcoreapp") ? @"../../../" : string.Empty;

            return relativePath;
        }
    }
}
