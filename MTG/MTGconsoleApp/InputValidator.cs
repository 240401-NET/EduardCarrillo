using System;
using System.Dynamic;
using System.Text.Json;
using System.Linq;

namespace MTG;
public static class InputValidator

{public static readonly string[] ValidTypes = ["Land", "Basic Land", "Creature", "Artifact", "Enchantment", "Instant", "Sorcery", "Planeswalker"];

        public static bool IsValidNonEmptyString(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

        public static bool IsValidCardType(string input)
        {
            return ValidTypes.Contains(input, StringComparer.OrdinalIgnoreCase);
        }

        public static bool IsValidValidInteger(string input, out int result)
        {
            return int.TryParse(input, out result) && result >= 0;
        }
    }