using TypingTrainerProject.DataClasses;

namespace TypingTrainerProject.App;

public static class ExerciseGenerator {
    public static Exercise Generate(string[] symbols) {
        var withUppercaseSymbols = AddUppercaseLetters(symbols);
        var random = new Random();
        var words = new List<string>();
        const int approximateExerciseLength = 300;

        var currentCharsAmount = 0;

        while (currentCharsAmount <= approximateExerciseLength) {
            const int wordMinLength = 1;
            const int wordMaxLength = 10;

            var stringLength = random.Next(wordMinLength, wordMaxLength);
            var word = "";

            for (var i = 0; i < stringLength; i++) {
                const int firstSymbolIndex = 0;
                var lastSymbolIndex = withUppercaseSymbols.Count;

                var randomSymbolIndex = random.Next(firstSymbolIndex, lastSymbolIndex);
                var symbol = withUppercaseSymbols[randomSymbolIndex];
                word += symbol;
            }

            word += ' ';
            currentCharsAmount += word.Length;
            words.Add(word);
        }

        return new Exercise(words);
    }

    private static List<string> AddUppercaseLetters(string[] initialList) {
        var withUppercaseSet = new HashSet<string>(initialList);

        foreach (var letter in initialList) {
            var uppercase = letter.ToUpper();
            withUppercaseSet.Add(uppercase);
        }

        return withUppercaseSet.ToList();
    }
}