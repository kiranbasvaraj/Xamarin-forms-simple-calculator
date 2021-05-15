using System;
using System.Diagnostics;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
namespace XFCalculatorApp.ViewModels
{
    public class CalculatorPageViewModel : ViewModelBase
    {
        private string _display { get; set; }

        public string Display
        {
            get { return _display; }
            set
            {
                _display = value;
                RaisePropertyChanged(nameof(Display));
            }

        }
        string x = "1+2+23";
        private string _result { get; set; }

        public string Result
        {
            get { return _result; }
            set
            {
                _result = value;
                RaisePropertyChanged(nameof(Result));
            }

        }


        public DelegateCommand<string> CalculateResult { get; set; }

        public CalculatorPageViewModel(INavigationService navigationService) : base(navigationService)
        {
          // var x1= int.Parse(x);
            CalculateResult = new DelegateCommand<string>(DoCalculations);
        }
        bool shouldCalRes = false;
        string oldOperation;


        //void Logic(string Operation)
        //{
        //    try
        //    {
        //        oldOperation = Operation;
        //           shouldCalRes = true;
        //        if (Operation == "+")
        //            Result = Result + int.Parse(Display ?? "0");

        //        else if (Operation == "-")
        //            Result = Result - int.Parse(Display ?? "0");
        //        else if (Operation == "*")
        //            Result = Result * int.Parse(Display ?? "0");
        //        else if (Operation == "/")
        //            Result = Result / int.Parse(Display ?? "0");

        //       // Display = string.Empty;
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex.StackTrace);
        //    }

        // }

        //new logic
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            OnClear();
        }

        int currentState = 1;
        string myoperator;
        double firstNumber, secondNumber;

        void OnNumSelected(string val)
        {
            string pressed = val;

            if (Result == "0" || currentState < 0)//at first current state is 1
            {
                Result = "";//here the text value will be cleared when pressing button

                if (currentState < 0) //at first current value is 1 so this condition is excluded
                    currentState *= -1;
            }

            Result += pressed;

            double number;//if  we are going  to assign two dynamic number for a given variable using try parse method 
            if (double.TryParse(this.Result, out number))
            {
                Result = number.ToString("N0");
                if (currentState == 1)
                {
                    firstNumber = number;//at first current state will be 1 and it will assign first number with the pressed number variable
                }
                else
                {
                    secondNumber = number;//it will be implemented as the number of current state changes i.e. 2
                }
            }
        }


        void OnSelectOperator(string portr)//event is called when the select operator is called 
        {
            currentState = -2;
            string pressed = portr;
            myoperator = pressed;
        }

        void OnClear()// this method is called when we press the AC button
        {
            firstNumber = 0;
            secondNumber = 0;
            currentState = 1;
            Result= "0";
        }

        void OnCalculate(string optr="") //This method is called when we have both first number and second number and we are going to evaluate those number
        {
            if (currentState == 2)
            {
                var result = Calculate(firstNumber, secondNumber, myoperator);

                Result = result.ToString();
                firstNumber = result;
                currentState = -1;
            }
        }


        private  double Calculate(double value1, double value2, string myoperator)
        {
            double result = 0;
            switch (myoperator)
            {
                case "/":
                    result = value1 / value2;
                    break;
                case "*":
                    result = value1 * value2;
                    break;
                case "+":
                    result = value1 + value2;
                    break;
                case "-":
                    result = value1 - value2;
                    break;
                

            }
            return result;

        }


        private void DoCalculations(string value)
        {
            switch (value)
            {
                case "+":
                    OnSelectOperator("+");
                    break;

                case "-":
                    OnSelectOperator("-");
                    break;

                case "*":
                    OnSelectOperator("*");
                    break;

                case "/":
                    OnSelectOperator("/");
                    break;

                case "c":
                    OnClear();
                    break;

                case "=":
                    OnCalculate("=");
                    break;

                default:
                    OnNumSelected(value); 
                    break;


            };

           

        }
    }
}
