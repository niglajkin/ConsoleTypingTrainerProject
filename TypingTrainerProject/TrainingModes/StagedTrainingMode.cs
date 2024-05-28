using TypingTrainerProject.App;
using TypingTrainerProject.DataClasses;

namespace TypingTrainerProject.TrainingModes;

public class StagedTrainingMode : TrainingMode {
    public override string Name => "Staged training mode";
    public override string Description => "Learn to type by step by step exercises";

    public override int Number => 1;

    public override string[] GettingInputMassage => [GettingRowMassage, GettingExerciseMassage];

    public override Predicate<string>[] CorrectInputCondition => [ExistingRowChosen, ExistingExerciseChosen];


    private string GettingRowMassage {
        get {
            const int firstElementIndex = 0;
            const int lastElementIndex = 9;

            var massage = $"Choose row:{Environment.NewLine}";

            var rowNumber = 1;
            foreach (var row in SymbolsDividedInRows) {
                var firstRowElement = row[firstElementIndex];
                var lastRowElement = row[lastElementIndex];
                massage += $"{rowNumber})Row [{firstRowElement} - {lastRowElement}]{Environment.NewLine}";
                rowNumber++;
            }
            
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
        const int lastRowNumber = 5;
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

    public override Exercise? GetExercise(string userChoice) {
        var userInput = userChoice.Split(' ');
        
        if (userInput.Any(s => s == "exit")) return null;
        
        var firstInputPart = userInput[0];
        var secondInputPart = userInput[1];

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


    private List<List<string>> SymbolsDividedInRows {
        get {
            var symbolsInRows = new List<List<string>>();
            var interimRow = new List<string>();

            const int rowLength = 10;
            foreach (var symbol in AllSymbols) {
                interimRow.Add(symbol);
                
                if (interimRow.Count == rowLength) {
                    symbolsInRows.Add([..interimRow]);
                    interimRow.Clear();
                }
                
            }

            return symbolsInRows;
        }
    }

    private List<RowExercisesMaterials> AllExerciseMaterials {
        get {
            var allExercisesMaterials = new List<RowExercisesMaterials>();

            var rowNumber = 1;
            foreach (var row in SymbolsDividedInRows) {
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