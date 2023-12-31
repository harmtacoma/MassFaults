namespace MassFaults.Console
{
  internal class ExcludeClass
  {
    public void ExcludeClassMethod()
    {
      // This is a useless C# program with various issues

      string[] words = new string[] { "Lorem", "ipsum", "dolor", "sit", "amet" };
      int[] numbers = { 1, 2, 3, 4, 5 };

      // Issue: Unused variable
      string unusedVariable = "I'm still unused";

      // Issue: Unused parameter
      int result = Sum(10, 20);

      // Issue: Useless loop
      for (int i = 0; i < 100; i++)
      {
        // Do nothing
      }

      // Issue: Empty catch block
      try
      {
        throw new Exception("This is an exception");
      }
      catch
      {
        // Empty catch block
      }

      // Issue: Unreachable code
      System.Console.WriteLine("This code will never be reached");

      // Issue: Magic number
      int magicNumber = numbers[3];

      // Issue: Useless method
      UselessMethod();

      // Issue: Duplicate code
      System.Console.WriteLine(DuplicateCode("Hello"));
      System.Console.WriteLine(DuplicateCode2("World"));

      System.Console.ReadLine();
    }

    static void UselessMethod()
    {
      System.Console.WriteLine("This method serves no purpose");
    }

    static int Sum(int a, int b)
    {
      // Parameters are unused
      return 0;
    }

    static string DuplicateCode(string input)
    {
      // This code is duplicated
      return input.ToUpper();
    }

    static string DuplicateCode2(string input)
    {
      // This code is duplicated
      return input.ToUpper();
    }
  }
}
