using TypingTrainerProject.DataClasses;

namespace TypingTrainerProject.TrainingModes;

public class RealTextTrainingMode : ITrainingMode {
    public string Name => "Real text training mode";
    public string Description => "Type a large real text";

    public string[] GettingInputMassage {
        get {
            var massage = $"Choose text to type:{Environment.NewLine}";

            foreach (var text in AllBuiltInTexts) {
                massage += $"{text.Number}){text.Title}{Environment.NewLine}";
            }

            massage += "Print [exit] to stop the application.";

            return [massage];
        }
    }

    public Predicate<string>[] CorrectInputCondition => [
        input => {
            var inputIsNumber = int.TryParse(input, out var inputAsNumber);
            var inputIsTextNumber = AllBuiltInTexts.Any(text => text.Number == inputAsNumber);
            var inputIsExit = input == "exit";

            return inputIsExit || (inputIsNumber && inputIsTextNumber);
        }
    ];


    public Exercise? GetExercise(string exerciseIdentifier) {
        if (exerciseIdentifier == "exit") return null;

        var textIndex = int.Parse(exerciseIdentifier) - 1;
        var text = AllBuiltInTexts[textIndex];
        var exercise = new Exercise(text.Words);

        return exercise;
    }

    private static List<Text> AllBuiltInTexts {
        get {
            const int forestTextNumber = 1;
            const string forestTextTitle = "Forests General Information";
            var forestText = new Text(forestTextNumber, forestTextTitle, [
                "Forests ", "are ", "essential ", "ecosystems ", "covering ", "about ", "31% ", "of ", "the ",
                "Earth's ", "land ", "area, ",
                "teeming ", "with ", "biodiversity ", "and ", "providing ", "habitats ", "for ", "countless ",
                "species ", "of ", "plants, ",
                "animals, ", "and ", "microorganisms. ", "They ", "play ", "a ", "crucial ", "role ", "in ",
                "regulating ", "the ", "climate ",
                "by ", "absorbing ", "carbon ", "dioxide ", "and ", "producing ", "oxygen, ", "thus ", "helping ",
                "to ", "mitigate ",
                "climate ", "change. ", "Forests ", "also ", "offer ", "valuable ", "resources, ", "such ", "as ",
                "timber, ", "medicinal ",
                "plants, ", "and ", "opportunities ", "for ", "recreation ", "and ", "tourism. ", "Furthermore, ",
                "they ", "protect ",
                "watersheds, ", "prevent ", "soil ", "erosion, ", "and ", "maintain ", "water ", "quality. ",
                "Sustainable ", "forest ",
                "management ", "is ", "vital ", "for ", "preserving ", "these ", "benefits, ", "ensuring ",
                "environmental ", "health, ",
                "and ", "supporting ", "human ", "well-being ", "for ", "future ", "generations. ", "Protecting ",
                "forests ", "is ",
                "crucial ", "to ", "maintaining ", "the ", "planet's ", "ecological ", "balance ", "and ",
                "biodiversity."
            ]);

            const int oceanTextNumber = 2;
            const string oceanTextTitle = "Oceans General Information";
            var oceanText = new Text(oceanTextNumber, oceanTextTitle, [
                "Oceans ", "cover ", "over ", "70% ", "of ", "the ", "Earth's ", "surface ", "and ", "are ",
                "integral ", "to ", "life ", "on ",
                "our ", "planet. ", "They ", "regulate ", "the ", "climate ", "by ", "absorbing ", "carbon ",
                "dioxide ", "and ", "heat, ",
                "influence ", "weather ", "patterns, ", "and ", "provide ", "a ", "home ", "to ", "diverse ", "marine ",
                "life. ", "Oceans ",
                "are ", "also ", "crucial ", "for ", "human ", "economies, ", "offering ", "resources ", "such ", "as ",
                "fish, ", "oil, ",
                "and ", "renewable ", "energy. ", "Preserving ", "the ", "ocean ", "ecosystems ", "is ", "vital ",
                "for ", "sustaining ",
                "biodiversity ", "and ", "ensuring ", "the ", "health ", "of ", "the ", "global ", "environment."
            ]);

            const int desertTextNumber = 3;
            const string desertTextTitle = "Deserts General Information";
            var desertText = new Text(desertTextNumber, desertTextTitle, [
                "Deserts ", "are ", "unique ", "ecosystems ", "that ", "cover ", "about ", "33% ", "of ", "the ",
                "Earth's ", "land ", "surface. ",
                "Characterized ", "by ", "low ", "precipitation, ", "they ", "experience ", "extreme ", "temperatures ",
                "and ", "harsh ",
                "living ", "conditions. ", "Despite ", "this, ", "deserts ", "support ", "a ", "variety ", "of ",
                "adapted ", "plants ", "and ",
                "animals. ", "Cacti ", "store ", "water, ", "while ", "creatures ", "like ", "the ", "fennec ", "fox ",
                "have ", "evolved ",
                "to ", "thrive ", "in ", "these ", "environments. ", "Deserts ", "also ", "play ", "a ", "role ", "in ",
                "global ", "climate ",
                "regulation ", "and ", "are ", "sources ", "of ", "minerals. ", "Protecting ", "these ", "fragile ",
                "ecosystems ", "is ",
                "crucial ", "for ", "biodiversity ", "and ", "environmental ", "health."
            ]);

            return [forestText, oceanText, desertText];
        }
    }
}