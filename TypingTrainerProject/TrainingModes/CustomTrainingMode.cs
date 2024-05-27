using TypingTrainerProject.App;
using TypingTrainerProject.DataClasses;

namespace TypingTrainerProject.TrainingModes;

public class CustomTrainingMode : ITrainingMode {
    public string Name => "Custom training mode";
    public string Description => "Choose symbols to practice";

    public string[] GettingInputMassage {
        get {
            var massage = $"Write all needed symbols in a row:{Environment.NewLine}";

            var symbolNumber = 1;
            foreach (var symbol in AllSymbols) {
                var currentRowEnded = symbolNumber % 5 == 0;

                massage += $"{symbol}   {(
                    currentRowEnded
                        ? $"{Environment.NewLine}{Environment.NewLine}"
                        : ""
                )}";

                symbolNumber++;
            }

            massage += "Print [exit] to get back to choosing mode.";

            return [massage];
        }
    }

    public Predicate<string>[] CorrectInputCondition => [
        input => {
            var selectedChars = input.Split(" ");

            var allSymbolsAvailable = selectedChars.All(s => AllSymbols.Contains(s));
            var inputIsExit = input == "exit";

            return allSymbolsAvailable || inputIsExit;
        }
    ];

    public Exercise? GetExercise(string exerciseNumber) {

        if (exerciseNumber is "exit") return null;
        
        var selectedSymbols = exerciseNumber.Split(" ");
        var exercise = ExerciseGenerator.Generate(selectedSymbols);

        return exercise;
    }

    private List<string> AllSymbols => [
        "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m",
        "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",

        "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",

        "!", "?", ":", "@", ";", "(", ")", "*", "."
    ];
}