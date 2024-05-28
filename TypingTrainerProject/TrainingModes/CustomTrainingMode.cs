using TypingTrainerProject.App;
using TypingTrainerProject.DataClasses;

namespace TypingTrainerProject.TrainingModes;

public class CustomTrainingMode : TrainingMode {
    public override string Name => "Custom training mode";
    public override string Description => "Choose symbols to practice";
    public override int Number => 2;

    public override string[] GettingInputMassage {
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

    public override Predicate<string>[] CorrectInputCondition => [
        input => {
            var selectedChars = input.Split(" ");

            var allSymbolsAvailable = selectedChars.All(s => AllSymbols.Contains(s));
            var inputIsExit = input == "exit";

            return allSymbolsAvailable || inputIsExit;
        }
    ];

    public override Exercise? GetExercise(string userChoice) {

        if (userChoice is "exit") return null;
        
        var selectedSymbols = userChoice.Split(" ");
        var exercise = ExerciseGenerator.Generate(selectedSymbols);

        return exercise;
    }
}