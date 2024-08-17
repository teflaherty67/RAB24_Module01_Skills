namespace RAB24_Module01_Skills
{
    [Transaction(TransactionMode.Manual)]
    public class Command1 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // this is a variable for the Revit application
            UIApplication uiapp = commandData.Application;

            // this is a variable for the current Revit model
            Document curDoc = uiapp.ActiveUIDocument.Document;

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

            // check a vaule and perform a single action if true
            if (number6 > 10)
            {
                // do something
            }

            // check a value and perform an action is true and another if false
            if (number5 == 100)
            {
                // do something if true
            }
            else
            {
                // do something else if false
            }

            // check multiple values and perform actions if true and false
            if (number6 > 100)
            {
                // do something if true
            }
            else if (number6 == 8)
            {
                // do something else if true
            }
            else
            {
                // do a third thing if false
            }

            // compound conditional statements
            // checking for two, or more, conditions using &&
            if (number6 > 10 &&  number5 == 100)
            {
                // do something if both are true
            }

            // check for either condition using ||
            if (number6 > 10 || number5 == 100)
            {
                // do something if either is true
            }

            // create a list
            List<string> list1 = new List<string>();

            // add items to list1
            list1.Add(text1);
            list1.Add(text2);
            list1.Add("this is some text");

            // create list and add items it - method 2
            List<int> list2 = new List<int> { 1, 2, 3, 4, 5 };

            // loop through a list using foreach loop
            foreach (string curString in list1)
            {
                // do something w/ curString variable
            }

            // for example
            int letterCount = 0;
            foreach (string curString in list1)
            {
                // letterCount = letterCount + curString.Length;
                letterCount += curString.Length;
            }

            // loop through a range of numbers
            for (int i = 0; i <= 10; i++)
            {
                // do something
            }

            // for example
            int numCount = 0;
            int counter = 100;
            for (int i = 0; i <= counter; i++)
            {
                numCount += i;
            }

            // to output the results
            TaskDialog.Show("Number Counter", "The number count is " + numCount.ToString());

            // create a transaction to lock the model
            Transaction t = new Transaction(curDoc);
            t.Start("Doing something in Revit");

            // create a floor level - see snippet in Revit API (www.revitapidocs.com)
            // elevation value is in decimal feet regardless of model's units
            double elevation = 100;
            Level newLevel = Level.Create(curDoc, elevation);
            newLevel.Name = "My New Level";

            // create a floor plan view - see snippet in Revit API (www.revitapidocs.com)
            // need to get a fllor plan view family type
            // by creating a Filtered Element Collector
            FilteredElementCollector colVFT = new FilteredElementCollector(curDoc)
                .OfClass(typeof(ViewFamilyType));

            ViewFamilyType floorPlanVFT = null;
            foreach (ViewFamilyType curVFT in colVFT)
            {
                if (curVFT.ViewFamily == ViewFamily.FloorPlan)
                {
                    floorPlanVFT = curVFT;
                    break;
                }
            }

            // create a view by specifying the document, view family type, and level
            ViewPlan newPlan = ViewPlan.Create(curDoc, floorPlanVFT.Id, newLevel.Id);
            newPlan.Name = "My new floor plan";

            // get ceiling plan view faily type
            ViewFamilyType ceilingPlanVFT = null;
            foreach (ViewFamilyType curVFT in colVFT)
            {
                if (curVFT.ViewFamily == ViewFamily.CeilingPlan)
                {
                    ceilingPlanVFT = curVFT;
                    break;
                }
            }

            // create a ceiling plan by using the ceiling plan view family type
            ViewPlan newClgPlan = ViewPlan.Create(curDoc, ceilingPlanVFT.Id, newLevel.Id);
            newClgPlan.Name = "My new ceiling plan";

            // create a sheet
            // first need to get the title block
            // by using a Filtered Element Collector
            FilteredElementCollector colTB = new FilteredElementCollector(curDoc)
                .OfCategory(BuiltInCategory.OST_TitleBlocks);

            // create the sheet
            ViewSheet newSheet = ViewSheet.Create(curDoc, colTB.FirstElementId());
            newSheet.Name = "My new sheet";
            newSheet.SheetNumber = "A101";

            // add a view to a sheet using Viewport.Create
            // see snippet in Revit API (www.revitapidocs.com)
            // first create an insertin point
            XYZ insPoint = new XYZ(1, 0.5, 0);
            
            Viewport newViewport = Viewport.Create(curDoc, newSheet.Id, newPlan.Id, insPoint);

            t.Commit();
            t.Dispose();

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
