using TypingTrainerProject.App;
using TypingTrainerProject.DataClasses;

namespace TypingTrainerProject.TrainingModes;

public class StagedTrainingMode : ITrainingMode {
    public string Name => "Staged training mode";
    public string Description => "Learn to type by step by step exercises";


    public string[] GettingInputMassage => [GettingRowMassage, GettingExerciseMassage];

    public Predicate<string>[] CorrectInputCondition => [ExistingRowChosen, ExistingExerciseChosen];


    private string GettingRowMassage {
        get {
            const int firstElementIndex = 0;
            const int lastElementIndex = 9;

            var massage = $"Choose row:{Environment.NewLine}";

            var rowNumber = 1;
            foreach (var row in SymbolsRows) {
                var firstRowElement = row[firstElementIndex];
                var lastRowElement = row[lastElementIndex];
                massage += $"{rowNumber})Row [{firstRowElement} - {lastRowElement}]{Environment.NewLine}";
                rowNumber++;
            }

            massage += $"{rowNumber})All symbols.{Environment.NewLine}";
            massage += "Print [exit] to get back to choosing mode.";

            return massage;
        }
    }

    private string GettingExerciseMassage {
        get {
            const int exercisesNumber = 8;

            var massage = $"Print Exercise number{Environment.NewLine}";

            for (var i = 1; i <= exercisesNumber; i++) {
                massage += $"Exercise Number {i}{Environment.NewLine}";
            }

            massage += "Print [exit] to get back to row";

            return massage;
        }
    }


    private Predicate<string> ExistingRowChosen => input => {
        const int firstRowNumber = 1;
        const int lastRowNumber = 6;
        var inputIsNumber = int.TryParse(input, out var rowNumber);
        var rowExist = rowNumber is >= firstRowNumber and <= lastRowNumber;
        var inputIsExit = input == "exit";

        return inputIsExit || (inputIsNumber && rowExist);
    };

    private Predicate<string> ExistingExerciseChosen => input => {
        const int firstExerciseNumber = 1;
        const int lastExerciseNumber = 8;
        var inputIsNumber = int.TryParse(input, out var rowNumber);
        var exerciseExist = rowNumber is >= firstExerciseNumber and <= lastExerciseNumber;
        var inputIsExit = input == "exit";

        return inputIsExit || (inputIsNumber && exerciseExist);
    };

    public Exercise? GetExercise(string exerciseNumber) {
        var userInput = exerciseNumber.Split(' ');
        var firstInputPart = userInput[0];
        var secondInputPart = userInput[1];

        if (userInput.Any(s => s == "exit")) return null;

        var rowNumber = int.Parse(firstInputPart);
        var exerciseIndex = int.Parse(secondInputPart) - 1;

        var allRowExerciseSymbols =
            AllExerciseMaterials
                .Find(materials => materials.RowNumber == rowNumber)!
                .Symbols;

        var neededExerciseSymbols = allRowExerciseSymbols[exerciseIndex];
        var exercise = ExerciseGenerator.Generate(neededExerciseSymbols);

        return exercise;
    }


    private List<List<string>> SymbolsRows => [
        ["a", "s", "d", "f", "g", "h", "j", "k", "l", ";"],

        ["q", "w", "e", "r", "t", "y", "u", "i", "o", "p"],

        ["z", "x", "c", "v", "b", "n", "m", ",", ".", "/"],

        ["!", "?", "-", "@", "[", "]", "(", ")", ":", "="],

        ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"]
    ];
    private List<RowExercisesMaterials> AllExerciseMaterials {
        get {
            var allExercisesMaterials = new List<RowExercisesMaterials>();

            var rowNumber = 1;
            foreach (var row in SymbolsRows) {
                const int rowLength = 10;
                var rowExercisesSymbols = new List<string[]>();

                for (var i = 0; i < rowLength; i += 2) {
                    var firstSymbol = row[i];
                    var secondSymbol = row[i + 1];
                    rowExercisesSymbols.Add([firstSymbol, secondSymbol]);
                }


                const int symbolsAmount = 5;
                const int firstSymbolIndex = 0;
                const int fifthSymbolIndex = 5;
                var firstFiveSymbolExercise = row.GetRange(firstSymbolIndex, symbolsAmount).ToArray();
                var secondFiveSymbolExercise = row.GetRange(fifthSymbolIndex, symbolsAmount).ToArray();
                rowExercisesSymbols.AddRange([firstFiveSymbolExercise, secondFiveSymbolExercise]);

                var allRowSymbolsExercise = row.ToArray();
                rowExercisesSymbols.Add(allRowSymbolsExercise);

                allExercisesMaterials.Add(new RowExercisesMaterials(
                    rowExercisesSymbols,
                    rowNumber
                ));

                rowNumber++;
            }

            return allExercisesMaterials;
        }
    }
}