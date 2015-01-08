﻿namespace GitVersion
{
    using System;
    using System.IO;

    public class NextVersionTxtFileFinder
    {
        EffectiveConfiguration configuration;
        string repositoryDirectory;

        public NextVersionTxtFileFinder(string repositoryDirectory, EffectiveConfiguration configuration)
        {
            this.repositoryDirectory = repositoryDirectory;
            this.configuration = configuration;
        }

        public bool TryGetNextVersion(out SemanticVersion semanticVersion)
        {
            var filePath = Path.Combine(repositoryDirectory, "NextVersion.txt");
            if (!File.Exists(filePath))
            {
                semanticVersion = null;
                return false;
            }

            var version = File.ReadAllText(filePath);
            if (string.IsNullOrEmpty(version))
            {
                semanticVersion = null;
                return false;
            }

            if (!SemanticVersion.TryParse(version, configuration.GitTagPrefix, out semanticVersion))
            {
                throw new ArgumentException("Make sure you have a valid semantic version in NextVersion.txt");
            }

            return true;
        }
    }
}