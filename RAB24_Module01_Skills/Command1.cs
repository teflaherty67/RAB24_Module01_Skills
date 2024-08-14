﻿namespace RAB24_Module01_Skills
{
    [Transaction(TransactionMode.Manual)]
    public class Command1 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // this is a variable for the Revit application
            UIApplication uiapp = commandData.Application;

            // this is a variable for the current Revit model
            Document doc = uiapp.ActiveUIDocument.Document;

            // create a comment using a double forward slash
            // comments to do get compiled
            // used to leave yourself notes in your code

            // let's create some variables
            // DataType VariableName = Value; <- always end the line with a semicolon!

            // create string variables
            string text1 = "this is my text";
            string text2 = "this is my next text";

            // combine strings
            string text3 = text1 + text2;
            string text4 = text1 + " " + text2 + "abcd";

            // create number variables
            int number1 = 10;
            double number2 = 20.5;

            // do some math
            double number3 = number1 + number2;
            double number4 = number3 - number2;
            double number5 = number4 / 100;
            double number6 = number5 * number4;

            // order of operations
            double number7 = (number6 + number5) / number4;

            // convert meters to feet
            double meters = 4;
            double metersToFeet = meters * 3.28084;

            // convert mm to feet
            double mm = 3500;
            double mmToFeet = mm / 304.8;
            double mmToFeet2 = (mm / 1000) * 3.28084;

            // find the remainder wehn dividing (ie. the modulo or mod)
            double remainder1 = 100 % 10; // equals 0 (100 / 10 = 10)
            double remainder2 = 100 % 9; // equals 1 (100 / 9 = 11 with remainder of 1)

            // increment a number by 1
            number6++;

            // decrement a number by 1
            number6--;

            // add number to value of variable
            number6 += 10;

            // use conditional logic to compare things
            // compare using boolean operators
            // == equals
            // != not equal
            // > greater than
            // < less than
            // >= greater than or equal to
            // <= less than or equal to

             

            return Result.Succeeded;
        }
        internal static PushButtonData GetButtonData()
        {
            // use this method to define the properties for this command in the Revit ribbon
            string buttonInternalName = "btnCommand1";
            string buttonTitle = "Button 1";
            string? methodBase = MethodBase.GetCurrentMethod().DeclaringType?.FullName;

            if (methodBase == null)
            {
                throw new InvalidOperationException("MethodBase.GetCurrentMethod().DeclaringType?.FullName is null");
            }
            else
            {
                Common.ButtonDataClass myButtonData1 = new Common.ButtonDataClass(
                    buttonInternalName,
                    buttonTitle,
                    methodBase,
                    Properties.Resources.Blue_32,
                    Properties.Resources.Blue_16,
                    "This is a tooltip for Button 1");

                return myButtonData1.Data;
            }
        }
    }

}
